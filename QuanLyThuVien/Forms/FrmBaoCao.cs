using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.UI;

namespace QuanLyThuVien.Forms;

public partial class FrmBaoCao : Form
{
    private readonly List<KeyValuePair<string, int>> _duLieuBieuDoMuon = new();

    public FrmBaoCao()
    {
        InitializeComponent();

        Theme.Apply(this);

        // Khôi phục định dạng tiêu đề và nút sau khi áp dụng Theme
        lblTitle.Font = Theme.FontTitle;
        lblTitle.ForeColor = Theme.PrimaryDark;

        Theme.StyleButton(btnXemBaoCao, Theme.Secondary);

        // Bật DoubleBuffered để vẽ biểu đồ mượt, không bị nhấp nháy
        typeof(Panel).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
            ?.SetValue(this.pnlBieuDoMuon, true);

        // Cấu hình giao diện cho các bảng dữ liệu
        CauHinhLuoi(this.dgvMuonTra);
        CauHinhLuoi(this.dgvTopSach);
        CauHinhLuoi(this.dgvTienPhat);
        CauHinhLuoi(this.dgvQuaHan);

        // Khởi tạo 4 thẻ KPI tổng quan
        for (int i = 0; i < 4; i++)
        {
            this.tblKpi.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        }

        this.tblKpi.Controls.Add(TaoTheKpi("LƯỢT MƯỢN TRONG KỲ", Color.FromArgb(37, 99, 235), out this.lblKpiLuotMuon), 0, 0);
        this.tblKpi.Controls.Add(TaoTheKpi("SÁCH ĐANG MƯỢN", Color.FromArgb(13, 148, 136), out this.lblKpiDangMuon), 1, 0);
        this.tblKpi.Controls.Add(TaoTheKpi("SÁCH QUÁ HẠN", Color.FromArgb(220, 38, 38), out this.lblKpiQuaHan), 2, 0);
        this.tblKpi.Controls.Add(TaoTheKpi("TỔNG THU TIỀN PHẠT", Color.FromArgb(217, 119, 6), out this.lblKpiTienPhat), 3, 0);
    }

    private void FrmBaoCao_Load(object? sender, EventArgs e)
    {
        // Mặc định lọc từ đầu tháng hiện tại đến hôm nay
        DateTime homNay = DateTime.Today;
        dtpTuNgay.Value = new DateTime(homNay.Year, homNay.Month, 1);
        dtpDenNgay.Value = homNay;

        NapDuLieuBaoCao();
    }

    private void btnXemBaoCao_Click(object? sender, EventArgs e)
    {
        if (dtpTuNgay.Value.Date > dtpDenNgay.Value.Date)
        {
            MessageBox.Show(
                "Ngày bắt đầu không được lớn hơn ngày kết thúc!",
                "Cảnh báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        NapDuLieuBaoCao();
    }

    private void NapDuLieuBaoCao()
    {
        using var context = new AppDbContext();

        DateTime tuNgay = dtpTuNgay.Value.Date;
        DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddTicks(-1);
        DateTime homNay = DateTime.Today;

        // 1. Báo cáo Mượn - Trả & Quá hạn
        var dsMuonTra = context.ChiTietPhieuMuons
            .AsNoTracking()
            .Where(ct => ct.PhieuMuon != null
                      && ct.PhieuMuon.NgayMuon >= tuNgay
                      && ct.PhieuMuon.NgayMuon <= denNgay)
            .Select(ct => new BaoCaoMuonTraDto
            {
                MaPhieu = ct.MaPhieuMuon,
                TenDocGia = ct.PhieuMuon!.DocGia != null ? (ct.PhieuMuon.DocGia.HoTen ?? "") : "",
                TenDauSach = ct.CuonSach != null && ct.CuonSach.DauSach != null
                    ? (ct.CuonSach.DauSach.TenDauSach ?? "")
                    : "",
                MaCuonSach = ct.MaCuonSach.ToString(),
                NgayMuon = ct.PhieuMuon.NgayMuon,
                HanTra = ct.PhieuMuon.NgayPhaiTra,
                NgayTraThucTe = ct.NgayTraThucTe,
                TrangThai = ct.NgayTraThucTe != null
                    ? "Đã trả"
                    : (homNay > ct.PhieuMuon.NgayPhaiTra ? "Quá hạn" : "Đang mượn")
            })
            .OrderByDescending(x => x.NgayMuon)
            .ToList();

        // Chuẩn hóa trạng thái quá hạn theo ngày (tránh lệch giờ/phút trong SQL)
        foreach (var item in dsMuonTra)
        {
            if (item.NgayTraThucTe == null)
            {
                item.TrangThai = homNay > item.HanTra.Date ? "Quá hạn" : "Đang mượn";
            }
        }

        dgvMuonTra.DataSource = dsMuonTra;

        // Dữ liệu biểu đồ 5 tháng gần nhất tính đến tháng của dtpDenNgay
        var thangKetThuc = new DateTime(dtpDenNgay.Value.Year, dtpDenNgay.Value.Month, 1);
        var soLuotTheoThang = dsMuonTra
            .GroupBy(x => new DateTime(x.NgayMuon.Year, x.NgayMuon.Month, 1))
            .ToDictionary(g => g.Key, g => g.Count());

        _duLieuBieuDoMuon.Clear();
        for (int i = 4; i >= 0; i--)
        {
            var thang = thangKetThuc.AddMonths(-i);
            soLuotTheoThang.TryGetValue(thang, out int soLuot);
            _duLieuBieuDoMuon.Add(new KeyValuePair<string, int>($"T{thang:MM/yy}", soLuot));
        }
        pnlBieuDoMuon.Invalidate();

        // 2. Top Đầu Sách Mượn Nhiều
        var tongSoCuonTheoDauSach = context.CuonSachs
            .AsNoTracking()
            .Where(c => c.DauSach != null)
            .GroupBy(c => c.DauSach!.MaDauSach)
            .ToDictionary(g => g.Key, g => g.Count());

        var topSachRaw = context.ChiTietPhieuMuons
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
                TenTheLoai = ct.CuonSach.DauSach.TheLoai != null ? ct.CuonSach.DauSach.TheLoai.TenTheLoai : ""
            })
            .Select(g => new
            {
                g.Key.MaDauSach,
                TenSach = g.Key.TenDauSach ?? "",
                TheLoai = g.Key.TenTheLoai ?? "",
                SoLuotMuon = g.Count()
            })
            .OrderByDescending(x => x.SoLuotMuon)
            .ToList();

        var topSach = topSachRaw.Select(x => new TopDauSachDto
        {
            MaDauSach = x.MaDauSach,
            TenSach = x.TenSach,
            TheLoai = x.TheLoai,
            SoLuotMuon = x.SoLuotMuon,
            TongSoCuon = tongSoCuonTheoDauSach.TryGetValue(x.MaDauSach, out int tong) ? tong : 0
        }).ToList();

        dgvTopSach.DataSource = topSach;

        // 3. Thống kê Hóa Đơn Phạt
        var dsPhat = context.HoaDonPhats
            .AsNoTracking()
            .Where(h => h.NgayLap >= tuNgay && h.NgayLap <= denNgay)
            .Select(h => new BaoCaoTienPhatDto
            {
                MaHoaDon = h.MaHoaDon,
                TenDocGia = h.DocGia != null ? (h.DocGia.HoTen ?? "") : "",
                NgayLap = h.NgayLap,
                SoTien = h.TienPhatQuaHan + h.TienBoiThuong,
                LyDo = h.LyDo ?? "",
                TrangThai = h.DaThanhToan ? "Đã thanh toán" : "Chưa thanh toán"
            })
            .OrderByDescending(x => x.NgayLap)
            .ToList();

        dgvTienPhat.DataSource = dsPhat;
        // 4. Báo cáo độc giả quá hạn chưa trả
        // Thống kê toàn bộ sách đang quá hạn, không giới hạn kỳ báo cáo.

        var dsDocGiaQuaHan = context.ChiTietPhieuMuons
            .AsNoTracking()
            .Where(ct =>
                ct.NgayTraThucTe == null
                && ct.PhieuMuon != null
                && ct.PhieuMuon.NgayPhaiTra.Date < homNay
                && ct.PhieuMuon.DocGia != null
                && ct.CuonSach != null
                && ct.CuonSach.DauSach != null)
            .Select(ct => new
            {
                ct.PhieuMuon!.MaDocGia,
                HoTen = ct.PhieuMuon.DocGia!.HoTen,
                SoDienThoai = ct.PhieuMuon.DocGia.SoDienThoai,
                TenDauSach = ct.CuonSach!.DauSach!.TenDauSach,
                HanTra = ct.PhieuMuon.NgayPhaiTra
            })
            .ToList();

        dgvQuaHan.DataSource = dsDocGiaQuaHan
            .OrderBy(x => x.HanTra)
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

        // Tổng lượt mượn trong kỳ báo cáo
        lblKpiLuotMuon.Text = dsMuonTra.Count.ToString("N0");

        // Tổng số sách hiện đang được mượn (không giới hạn kỳ báo cáo)
        int dangMuon = context.ChiTietPhieuMuons
            .Count(ct => ct.NgayTraThucTe == null);

        // Tổng số sách đang mượn và đã quá hạn
        int quaHan = context.ChiTietPhieuMuons
            .Count(ct => ct.NgayTraThucTe == null
                      && ct.PhieuMuon!.NgayPhaiTra < homNay);

        lblKpiDangMuon.Text = dangMuon.ToString("N0");
        lblKpiQuaHan.Text = quaHan.ToString("N0");

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
        e.Graphics.Clear(pnlBieuDoMuon.BackColor);

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
        int mocTren = Math.Max(4, (int)Math.Ceiling(giaTriLonNhat / 4d) * 4);
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
            float x = plot.Left + slotWidth * i + (slotWidth - barWidth) / 2f;
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

    private void btnXuatCsv_Click(object? sender, EventArgs e)
    {
        DataGridView gridHienTai = tabBaoCao.SelectedIndex switch
        {
            0 => dgvMuonTra,
            1 => dgvTopSach,
            2 => dgvTienPhat,
            3 => dgvQuaHan,
            _ => dgvMuonTra
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

        if (sfd.ShowDialog() != DialogResult.OK)
            return;

        var sb = new StringBuilder();
        var headers = gridHienTai.Columns
            .Cast<DataGridViewColumn>()
            .Where(c => c.Visible)
            .Select(c => $"\"{c.HeaderText.Replace("\"", "\"\"")}\"");
        sb.AppendLine(string.Join(",", headers));

        foreach (DataGridViewRow row in gridHienTai.Rows)
        {
            if (row.IsNewRow) continue;
            var cells = row.Cells
                .Cast<DataGridViewCell>()
                .Where(c => c.OwningColumn.Visible)
                .Select(c => $"\"{c.Value?.ToString()?.Replace("\"", "\"\"")}\"");
            sb.AppendLine(string.Join(",", cells));
        }

        // Ghi UTF-8 có BOM để Excel hiển thị đúng tiếng Việt có dấu
        var utf8WithBom = new UTF8Encoding(encoderShouldEmitUTF8Identifier: true);
        File.WriteAllText(sfd.FileName, sb.ToString(), utf8WithBom);

        MessageBox.Show("Xuất báo cáo CSV thành công!", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private Panel TaoTheKpi(string tieuDe, Color mauNhan, out Label lblGiaTri)
    {
        var card = new Panel
        {
            Dock = DockStyle.Fill,
            Margin = new Padding(6),
            BackColor = Color.White,
            BorderStyle = BorderStyle.FixedSingle
        };

        var lblHeader = new Label
        {
            Text = tieuDe,
            Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
            ForeColor = Color.Gray,
            Location = new Point(12, 10),
            AutoSize = true
        };

        lblGiaTri = new Label
        {
            Text = "0",
            Font = new Font("Segoe UI", 15F, FontStyle.Bold),
            ForeColor = mauNhan,
            Location = new Point(12, 34),
            AutoSize = true
        };

        card.Controls.Add(lblHeader);
        card.Controls.Add(lblGiaTri);
        return card;
    }

    private void CauHinhLuoi(DataGridView dgv)
    {
        dgv.Dock = DockStyle.Fill;
        dgv.BackgroundColor = Color.White;
        dgv.AllowUserToAddRows = false;
        dgv.ReadOnly = true;
        dgv.RowHeadersVisible = false;
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        dgv.DataBindingComplete += (s, e) =>
        {
            foreach (DataGridViewColumn c in dgv.Columns)
            {
                c.MinimumWidth = 90;

                if (TieuDeCot.TryGetValue(c.DataPropertyName, out var tieuDe))
                    c.HeaderText = tieuDe;

                if (c.ValueType == typeof(DateTime) || c.ValueType == typeof(DateTime?))
                    c.DefaultCellStyle.Format = "dd/MM/yyyy";

                if (c.DataPropertyName == "SoTien")
                    c.DefaultCellStyle.Format = "N0";
            }
        };
    }
    private static readonly Dictionary<string, string> TieuDeCot = new()
    {
        ["MaPhieu"] = "Mã phiếu",
        ["TenDocGia"] = "Độc giả",
        ["TenDauSach"] = "Đầu sách",
        ["MaCuonSach"] = "Mã cuốn",
        ["NgayMuon"] = "Ngày mượn",
        ["HanTra"] = "Hạn trả",
        ["NgayTraThucTe"] = "Ngày trả",
        ["TrangThai"] = "Trạng thái",
        ["MaDauSach"] = "Mã đầu sách",
        ["TenSach"] = "Tên sách",
        ["TheLoai"] = "Thể loại",
        ["SoLuotMuon"] = "Lượt mượn",
        ["TongSoCuon"] = "Tổng số cuốn",
        ["MaHoaDon"] = "Mã hóa đơn",
        ["NgayLap"] = "Ngày lập",
        ["SoTien"] = "Số tiền",
        ["LyDo"] = "Lý do",
        ["MaDocGia"] = "Mã độc giả",
        ["HoTen"] = "Họ tên",
        ["SoDienThoai"] = "SĐT",
        ["SoNgayTre"] = "Số ngày trễ"
    };
}