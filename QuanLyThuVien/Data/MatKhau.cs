using System.Security.Cryptography;
using System.Text;

namespace QuanLyThuVien.Data;

public static class MatKhau
{
    private const string TienTo = "PBKDF2";
    private const int SoVong = 100_000;
    private const int DoDaiMuoi = 16;
    private const int DoDaiBam = 32;

    public static string Bam(string matKhau)
    {
        byte[] muoi = RandomNumberGenerator.GetBytes(DoDaiMuoi);
        byte[] bam = Rfc2898DeriveBytes.Pbkdf2(matKhau, muoi, SoVong, HashAlgorithmName.SHA256, DoDaiBam);
        return $"{TienTo}${SoVong}${Convert.ToBase64String(muoi)}${Convert.ToBase64String(bam)}";
    }

    public static bool Kiem(string matKhauNhap, string chuoiDaLuu)
    {
        if (string.IsNullOrEmpty(chuoiDaLuu)) return false;

        if (chuoiDaLuu.StartsWith(TienTo + "$", StringComparison.Ordinal))
        {
            string[] phan = chuoiDaLuu.Split('$');
            if (phan.Length != 4 || !int.TryParse(phan[1], out int soVong) || soVong < 1)
                return false;

            byte[] muoi, bamLuu;
            try
            {
                muoi = Convert.FromBase64String(phan[2]);
                bamLuu = Convert.FromBase64String(phan[3]);
            }
            catch (FormatException)
            {
                return false;
            }
            if (muoi.Length == 0 || bamLuu.Length == 0) return false;

            byte[] bamNhap = Rfc2898DeriveBytes.Pbkdf2(
                matKhauNhap, muoi, soVong, HashAlgorithmName.SHA256, bamLuu.Length);
            return CryptographicOperations.FixedTimeEquals(bamNhap, bamLuu);
        }

        // Định dạng cũ
        return string.Equals(BamCu(matKhauNhap), chuoiDaLuu, StringComparison.OrdinalIgnoreCase);
    }

    // true nếu chuỗi đã lưu còn là định dạng cũ, cần nâng cấp
    public static bool CanNangCap(string chuoiDaLuu)
        => !chuoiDaLuu.StartsWith(TienTo + "$", StringComparison.Ordinal);

    public const int DoDaiToiThieu = 6;
    public const int DoDaiToiDa = 64;

    private static readonly HashSet<string> MatKhauYeu = new(StringComparer.OrdinalIgnoreCase)
    {
        "123", "123456", "1234567", "12345678", "123456789", "111111", "000000",
        "password", "matkhau", "qwerty", "abc123", "admin123", "nlu123"
    };

    public static string? KiemTraDoManh(string matKhau, string? khongDuocTrung = null)
    {
        if (matKhau.Length < DoDaiToiThieu)
            return $"Mật khẩu cần ít nhất {DoDaiToiThieu} ký tự.";
        if (matKhau.Length > DoDaiToiDa)
            return $"Mật khẩu tối đa {DoDaiToiDa} ký tự.";
        if (MatKhauYeu.Contains(matKhau))
            return "Mật khẩu này quá dễ đoán, hãy chọn mật khẩu khác.";
        if (!matKhau.Any(char.IsLetter) || !matKhau.Any(char.IsDigit))
            return "Mật khẩu cần có cả chữ và số.";
        if (!string.IsNullOrEmpty(khongDuocTrung)
            && string.Equals(matKhau, khongDuocTrung, StringComparison.OrdinalIgnoreCase))
            return "Mật khẩu không được trùng với mã số sinh viên.";
        return null;
    }

    private static string BamCu(string matKhau)
    {
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes("NLU_SALT_" + matKhau));
        return Convert.ToHexString(bytes);
    }
}