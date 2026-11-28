using System.ComponentModel;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.DTOs;
using QuanLyThuVien.Entities;
using QuanLyThuVien.UI;

namespace QuanLyThuVien.Forms;

public partial class FrmDocGia : Form
{
    private readonly BindingList<DocGiaDto> _ds = new();
    private readonly BindingSource _bs = new();
    private bool _dangLamMoi;

    public FrmDocGia()
    {
        InitializeComponent();
        Theme.Apply(this);

        _bs.DataSource = _ds;
        dgvDocGia.DataSource = _bs;

        txtMaDocGia.CharacterCasing = CharacterCasing.Upper;
        txtMaDocGia.MaxLength = 10;
        AutoValidate = AutoValidate.EnableAllowFocusChange;

        Load += FrmDocGia_Load;
        dgvDocGia.SelectionChanged += dgvDocGia_SelectionChanged;
        txtMaDocGia.Validating += txtMaDocGia_Validating;
        txtHoTen.Validating += txtHoTen_Validating;
        txtSoDienThoai.Validating += txtSoDienThoai_Validating;

        btnThem.Click += btnThem_Click;
        btnSua.Click += btnSua_Click;
        btnKhoa.Click += btnKhoa_Click;
        btnXoa.Click += btnXoa_Click;
        btnLamMoi.Click += btnLamMoi_Click;

        btnTraHet.Click += btnTraHet_Click;

        // Chỉ thu tiền theo hóa đơn: ẩn phần "trả một phần" cũ
        btnThanhToan.Visible = false;
        lblSoTienTra.Visible = false;
        nudSoTienTra.Visible = false;

        btnTraHet.Text = "Thu hết nợ";
        btnTraHet.Size = new Size(150, btnLamMoi.Height);                 // cao bằng các nút khác
        btnTraHet.Location = new Point(btnLamMoi.Right + 40, btnLamMoi.Top);

        lblTieuDe.Font = Theme.FontTitle;
        lblTieuDe.ForeColor = Theme.PrimaryDark;
    }

    // ===================== SỰ KIỆN =====================

    private async void FrmDocGia_Load(object? sender, EventArgs e)
    {
        try
        {
            CauHinhLuoi();
            await NapDanhSachAsync(null);
        }
        catch (Exception ex)
        {
            BaoLoi(ex);
        }
    }

    // Chọn một dòng thì đổ dữ liệu lên các ô nhập
    private void dgvDocGia_SelectionChanged(object? sender, EventArgs e)
    {
        if (_dangLamMoi) return;
        if (LayDongChon() is not DocGiaDto d) return;

        txtMaDocGia.Text = d.MaDocGia;
        txtMaDocGia.ReadOnly = true;   // khóa chính, không cho sửa
        txtHoTen.Text = d.HoTen;
        txtSoDienThoai.Text = d.SoDienThoai;
        lblTienNoPhat.Text = $"Nợ phạt: {d.TienNoPhat:N0} VNĐ";
        btnKhoa.Text = d.BiKhoaThe ? "Mở khóa thẻ" : "Khóa thẻ";
        errProvider.Clear();
    }

    private async void btnThem_Click(object? sender, EventArgs e)
    {
        try
        {
            if (txtMaDocGia.ReadOnly)
            {
                MessageBox.Show("Hãy bấm \"Làm mới\" rồi nhập thông tin độc giả mới.",
                    "Chưa ở chế độ thêm mới", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!KiemTraDauVao()) return;

            string ma = txtMaDocGia.Text.Trim().ToUpperInvariant();

            using var db = new AppDbContext();
            if (await db.DocGias.AnyAsync(d => d.MaDocGia == ma))
            {
                MessageBox.Show($"Mã độc giả \"{ma}\" đã tồn tại.", "Trùng mã",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            db.DocGias.Add(new DocGia
            {
                MaDocGia = ma,
                HoTen = txtHoTen.Text.Trim(),
                SoDienThoai = txtSoDienThoai.Text.Trim(),
                BiKhoaThe = false,
                TienNoPhat = 0
            });
            await db.SaveChangesAsync();

            await NapDanhSachAsync(ma);
            Toast.ThanhCong(this, $"Đã thêm độc giả \"{ma}\" thành công!");

        }
        catch (Exception ex)
        {
            BaoLoi(ex);
        }
    }

    private async void btnSua_Click(object? sender, EventArgs e)
    {
        try
        {
            if (LayDongChon() is not DocGiaDto d)
            {
                MessageBox.Show("Hãy chọn một độc giả trong bảng để sửa.", "Chưa chọn",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!KiemTraDauVao()) return;

            using var db = new AppDbContext();
            var dg = await db.DocGias.FirstOrDefaultAsync(x => x.MaDocGia == d.MaDocGia);
            if (dg == null) return;

            dg.HoTen = txtHoTen.Text.Trim();
            dg.SoDienThoai = txtSoDienThoai.Text.Trim();
            await db.SaveChangesAsync();

            await NapDanhSachAsync(d.MaDocGia);
            Toast.ThanhCong(this, "Cập nhật thông tin độc giả thành công!");

        }
        catch (Exception ex)
        {
            BaoLoi(ex);
        }
    }

    // Khóa thẻ / Mở khóa thẻ (đổi qua lại)
    private async void btnKhoa_Click(object? sender, EventArgs e)
    {
        try
        {
            if (LayDongChon() is not DocGiaDto d)
            {
                MessageBox.Show("Hãy chọn một độc giả trong bảng.", "Chưa chọn",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var db = new AppDbContext();
            var dg = await db.DocGias.FirstOrDefaultAsync(x => x.MaDocGia == d.MaDocGia);
            if (dg == null) return;

            dg.BiKhoaThe = !dg.BiKhoaThe;
            await db.SaveChangesAsync();

            await NapDanhSachAsync(d.MaDocGia);
            Toast.ThanhCong(this, dg.BiKhoaThe ? "Đã khóa thẻ độc giả!" : "Đã mở khóa thẻ độc giả!");

        }
        catch (Exception ex)
        {
            BaoLoi(ex);
        }
    }

    private async void btnXoa_Click(object? sender, EventArgs e)
    {
        try
        {
            if (LayDongChon() is not DocGiaDto d)
            {
                Toast.CanhBao(this, "Hãy chọn một độc giả trong bảng để xóa.");
                return;
            }

            using var db = new AppDbContext();

            // Có lịch sử (phiếu mượn, hóa đơn phạt, yêu cầu mượn) thì không xóa cứng
            bool coLichSu = await db.PhieuMuons.AnyAsync(p => p.MaDocGia == d.MaDocGia)
                         || await db.HoaDonPhats.AnyAsync(h => h.MaDocGia == d.MaDocGia)
                         || await db.YeuCauMuons.AnyAsync(y => y.MaDocGia == d.MaDocGia);
            if (coLichSu)
            {
                MessageBox.Show(
                    "Không thể xóa: độc giả này đã có phiếu mượn, hóa đơn phạt hoặc yêu cầu mượn.\n" +
                    "Hãy dùng nút \"Khóa thẻ\" để ngừng cho mượn.",
                    "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var kq = MessageBox.Show($"Xóa độc giả \"{d.HoTen}\"?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq != DialogResult.Yes) return;

            var dg = await db.DocGias.FirstOrDefaultAsync(x => x.MaDocGia == d.MaDocGia);
            if (dg == null) return;

            var tk = await db.TaiKhoans.FirstOrDefaultAsync(t => t.MaDocGia == dg.MaDocGia);
            if (tk != null) db.TaiKhoans.Remove(tk);
            db.DocGias.Remove(dg);
            await db.SaveChangesAsync();

            await NapDanhSachAsync(null);
            Toast.ThanhCong(this, $"Đã xóa độc giả \"{d.HoTen}\" thành công!");
        }
        catch (Exception ex)
        {
            BaoLoi(ex);
        }
    }

    private void btnLamMoi_Click(object? sender, EventArgs e)
    {
        BoChonVaLamMoiKhungNhap();
        txtMaDocGia.Focus();
    }



    // Thu toàn bộ nợ phạt của độc giả: đánh dấu mọi hóa đơn chưa thanh toán + đưa nợ về 0
    private async void btnTraHet_Click(object? sender, EventArgs e)
    {
        try
        {
            if (LayDongChon() is not DocGiaDto d)
            {
                Toast.CanhBao(this, "Hãy chọn một độc giả trong bảng.");
                return;
            }

            using var db = new AppDbContext();
            var dg = await db.DocGias.FirstOrDefaultAsync(x => x.MaDocGia == d.MaDocGia);
            if (dg == null) return;

            bool conHoaDon = await db.HoaDonPhats
                .AnyAsync(h => h.MaDocGia == dg.MaDocGia && !h.DaThanhToan);

            // Đọc số nợ mới nhất từ CSDL (màn hình Trả sách có thể vừa cộng thêm phạt)
            if (dg.TienNoPhat <= 0 && !conHoaDon)
            {
                Toast.ThongTin(this, "Độc giả này không còn nợ phạt.");
                await NapDanhSachAsync(d.MaDocGia);
                return;
            }

            var hoi = MessageBox.Show(
                $"Thu {dg.TienNoPhat:N0} VNĐ tiền phạt của \"{dg.HoTen}\" và đánh dấu mọi hóa đơn là đã thanh toán?",
                "Xác nhận thu tiền", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (hoi != DialogResult.Yes) return;

            decimal daThu = await PhatService.ThuTatCaAsync(db, dg.MaDocGia);
            await db.SaveChangesAsync();

            await NapDanhSachAsync(d.MaDocGia);
            Toast.ThanhCong(this, $"Đã thu {daThu:N0} VNĐ tiền phạt!");
        }
        catch (Exception ex)
        {
            BaoLoi(ex);
        }
    }


    // ===================== KIỂM TRA DỮ LIỆU (Validating) =====================
    // Chỉ hiện biểu tượng cảnh báo, KHÔNG đặt e.Cancel = true
    // (nếu đặt, người dùng bị "nhốt" trong ô nhập và không đóng được cửa sổ).

    private void txtMaDocGia_Validating(object? sender, CancelEventArgs e)
    {
        if (txtMaDocGia.ReadOnly)
        {
            errProvider.SetError(txtMaDocGia, "");
            return;
        }

        string ma = txtMaDocGia.Text.Trim();
        if (!Regex.IsMatch(ma, @"^[A-Z0-9]{3,10}$"))
            errProvider.SetError(txtMaDocGia, "Mã gồm 3 đến 10 ký tự, chỉ chữ HOA và số, không khoảng trắng");
        else
            errProvider.SetError(txtMaDocGia, "");
    }

    private void txtHoTen_Validating(object? sender, CancelEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            errProvider.SetError(txtHoTen, "Họ tên không được để trống");
        else
            errProvider.SetError(txtHoTen, "");
    }

    private void txtSoDienThoai_Validating(object? sender, CancelEventArgs e)
    {
        if (!Regex.IsMatch(txtSoDienThoai.Text.Trim(), @"^0\d{9}$"))
            errProvider.SetError(txtSoDienThoai, "Số điện thoại gồm 10 số và bắt đầu bằng số 0");
        else
            errProvider.SetError(txtSoDienThoai, "");
    }

    // Kiểm tra toàn bộ form trước khi lưu (Form-level Validation)
    private bool KiemTraDauVao()
    {
        ValidateChildren();   // chạy lại cả 3 hàm Validating để cập nhật biểu tượng lỗi

        if (errProvider.GetError(txtMaDocGia) != "" ||
            errProvider.GetError(txtHoTen) != "" ||
            errProvider.GetError(txtSoDienThoai) != "")
        {
            MessageBox.Show("Vui lòng sửa các ô có biểu tượng cảnh báo đỏ.", "Dữ liệu chưa hợp lệ",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
        return true;
    }

    // ===================== LƯỚI VÀ HÀM PHỤ =====================

    private async Task NapDanhSachAsync(string? maCanChon)
    {
        using var db = new AppDbContext();
        var ds = await db.DocGias
            .AsNoTracking()
            .OrderBy(d => d.MaDocGia)
            .ToListAsync();

        _ds.RaiseListChangedEvents = false;
        _ds.Clear();
        foreach (var d in ds)
            _ds.Add(new DocGiaDto(d.MaDocGia, d.HoTen, d.SoDienThoai, d.BiKhoaThe, d.TienNoPhat));
        _ds.RaiseListChangedEvents = true;
        _ds.ResetBindings();

        if (maCanChon != null && ChonDong(maCanChon))
            return;

        BoChonVaLamMoiKhungNhap();
    }

    // Lấy dòng đang được chọn thật sự (null nếu chưa chọn dòng nào)
    private DocGiaDto? LayDongChon()
    {
        if (dgvDocGia.SelectedRows.Count == 0) return null;
        return dgvDocGia.SelectedRows[0].DataBoundItem as DocGiaDto;
    }

    private bool ChonDong(string ma)
    {
        foreach (DataGridViewRow dong in dgvDocGia.Rows)
        {
            if (dong.DataBoundItem is DocGiaDto x && x.MaDocGia == ma)
            {
                dgvDocGia.ClearSelection();
                dong.Selected = true;
                dgvDocGia.CurrentCell = dong.Cells["MaDocGia"];
                return true;
            }
        }
        return false;
    }

    private void BoChonVaLamMoiKhungNhap()
    {
        _dangLamMoi = true;
        dgvDocGia.ClearSelection();
        _dangLamMoi = false;

        txtMaDocGia.ReadOnly = false;
        txtMaDocGia.Clear();
        txtHoTen.Clear();
        txtSoDienThoai.Clear();
        nudSoTienTra.Value = 0;
        lblTienNoPhat.Text = "Nợ phạt: 0 VNĐ";
        btnKhoa.Text = "Khóa thẻ";
        errProvider.Clear();
    }

    private void CauHinhLuoi()
    {
        dgvDocGia.ReadOnly = true;
        dgvDocGia.AllowUserToAddRows = false;
        dgvDocGia.AllowUserToDeleteRows = false;
        dgvDocGia.MultiSelect = false;
        dgvDocGia.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvDocGia.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvDocGia.RowHeadersVisible = false;

        if (dgvDocGia.Columns["MaDocGia"] is { } c1) c1.HeaderText = "Mã độc giả";
        if (dgvDocGia.Columns["HoTen"] is { } c2) c2.HeaderText = "Họ tên";
        if (dgvDocGia.Columns["SoDienThoai"] is { } c3) c3.HeaderText = "Số điện thoại";
        if (dgvDocGia.Columns["BiKhoaThe"] is { } c4) c4.HeaderText = "Khóa thẻ";
        if (dgvDocGia.Columns["TienNoPhat"] is { } c5)
        {
            c5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            c5.HeaderText = "Nợ phạt";
            c5.DefaultCellStyle.Format = "N0";
        }
    }

    private static void BaoLoi(Exception ex)
    {
        NhatKy.Ghi(ex);
#if DEBUG
        MessageBox.Show($"Có lỗi xảy ra:\n{ex.Message}", "Lỗi",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
#else
        MessageBox.Show("Có lỗi xảy ra. Chi tiết đã được ghi vào file error.log.", "Lỗi",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
    }
}
