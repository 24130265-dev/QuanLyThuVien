using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Entities;

public class PhieuMuon
{
    public int MaPhieuMuon { get; set; }
    public string MaDocGia { get; set; } = string.Empty;
    public DateTime NgayMuon { get; set; }
    public DateTime NgayPhaiTra { get; set; }

    public DocGia? DocGia { get; set; }
    public List<ChiTietPhieuMuon> ChiTietPhieuMuons { get; set; } = new();
}
