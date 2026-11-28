using System.ComponentModel.DataAnnotations;
using QuanLyThuVien.Data;

namespace QuanLyThuVien.Entities;

public class TaiKhoan
{
    [Key]
    public int MaTaiKhoan { get; set; }

    [Required, MaxLength(100)]
    public string TenDangNhap { get; set; } = string.Empty;   // email trường hoặc mã đăng nhập

    [Required]
    public string MatKhauHash { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string HoTen { get; set; } = string.Empty;

    [Required, MaxLength(20)]
    public string VaiTro { get; set; } = LoaiTaiKhoan.DocGia;            // "ThuThu" hoặc "DocGia"            // "ThuThu" hoặc "DocGia"

    [MaxLength(20)]
    public string? MaDocGia { get; set; }                     // null nếu là Thủ thư

    public bool BatBuocDoiMatKhau { get; set; } = false;
}