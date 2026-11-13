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
    private readonly AppDbContext _context;
    
    public FrmBaoCao()
    {
        InitializeComponent();
    }

    private void FrmBaoCao_Load(object sender, EventArgs e)
    {
        dtpTuNgay.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        dtpDenNgay.Value = DateTime.Today;
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
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddTicks(-1);
            DateTime homNay = DateTime.Today;

                // 1. Báo cáo Mượn - Trả & Quá hạn
        var dsMuonTra = _context.ChiTietPhieuMuons
            .Include(ct => ct.PhieuMuon)
                .ThenInclude(p => p.DocGia)
            .Include(ct => ct.CuonSach)
                .ThenInclude(cs => cs.DauSach)
            .Where(ct => ct.PhieuMuon.NgayMuon >= tuNgay && ct.PhieuMuon.NgayMuon <= denNgay)
            .Select(ct => new BaoCaoMuonTraDto
            {
                MaPhieu = ct.MaPhieuMuon,
                TenDocGia = ct.PhieuMuon.DocGia.HoTen,
                TenDauSach = ct.CuonSach.DauSach.TenDauSach,
                MaCuonSach = ct.MaCuonSach.ToString(),
                NgayMuon = ct.PhieuMuon.NgayMuon,
                HanTra = ct.PhieuMuon.NgayPhaiTra,
                NgayTraThucTe = ct.NgayTraThucTe,
                TrangThai = ct.NgayTraThucTe != null
                    ? "Đã trả"
                    : (homNay > ct.PhieuMuon.NgayPhaiTra.Date ? "Quá hạn" : "Đang mượn")
            })
            .OrderByDescending(x => x.NgayMuon)
            .ToList();

        dgvMuonTra.DataSource = dsMuonTra;

        // 2. Top Đầu Sách Mượn Nhiều
        var topSach = _context.ChiTietPhieuMuons
            .Include(ct => ct.PhieuMuon)
            .Include(ct => ct.CuonSach)
                .ThenInclude(cs => cs.DauSach)
                    .ThenInclude(ds => ds.TheLoai)
            .Where(ct => ct.PhieuMuon.NgayMuon >= tuNgay && ct.PhieuMuon.NgayMuon <= denNgay)
            .GroupBy(ct => new
            {
                ct.CuonSach.DauSach.MaDauSach,
                ct.CuonSach.DauSach.TenDauSach,
                TenTheLoai = ct.CuonSach.DauSach.TheLoai.TenTheLoai
            })
            .Select(g => new TopDauSachDto
            {
                MaDauSach = g.Key.MaDauSach,
                TenSach = g.Key.TenDauSach,
                TheLoai = g.Key.TenTheLoai,
                SoLuotMuon = g.Count(),
                TongSoCuon = _context.CuonSachs.Count(c => c.DauSach.MaDauSach == g.Key.MaDauSach)
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
                TenDocGia = h.DocGia.HoTen,
                NgayLap = h.NgayLap,
                // Đổi h.TienPhat thành tên thuộc tính số tiền trong Entities/HoaDonPhat.cs của bạn
                SoTien = h.TienPhatQuaHan,
                LyDo = h.LyDo,
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

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        _context?.Dispose();
        base.OnFormClosed(e);
    }
}