using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Entities;

public class ChiTietPhieuMuon
{
    public int Id { get; set; }
    public int MaPhieuMuon { get; set; }
    public string MaCuonSach { get; set; } = string.Empty;
    public DateTime? NgayTraThucTe { get; set; }
    public string? TinhTrangKhiTra { get; set; }

    public PhieuMuon? PhieuMuon { get; set; }
    public CuonSach? CuonSach { get; set; }
}
