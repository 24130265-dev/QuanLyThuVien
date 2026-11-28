using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.DTOs;
using QuanLyThuVien.Entities;
using QuanLyThuVien.UI;

namespace QuanLyThuVien.Forms;

public partial class FrmMuonSach : Form
{
    private const int SoSachToiDa = MuonService.SoSachToiDa;
    private const int SoNgayMuon = MuonService.SoNgayMuon;

    private readonly BindingList<GioMuonDto> _gio = new();
    private string? _maDocGia;           // Độc giả đã qua kiểm tra, null nếu chưa
    private int _soSachDangGiu;          // Số cuốn độc giả đang giữ trong CSDL (chưa trả)
    private int? _maYeuCauDangXuLy;      // Yêu cầu online đang được xử lý (chỉ đổi "Đã duyệt" khi lập phiếu xong)

    public FrmMuonSach()
    {
        InitializeComponent();
        Theme.Apply(this);

        dgvGioMuon.DataSource = _gio;
        dgvGioMuon.DataBindingComplete += (s, e) => CauHinhLuoi();

        dtpNgayMuon.Format = DateTimePickerFormat.Short;
        dtpNgayPhaiTra.Format = DateTimePickerFormat.Short;
        dtpNgayPhaiTra.Enabled = false;  // Tự tính, không cho sửa tay

        // Giãn ô nhập khỏi nhãn (phông của Theme làm nhãn rộng hơn lúc thiết kế)
        grpChonSach.PerformLayout();
        grpThongTinDocGia.PerformLayout();
        txtMaCuonSach.Left = lblMaCuonSach.Right + 10;
        dtpNgayPhaiTra.Left = lblNgayPhaiTra.Right + 10;

        Load += FrmMuonSach_Load;
        btnKiemTra.Click += btnKiemTra_Click;
        btnThemVaoGio.Click += btnThemVaoGio_Click;
        btnXoaKhoiGio.Click += btnXoaKhoiGio_Click;
        btnLapPhieu.Click += btnLapPhieu_Click;
        btnLamMoi.Click += btnLamMoi_Click;
        dtpNgayMuon.ValueChanged += (s, e) => CapNhatNgayPhaiTra();

        // Nhấn Enter ở ô nhập thì tự bấm nút tương ứng (tiện khi quét mã vạch)
        txtMaDocGia.KeyDown += (s, e) =>
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;
            btnKiemTra.PerformClick();
        };
        txtMaCuonSach.KeyDown += (s, e) =>
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;
            btnThemVaoGio.PerformClick();
        };
    }

    // ===================== SỰ KIỆN =====================

    private void FrmMuonSach_Load(object? sender, EventArgs e)
    {
        CauHinhLuoi();
        LamMoi();
    }

    private async void btnKiemTra_Click(object? sender, EventArgs e)
    {
        btnKiemTra.Enabled = false;
        try
        {
            await KiemTraDocGiaAsync();
        }
        catch (Exception ex)
        {
            BaoLoi(ex);
        }
        finally
        {
            btnKiemTra.Enabled = true;
        }
    }

    private async void btnThemVaoGio_Click(object? sender, EventArgs e)
    {
        btnThemVaoGio.Enabled = false;
        try
        {
            if (_maDocGia == null)
            {
                Toast.ThongTin(this, "Hãy kiểm tra độc giả trước khi chọn sách.");
                return;
            }

            string maCuon = txtMaCuonSach.Text.Trim().ToUpperInvariant();
            if (maCuon == "") return;

            // Chặn quét trùng 1 cuốn vào cùng giỏ
            if (_gio.Any(g => g.MaCuonSach == maCuon))
            {
                Toast.CanhBao(this, $"Cuốn \"{maCuon}\" đã có trong giỏ.");
                return;
            }

            // Số sách đang giữ + số sách trong giỏ phải còn dưới mức tối đa
            if (!ConSlot())
            {
                Toast.CanhBao(this,
                    $"Vượt quá giới hạn {SoSachToiDa} cuốn. Đang giữ {_soSachDangGiu}, trong giỏ {_gio.Count}.");
                return;
            }

            using var db = new AppDbContext();
            var cuon = await db.CuonSachs
                .AsNoTracking()
                .Include(c => c.DauSach)
                .FirstOrDefaultAsync(c => c.MaCuonSach == maCuon);

            if (cuon == null)
            {
                Toast.CanhBao(this, $"Không tìm thấy cuốn sách có mã \"{maCuon}\".");
                return;
            }

            // Chỉ cuốn "Sẵn sàng" mới được vào giỏ
            if (cuon.TrangThai != TrangThaiSach.SanSang)
            {
                Toast.CanhBao(this, $"Cuốn \"{maCuon}\" đang ở trạng thái \"{cuon.TrangThai}\" nên không thể mượn.");
                return;
            }

            _gio.Add(new GioMuonDto(
                cuon.MaCuonSach,
                cuon.DauSach?.TenDauSach ?? "",
                cuon.DauSach?.TacGia ?? ""));

            txtMaCuonSach.Clear();
            txtMaCuonSach.Focus();
            CapNhatDem();
        }
        catch (Exception ex)
        {
            BaoLoi(ex);
        }
        finally
        {
            btnThemVaoGio.Enabled = true;
        }
    }

    private void btnXoaKhoiGio_Click(object? sender, EventArgs e)
    {
        if (dgvGioMuon.CurrentRow?.DataBoundItem is GioMuonDto dong)
        {
            _gio.Remove(dong);
            CapNhatDem();
        }
    }

    private async void btnLapPhieu_Click(object? sender, EventArgs e)
    {
        btnLapPhieu.Enabled = false;
        try
        {
            if (_maDocGia == null || _gio.Count == 0)
            {
                Toast.ThongTin(this, "Giỏ mượn đang trống.");
                return;
            }

            var ngayMuon = dtpNgayMuon.Value.Date;
            if (ngayMuon > DateTime.Today)
            {
                Toast.CanhBao(this, "Ngày mượn không được lớn hơn ngày hiện tại.");
                return;
            }

            using var db = new AppDbContext();

            // Kiểm tra lại với CSDL bằng cùng quy tắc: tránh xung đột nếu màn hình khác vừa cập nhật
            var kq = await MuonService.KiemTraAsync(db, _maDocGia, _gio.Count);
            if (!kq.Du)
            {
                Toast.CanhBao(this, "Không thể lập phiếu. " + kq.MoTa);
                return;
            }

            var dsMa = _gio.Select(g => g.MaCuonSach).ToList();
            var cacCuon = await db.CuonSachs.Where(c => dsMa.Contains(c.MaCuonSach)).ToListAsync();

            if (cacCuon.Count != dsMa.Count || cacCuon.Any(c => c.TrangThai != TrangThaiSach.SanSang))
            {
                Toast.CanhBao(this, "Có cuốn trong giỏ không còn ở trạng thái \"Sẵn sàng\". Hãy làm mới và chọn lại.");
                return;
            }

            var phieu = new PhieuMuon
            {
                MaDocGia = _maDocGia,
                NgayMuon = ngayMuon,
                NgayPhaiTra = ngayMuon.AddDays(SoNgayMuon)
            };

            foreach (var cuon in cacCuon)
            {
                phieu.ChiTietPhieuMuons.Add(new ChiTietPhieuMuon
                {
                    MaCuonSach = cuon.MaCuonSach,
                    NgayTraThucTe = null
                });
                cuon.TrangThai = TrangThaiSach.DangMuon;
            }

            // Phiếu xuất phát từ yêu cầu online: đổi "Đã duyệt" trong CÙNG lần lưu với phiếu mượn.
            // Chỉ đổi khi cuốn trong giỏ đúng là đầu sách mà độc giả đã yêu cầu.
            if (_maYeuCauDangXuLy is int maYc)
            {
                var yc = await db.YeuCauMuons.FirstOrDefaultAsync(y => y.MaYeuCau == maYc);
                if (yc != null
                    && yc.TrangThai == TrangThaiYeuCau.ChoDuyet
                    && yc.MaDocGia == _maDocGia
                    && cacCuon.Any(c => c.MaDauSach == yc.MaDauSach))
                {
                    yc.TrangThai = TrangThaiYeuCau.DaDuyet;
                }
            }

            db.PhieuMuons.Add(phieu);
            await db.SaveChangesAsync();

            Toast.ThanhCong(this, $"Lập phiếu mượn thành công! Mã phiếu: {phieu.MaPhieuMuon}");

            LamMoi();
        }
        catch (DbUpdateConcurrencyException)
        {
            Toast.CanhBao(this, "Có cuốn vừa được cho mượn hoặc đổi trạng thái ở nơi khác. Hãy làm mới và chọn lại.");
        }
        catch (Exception ex)
        {
            BaoLoi(ex);
        }
        finally
        {
            CapNhatDem();
        }
    }

    private void btnLamMoi_Click(object? sender, EventArgs e)
    {
        LamMoi();
    }

    private async void btnDuyetYeuCau_Click(object? sender, EventArgs e)
    {
        using var dlg = new DlgDuyetYeuCau();
        if (dlg.ShowDialog(this) != DialogResult.OK) return;
        try
        {
            txtMaDocGia.Text = dlg.MaDocGiaDuocChon;
            await KiemTraDocGiaAsync();

            // Chỉ ghi nhận yêu cầu khi độc giả thật sự đủ điều kiện
            if (string.Equals(_maDocGia, dlg.MaDocGiaDuocChon, StringComparison.OrdinalIgnoreCase))
            {
                _maYeuCauDangXuLy = dlg.MaYeuCauDuocChon;
                txtMaCuonSach.Text = dlg.MaCuonSachGoiY ?? "";
                Toast.ThongTin(this, "Bấm \"Thêm vào giỏ\" rồi \"Xác nhận cho mượn\" để hoàn tất yêu cầu.");
            }
        }
        catch (Exception ex)
        {
            BaoLoi(ex);
        }
    }

    // ===================== NGHIỆP VỤ =====================

    private async Task KiemTraDocGiaAsync()
    {
        _gio.Clear();
        _maDocGia = null;
        _maYeuCauDangXuLy = null;
        _soSachDangGiu = 0;
        grpChonSach.Enabled = false;
        CapNhatDem();

        string ma = txtMaDocGia.Text.Trim().ToUpperInvariant();
        if (ma == "")
        {
            Toast.ThongTin(this, "Hãy nhập mã độc giả.");
            return;
        }

        using var db = new AppDbContext();
        var kq = await MuonService.KiemTraAsync(db, ma);
        if (!kq.Du)
        {
            Chan(kq.MoTa);
            return;
        }

        _maDocGia = ma;
        _soSachDangGiu = kq.DangGiu;
        grpChonSach.Enabled = true;
        lblThongTinDocGia.ForeColor = Color.ForestGreen;
        lblThongTinDocGia.Text =
            $"{kq.DocGia!.HoTen} | SĐT: {kq.DocGia.SoDienThoai} | Đang giữ: {kq.DangGiu}/{SoSachToiDa} cuốn | Đủ điều kiện mượn";
        CapNhatDem();
        txtMaCuonSach.Focus();
    }

    // ===================== HÀM PHỤ =====================

    // Còn được thêm cuốn vào giỏ không (đang giữ + trong giỏ < tối đa)
    private bool ConSlot() => _soSachDangGiu + _gio.Count < SoSachToiDa;

    private void Chan(string lyDo)
    {
        lblThongTinDocGia.ForeColor = Color.Firebrick;
        lblThongTinDocGia.Text = lyDo.Replace("\n", " ");
        Toast.CanhBao(this, lyDo);
    }

    private void LamMoi()
    {
        _gio.Clear();
        _maDocGia = null;
        _maYeuCauDangXuLy = null;
        _soSachDangGiu = 0;

        txtMaDocGia.Clear();
        txtMaCuonSach.Clear();
        grpChonSach.Enabled = false;

        lblThongTinDocGia.ForeColor = SystemColors.ControlText;
        lblThongTinDocGia.Text = "Nhập mã độc giả rồi bấm Kiểm tra.";

        dtpNgayMuon.Value = DateTime.Today;
        CapNhatNgayPhaiTra();
        CapNhatDem();
        txtMaDocGia.Focus();
    }

    private void CapNhatNgayPhaiTra()
    {
        dtpNgayPhaiTra.Value = dtpNgayMuon.Value.Date.AddDays(SoNgayMuon);
    }

    private void CapNhatDem()
    {
        lblSoLuong.Text = $"Đang giữ: {_soSachDangGiu} | Trong giỏ: {_gio.Count} | Tối đa: {SoSachToiDa}";
        btnLapPhieu.Enabled = _maDocGia != null && _gio.Count > 0;
    }

    private void CauHinhLuoi()
    {
        dgvGioMuon.ReadOnly = true;
        dgvGioMuon.AllowUserToAddRows = false;
        dgvGioMuon.AllowUserToDeleteRows = false;
        dgvGioMuon.MultiSelect = false;
        dgvGioMuon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvGioMuon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvGioMuon.RowHeadersVisible = false;

        if (dgvGioMuon.Columns["MaCuonSach"] is { } c1) c1.HeaderText = "Mã cuốn sách";
        if (dgvGioMuon.Columns["TenDauSach"] is { } c2) c2.HeaderText = "Tên đầu sách";
        if (dgvGioMuon.Columns["TacGia"] is { } c3) c3.HeaderText = "Tác giả";
    }

    private void BaoLoi(Exception ex)
    {
        NhatKy.Ghi(ex);
        Toast.Loi(this, "Có lỗi xảy ra. Vui lòng thử lại.");
    }
}