using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Entities;

namespace QuanLyThuVien.Data;


public static class PhienDangNhap
{
    public static int MaTaiKhoan { get; private set; }
    public static string TenDangNhap { get; private set; } = string.Empty;
    public static string HoTen { get; private set; } = string.Empty;
    public static string VaiTro { get; private set; } = string.Empty;   // LoaiTaiKhoan
    public static string? MaDocGia { get; private set; }

    public static bool LaThuThu => VaiTro == LoaiTaiKhoan.ThuThu;
    public static bool LaSinhVien => VaiTro == LoaiTaiKhoan.DocGia;


    private static readonly string FileNhoDangNhap =
        Path.Combine(AppContext.BaseDirectory, "last_login.txt");

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

    // Chạy MỘT LẦN khi mở app: tạo bảng, chỉ mục duy nhất, tài khoản Thủ thư mặc định
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
        db.Database.ExecuteSqlRaw(@"
            CREATE TABLE IF NOT EXISTS ""YeuCauMuons"" (
                ""MaYeuCau"" INTEGER NOT NULL CONSTRAINT ""PK_YeuCauMuons"" PRIMARY KEY AUTOINCREMENT,
                ""MaDocGia"" TEXT NOT NULL,
                ""MaDauSach"" TEXT NOT NULL,
                ""NgayGui"" TEXT NOT NULL,
                ""TrangThai"" TEXT NOT NULL,
                CONSTRAINT ""FK_YeuCauMuons_DocGias"" FOREIGN KEY (""MaDocGia"") REFERENCES ""DocGias"" (""MaDocGia"") ON DELETE RESTRICT,
                CONSTRAINT ""FK_YeuCauMuons_DauSachs"" FOREIGN KEY (""MaDauSach"") REFERENCES ""DauSachs"" (""MaDauSach"") ON DELETE RESTRICT
            );");

        TaoChiMucDuyNhat(db);

        // Tài khoản Thủ thư mặc định 
        if (!db.TaiKhoans.Any(t => t.VaiTro == LoaiTaiKhoan.ThuThu))
        {
            string matKhauMacDinh = MatKhau.Bam("123");
            db.TaiKhoans.AddRange(
                new TaiKhoan
                {
                    TenDangNhap = "admin@hcmuaf.edu.vn",
                    MatKhauHash = matKhauMacDinh,
                    HoTen = "Quản trị viên Thư viện",
                    VaiTro = LoaiTaiKhoan.ThuThu,
                    MaDocGia = null,
                    BatBuocDoiMatKhau = false
                },
                new TaiKhoan
                {
                    TenDangNhap = "thuthu@hcmuaf.edu.vn",
                    MatKhauHash = matKhauMacDinh,
                    HoTen = "Thủ thư trực quầy",
                    VaiTro = LoaiTaiKhoan.ThuThu,
                    MaDocGia = null,
                    BatBuocDoiMatKhau = false
                });
            db.SaveChanges();
        }

        DongBoTaiKhoanDocGia(db);
    }

    // Tạo tài khoản cho độc giả chưa có
    public static void DongBoTaiKhoanDocGia(AppDbContext db)
    {
        var docGiaThieuTk = db.DocGias.AsNoTracking()
            .Where(d => !db.TaiKhoans.Any(t => t.MaDocGia == d.MaDocGia))
            .ToList();
        if (docGiaThieuTk.Count == 0) return;

        var tenDaDung = db.TaiKhoans.AsNoTracking()
            .Select(t => t.TenDangNhap.ToLower())
            .ToHashSet();

        string matKhauMacDinh = MatKhau.Bam("123");
        foreach (var dg in docGiaThieuTk)
        {
            string email = $"{dg.MaDocGia.ToLowerInvariant()}@st.hcmuaf.edu.vn";
            if (!tenDaDung.Add(email)) continue;   // email đã bị tài khoản khác dùng

            db.TaiKhoans.Add(new TaiKhoan
            {
                TenDangNhap = email,
                MatKhauHash = matKhauMacDinh,
                HoTen = dg.HoTen,
                VaiTro = LoaiTaiKhoan.DocGia,
                MaDocGia = dg.MaDocGia,
                BatBuocDoiMatKhau = true
            });
        }
        db.SaveChanges();
    }

    // Chặn trùng tên đăng nhập
    private static void TaoChiMucDuyNhat(AppDbContext db)
    {
        try
        {
            db.Database.ExecuteSqlRaw(
                @"CREATE UNIQUE INDEX IF NOT EXISTS ""IX_TaiKhoans_TenDangNhap"" ON ""TaiKhoans"" (""TenDangNhap"" COLLATE NOCASE);");
            db.Database.ExecuteSqlRaw(
                @"CREATE UNIQUE INDEX IF NOT EXISTS ""IX_TaiKhoans_MaDocGia"" ON ""TaiKhoans"" (""MaDocGia"");");
        }
        catch (Exception ex)
        {
            NhatKy.Ghi(ex);   
        }
    }
}