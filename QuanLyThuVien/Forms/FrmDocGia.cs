using System.ComponentModel;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.DTOs;
using QuanLyThuVien.Entities;
using QuanLyThuVien.UI;

namespace QuanLyThuVien.Forms;

// ============================================================================
// MÀN HÌNH QUẢN LÝ ĐỘC GIẢ (Thành viên 2)
//
// Control cần kéo thả trong Designer (đặt đúng (Name)):
//   txtMaDocGia, txtHoTen, txtSoDienThoai
//   lblTienNoPhat, nudSoTienTra (NumericUpDown: Minimum 0, Maximum 100000000,
//                                Increment 1000, ThousandsSeparator True)
//   btnThem, btnSua, btnKhoa, btnXoa, btnLamMoi, btnThanhToan, btnTraHet
//   dgvDocGia (DataGridView), errProvider (ErrorProvider)
//
// LƯU Ý: các sự kiện được nối ngay trong constructor bên dưới.
//        KHÔNG bấm đúp vào control để tạo sự kiện (sẽ bị nối trùng).
// ============================================================================
public partial class FrmDocGia : Form
{
    private readonly BindingList<DocGiaDto> _ds = new();
    private readonly BindingSource _bs = new();
    private bool _dangLamMoi;   // true khi đang bỏ chọn dòng, để bỏ qua SelectionChanged

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
        btnThanhToan.Click += btnThanhToan_Click;
        btnTraHet.Click += btnTraHet_Click;
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
                MessageBox.Show("Hãy chọn một độc giả trong bảng để xóa.", "Chưa chọn",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var db = new AppDbContext();

            // Độc giả đã có lịch sử thì không xóa cứng, hướng dẫn chuyển sang Khóa thẻ
            bool coLichSu = await db.PhieuMuons.AnyAsync(p => p.MaDocGia == d.MaDocGia)
                         || await db.HoaDonPhats.AnyAsync(h => h.MaDocGia == d.MaDocGia);
            if (coLichSu)
            {
                MessageBox.Show(
                    "Không thể xóa: độc giả này đã có phiếu mượn hoặc hóa đơn phạt.\n" +
                    "Hãy dùng nút \"Khóa thẻ\" để ngừng cho mượn.",
                    "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var kq = MessageBox.Show($"Xóa độc giả \"{d.HoTen}\"?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq != DialogResult.Yes) return;

            var dg = await db.DocGias.FirstOrDefaultAsync(x => x.MaDocGia == d.MaDocGia);
            if (dg == null) return;
            db.DocGias.Remove(dg);
            await db.SaveChangesAsync();

            await NapDanhSachAsync(null);
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

    // Trả một phần số tiền nợ (giảm trừ)
    private async void btnThanhToan_Click(object? sender, EventArgs e)
    {
        try
        {
            await GhiNhanThanhToanAsync(traHet: false);
        }
        catch (Exception ex)
        {
            BaoLoi(ex);
        }
    }

    // Trả hết nợ (đưa về 0)
    private async void btnTraHet_Click(object? sender, EventArgs e)
    {
        try
        {
            await GhiNhanThanhToanAsync(traHet: true);
        }
        catch (Exception ex)
        {
            BaoLoi(ex);
        }
    }

    // ===================== NGHIỆP VỤ =====================

    private async Task GhiNhanThanhToanAsync(bool traHet)
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

        // Dùng số nợ mới nhất trong CSDL (TV3 có thể vừa cộng thêm tiền phạt)
        if (dg.TienNoPhat <= 0)
        {
            MessageBox.Show("Độc giả này không còn nợ phạt.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            await NapDanhSachAsync(d.MaDocGia);
            return;
        }

        decimal soTien = traHet ? dg.TienNoPhat : nudSoTienTra.Value;
        if (soTien <= 0)
        {
            MessageBox.Show("Hãy nhập số tiền thanh toán lớn hơn 0.", "Thiếu số tiền",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        if (soTien > dg.TienNoPhat) soTien = dg.TienNoPhat;

        dg.TienNoPhat -= soTien;

        // ĐỒNG BỘ VỚI HÓA ĐƠN PHẠT CỦA TV3 (hãy xác nhận cách làm này với TV3):
        // khi nợ về 0 thì các hóa đơn chưa thanh toán của độc giả được đánh dấu đã thanh toán.
        if (dg.TienNoPhat == 0)
        {
            var hoaDons = await db.HoaDonPhats
                .Where(h => h.MaDocGia == dg.MaDocGia && !h.DaThanhToan)
                .ToListAsync();
            foreach (var h in hoaDons) h.DaThanhToan = true;
        }

        await db.SaveChangesAsync();

        nudSoTienTra.Value = 0;
        await NapDanhSachAsync(d.MaDocGia);
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
            c5.HeaderText = "Nợ phạt";
            c5.DefaultCellStyle.Format = "N0";
        }
    }

    private static void BaoLoi(Exception ex)
    {
        MessageBox.Show($"Có lỗi xảy ra:\n{ex.Message}", "Lỗi",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
