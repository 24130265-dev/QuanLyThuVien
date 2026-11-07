using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Entities;

namespace QuanLyThuVien.Data;

public class AppDbContext : DbContext
{
    public DbSet<TheLoai> TheLoais { get; set; }
    public DbSet<DauSach> DauSachs { get; set; }
    public DbSet<CuonSach> CuonSachs { get; set; }
    public DbSet<DocGia> DocGias { get; set; }
    public DbSet<PhieuMuon> PhieuMuons { get; set; }
    public DbSet<ChiTietPhieuMuon> ChiTietPhieuMuons { get; set; }
    public DbSet<HoaDonPhat> HoaDonPhats { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\MSSQLLocalDB;Database=QuanLyThuVienDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

      
        modelBuilder.Entity<TheLoai>(e =>
        {
            e.HasKey(x => x.MaTheLoai);
            e.Property(x => x.TenTheLoai).IsRequired().HasMaxLength(100);
            e.HasOne(x => x.TheLoaiCha)
             .WithMany(x => x.TheLoaiCons)
             .HasForeignKey(x => x.MaTheLoaiCha)
             .OnDelete(DeleteBehavior.Restrict);
        });


        modelBuilder.Entity<DauSach>(e =>
        {
            e.HasKey(x => x.MaDauSach);
            e.Property(x => x.TenDauSach).IsRequired().HasMaxLength(200);
            e.Property(x => x.GiaBia).HasColumnType("decimal(18,2)");
            e.HasOne(x => x.TheLoai)
             .WithMany(x => x.DauSachs)
             .HasForeignKey(x => x.MaTheLoai)
             .OnDelete(DeleteBehavior.Restrict);
        });


        modelBuilder.Entity<CuonSach>(e =>
        {
            e.HasKey(x => x.MaCuonSach);
            e.Property(x => x.TrangThai).IsRequired().HasMaxLength(50);
            e.HasOne(x => x.DauSach)
             .WithMany(x => x.CuonSachs)
             .HasForeignKey(x => x.MaDauSach)
             .OnDelete(DeleteBehavior.Restrict);
        });

     
        modelBuilder.Entity<DocGia>(e =>
        {
            e.HasKey(x => x.MaDocGia);
            e.Property(x => x.HoTen).IsRequired().HasMaxLength(100);
            e.Property(x => x.TienNoPhat).HasColumnType("decimal(18,2)");
        });

     
        modelBuilder.Entity<PhieuMuon>(e =>
        {
            e.HasKey(x => x.MaPhieuMuon);
            e.HasOne(x => x.DocGia)
             .WithMany(x => x.PhieuMuons)
             .HasForeignKey(x => x.MaDocGia)
             .OnDelete(DeleteBehavior.Restrict);
        });

       
        modelBuilder.Entity<ChiTietPhieuMuon>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasOne(x => x.PhieuMuon)
             .WithMany(x => x.ChiTietPhieuMuons)
             .HasForeignKey(x => x.MaPhieuMuon)
             .OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.CuonSach)
             .WithMany(x => x.ChiTietPhieuMuons)
             .HasForeignKey(x => x.MaCuonSach)
             .OnDelete(DeleteBehavior.Restrict);
        });

        
        modelBuilder.Entity<HoaDonPhat>(e =>
        {
            e.HasKey(x => x.MaHoaDon);
            e.Property(x => x.TienPhatQuaHan).HasColumnType("decimal(18,2)");
            e.Property(x => x.TienBoiThuong).HasColumnType("decimal(18,2)");
            e.HasOne(x => x.DocGia)
             .WithMany(x => x.HoaDonPhats)
             .HasForeignKey(x => x.MaDocGia)
             .OnDelete(DeleteBehavior.Restrict);
        });

        // DỮ LIỆU MẪU (SEED DATA)

        modelBuilder.Entity<TheLoai>().HasData(
            new TheLoai { MaTheLoai = "TL_CNTT", TenTheLoai = "Công nghệ thông tin", MaTheLoaiCha = null },
            new TheLoai { MaTheLoai = "TL_LAPTRINH", TenTheLoai = "Lập trình C#", MaTheLoaiCha = "TL_CNTT" },
            new TheLoai { MaTheLoai = "TL_CSDL", TenTheLoai = "Cơ sở dữ liệu", MaTheLoaiCha = "TL_CNTT" },
            new TheLoai { MaTheLoai = "TL_KINHTE", TenTheLoai = "Kinh tế", MaTheLoaiCha = null },
            new TheLoai { MaTheLoai = "TL_QUANTRI", TenTheLoai = "Quản trị", MaTheLoaiCha = "TL_KINHTE" }
        );

        modelBuilder.Entity<DauSach>().HasData(
            new DauSach { MaDauSach = "SACH01", TenDauSach = "Lập trình C#", TacGia = "Nguyễn Văn A", NamXuatBan = 2024, GiaBia = 150000m, MaTheLoai = "TL_LAPTRINH" },
            new DauSach { MaDauSach = "SACH02", TenDauSach = "Thực hành SQL Server", TacGia = "Trần Thị B", NamXuatBan = 2023, GiaBia = 120000m, MaTheLoai = "TL_CSDL" }
        );

        modelBuilder.Entity<CuonSach>().HasData(
            new CuonSach { MaCuonSach = "SACH01_1", MaDauSach = "SACH01", ViTriKe = "Kệ A1 - Tầng 2", TrangThai = TrangThaiSach.SanSang },
            new CuonSach { MaCuonSach = "SACH01_2", MaDauSach = "SACH01", ViTriKe = "Kệ A1 - Tầng 2", TrangThai = TrangThaiSach.SanSang },
            new CuonSach { MaCuonSach = "SACH01_3", MaDauSach = "SACH01", ViTriKe = "Kệ A1 - Tầng 2", TrangThai = TrangThaiSach.DangMuon },
            new CuonSach { MaCuonSach = "SACH02_1", MaDauSach = "SACH02", ViTriKe = "Kệ B2 - Tầng 1", TrangThai = TrangThaiSach.SanSang },
            new CuonSach { MaCuonSach = "SACH02_2", MaDauSach = "SACH02", ViTriKe = "Kệ B2 - Tầng 1", TrangThai = TrangThaiSach.SanSang }
        );

        modelBuilder.Entity<DocGia>().HasData(
            new DocGia { MaDocGia = "DG01", HoTen = "Lê Hoàng Nam", SoDienThoai = "0901234567", BiKhoaThe = false, TienNoPhat = 0m },
            new DocGia { MaDocGia = "DG02", HoTen = "Phạm Minh Tuấn", SoDienThoai = "0902345678", BiKhoaThe = true, TienNoPhat = 0m },
            new DocGia { MaDocGia = "DG03", HoTen = "Võ Thị Lan", SoDienThoai = "0903456789", BiKhoaThe = false, TienNoPhat = 15000m }
        );

        // Phiếu mượn đã quá hạn: DG01 mượn SACH01_3
        modelBuilder.Entity<PhieuMuon>().HasData(
            new PhieuMuon
            {
                MaPhieuMuon = 1,
                MaDocGia = "DG01",
                NgayMuon = new DateTime(2026, 10, 1),
                NgayPhaiTra = new DateTime(2026, 10, 15)
            }
        );

        modelBuilder.Entity<ChiTietPhieuMuon>().HasData(
            new ChiTietPhieuMuon
            {
                Id = 1,
                MaPhieuMuon = 1,
                MaCuonSach = "SACH01_3",
                NgayTraThucTe = null,
                TinhTrangKhiTra = null
            }
        );

        // Hóa đơn phạt chưa thanh toán của DG03
        modelBuilder.Entity<HoaDonPhat>().HasData(
            new HoaDonPhat
            {
                MaHoaDon = 1,
                MaDocGia = "DG03",
                MaPhieuMuon = null,
                NgayLap = new DateTime(2026, 10, 20),
                TienPhatQuaHan = 15000m,
                TienBoiThuong = 0m,
                LyDo = "Trả sách trễ 3 ngày",
                DaThanhToan = false
            }
        );
    }
}