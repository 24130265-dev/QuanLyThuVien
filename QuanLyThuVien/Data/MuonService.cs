using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Entities;

namespace QuanLyThuVien.Data;

public enum LyDoChan { KhongCo, KhongTimThay, KhoaThe, NoPhat, DayGioiHan }

public sealed record KetQuaDieuKien(
    LyDoChan Chan, string MaDocGia, DocGia? DocGia, int DangGiu, decimal SoNo, int SoMuonThem)
{
    public bool Du => Chan == LyDoChan.KhongCo;


    public string MoTa => Chan switch
    {
        LyDoChan.KhongTimThay => $"Không tìm thấy độc giả có mã \"{MaDocGia}\".",
        LyDoChan.KhoaThe => $"Thẻ của {DocGia!.HoTen} đang bị khóa, không thể mượn sách.",
        LyDoChan.NoPhat => $"{DocGia!.HoTen} đang nợ phạt {SoNo:N0} VNĐ.\nYêu cầu đóng phạt trước (màn hình Quản lý Độc giả).",
        LyDoChan.DayGioiHan when SoMuonThem <= 1 =>
            $"{DocGia!.HoTen} đang giữ {DangGiu} cuốn chưa trả, đã đạt mức tối đa {MuonService.SoSachToiDa} cuốn.",
        LyDoChan.DayGioiHan =>
            $"Vượt quá giới hạn {MuonService.SoSachToiDa} cuốn: {DocGia!.HoTen} đang giữ {DangGiu} cuốn, muốn mượn thêm {SoMuonThem} cuốn.",
        _ => string.Empty
    };
}


public static class MuonService
{
    public const int SoSachToiDa = 3;   // tối đa 3 cuốn đang giữ
    public const int SoNgayMuon = 14;   // ngày phải trả = ngày mượn + 14

    public static Task<int> DemSachDangGiuAsync(AppDbContext db, string maDocGia)
        => db.ChiTietPhieuMuons.CountAsync(
            ct => ct.NgayTraThucTe == null && ct.PhieuMuon!.MaDocGia == maDocGia);

    /// <param name="soMuonThem">Số cuốn định mượn thêm (0 = chỉ kiểm tra còn được mượn tối thiểu 1 cuốn).</param>
    public static async Task<KetQuaDieuKien> KiemTraAsync(AppDbContext db, string maDocGia, int soMuonThem = 0)
    {
        var dg = await db.DocGias.AsNoTracking().FirstOrDefaultAsync(d => d.MaDocGia == maDocGia);
        if (dg == null)
            return new(LyDoChan.KhongTimThay, maDocGia, null, 0, 0, soMuonThem);

        int dangGiu = await DemSachDangGiuAsync(db, maDocGia);

        // Nợ = số lớn hơn giữa TienNoPhat và tổng hóa đơn chưa thanh toán (cộng trong bộ nhớ vì SQLite không SUM decimal)
        var hoaDon = await db.HoaDonPhats.AsNoTracking()
            .Where(h => h.MaDocGia == maDocGia && !h.DaThanhToan)
            .Select(h => new { h.TienPhatQuaHan, h.TienBoiThuong })
            .ToListAsync();
        decimal soNo = Math.Max(dg.TienNoPhat, hoaDon.Sum(h => h.TienPhatQuaHan + h.TienBoiThuong));

        if (dg.BiKhoaThe) return new(LyDoChan.KhoaThe, maDocGia, dg, dangGiu, soNo, soMuonThem);
        if (soNo > 0) return new(LyDoChan.NoPhat, maDocGia, dg, dangGiu, soNo, soMuonThem);
        if (dangGiu + Math.Max(1, soMuonThem) > SoSachToiDa)
            return new(LyDoChan.DayGioiHan, maDocGia, dg, dangGiu, soNo, soMuonThem);

        return new(LyDoChan.KhongCo, maDocGia, dg, dangGiu, soNo, soMuonThem);
    }
}