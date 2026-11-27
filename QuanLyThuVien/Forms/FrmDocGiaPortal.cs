using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.DTOs;
using QuanLyThuVien.Entities;
using QuanLyThuVien.UI;
using System.Reflection.PortableExecutable;

namespace QuanLyThuVien.Forms;

public partial class FrmDocGiaPortal : Form
{
    private readonly string _maDocGia;
    private string _tenDocGia = string.Empty;
    private bool _dangXuat;
    private bool _dangNap;                         // true khi đang nạp ComboBox, để bỏ qua sự kiện
    private List<TheLoai> _tatCaTheLoai = new();

    // Program.cs đọc cờ này để biết nên quay lại đăng nhập hay thoát hẳn
    public bool DaDangXuat => _dangXuat;

    // Các dòng hiển thị trong bảng (khai báo ngay trong form, không đụng vào thư mục DTOs)
    private sealed record DongLichSu(int MaPhieu, string TenSach, string MaCuon,
        string NgayMuon, string HanTra, string NgayTra, string TrangThai);
    private sealed record DongYeuCau(int MaYeuCau, string TenSach, string NgayGui, string TrangThai);
    private sealed record DongHoaDon(int MaHoaDon, string NgayLap, string LyDo, string SoTien, string TrangThai);
    private sealed record MucTheLoai(string Ma, string Ten);

    public FrmDocGiaPortal()
    {
        InitializeComponent();
        _maDocGia = PhienDangNhap.MaDocGia ?? string.Empty;

        Theme.Apply(this);
        Theme.FadeIn(this);
        Theme.StyleStatusStrip(statusStrip1);

        // Thanh tiêu đề
        pnlHeader.BackColor = Theme.CardBg;
        lblTieuDe.Font = Theme.FontTitle;
        lblTieuDe.ForeColor = Theme.PrimaryDark;
        lblThongTin.ForeColor = Theme.Neutral;
        lblGhiChu.ForeColor = Theme.Neutral;
        Theme.StyleButton(btnDoiMatKhau, Theme.Secondary);
        Theme.StyleButton(btnDangXuat, Theme.Neutral);

        // Nền các tab đồng màu với form
        tabTraCuu.UseVisualStyleBackColor = false;
        tabTraCuu.BackColor = Theme.BgMain;
        tabLichSu.UseVisualStyleBackColor = false;
        tabLichSu.BackColor = Theme.BgMain;

        CauHinhLuoi();
        btnGoiYeuCau.Enabled = false;
    }

    // =====================================================================
    // Cấu hình cột cho 4 bảng
    // =====================================================================
    private static DataGridViewTextBoxColumn Cot(string prop, string tieuDe, float trongSo)
        => new()
        {
            Name = prop,
            DataPropertyName = prop,
            HeaderText = tieuDe,
            FillWeight = trongSo,
            MinimumWidth = 60,
            ReadOnly = true
        };

    private void CauHinhLuoi()
    {
        foreach (var dgv in new[] { dgvSachThuVien, dgvLichSuMuon, dgvYeuCauCuaToi, dgvHoaDon })
        {
            dgv.AutoGenerateColumns = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ReadOnly = true;
            dgv.MultiSelect = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.CellFormatting += dgv_CellFormatting;
        }

        dgvSachThuVien.Columns.AddRange(
            Cot("MaDauSach", "Mã đầu sách", 12),
            Cot("TenDauSach", "Tên đầu sách", 30),
            Cot("TacGia", "Tác giả", 20),
            Cot("NamXuatBan", "Năm XB", 8),
            Cot("TenTheLoai", "Thể loại", 18),
            Cot("TongSoCuon", "Số bản sao", 10),
            Cot("SoCuonSanSang", "Còn sẵn sàng", 12));

        dgvLichSuMuon.Columns.AddRange(
            Cot("MaPhieu", "Mã phiếu", 8),
            Cot("TenSach", "Tên đầu sách", 30),
            Cot("MaCuon", "Mã cuốn", 12),
            Cot("NgayMuon", "Ngày mượn", 12),
            Cot("HanTra", "Hạn trả", 12),
            Cot("NgayTra", "Ngày trả", 12),
            Cot("TrangThai", "Trạng thái", 12));

        dgvYeuCauCuaToi.Columns.AddRange(
            Cot("MaYeuCau", "Mã yêu cầu", 12),
            Cot("TenSach", "Tên đầu sách", 40),
            Cot("NgayGui", "Ngày gửi", 20),
            Cot("TrangThai", "Trạng thái", 18));

        dgvHoaDon.Columns.AddRange(
            Cot("MaHoaDon", "Mã hóa đơn", 12),
            Cot("NgayLap", "Ngày lập", 14),
            Cot("LyDo", "Lý do phạt", 40),
            Cot("SoTien", "Số tiền", 16),
            Cot("TrangThai", "Trạng thái", 16));
    }

    // Tô màu chữ theo trạng thái
    private void dgv_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (sender is not DataGridView dgv || e.RowIndex < 0 || e.CellStyle == null) return;
        string cot = dgv.Columns[e.ColumnIndex].DataPropertyName;

        if (cot == "TrangThai" && e.Value is string v)
        {
            e.CellStyle.ForeColor = v switch
            {
                "Quá hạn" or "Từ chối" or "Chưa thanh toán" => Theme.Danger,
                "Đang mượn" or "Chờ duyệt" => Color.FromArgb(217, 119, 6),
                "Đã trả" or "Đã duyệt" or "Đã thanh toán" => Color.FromArgb(22, 163, 74),
                _ => e.CellStyle.ForeColor
            };
        }
        else if (cot == "SoCuonSanSang" && e.Value is int n && n == 0)
        {
            e.CellStyle.ForeColor = Theme.Danger;
        }
    }

    // =====================================================================
    // Vòng đời form
    // =====================================================================
    private async void FrmDocGiaPortal_Load(object? sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(_maDocGia))
        {
            MessageBox.Show("Tài khoản này chưa được gắn với độc giả nào.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _dangXuat = true;          // quay lại màn hình đăng nhập
            Close();
            return;
        }

        CapNhatDongHo();
        timerDongHo.Start();
        await KiemTraKetNoiAsync();
        await NapThongTinDocGiaAsync();
        await NapTheLoaiAsync();
        await NapSachAsync();
    }

    private void FrmDocGiaPortal_FormClosing(object? sender, FormClosingEventArgs e)
    {
        if (_dangXuat || e.CloseReason != CloseReason.UserClosing) return;

        var kq = MessageBox.Show("Bạn có chắc muốn thoát chương trình?", "Xác nhận",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (kq == DialogResult.No)
            e.Cancel = true;
    }

    private void timerDongHo_Tick(object? sender, EventArgs e) => CapNhatDongHo();

    private void CapNhatDongHo()
    {
        lblDongHo.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
    }

    private async Task KiemTraKetNoiAsync()
    {
        try
        {
            using var db = new AppDbContext();
            bool ok = await db.Database.CanConnectAsync();
            lblKetNoi.Text = ok ? "●  CSDL SQLite: Đã kết nối" : "●  CSDL SQLite: Mất kết nối";
            lblKetNoi.ForeColor = ok ? Color.FromArgb(22, 163, 74) : Theme.Danger;
        }
        catch
        {
            lblKetNoi.Text = "●  CSDL SQLite: Mất kết nối";
            lblKetNoi.ForeColor = Theme.Danger;
        }
    }

    // =====================================================================
    // Thanh tiêu đề: thông tin độc giả + nợ phạt
    // =====================================================================
    private async Task NapThongTinDocGiaAsync()
    {
        using var db = new AppDbContext();
        var dg = await db.DocGias.AsNoTracking().FirstOrDefaultAsync(d => d.MaDocGia == _maDocGia);
        if (dg == null) return;

        _tenDocGia = dg.HoTen;
        lblThongTin.Text = $"Độc giả: {dg.HoTen} ({dg.MaDocGia})"
                         + (dg.BiKhoaThe ? "   |   THẺ ĐANG BỊ KHÓA" : "");
        lblThongTin.ForeColor = dg.BiKhoaThe ? Theme.Danger : Theme.Neutral;

        lblTienNoPhat.Text = $"Tiền nợ phạt: {dg.TienNoPhat:N0} đ";
        lblTienNoPhat.ForeColor = dg.TienNoPhat > 0 ? Theme.Danger : Color.FromArgb(22, 163, 74);

        grpHoaDon.Text = $"HÓA ĐƠN PHẠT CỦA TÔI  (Tổng nợ hiện tại: {dg.TienNoPhat:N0} đ)";
        grpHoaDon.Invalidate();
    }

    // =====================================================================
    // TAB 1: Tra cứu sách + gửi yêu cầu mượn
    // =====================================================================
    private async Task NapTheLoaiAsync()
    {
        using var db = new AppDbContext();
        _tatCaTheLoai = await db.TheLoais.AsNoTracking().ToListAsync();

        var tenTheoMa = _tatCaTheLoai.ToDictionary(t => t.MaTheLoai, t => t.TenTheLoai);
        var muc = _tatCaTheLoai
            .Select(t => new MucTheLoai(
                t.MaTheLoai,
                t.MaTheLoaiCha != null && tenTheoMa.TryGetValue(t.MaTheLoaiCha, out var tenCha)
                    ? $"{tenCha} › {t.TenTheLoai}"
                    : t.TenTheLoai))
            .OrderBy(m => m.Ten)
            .ToList();
        muc.Insert(0, new MucTheLoai("", "Tất cả thể loại"));

        _dangNap = true;
        cboTheLoai.DisplayMember = nameof(MucTheLoai.Ten);
        cboTheLoai.ValueMember = nameof(MucTheLoai.Ma);
        cboTheLoai.DataSource = muc;
        cboTheLoai.SelectedIndex = 0;
        _dangNap = false;
    }

    // Lấy thể loại được chọn và toàn bộ thể loại con cháu của nó
    private HashSet<string> LayTheLoaiVaCon(string ma)
    {
        var ketQua = new HashSet<string> { ma };
        var hangDoi = new Queue<string>();
        hangDoi.Enqueue(ma);

        while (hangDoi.Count > 0)
        {
            string cha = hangDoi.Dequeue();
            foreach (var con in _tatCaTheLoai.Where(t => t.MaTheLoaiCha == cha))
            {
                if (ketQua.Add(con.MaTheLoai))
                    hangDoi.Enqueue(con.MaTheLoai);
            }
        }
        return ketQua;
    }

    private async Task NapSachAsync()
    {
        using var db = new AppDbContext();
        var tatCa = await db.DauSachs.AsNoTracking()
            .Include(d => d.TheLoai)
            .Include(d => d.CuonSachs)
            .ToListAsync();

        string tuKhoa = txtTimKiem.Text.Trim();
        string maTheLoai = cboTheLoai.SelectedValue as string ?? "";
        HashSet<string>? tapTheLoai = maTheLoai.Length > 0 ? LayTheLoaiVaCon(maTheLoai) : null;

        // Lọc trong bộ nhớ để tìm không phân biệt hoa thường kể cả tiếng Việt có dấu
        var ds = tatCa
            .Where(d => tapTheLoai == null || tapTheLoai.Contains(d.MaTheLoai))
            .Where(d => tuKhoa.Length == 0
                     || d.TenDauSach.Contains(tuKhoa, StringComparison.CurrentCultureIgnoreCase)
                     || d.TacGia.Contains(tuKhoa, StringComparison.CurrentCultureIgnoreCase)
                     || d.MaDauSach.Contains(tuKhoa, StringComparison.CurrentCultureIgnoreCase))
            .OrderBy(d => d.MaDauSach)
            .Select(d => new DauSachDto(
                d.MaDauSach, d.TenDauSach, d.TacGia, d.NamXuatBan, d.GiaBia,
                d.MaTheLoai, d.TheLoai?.TenTheLoai ?? "",
                d.CuonSachs.Count,
                d.CuonSachs.Count(c => c.TrangThai == TrangThaiSach.SanSang)))
            .ToList();

        dgvSachThuVien.DataSource = ds;
        CapNhatSachDangChon();
    }

    private void CapNhatSachDangChon()
    {
        if (dgvSachThuVien.CurrentRow?.DataBoundItem is DauSachDto d)
        {
            lblSachChon.Text = $"Đầu sách đang chọn: {d.MaDauSach} - {d.TenDauSach}";
            btnGoiYeuCau.Enabled = true;
        }
        else
        {
            lblSachChon.Text = "Đầu sách đang chọn: (chưa chọn)";
            btnGoiYeuCau.Enabled = false;
        }
    }

    private void dgvSachThuVien_SelectionChanged(object? sender, EventArgs e) => CapNhatSachDangChon();

    private async void btnTimKiem_Click(object? sender, EventArgs e) => await NapSachAsync();

    private void txtTimKiem_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter) return;
        e.SuppressKeyPress = true;
        btnTimKiem.PerformClick();
    }

    private async void cboTheLoai_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (_dangNap) return;
        await NapSachAsync();
    }

    private async void btnGoiYeuCau_Click(object? sender, EventArgs e)
    {
        if (dgvSachThuVien.CurrentRow?.DataBoundItem is not DauSachDto sach)
        {
            Toast.CanhBao(this, "Hãy chọn một đầu sách trong danh sách.");
            return;
        }

        using var db = new AppDbContext();
        var dg = await db.DocGias.AsNoTracking().FirstOrDefaultAsync(d => d.MaDocGia == _maDocGia);
        if (dg == null) return;

        if (dg.BiKhoaThe)
        {
            Toast.Loi(this, "Thẻ của bạn đang bị khóa, không thể gửi yêu cầu mượn.");
            return;
        }
        if (dg.TienNoPhat > 0)
        {
            Toast.CanhBao(this, $"Bạn còn nợ phạt {dg.TienNoPhat:N0} đ. Hãy thanh toán tại quầy trước khi mượn.");
            return;
        }
        if (sach.SoCuonSanSang == 0)
        {
            Toast.CanhBao(this, "Đầu sách này hiện chưa có bản sẵn sàng.");
            return;
        }

        bool daGui = await db.YeuCauMuons.AnyAsync(y =>
            y.MaDocGia == _maDocGia &&
            y.MaDauSach == sach.MaDauSach &&
            y.TrangThai == TrangThaiYeuCau.ChoDuyet);
        if (daGui)
        {
            Toast.CanhBao(this, "Bạn đã gửi yêu cầu cho đầu sách này và đang chờ duyệt.");
            return;
        }

        db.YeuCauMuons.Add(new YeuCauMuon
        {
            MaDocGia = _maDocGia,
            MaDauSach = sach.MaDauSach,
            NgayGui = DateTime.Now,
            TrangThai = TrangThaiYeuCau.ChoDuyet
        });
        await db.SaveChangesAsync();

        Toast.ThanhCong(this, $"Đã gửi yêu cầu mượn \"{sach.TenDauSach}\". Hãy nhận sách tại quầy khi được duyệt.");
    }

    // =====================================================================
    // TAB 2: Lịch sử mượn, yêu cầu đã gửi, hóa đơn phạt
    // =====================================================================
    private async void tabControl1_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (tabControl1.SelectedTab == tabLichSu)
            await NapLichSuAsync();
    }

    private async Task NapLichSuAsync()
    {
        using var db = new AppDbContext();
        DateTime homNay = DateTime.Today;

        // 1. Lịch sử mượn (mọi truy vấn đều lọc theo độc giả đang đăng nhập)
        var chiTiet = await db.ChiTietPhieuMuons.AsNoTracking()
            .Include(c => c.PhieuMuon)
            .Include(c => c.CuonSach).ThenInclude(cs => cs!.DauSach)
            .Where(c => c.PhieuMuon != null && c.PhieuMuon.MaDocGia == _maDocGia)
            .ToListAsync();

        dgvLichSuMuon.DataSource = chiTiet
            .OrderByDescending(c => c.PhieuMuon!.NgayMuon)
            .ThenByDescending(c => c.Id)
            .Select(c =>
            {
                var phieu = c.PhieuMuon!;
                string trangThai = c.NgayTraThucTe != null
                    ? "Đã trả"
                    : (homNay > phieu.NgayPhaiTra.Date ? "Quá hạn" : "Đang mượn");
                return new DongLichSu(
                    c.MaPhieuMuon,
                    c.CuonSach?.DauSach?.TenDauSach ?? "",
                    c.MaCuonSach,
                    phieu.NgayMuon.ToString("dd/MM/yyyy"),
                    phieu.NgayPhaiTra.ToString("dd/MM/yyyy"),
                    c.NgayTraThucTe?.ToString("dd/MM/yyyy") ?? "",
                    trangThai);
            })
            .ToList();

        // 2. Yêu cầu mượn đã gửi
        var yeuCau = await db.YeuCauMuons.AsNoTracking()
            .Include(y => y.DauSach)
            .Where(y => y.MaDocGia == _maDocGia)
            .ToListAsync();

        dgvYeuCauCuaToi.DataSource = yeuCau
            .OrderByDescending(y => y.NgayGui)
            .Select(y => new DongYeuCau(
                y.MaYeuCau,
                y.DauSach?.TenDauSach ?? y.MaDauSach,
                y.NgayGui.ToString("dd/MM/yyyy HH:mm"),
                y.TrangThai))
            .ToList();

        // 3. Hóa đơn phạt
        var hoaDon = await db.HoaDonPhats.AsNoTracking()
            .Where(h => h.MaDocGia == _maDocGia)
            .ToListAsync();

        dgvHoaDon.DataSource = hoaDon
            .OrderByDescending(h => h.NgayLap)
            .Select(h => new DongHoaDon(
                h.MaHoaDon,
                h.NgayLap.ToString("dd/MM/yyyy"),
                h.LyDo ?? "",
                (h.TienPhatQuaHan + h.TienBoiThuong).ToString("N0") + " đ",
                h.DaThanhToan ? "Đã thanh toán" : "Chưa thanh toán"))
            .ToList();

        await NapThongTinDocGiaAsync();   // làm mới số nợ ở tiêu đề
    }

    // =====================================================================
    // Đổi mật khẩu / Đăng xuất
    // =====================================================================
    private void btnDoiMatKhau_Click(object? sender, EventArgs e)
    {
        using var dlg = new DlgDoiMatKhau(PhienDangNhap.MaTaiKhoan);
        if (dlg.ShowDialog(this) == DialogResult.OK)
            Toast.ThanhCong(this, "Đã đổi mật khẩu thành công.");
    }

    private void btnDangXuat_Click(object? sender, EventArgs e)
    {
        var kq = MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Xác nhận",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (kq != DialogResult.Yes) return;

        _dangXuat = true;
        PhienDangNhap.DangXuat();
        Close();
    }
}