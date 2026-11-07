using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Entities;

public class DocGia
{
    public string MaDocGia { get; set; } = string.Empty;
    public string HoTen { get; set; } = string.Empty;
    public string SoDienThoai { get; set; } = string.Empty;
    public bool BiKhoaThe { get; set; } = false;
    public decimal TienNoPhat { get; set; } = 0;

    public List<PhieuMuon> PhieuMuons { get; set; } = new();
    public List<HoaDonPhat> HoaDonPhats { get; set; } = new();
}