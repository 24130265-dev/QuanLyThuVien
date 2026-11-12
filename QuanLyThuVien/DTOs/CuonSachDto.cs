using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.DTOs
{
    public record CuonSachDto(
        string MaCuonSach,
        string? ViTriKe,
        string TrangThai
        );
  
}
