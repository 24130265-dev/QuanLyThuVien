using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Entities;

namespace QuanLyThuVien.Data;

// BỘ NHỚ LƯU PHIÊN ĐĂNG NHẬP HIỆN TẠI & QUẢN LÝ TÀI KHOẢN
public static class PhienDangNhap
{
    // Thông tin người dùng đang đăng nhập (mọi Form đều gọi trực tiếp được)
    public static int MaTaiKhoan { get; private set; }
    public static string TenDangNhap { get; private set; } = string.Empty;
    public static string HoTen { get; private set; } = string.Empty;
    public static string VaiTro { get; private set; } = string.Empty; // "ThuThu" hoặc "DocGia"
    public static string? MaDocGia { get; private set; }

    public static bool LaThuThu => VaiTro == "ThuThu";
    public static bool LaSinhVien => VaiTro == "DocGia";

    // File nhỏ lưu tên đăng nhập lần cuối trên máy
    private static readonly string FileNhoDangNhap =
        Path.Combine(AppContext.BaseDirectory, "last_login.txt");

    // Ghi nhận thông tin khi đăng nhập thành công
    public static void LuuPhien(TaiKhoan tk)
    {
        MaTaiKhoan = tk.MaTaiKhoan;
        TenDangNhap = tk.TenDangNhap;
        HoTen = tk.HoTen;
        VaiTro = tk.VaiTro;
        MaDocGia = tk.MaDocGia;

        try { File.WriteAllText(FileNhoDangNhap, tk.TenDangNhap); } catch { }
    }

    public static string DocTaiKhoanLanCuoi()
    {
        try { return File.Exists(FileNhoDangNhap) ? File.ReadAllText(FileNhoDangNhap).Trim() : ""; }
        catch { return ""; }
    }

    public static void DangXuat()
    {
        MaTaiKhoan = 0;
        TenDangNhap = string.Empty;
        HoTen = string.Empty;
        VaiTro = string.Empty;
        MaDocGia = null;
    }

    // Tự động tạo bảng TaiKhoans và nạp tài khoản Thủ thư + Sinh viên nếu chưa có
    public static void KhoiTaoDuLieuTaiKhoan(AppDbContext db)
    {
        
        db.Database.ExecuteSqlRaw(@"
            CREATE TABLE IF NOT EXISTS ""TaiKhoans"" (
                ""MaTaiKhoan"" INTEGER NOT NULL CONSTRAINT ""PK_TaiKhoans"" PRIMARY KEY AUTOINCREMENT,
                ""TenDangNhap"" TEXT NOT NULL,
                ""MatKhauHash"" TEXT NOT NULL,
                ""HoTen"" TEXT NOT NULL,
                ""VaiTro"" TEXT NOT NULL,
                ""MaDocGia"" TEXT NULL,
                ""BatBuocDoiMatKhau"" INTEGER NOT NULL DEFAULT 0
            );");

        string matKhauMacDinh = MatKhau.Bam("123");

        // A. Tạo 2 tài khoản THỦ THƯ mặc định nếu chưa có
        if (!db.TaiKhoans.Any(t => t.VaiTro == "ThuThu"))
        {
            db.TaiKhoans.AddRange(
                new TaiKhoan
                {
                    TenDangNhap = "admin@hcmuaf.edu.vn",
                    MatKhauHash = matKhauMacDinh,
                    HoTen = "Quản trị viên Thư viện",
                    VaiTro = "ThuThu",
                    MaDocGia = null,
                    BatBuocDoiMatKhau = false
                },
                new TaiKhoan
                {
                    TenDangNhap = "thuthu@hcmuaf.edu.vn",
                    MatKhauHash = matKhauMacDinh,
                    HoTen = "Thủ thư trực quầy",
                    VaiTro = "ThuThu",
                    MaDocGia = null,
                    BatBuocDoiMatKhau = false
                }
            );
            db.SaveChanges();
        }

        // B. Tự động tạo tài khoản SINH VIÊN cho mọi Độc giả đang có trong bảng DocGias
        var dsDocGia = db.DocGias.AsNoTracking().ToList();
        bool coThemMoi = false;

        foreach (var dg in dsDocGia)
        {
            string emailSv = $"{dg.MaDocGia.ToLowerInvariant()}@st.hcmuaf.edu.vn";
            bool daCo = db.TaiKhoans.Any(t => t.MaDocGia == dg.MaDocGia || t.TenDangNhap == emailSv);

            if (!daCo)
            {
                db.TaiKhoans.Add(new TaiKhoan
                {
                    TenDangNhap = emailSv,
                    MatKhauHash = matKhauMacDinh,
                    HoTen = dg.HoTen,
                    VaiTro = "DocGia",
                    MaDocGia = dg.MaDocGia,
                    BatBuocDoiMatKhau = true
                });
                coThemMoi = true;
            }
        }

        if (coThemMoi)
            db.SaveChanges();
    }
}