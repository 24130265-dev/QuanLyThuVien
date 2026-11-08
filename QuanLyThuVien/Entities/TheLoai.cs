using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Entities;

public class TheLoai
{
    public string MaTheLoai { get; set; } = string.Empty;
    public string TenTheLoai { get; set; } = string.Empty;
    public string? MaTheLoaiCha { get; set; }

    public TheLoai? TheLoaiCha { get; set; }
    public List<TheLoai> TheLoaiCons { get; set; } = new();
    public List<DauSach> DauSachs { get; set; } = new();
}
