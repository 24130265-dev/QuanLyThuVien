using QuanLyThuVien.Data;

namespace QuanLyThuVien.Entities;

// Yêu cầu mượn sách online
public class YeuCauMuon
{
    public int MaYeuCau { get; set; }
    public string MaDocGia { get; set; } = string.Empty;
    public string MaDauSach { get; set; } = string.Empty;
    public DateTime NgayGui { get; set; } = DateTime.Now;
    public string TrangThai { get; set; } = TrangThaiYeuCau.ChoDuyet;

    public DocGia? DocGia { get; set; }
    public DauSach? DauSach { get; set; }
}