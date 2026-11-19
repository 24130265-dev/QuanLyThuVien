using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
namespace QuanLyThuVien.Forms;

public partial class FrmBaoCao : Form
{
    private readonly List<KeyValuePair<string, int>> _duLieuBieuDoMuon = new();

    public FrmBaoCao()
    {
        InitializeComponent();
    }

    private void FrmBaoCao_Load(object sender, EventArgs e)
    {
        NapDuLieuBaoCao();
    }
    
    private void btnXemBaoCao_Click(object sender, EventArgs e)
    {
        if (dtpTuNgay.Value.Date > dtpDenNgay.Value.Date)
        {
            MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        NapDuLieuBaoCao();
    }
    
    private void NapDuLieuBaoCao()
    {
        using var _context = new AppDbContext();

        DateTime tuNgay = dtpTuNgay.Value.Date;
        DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddTicks(-1);
        DateTime homNay = DateTime.Today;

        // 1. Báo cáo Mượn - Trả & Quá hạn
        var dsMuonTra = _context.ChiTietPhieuMuons
            .Include(ct => ct.PhieuMuon)
                .ThenInclude(p => p!.DocGia)
            .Include(ct => ct.CuonSach)
                .ThenInclude(cs => cs!.DauSach)
            .Where(ct => ct.PhieuMuon != null && ct.PhieuMuon.NgayMuon >= tuNgay && ct.PhieuMuon.NgayMuon <= denNgay)
            .Select(ct => new BaoCaoMuonTraDto
            {
                MaPhieu = ct.MaPhieuMuon,
                TenDocGia = (ct.PhieuMuon != null && ct.PhieuMuon.DocGia != null) 
                    ? ct.PhieuMuon.DocGia.HoTen ?? "" 
                    : "",
                TenDauSach = (ct.CuonSach != null && ct.CuonSach.DauSach != null) 
                    ? ct.CuonSach.DauSach.TenDauSach ?? "" 
                    : "",
                MaCuonSach = ct.MaCuonSach.ToString(),
                NgayMuon = ct.PhieuMuon != null ? ct.PhieuMuon.NgayMuon : DateTime.MinValue,
                HanTra = ct.PhieuMuon != null ? ct.PhieuMuon.NgayPhaiTra : DateTime.MinValue,
                NgayTraThucTe = ct.NgayTraThucTe,
                TrangThai = ct.NgayTraThucTe != null
                    ? "Đã trả"
                    : (ct.PhieuMuon != null && homNay > ct.PhieuMuon.NgayPhaiTra.Date ? "Quá hạn" : "Đang mượn")
            })
            .OrderByDescending(x => x.NgayMuon)
            .ToList();

        dgvMuonTra.DataSource = dsMuonTra;
        var thangKetThuc = new DateTime(dtpDenNgay.Value.Year, dtpDenNgay.Value.Month, 1);
        var soLuotTheoThang = dsMuonTra
            .GroupBy(x => new DateTime(x.NgayMuon.Year, x.NgayMuon.Month, 1))
            .ToDictionary(g => g.Key, g => g.Count());

        _duLieuBieuDoMuon.Clear();
        for (int i = 4; i >= 0; i--)
        {
            var thang = thangKetThuc.AddMonths(-i);
            soLuotTheoThang.TryGetValue(thang, out int soLuot);
            _duLieuBieuDoMuon.Add(new KeyValuePair<string, int>($"Tháng {thang:MM}", soLuot));
        }
        pnlBieuDoMuon.Invalidate();

        // 2. Top Đầu Sách Mượn Nhiều
        var topSach = _context.ChiTietPhieuMuons
            .Include(ct => ct.PhieuMuon)
            .Include(ct => ct.CuonSach)
                .ThenInclude(cs => cs!.DauSach)
                    .ThenInclude(ds => ds!.TheLoai)
            .Where(ct => ct.PhieuMuon != null 
                      && ct.CuonSach != null 
                      && ct.CuonSach.DauSach != null
                      && ct.PhieuMuon.NgayMuon >= tuNgay 
                      && ct.PhieuMuon.NgayMuon <= denNgay)
            .GroupBy(ct => new
            {
                ct.CuonSach!.DauSach!.MaDauSach,
                ct.CuonSach.DauSach.TenDauSach,
                TenTheLoai = ct.CuonSach.DauSach.TheLoai != null ? ct.CuonSach.DauSach.TheLoai.TenTheLoai : ""
            })
            .Select(g => new TopDauSachDto
            {
                MaDauSach = g.Key.MaDauSach,
                TenSach = g.Key.TenDauSach ?? "",
                TheLoai = g.Key.TenTheLoai ?? "",
                SoLuotMuon = g.Count(),
                TongSoCuon = _context.CuonSachs.Count(c => c.DauSach != null && c.DauSach.MaDauSach == g.Key.MaDauSach)
            })
            .OrderByDescending(x => x.SoLuotMuon)
            .ToList();

        dgvTopSach.DataSource = topSach;

        // 3. Thống kê Hóa Đơn Phạt
        var dsPhat = _context.HoaDonPhats
            .Include(h => h.DocGia)
            .Where(h => h.NgayLap >= tuNgay && h.NgayLap <= denNgay)
            .Select(h => new BaoCaoTienPhatDto
            {
                MaHoaDon = h.MaHoaDon,
                TenDocGia = h.DocGia != null ? h.DocGia.HoTen ?? "" : "",
                NgayLap = h.NgayLap,
                SoTien = h.TienPhatQuaHan,
                LyDo = h.LyDo ?? "",
                TrangThai = h.DaThanhToan ? "Đã thanh toán" : "Chưa thanh toán"
            })
            .OrderByDescending(x => x.NgayLap)
            .ToList();

        dgvTienPhat.DataSource = dsPhat;

        // 4. Cập nhật 4 thẻ KPI
        lblKpiLuotMuon.Text = dsMuonTra.Count.ToString("N0");
        lblKpiDangMuon.Text = dsMuonTra.Count(x => x.TrangThai == "Đang mượn").ToString("N0");
        lblKpiQuaHan.Text = dsMuonTra.Count(x => x.TrangThai == "Quá hạn").ToString("N0");

        decimal tongThuPhat = dsPhat
            .Where(x => x.TrangThai == "Đã thanh toán")
            .Sum(x => x.SoTien);
        lblKpiTienPhat.Text = $"{tongThuPhat:N0} VNĐ";
    }

    private void pnlBieuDoMuon_Paint(object? sender, PaintEventArgs e)
    {
        var bounds = pnlBieuDoMuon.ClientRectangle;
        if (bounds.Width < 100 || bounds.Height < 140)
            return;

        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        e.Graphics.Clear(Color.White);
        using var titleFont = new Font("Segoe UI", 9F, FontStyle.Bold);
        using var labelFont = new Font("Segoe UI", 8F);
        using var titleBrush = new SolidBrush(Color.FromArgb(30, 41, 59));
        using var labelBrush = new SolidBrush(Color.FromArgb(30, 41, 59));
        using var gridPen = new Pen(Color.FromArgb(226, 232, 240));
        using var axisPen = new Pen(Color.FromArgb(100, 116, 139));
        using var barBrush = new SolidBrush(Color.FromArgb(15, 118, 110));

        e.Graphics.DrawString(
            "BIỂU ĐỒ SỐ LƯỢT MƯỢN THEO THÁNG",
            titleFont,
            titleBrush,
            new RectangleF(12, 10, bounds.Width - 24, 24));

        var plot = new Rectangle(46, 48, bounds.Width - 62, bounds.Height - 92);
        if (plot.Width <= 0 || plot.Height <= 0)
            return;

        int giaTriLonNhat = Math.Max(4, _duLieuBieuDoMuon.Select(x => x.Value).DefaultIfEmpty().Max());
        int mocTren = (int)Math.Ceiling(giaTriLonNhat / 4d) * 4;
        using var centered = new StringFormat { Alignment = StringAlignment.Center };

        for (int i = 0; i <= 4; i++)
        {
            int giaTri = mocTren * i / 4;
            float y = plot.Bottom - plot.Height * i / 4f;
            e.Graphics.DrawLine(i == 0 ? axisPen : gridPen, plot.Left, y, plot.Right, y);
            e.Graphics.DrawString(
                giaTri.ToString(),
                labelFont,
                labelBrush,
                new RectangleF(4, y - 9, 36, 18),
                centered);
        }
        e.Graphics.DrawLine(axisPen, plot.Left, plot.Top, plot.Left, plot.Bottom);

        if (_duLieuBieuDoMuon.Count == 0)
            return;

        float slotWidth = plot.Width / (float)_duLieuBieuDoMuon.Count;
        float barWidth = slotWidth * 0.56f;
        for (int i = 0; i < _duLieuBieuDoMuon.Count; i++)
        {
            var item = _duLieuBieuDoMuon[i];
            float barHeight = plot.Height * item.Value / mocTren;
            float x = plot.Left + slotWidth * i + (slotWidth - barWidth) / 2;
            float y = plot.Bottom - barHeight;
            e.Graphics.FillRectangle(barBrush, x, y, barWidth, barHeight);
            e.Graphics.DrawString(
                item.Value.ToString(),
                labelFont,
                labelBrush,
                new RectangleF(x - 6, Math.Max(plot.Top, y - 20), barWidth + 12, 18),
                centered);
            e.Graphics.DrawString(
                item.Key,
                labelFont,
                labelBrush,
                new RectangleF(plot.Left + slotWidth * i, plot.Bottom + 5, slotWidth, 20),
                centered);
        }
    }

    private void btnXuatCsv_Click(object sender, EventArgs e)
    {
        DataGridView gridHienTai = tabBaoCao.SelectedIndex switch
        {
            0 => dgvMuonTra,
            1 => dgvTopSach,
            _ => dgvTienPhat
        };

        if (gridHienTai.Rows.Count == 0)
        {
            MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var sfd = new SaveFileDialog
        {
            Filter = "CSV file (*.csv)|*.csv",
            FileName = $"BaoCaoThuVien_{DateTime.Now:yyyyMMdd_HHmm}.csv"
        };

        if (sfd.ShowDialog() != DialogResult.OK) return;

        var sb = new StringBuilder();
        var headers = gridHienTai.Columns.Cast<DataGridViewColumn>().Select(c => $"\"{c.HeaderText}\"");
        sb.AppendLine(string.Join(",", headers));

        foreach (DataGridViewRow row in gridHienTai.Rows)
        {
            var cells = row.Cells.Cast<DataGridViewCell>().Select(c => $"\"{c.Value?.ToString()?.Replace("\"", "\"\"")}\"");
            sb.AppendLine(string.Join(",", cells));
        }

        File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
        MessageBox.Show("Xuất báo cáo CSV thành công!", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}