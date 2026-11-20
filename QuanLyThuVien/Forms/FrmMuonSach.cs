using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.DTOs;
using QuanLyThuVien.Entities;
using QuanLyThuVien.UI;

namespace QuanLyThuVien.Forms;

public partial class FrmMuonSach : Form
{
    private const int SoSachToiDa = 3;   // Đề bài: tối đa 3 cuốn
    private const int SoNgayMuon = 14;   // Đề bài: ngày phải trả = ngày mượn + 14

    private readonly BindingList<GioMuonDto> _gio = new();
    private string? _maDocGia;           // Độc giả đã qua kiểm tra, null nếu chưa
    private int _soSachDangGiu;          // Số cuốn độc giả đang giữ trong CSDL (chưa trả)

    public FrmMuonSach()
    {
        InitializeComponent();
        Theme.Apply(this);

        dgvGioMuon.DataSource = _gio;
        dgvGioMuon.DataBindingComplete += (s, e) => CauHinhLuoi();

        dtpNgayMuon.Format = DateTimePickerFormat.Short;
        dtpNgayPhaiTra.Format = DateTimePickerFormat.Short;
        dtpNgayPhaiTra.Enabled = false;  // Tự tính, không cho sửa tay

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
                MessageBox.Show("Hãy kiểm tra độc giả trước khi chọn sách.", "Chưa kiểm tra độc giả",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string maCuon = txtMaCuonSach.Text.Trim().ToUpperInvariant();
            if (maCuon == "") return;

            // Chặn quét trùng 1 cuốn vào cùng giỏ
            if (_gio.Any(g => g.MaCuonSach == maCuon))
            {
                MessageBox.Show($"Cuốn \"{maCuon}\" đã có trong giỏ.", "Trùng cuốn sách",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Công thức giới hạn: số sách đang giữ + số sách trong giỏ <= 3
            if (_soSachDangGiu + _gio.Count >= SoSachToiDa)
            {
                MessageBox.Show(
                    $"Vượt quá giới hạn {SoSachToiDa} cuốn.\n" +
                    $"Độc giả đang giữ {_soSachDangGiu} cuốn, trong giỏ đã có {_gio.Count} cuốn.",
                    "Vượt giới hạn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var db = new AppDbContext();
            var cuon = await db.CuonSachs
                .AsNoTracking()
                .Include(c => c.DauSach)
                .FirstOrDefaultAsync(c => c.MaCuonSach == maCuon);

            if (cuon == null)
            {
                MessageBox.Show($"Không tìm thấy cuốn sách có mã \"{maCuon}\".", "Không tìm thấy",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Chỉ cuốn "Sẵn sàng" mới được vào giỏ
            if (cuon.TrangThai != TrangThaiSach.SanSang)
            {
                MessageBox.Show($"Cuốn \"{maCuon}\" đang ở trạng thái \"{cuon.TrangThai}\" nên không thể mượn.",
                    "Không thể mượn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show("Giỏ mượn đang trống.", "Chưa có sách",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var ngayMuon = dtpNgayMuon.Value.Date;
            if (ngayMuon > DateTime.Today)
            {
                MessageBox.Show("Ngày mượn không được lớn hơn ngày hiện tại.",
                    "Ngày mượn không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var db = new AppDbContext();

            // Kiểm tra lại với CSDL: tránh xung đột nếu màn hình khác vừa cập nhật
            var docGia = await db.DocGias.FirstOrDefaultAsync(d => d.MaDocGia == _maDocGia);
            if (docGia == null || docGia.BiKhoaThe || docGia.TienNoPhat > 0)
            {
                MessageBox.Show("Độc giả không còn đủ điều kiện mượn (khóa thẻ hoặc nợ phạt). Hãy kiểm tra lại.",
                    "Không thể lập phiếu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int dangGiu = await DemSachDangGiuAsync(db, _maDocGia);
            if (dangGiu + _gio.Count > SoSachToiDa)
            {
                MessageBox.Show($"Vượt quá giới hạn {SoSachToiDa} cuốn. Hãy kiểm tra lại độc giả.",
                    "Vượt giới hạn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dsMa = _gio.Select(g => g.MaCuonSach).ToList();
            var cacCuon = await db.CuonSachs.Where(c => dsMa.Contains(c.MaCuonSach)).ToListAsync();

            if (cacCuon.Count != dsMa.Count || cacCuon.Any(c => c.TrangThai != TrangThaiSach.SanSang))
            {
                MessageBox.Show("Có cuốn trong giỏ không còn ở trạng thái \"Sẵn sàng\". Hãy làm mới và chọn lại.",
                    "Không thể lập phiếu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            db.PhieuMuons.Add(phieu);
            await db.SaveChangesAsync();

            MessageBox.Show(
                $"Đã lập phiếu mượn số {phieu.MaPhieuMuon}.\nNgày phải trả: {phieu.NgayPhaiTra:dd/MM/yyyy}",
                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LamMoi();
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

    // ===================== NGHIỆP VỤ =====================

    private async Task KiemTraDocGiaAsync()
    {
        _gio.Clear();
        _maDocGia = null;
        _soSachDangGiu = 0;
        grpChonSach.Enabled = false;
        CapNhatDem();

        string ma = txtMaDocGia.Text.Trim().ToUpperInvariant();
        if (ma == "")
        {
            MessageBox.Show("Hãy nhập mã độc giả.", "Thiếu thông tin",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var db = new AppDbContext();
        var dg = await db.DocGias.AsNoTracking().FirstOrDefaultAsync(d => d.MaDocGia == ma);
        if (dg == null)
        {
            Chan($"Không tìm thấy độc giả có mã \"{ma}\".");
            return;
        }

        // Điều kiện chặn 1: thẻ bị khóa
        if (dg.BiKhoaThe)
        {
            Chan($"Thẻ của {dg.HoTen} đang bị khóa, không thể mượn sách.");
            return;
        }

        // Điều kiện chặn 2: còn nợ phạt
        if (dg.TienNoPhat > 0)
        {
            Chan($"{dg.HoTen} đang nợ phạt {dg.TienNoPhat:N0} VNĐ.\n" +
                 "Yêu cầu đóng phạt trước (màn hình Quản lý Độc giả).");
            return;
        }

        // Điều kiện chặn 3: đã giữ đủ số cuốn tối đa
        int dangGiu = await DemSachDangGiuAsync(db, ma);
        if (dangGiu >= SoSachToiDa)
        {
            Chan($"{dg.HoTen} đang giữ {dangGiu} cuốn chưa trả, đã đạt mức tối đa {SoSachToiDa} cuốn.");
            return;
        }

        _maDocGia = ma;
        _soSachDangGiu = dangGiu;
        grpChonSach.Enabled = true;
        lblThongTinDocGia.ForeColor = Color.ForestGreen;
        lblThongTinDocGia.Text =
            $"{dg.HoTen} | SĐT: {dg.SoDienThoai} | Đang giữ: {dangGiu}/{SoSachToiDa} cuốn | Đủ điều kiện mượn";
        CapNhatDem();
        txtMaCuonSach.Focus();
    }

    private static Task<int> DemSachDangGiuAsync(AppDbContext db, string maDocGia)
    {
        return db.ChiTietPhieuMuons.CountAsync(
            ct => ct.NgayTraThucTe == null && ct.PhieuMuon!.MaDocGia == maDocGia);
    }

    // ===================== HÀM PHỤ =====================

    private void Chan(string lyDo)
    {
        lblThongTinDocGia.ForeColor = Color.Firebrick;
        lblThongTinDocGia.Text = lyDo;
        MessageBox.Show(lyDo, "Không thể cho mượn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    private void LamMoi()
    {
        _gio.Clear();
        _maDocGia = null;
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

    private static void BaoLoi(Exception ex)
    {
        MessageBox.Show($"Có lỗi xảy ra:\n{ex.Message}", "Lỗi",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}