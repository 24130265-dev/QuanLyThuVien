using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Entities;

public class DauSach
{
    public string MaDauSach { get; set; } = string.Empty;
    public string TenDauSach { get; set; } = string.Empty;
    public string TacGia { get; set; } = string.Empty;
    public int NamXuatBan { get; set; }
    public decimal GiaBia { get; set; }
    public string MaTheLoai { get; set; } = string.Empty;

    public TheLoai? TheLoai { get; set; }
    public List<CuonSach> CuonSachs { get; set; } = new();
}
