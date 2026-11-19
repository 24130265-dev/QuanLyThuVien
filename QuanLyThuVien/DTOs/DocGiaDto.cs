namespace QuanLyThuVien.DTOs;

// Một dòng hiển thị trên dgvDocGia (không kéo theo các cột Navigation Property của Entity)
public record DocGiaDto(
    string MaDocGia,
    string HoTen,
    string SoDienThoai,
    bool BiKhoaThe,
    decimal TienNoPhat);
