using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.DTOs
{
    public record DauSachDto(
        string MaDauSach,
        string TenDauSach,
        string TacGia,
        string NhaXuatBan,
        int NamXuatBan,
        decimal GiaBia,
        string MaTheLoai,
        string TenTheLoai,
        int TongSoSach,
        int SoCuonSanSang);

}
