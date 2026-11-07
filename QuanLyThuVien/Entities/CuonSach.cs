using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QuanLyThuVien.Data;

namespace QuanLyThuVien.Entities;

public class CuonSach
{
    public string MaCuonSach { get; set; } = string.Empty;
    public string MaDauSach { get; set; } = string.Empty;
    public string? ViTriKe { get; set; }
    public string TrangThai { get; set; } = TrangThaiSach.SanSang;

    public DauSach? DauSach { get; set; }
    public List<ChiTietPhieuMuon> ChiTietPhieuMuons { get; set; } = new();
}
