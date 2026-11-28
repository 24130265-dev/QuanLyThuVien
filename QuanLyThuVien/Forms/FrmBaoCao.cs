using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.UI;

namespace QuanLyThuVien.Forms;

public partial class FrmBaoCao : Form
{
    private readonly List<KeyValuePair<string, int>> _duLieuBieuDoMuon = new();
    private bool _dangTai;

    private readonly Font _fontKpiTitle = new("Segoe UI", 8.5F, FontStyle.Bold);
    private readonly Font _fontKpiValue = new("Segoe UI", 15.5F, FontStyle.Bold);
    private readonly Font _fontKpiValueCompact = new("Segoe UI", 12.5F, FontStyle.Bold);
    private readonly Font _fontKpiSub = new("Segoe UI", 8F, FontStyle.Regular);
    private readonly Font _fontEmptyGrid = new("Segoe UI", 10F, FontStyle.Italic);
    private readonly Font _fontQuickBtn = new("Segoe UI", 8.5F, FontStyle.Bold);
    private readonly ToolTip _tip = new();

    private static readonly Dictionary<string, (string Header, float Weight, int MinWidth)> CauHinhCot = new()
    {
        // Tab 1: Mượn - Trả
        ["MaPhieu"]       = ("Phiếu", 9, 55),
        ["TenDocGia"]     = ("Độc giả", 17, 105),
        ["TenDauSach"]    = ("Đầu sách", 23, 125),
        ["MaCuonSach"]    = ("Mã cuốn", 11, 75),
        ["NgayMuon"]      = ("Ngày mượn", 12, 105),
        ["HanTra"]        = ("Hạn trả", 12, 95),
        ["NgayTraThucTe"] = ("Ngày trả", 12, 95),
        ["TrangThai"]     = ("Trạng thái", 14, 92),

        // Tab 2: Top đầu sách
        ["MaDauSach"]     = ("Mã đầu sách", 14, 95),
        ["TenSach"]       = ("Tên đầu sách", 34, 160),
        ["TheLoai"]       = ("Thể loại", 22, 110),
        ["SoLuotMuon"]    = ("Lượt mượn", 15, 90),
        ["TongSoCuon"]    = ("Tổng số cuốn", 15, 95),

        // Tab 3: Hóa đơn phạt
        ["MaHoaDon"]      = ("Mã HĐ", 10, 65),
        ["NgayLap"]       = ("Ngày lập", 14, 90),
        ["SoTien"]        = ("Số tiền (VNĐ)", 16, 105),
        ["LyDo"]          = ("Lý do phạt", 30, 150),

        // Tab 4: Độc giả quá hạn
        ["MaDocGia"]      = ("Mã ĐG", 12, 80),
        ["HoTen"]         = ("Họ tên", 20, 120),
        ["SoDienThoai"]   = ("Số điện thoại", 14, 100),
        ["SoNgayTre"]     = ("Số ngày trễ", 12, 85)
    };

    private static readonly HashSet<string> CotCanPhai = new()
    {
        "SoTien", "SoLuotMuon", "TongSoCuon", "SoNgayTre"
    };

    private static readonly HashSet<string> CotCanGiua = new()
    {
        "MaPhieu", "MaHoaDon", "MaCuonSach", "MaDocGia", "MaDauSach"
    };

    public FrmBaoCao()
    {
        InitializeComponent();
        
        Theme.Apply(this);

        // Bật DoubleBuffered và tự vẽ lại biểu đồ khi thay đổi kích thước
        typeof(Panel).GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
            ?.SetValue(pnlBieuDoMuon, true);
        pnlBieuDoMuon.Resize += (_, _) => pnlBieuDoMuon.Invalidate();

        KhoiTaoKhungBieuDoMacDinh(DateTime.Today);
        KhoiTaoTheKpi();
        CauHinhGiaoDienBaoCao();

        CauHinhLuoi(dgvMuonTra);
        CauHinhLuoi(dgvTopSach);
        CauHinhLuoi(dgvTienPhat);
        CauHinhLuoi(dgvQuaHan);

        _tip.SetToolTip(dtpTuNgay,
    "Lọc theo NGÀY MƯỢN (tab Mượn - Trả, Top đầu sách) và NGÀY LẬP hóa đơn (tab Tiền phạt). " +
    "Tab Độc giả quá hạn luôn tính đến hôm nay.");
        _tip.SetToolTip(dtpDenNgay, _tip.GetToolTip(dtpTuNgay));
    }

    // =====================================================================
    // VÒNG ĐỜI & SỰ KIỆN
    // =====================================================================
    private async void FrmBaoCao_Load(object? sender, EventArgs e)
    {
        DateTime homNay = DateTime.Today;
        dtpTuNgay.Value = new DateTime(homNay.Year, homNay.Month, 1);
        dtpDenNgay.Value = homNay;

        CapNhatChieuCaoHeader();
        await NapDuLieuBaoCaoAsync();
    }

    private async void btnXemBaoCao_Click(object? sender, EventArgs e)
    {
        if (dtpTuNgay.Value.Date > dtpDenNgay.Value.Date)
        {
            Toast.CanhBao(this, "Ngày bắt đầu không được lớn hơn ngày kết thúc.");
            return;
        }
        await NapDuLieuBaoCaoAsync();
    }

    private async void btnThangNay_Click(object? sender, EventArgs e)
    {
        var h = DateTime.Today;
        await ApDungBoLocNhanhAsync(new DateTime(h.Year, h.Month, 1), h);
    }

    private async void btn30Ngay_Click(object? sender, EventArgs e)
    {
        var h = DateTime.Today;
        await ApDungBoLocNhanhAsync(h.AddDays(-29), h);
    }

    private async void btnNamNay_Click(object? sender, EventArgs e)
    {
        var h = DateTime.Today;
        await ApDungBoLocNhanhAsync(new DateTime(h.Year, 1, 1), h);
    }

    private async Task ApDungBoLocNhanhAsync(DateTime tuNgay, DateTime denNgay)
    {
        dtpTuNgay.Value = tuNgay;
        dtpDenNgay.Value = denNgay;
        await NapDuLieuBaoCaoAsync();
    }

    // =====================================================================
    // XUẤT FILE CSV (THEO TAB ĐANG MỞ)
    // =====================================================================
    private void btnXuatCsv_Click(object? sender, EventArgs e)
    {
        var (gridHienTai, tenFile) = LayLuoiDangHienThi();

        if (gridHienTai.Rows.Count == 0)
        {
            Toast.CanhBao(this, "Không có dữ liệu để xuất trong tab hiện tại.");
            return;
        }

        using var sfd = new SaveFileDialog
        {
            Filter = "CSV file (*.csv)|*.csv",
            FileName = $"BaoCao_{tenFile}_{DateTime.Now:yyyyMMdd_HHmm}.csv"
        };

        if (sfd.ShowDialog(this) != DialogResult.OK)
            return;

        try
        {
            var sb = new StringBuilder();
            var visibleCols = gridHienTai.Columns
                .Cast<DataGridViewColumn>()
                .Where(c => c.Visible)
                .OrderBy(c => c.DisplayIndex)
                .ToList();

            sb.AppendLine(string.Join(",",
                visibleCols.Select(c => $"\"{c.HeaderText.Replace("\"", "\"\"")}\"")));

            foreach (DataGridViewRow row in gridHienTai.Rows)
            {
                if (row.IsNewRow) continue;

                var cells = visibleCols.Select(col =>
                {
                    string giaTri = row.Cells[col.Index].FormattedValue?.ToString() ?? "";
                    return $"\"{giaTri.Replace("\"", "\"\"")}\"";
                });
                sb.AppendLine(string.Join(",", cells));
            }

            // UTF-8 có BOM để Microsoft Excel đọc chuẩn tiếng Việt có dấu
            var utf8WithBom = new UTF8Encoding(encoderShouldEmitUTF8Identifier: true);
            File.WriteAllText(sfd.FileName, sb.ToString(), utf8WithBom);

            Toast.ThanhCong(this, "Xuất báo cáo CSV thành công!");
        }
        catch (IOException ex)
        {
            Toast.Loi(this, $"Không thể ghi file (có thể file đang mở ở Excel): {ex.Message}");
        }
        catch (Exception ex)
        {
            Toast.Loi(this, $"Xuất CSV thất bại: {ex.Message}");
        }
    }

    private (DataGridView Grid, string TenFile) LayLuoiDangHienThi()
    {
        var tab = tabBaoCao.SelectedTab;
        if (tab == tabTopSach) return (dgvTopSach, "TopDauSach");
        if (tab == tabTienPhat) return (dgvTienPhat, "HoaDonPhat");
        if (tab == tabQuaHan) return (dgvQuaHan, "DocGiaQuaHan");
        return (dgvMuonTra, "MuonTra");
    }

    // =====================================================================
    // NẠP DỮ LIỆU BÁO CÁO (ASYNC)
    // =====================================================================
    private async Task NapDuLieuBaoCaoAsync()
    {
        if (_dangTai) return;
        _dangTai = true;
        DatTrangThaiDieuKhien(false);

        try
        {
            using var context = new AppDbContext();

            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddTicks(-1);
            DateTime homNay = DateTime.Today;

            // 1. Báo cáo Mượn - Trả trong kỳ lọc
            var dsMuonTraRaw = await context.ChiTietPhieuMuons
                .AsNoTracking()
                .Where(ct => ct.PhieuMuon != null
                          && ct.PhieuMuon.NgayMuon >= tuNgay
                          && ct.PhieuMuon.NgayMuon <= denNgay)
                .OrderByDescending(ct => ct.PhieuMuon!.NgayMuon)
                .Select(ct => new
                {
                    MaPhieu = ct.MaPhieuMuon,
                    TenDocGia = ct.PhieuMuon!.DocGia != null ? ct.PhieuMuon.DocGia.HoTen : "",
                    TenDauSach = ct.CuonSach != null && ct.CuonSach.DauSach != null
                        ? ct.CuonSach.DauSach.TenDauSach
                        : "",
                    ct.MaCuonSach,
                    ct.PhieuMuon.NgayMuon,
                    HanTra = ct.PhieuMuon.NgayPhaiTra,
                    ct.NgayTraThucTe
                })
                .ToListAsync();

            var dsMuonTra = dsMuonTraRaw.Select(ct => new BaoCaoMuonTraDto
            {
                MaPhieu = ct.MaPhieu,
                TenDocGia = ct.TenDocGia ?? "",
                TenDauSach = ct.TenDauSach ?? "",
                MaCuonSach = ct.MaCuonSach,
                NgayMuon = ct.NgayMuon,
                HanTra = ct.HanTra,
                NgayTraThucTe = ct.NgayTraThucTe,
                TrangThai = ct.NgayTraThucTe != null
                    ? "Đã trả"
                    : (homNay > ct.HanTra.Date ? "Quá hạn" : "Đang mượn")
            }).ToList();

            // 2. Biểu đồ 5 tháng gần nhất tính đến tháng của dtpDenNgay
            var thangKetThuc = new DateTime(dtpDenNgay.Value.Year, dtpDenNgay.Value.Month, 1);
            var thangBatDau = thangKetThuc.AddMonths(-4);
            var ngayCuoiBieuDo = thangKetThuc.AddMonths(1).AddTicks(-1);

            var ngayMuon5Thang = await context.ChiTietPhieuMuons
                .AsNoTracking()
                .Where(ct => ct.PhieuMuon != null
                          && ct.PhieuMuon.NgayMuon >= thangBatDau
                          && ct.PhieuMuon.NgayMuon <= ngayCuoiBieuDo)
                .Select(ct => ct.PhieuMuon!.NgayMuon)
                .ToListAsync();

            var soLuotTheoThang = ngayMuon5Thang
                .GroupBy(d => new DateTime(d.Year, d.Month, 1))
                .ToDictionary(g => g.Key, g => g.Count());

            var duLieuBieuDoMoi = new List<KeyValuePair<string, int>>(5);
            for (int i = 4; i >= 0; i--)
            {
                var thang = thangKetThuc.AddMonths(-i);
                soLuotTheoThang.TryGetValue(thang, out int soLuot);
                duLieuBieuDoMoi.Add(new KeyValuePair<string, int>($"T{thang:MM/yy}", soLuot));
            }

            // 3. Top Đầu Sách Mượn Nhiều trong kỳ lọc
            var tongSoCuonTheoDauSach = await context.CuonSachs
                .AsNoTracking()
                .GroupBy(c => c.MaDauSach)
                .Select(g => new { MaDauSach = g.Key, Tong = g.Count() })
                .ToDictionaryAsync(x => x.MaDauSach, x => x.Tong);

            var topSachRaw = await context.ChiTietPhieuMuons
                .AsNoTracking()
                .Where(ct => ct.PhieuMuon != null
                          && ct.CuonSach != null
                          && ct.CuonSach.DauSach != null
                          && ct.PhieuMuon.NgayMuon >= tuNgay
                          && ct.PhieuMuon.NgayMuon <= denNgay)
                .GroupBy(ct => new
                {
                    ct.CuonSach!.DauSach!.MaDauSach,
                    ct.CuonSach.DauSach.TenDauSach,
                    TenTheLoai = ct.CuonSach.DauSach.TheLoai != null
                        ? ct.CuonSach.DauSach.TheLoai.TenTheLoai
                        : ""
                })
                .Select(g => new
                {
                    g.Key.MaDauSach,
                    TenSach = g.Key.TenDauSach ?? "",
                    TheLoai = g.Key.TenTheLoai ?? "",
                    SoLuotMuon = g.Count()
                })
                .OrderByDescending(x => x.SoLuotMuon)
                .ToListAsync();

            var topSach = topSachRaw.Select(x => new TopDauSachDto
            {
                MaDauSach = x.MaDauSach,
                TenSach = x.TenSach,
                TheLoai = x.TheLoai,
                SoLuotMuon = x.SoLuotMuon,
                TongSoCuon = tongSoCuonTheoDauSach.TryGetValue(x.MaDauSach, out int tong) ? tong : 0
            }).ToList();

            // 4. Thống kê Hóa Đơn Phạt trong kỳ lọc
            var dsPhat = await context.HoaDonPhats
                .AsNoTracking()
                .Where(h => h.NgayLap >= tuNgay && h.NgayLap <= denNgay)
                .OrderByDescending(h => h.NgayLap)
                .Select(h => new BaoCaoTienPhatDto
                {
                    MaHoaDon = h.MaHoaDon,
                    TenDocGia = h.DocGia != null ? (h.DocGia.HoTen ?? "") : "",
                    NgayLap = h.NgayLap,
                    SoTien = h.TienPhatQuaHan + h.TienBoiThuong,
                    LyDo = h.LyDo ?? "",
                    TrangThai = h.DaThanhToan ? "Đã thanh toán" : "Chưa thanh toán"
                })
                .ToListAsync();

            // 5. Độc giả quá hạn chưa trả (toàn hệ thống)
            var dsDocGiaQuaHanRaw = await context.ChiTietPhieuMuons
                .AsNoTracking()
                .Where(ct => ct.NgayTraThucTe == null
                          && ct.PhieuMuon != null
                          && ct.PhieuMuon.NgayPhaiTra < homNay
                          && ct.PhieuMuon.DocGia != null
                          && ct.CuonSach != null
                          && ct.CuonSach.DauSach != null)
                .OrderBy(ct => ct.PhieuMuon!.NgayPhaiTra)
                .Select(ct => new
                {
                    ct.PhieuMuon!.MaDocGia,
                    HoTen = ct.PhieuMuon.DocGia!.HoTen,
                    SoDienThoai = ct.PhieuMuon.DocGia.SoDienThoai,
                    TenDauSach = ct.CuonSach!.DauSach!.TenDauSach,
                    HanTra = ct.PhieuMuon.NgayPhaiTra
                })
                .ToListAsync();

            var dsDocGiaQuaHan = dsDocGiaQuaHanRaw
                .Select(x => new
                {
                    x.MaDocGia,
                    HoTen = x.HoTen ?? "",
                    SoDienThoai = x.SoDienThoai ?? "",
                    TenDauSach = x.TenDauSach ?? "",
                    x.HanTra,
                    SoNgayTre = (homNay - x.HanTra.Date).Days
                })
                .ToList();

            // 6. Chỉ số KPI toàn hệ thống
            int dangMuon = await context.ChiTietPhieuMuons
                .AsNoTracking()
                .CountAsync(ct => ct.NgayTraThucTe == null);

            int quaHan = await context.ChiTietPhieuMuons
                .AsNoTracking()
                .CountAsync(ct => ct.NgayTraThucTe == null
                               && ct.PhieuMuon != null
                               && ct.PhieuMuon.NgayPhaiTra < homNay);

            decimal tongThuPhat = dsPhat
                .Where(x => x.TrangThai == "Đã thanh toán")
                .Sum(x => x.SoTien);

            if (IsDisposed) return;

            // 7. Đổ dữ liệu lên giao diện
            dgvMuonTra.DataSource = dsMuonTra;
            dgvTopSach.DataSource = topSach;
            dgvTienPhat.DataSource = dsPhat;
            dgvQuaHan.DataSource = dsDocGiaQuaHan;

            dgvMuonTra.Invalidate();
            dgvTopSach.Invalidate();
            dgvTienPhat.Invalidate();
            dgvQuaHan.Invalidate();

            _duLieuBieuDoMuon.Clear();
            _duLieuBieuDoMuon.AddRange(duLieuBieuDoMoi);
            pnlBieuDoMuon.Invalidate();

            lblKpiLuotMuon.Text = dsMuonTra.Count.ToString("N0");
            lblKpiDangMuon.Text = dangMuon.ToString("N0");
            lblKpiQuaHan.Text = quaHan.ToString("N0");

            string textTienPhat = $"{tongThuPhat:N0} VNĐ";
            lblKpiTienPhat.Font = textTienPhat.Length > 11 ? _fontKpiValueCompact : _fontKpiValue;
            lblKpiTienPhat.Text = textTienPhat;
        }
        catch (Exception ex)
        {
            NhatKy.Ghi(ex);
            if (!IsDisposed)
            {
                KhoiTaoKhungBieuDoMacDinh(dtpDenNgay.Value);
                pnlBieuDoMuon.Invalidate();
#if DEBUG
                MessageBox.Show(this, ex.ToString(), "Chi tiết lỗi tải báo cáo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
#else
                Toast.Loi(this, "Không thể tải báo cáo. Chi tiết đã được ghi vào error.log.");
#endif
            }
        }
        finally
        {
            _dangTai = false;
            if (!IsDisposed)
                DatTrangThaiDieuKhien(true);
        }
    }

    private void KhoiTaoKhungBieuDoMacDinh(DateTime mocDenNgay)
    {
        _duLieuBieuDoMuon.Clear();
        var thangKetThuc = new DateTime(mocDenNgay.Year, mocDenNgay.Month, 1);
        for (int i = 4; i >= 0; i--)
        {
            var thang = thangKetThuc.AddMonths(-i);
            _duLieuBieuDoMuon.Add(new KeyValuePair<string, int>($"T{thang:MM/yy}", 0));
        }
    }

    private void DatTrangThaiDieuKhien(bool kichHoat)
    {
        btnXemBaoCao.Enabled = kichHoat;
        btnXuatCsv.Enabled = kichHoat;
        btnThangNay.Enabled = kichHoat;
        btn30Ngay.Enabled = kichHoat;
        btnNamNay.Enabled = kichHoat;
        Cursor = kichHoat ? Cursors.Default : Cursors.WaitCursor;
    }

    // =====================================================================
    // VẼ BIỂU ĐỒ CỘT 5 THÁNG GẦN NHẤT
    // =====================================================================
    private void pnlBieuDoMuon_Paint(object? sender, PaintEventArgs e)
    {
        var bounds = pnlBieuDoMuon.ClientRectangle;
        if (bounds.Width < 120 || bounds.Height < 150)
            return;

        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.Clear(pnlBieuDoMuon.BackColor);

        using (var cardBorder = new Pen(Theme.Border))
            g.DrawRectangle(cardBorder, 0, 0, bounds.Width - 1, bounds.Height - 1);

        using var titleFont = new Font("Segoe UI", 9F, FontStyle.Bold);
        using var subFont = new Font("Segoe UI", 8F, FontStyle.Regular);
        using var labelFont = new Font("Segoe UI", 8F, FontStyle.Regular);
        using var valFont = new Font("Segoe UI", 8.5F, FontStyle.Bold);

        using var titleBrush = new SolidBrush(Theme.PrimaryDark);
        using var subBrush = new SolidBrush(Theme.Neutral);
        using var labelBrush = new SolidBrush(Theme.TextDark);
        using var gridPen = new Pen(Color.FromArgb(226, 232, 240)) { DashStyle = DashStyle.Dash };
        using var axisPen = new Pen(Color.FromArgb(148, 163, 184));
        using var barBrush = new SolidBrush(Color.FromArgb(45, 160, 150));
        using var barLatestBrush = new SolidBrush(Theme.Primary);
        using var zeroBrush = new SolidBrush(Color.FromArgb(203, 213, 225));

        using var ellipsis = new StringFormat
        {
            Trimming = StringTrimming.EllipsisCharacter,
            FormatFlags = StringFormatFlags.NoWrap
        };

        g.DrawString(
            "LƯỢT MƯỢN 5 THÁNG GẦN NHẤT",
            titleFont,
            titleBrush,
            new RectangleF(12, 12, bounds.Width - 24, 20),
            ellipsis);

        g.DrawString(
            $"Mốc tính đến tháng {dtpDenNgay.Value:MM/yyyy}",
            subFont,
            subBrush,
            new RectangleF(12, 32, bounds.Width - 24, 16),
            ellipsis);

        var plot = new Rectangle(38, 60, bounds.Width - 52, bounds.Height - 98);
        if (plot.Width <= 20 || plot.Height <= 40)
            return;

        int maxVal = Math.Max(4, _duLieuBieuDoMuon.Select(x => x.Value).DefaultIfEmpty().Max());
        int mocTren = Math.Max(4, (int)Math.Ceiling(maxVal / 4d) * 4);

        using var centered = new StringFormat { Alignment = StringAlignment.Center };
        using var rightAlign = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };

        for (int i = 0; i <= 4; i++)
        {
            int giaTri = mocTren * i / 4;
            float y = plot.Bottom - plot.Height * i / 4f;
            g.DrawLine(i == 0 ? axisPen : gridPen, plot.Left, y, plot.Right, y);
            g.DrawString(
                giaTri.ToString("N0"),
                labelFont,
                subBrush,
                new RectangleF(2, y - 9, 30, 18),
                rightAlign);
        }

        if (_duLieuBieuDoMuon.Count == 0)
            return;

        float slotWidth = plot.Width / (float)_duLieuBieuDoMuon.Count;
        float barWidth = Math.Min(38f, slotWidth * 0.54f);

        for (int i = 0; i < _duLieuBieuDoMuon.Count; i++)
        {
            var item = _duLieuBieuDoMuon[i];
            float x = plot.Left + slotWidth * i + (slotWidth - barWidth) / 2f;
            bool laThangCuoi = i == _duLieuBieuDoMuon.Count - 1;

            if (item.Value > 0)
            {
                float barHeight = Math.Max(6f, plot.Height * item.Value / (float)mocTren);
                float y = plot.Bottom - barHeight;

                using var path = TaoCotBoGocTren(new RectangleF(x, y, barWidth, barHeight), 5f);
                g.FillPath(laThangCuoi ? barLatestBrush : barBrush, path);

                g.DrawString(
                    item.Value.ToString("N0"),
                    valFont,
                    laThangCuoi ? titleBrush : labelBrush,
                    new RectangleF(x - 10, Math.Max(plot.Top - 16, y - 20), barWidth + 20, 18),
                    centered);
            }
            else
            {
                g.FillRectangle(zeroBrush, x, plot.Bottom - 2, barWidth, 2);
                g.DrawString(
                    "0",
                    labelFont,
                    subBrush,
                    new RectangleF(x - 8, plot.Bottom - 20, barWidth + 16, 16),
                    centered);
            }

            g.DrawString(
                item.Key,
                laThangCuoi ? valFont : labelFont,
                laThangCuoi ? titleBrush : subBrush,
                new RectangleF(plot.Left + slotWidth * i, plot.Bottom + 8, slotWidth, 20),
                centered);
        }
    }

    private static GraphicsPath TaoCotBoGocTren(RectangleF r, float radius)
    {
        float d = Math.Min(radius * 2f, Math.Min(r.Width, r.Height));
        var path = new GraphicsPath();
        path.AddArc(r.X, r.Y, d, d, 180, 90);
        path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
        path.AddLine(r.Right, r.Bottom, r.X, r.Bottom);
        path.CloseFigure();
        return path;
    }

    // =====================================================================
    // THIẾT LẬP GIAO DIỆN: KPI, HEADER, NÚT BỘ LỌC & LƯỚI
    // =====================================================================
    private void KhoiTaoTheKpi()
    {
        tblKpi.Controls.Clear();
        tblKpi.ColumnStyles.Clear();
        tblKpi.RowStyles.Clear();

        tblKpi.ColumnCount = 4;
        tblKpi.RowCount = 1;
        tblKpi.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        for (int i = 0; i < 4; i++)
            tblKpi.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));

        tblKpi.Controls.Add(TaoTheKpi("LƯỢT MƯỢN TRONG KỲ", "Theo khoảng ngày đang lọc",
            Color.FromArgb(37, 99, 235), out lblKpiLuotMuon), 0, 0);
        tblKpi.Controls.Add(TaoTheKpi("SÁCH ĐANG MƯỢN", "Toàn hệ thống (chưa trả)",
            Color.FromArgb(13, 148, 136), out lblKpiDangMuon), 1, 0);
        tblKpi.Controls.Add(TaoTheKpi("SÁCH QUÁ HẠN", "Đang mượn quá hạn trả",
            Color.FromArgb(220, 38, 38), out lblKpiQuaHan), 2, 0);
        tblKpi.Controls.Add(TaoTheKpi("TỔNG THU TIỀN PHẠT", "Đã thanh toán trong kỳ",
            Color.FromArgb(217, 119, 6), out lblKpiTienPhat), 3, 0);
    }

    private Panel TaoTheKpi(string tieuDe, string moTaPhu, Color mauNhan, out Label lblGiaTri)
    {
        var card = new Panel
        {
            Dock = DockStyle.Fill,
            Margin = new Padding(6),
            Padding = new Padding(14, 8, 10, 8),
            BackColor = Theme.CardBg
        };

        card.Paint += (_, e) =>
        {
            using var vien = new Pen(Theme.Border);
            using var daiMau = new SolidBrush(mauNhan);
            e.Graphics.DrawRectangle(vien, 0, 0, card.Width - 1, card.Height - 1);
            e.Graphics.FillRectangle(daiMau, 0, 0, 4, card.Height);
        };
        card.Resize += (_, _) => card.Invalidate();

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 3,
            BackColor = Color.Transparent,
            Margin = new Padding(0),
            Padding = new Padding(0)
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        var lblHeader = new Label
        {
            Text = tieuDe,
            Font = _fontKpiTitle,
            ForeColor = Theme.Neutral,
            Dock = DockStyle.Fill,
            AutoSize = true,
            AutoEllipsis = true,
            Margin = new Padding(0, 0, 0, 2)
        };

        lblGiaTri = new Label
        {
            Text = "0",
            Font = _fontKpiValue,
            ForeColor = mauNhan,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft,
            AutoEllipsis = true,
            Margin = new Padding(0)
        };

        var lblSub = new Label
        {
            Text = moTaPhu,
            Font = _fontKpiSub,
            ForeColor = Color.FromArgb(148, 163, 184),
            Dock = DockStyle.Fill,
            AutoSize = true,
            AutoEllipsis = true,
            Margin = new Padding(0, 2, 0, 0)
        };

        layout.Controls.Add(lblHeader, 0, 0);
        layout.Controls.Add(lblGiaTri, 0, 1);
        layout.Controls.Add(lblSub, 0, 2);

        card.Controls.Add(layout);
        return card;
    }

    private void CauHinhGiaoDienBaoCao()
    {
        pnlHeader.BackColor = Theme.CardBg;
        flpBoLoc.BackColor = Theme.CardBg;
        pnlBieuDoMuon.BackColor = Theme.CardBg;

        lblTitle.Font = Theme.FontTitle;
        lblTitle.ForeColor = Theme.PrimaryDark;

        CauHinhNutBoLoc(btnXemBaoCao, Theme.Secondary, Theme.FontBold, chieuCao: 32, coIcon: false, chieuRongToiThieu: 118);
        CauHinhNutBoLoc(btnXuatCsv, Theme.Primary, Theme.FontBold, chieuCao: 32, coIcon: true, chieuRongToiThieu: 120);
        CauHinhNutBoLoc(btnThangNay, Theme.Neutral, _fontQuickBtn, chieuCao: 30, coIcon: false, chieuRongToiThieu: 86);
        CauHinhNutBoLoc(btn30Ngay, Theme.Neutral, _fontQuickBtn, chieuCao: 30, coIcon: false, chieuRongToiThieu: 74);
        CauHinhNutBoLoc(btnNamNay, Theme.Neutral, _fontQuickBtn, chieuCao: 30, coIcon: false, chieuRongToiThieu: 80);

        pnlHeader.Resize += (_, _) => CapNhatChieuCaoHeader();
        CapNhatChieuCaoHeader();

        foreach (TabPage tab in tabBaoCao.TabPages)
        {
            tab.UseVisualStyleBackColor = false;
            tab.BackColor = Theme.BgMain;
        }
    }

    private static void CauHinhNutBoLoc(Button btn, Color mauNen, Font font, int chieuCao, bool coIcon, int chieuRongToiThieu)
    {
        var kichThuocChu = TextRenderer.MeasureText(
            btn.Text,
            font,
            new Size(int.MaxValue, int.MaxValue),
            TextFormatFlags.SingleLine | TextFormatFlags.NoPadding);

        int phanDem = coIcon ? 50 : 28;
        btn.AutoSize = false;
        btn.Size = new Size(Math.Max(chieuRongToiThieu, kichThuocChu.Width + phanDem), chieuCao);

        Theme.StyleButton(btn, mauNen);

        btn.Font = font;
        btn.Padding = Padding.Empty;
        btn.TextAlign = ContentAlignment.MiddleCenter;
        
        btn.FlatStyle = FlatStyle.Flat;
        btn.FlatAppearance.BorderSize = 0;
        btn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
    }

    private void CapNhatChieuCaoHeader()
    {
        if (pnlHeader.ClientSize.Width <= 0) return;

        int chieuRongKhaDung = Math.Max(260, pnlHeader.ClientSize.Width - pnlHeader.Padding.Horizontal);
        flpBoLoc.MaximumSize = new Size(chieuRongKhaDung, 0);

        var kichThuocBoLoc = flpBoLoc.GetPreferredSize(new Size(chieuRongKhaDung, 0));
        int chieuCaoMoi = pnlHeader.Padding.Vertical + lblTitle.Height + kichThuocBoLoc.Height + 6;

        if (pnlHeader.Height != chieuCaoMoi)
            pnlHeader.Height = chieuCaoMoi;
    }

    private void CauHinhLuoi(DataGridView dgv)
    {
        dgv.Dock = DockStyle.Fill;
        dgv.BackgroundColor = Theme.CardBg;
        dgv.BorderStyle = BorderStyle.None;
        dgv.AllowUserToAddRows = false;
        dgv.AllowUserToDeleteRows = false;
        dgv.ReadOnly = true;
        dgv.MultiSelect = false;
        dgv.RowHeadersVisible = false;
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgv.ColumnHeadersHeight = 42;
        dgv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
        dgv.RowTemplate.Height = 34;

        dgv.DataBindingComplete += (_, _) =>
        {
            
            var cheDoCu = dgv.AutoSizeColumnsMode;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            try
            {
                foreach (DataGridViewColumn c in dgv.Columns)
                {
                    string prop = string.IsNullOrEmpty(c.DataPropertyName) ? c.Name : c.DataPropertyName;

                    if (CauHinhCot.TryGetValue(prop, out var cfg))
                    {
                        c.HeaderText = cfg.Header;
                        c.FillWeight = Math.Max(1F, cfg.Weight);
                        c.MinimumWidth = cfg.MinWidth > 0 ? cfg.MinWidth : 80;
                    }
                    else
                    {
                        c.MinimumWidth = 80;
                    }

                    if (c.ValueType == typeof(DateTime) || c.ValueType == typeof(DateTime?))
                    {
                        c.DefaultCellStyle.Format = "dd/MM/yyyy";
                        c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    else if (CotCanPhai.Contains(prop))
                    {
                        c.DefaultCellStyle.Format = "N0";
                        c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                    else if (CotCanGiua.Contains(prop))
                    {
                        c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
            }
            finally
            {
                dgv.AutoSizeColumnsMode = cheDoCu;
            }
        };

        dgv.CellFormatting += (_, e) =>
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.CellStyle == null) return;
            string prop = dgv.Columns[e.ColumnIndex].DataPropertyName;

            if (prop == "TrangThai" && e.Value is string trangThai)
            {
                e.CellStyle.Font = Theme.FontBold;
                e.CellStyle.ForeColor = trangThai switch
                {
                    "Quá hạn" or "Chưa thanh toán" => Theme.Danger,
                    "Đã trả" or "Đã thanh toán"    => Color.FromArgb(22, 163, 74),
                    "Đang mượn"                    => Theme.Secondary,
                    _                              => Theme.TextDark
                };
            }
            else if (prop == "SoNgayTre" && e.Value is int ngayTre && ngayTre > 0)
            {
                e.CellStyle.Font = Theme.FontBold;
                e.CellStyle.ForeColor = Theme.Danger;
            }
        };

        dgv.Paint += (_, e) =>
        {
            if (dgv.Rows.Count > 0) return;

            int headerH = dgv.ColumnHeadersVisible && dgv.Columns.Count > 0 ? dgv.ColumnHeadersHeight : 0;
            int bodyH = Math.Max(40, dgv.ClientSize.Height - headerH);
            var rect = new RectangleF(16, headerH, Math.Max(100, dgv.ClientSize.Width - 32), bodyH);

            using var brush = new SolidBrush(Color.FromArgb(148, 163, 184));
            using var sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            e.Graphics.DrawString("Không có dữ liệu trong khoảng thời gian đã chọn", _fontEmptyGrid, brush, rect, sf);
        };
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        _fontKpiTitle.Dispose();
        _fontKpiValue.Dispose();
        _fontKpiValueCompact.Dispose();
        _fontKpiSub.Dispose();
        _fontEmptyGrid.Dispose();
        _fontQuickBtn.Dispose();
        _tip.Dispose();
        base.OnFormClosed(e);
    }

    // =====================================================================
    // CÁC LỚP DTO HIỂN THỊ BÁO CÁO
    // =====================================================================
    public class BaoCaoMuonTraDto
    {
        public int MaPhieu { get; set; }
        public string TenDocGia { get; set; } = string.Empty;
        public string TenDauSach { get; set; } = string.Empty;
        public string MaCuonSach { get; set; } = string.Empty;
        public DateTime NgayMuon { get; set; }
        public DateTime HanTra { get; set; }
        public DateTime? NgayTraThucTe { get; set; }
        public string TrangThai { get; set; } = string.Empty;
    }

    public class TopDauSachDto
    {
        public string MaDauSach { get; set; } = string.Empty;
        public string TenSach { get; set; } = string.Empty;
        public string TheLoai { get; set; } = string.Empty;
        public int SoLuotMuon { get; set; }
        public int TongSoCuon { get; set; }
    }

    public class BaoCaoTienPhatDto
    {
        public int MaHoaDon { get; set; }
        public string TenDocGia { get; set; } = string.Empty;
        public DateTime NgayLap { get; set; }
        public decimal SoTien { get; set; }
        public string LyDo { get; set; } = string.Empty;
        public string TrangThai { get; set; } = string.Empty;
    }
}