using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Entities;

namespace QuanLyThuVien.Data;

/// <summary>
/// Nơi DUY NHẤT được tạo / thu hóa đơn phạt và cập nhật DocGia.TienNoPhat.
/// Các hàm chỉ thay đổi trong DbContext, người gọi tự gọi SaveChanges
/// để hóa đơn + nợ cùng nằm trong một transaction.
/// </summary>
public static class PhatService
{
    // Tạo hóa đơn phạt. Chưa thanh toán thì cộng vào nợ của độc giả.
    public static async Task TaoHoaDonAsync(AppDbContext db, string maDocGia, int? maPhieuMuon,
        decimal tienPhatQuaHan, decimal tienBoiThuong, string? lyDo, bool daThanhToan)
    {
        decimal tong = tienPhatQuaHan + tienBoiThuong;
        if (tong <= 0) return;

        db.HoaDonPhats.Add(new HoaDonPhat
        {
            MaDocGia = maDocGia,
            MaPhieuMuon = maPhieuMuon,
            NgayLap = DateTime.Now,
            TienPhatQuaHan = tienPhatQuaHan,
            TienBoiThuong = tienBoiThuong,
            LyDo = string.IsNullOrWhiteSpace(lyDo) ? "Phạt vi phạm mượn trả" : lyDo,
            DaThanhToan = daThanhToan
        });

        if (!daThanhToan)
        {
            var dg = await db.DocGias.FirstOrDefaultAsync(d => d.MaDocGia == maDocGia);
            if (dg != null) dg.TienNoPhat += tong;
        }
    }

    // Thu tiền một hóa đơn. Trả false nếu hóa đơn không tồn tại hoặc đã thanh toán.
    public static async Task<bool> ThuHoaDonAsync(AppDbContext db, int maHoaDon)
    {
        var hd = await db.HoaDonPhats.FirstOrDefaultAsync(h => h.MaHoaDon == maHoaDon);
        if (hd == null || hd.DaThanhToan) return false;

        hd.DaThanhToan = true;

        var dg = await db.DocGias.FirstOrDefaultAsync(d => d.MaDocGia == hd.MaDocGia);
        if (dg != null)
            dg.TienNoPhat = await TinhNoConLaiAsync(db, hd.MaDocGia, boQuaHoaDon: hd.MaHoaDon);

        return true;
    }

    // Thu toàn bộ hóa đơn chưa thanh toán của một độc giả. Trả về tổng số tiền đã thu.
    public static async Task<decimal> ThuTatCaAsync(AppDbContext db, string maDocGia)
    {
        var dg = await db.DocGias.FirstOrDefaultAsync(d => d.MaDocGia == maDocGia);
        if (dg == null) return 0;

        var hoaDons = await db.HoaDonPhats
            .Where(h => h.MaDocGia == maDocGia && !h.DaThanhToan)
            .ToListAsync();
        foreach (var h in hoaDons) h.DaThanhToan = true;

        decimal daThu = dg.TienNoPhat;
        dg.TienNoPhat = 0;
        return daThu;
    }

    // Nợ còn lại = tổng hóa đơn chưa thanh toán 
    private static async Task<decimal> TinhNoConLaiAsync(AppDbContext db, string maDocGia, int boQuaHoaDon)
    {
        var ds = await db.HoaDonPhats.AsNoTracking()
            .Where(h => h.MaDocGia == maDocGia && !h.DaThanhToan && h.MaHoaDon != boQuaHoaDon)
            .Select(h => new { h.TienPhatQuaHan, h.TienBoiThuong })
            .ToListAsync();
        return ds.Sum(x => x.TienPhatQuaHan + x.TienBoiThuong);
    }
}