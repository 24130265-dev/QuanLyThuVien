using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Entities;

public class HoaDonPhat
{
    public int MaHoaDon { get; set; }
    public string MaDocGia { get; set; } = string.Empty;
    public int? MaPhieuMuon { get; set; }
    public DateTime NgayLap { get; set; }
    public decimal TienPhatQuaHan { get; set; }
    public decimal TienBoiThuong { get; set; }
    public string? LyDo { get; set; }
    public bool DaThanhToan { get; set; }

    public DocGia? DocGia { get; set; }
}
