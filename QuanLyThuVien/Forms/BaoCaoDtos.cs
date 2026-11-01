namespace QuanLyThuVien.Forms;

public record SachDangMuonDTO (
    string MaCuonSach,
    string TenDauSach,
    string NguoiMuon,
    string SDT,
    DateTime NgayMuon,
    DateTime NgayHenTra);

public record BaoCaoQuaHanDTO(
    string MaCuonSach,
    string TenDauSach,
    string NguoiMuon,
    string SDT,
    DateTime NgayMuon,
    DateTime NgayHenTra,
    int SoNgayQuaHan,
    decimal TienPhatTamTinh
);


    