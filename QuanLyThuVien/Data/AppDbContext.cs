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
            optionsBuilder.UseSqlite("Data Source=QuanLyThuVien.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TheLoai>(static e =>
        {
            e.HasKey(static x => x.MaTheLoai);
            e.Property(static x => x.TenTheLoai).IsRequired().HasMaxLength(100);
            e.HasOne(static x => x.TheLoaiCha)
             .WithMany(static x => x.TheLoaiCons)
             .HasForeignKey(static x => x.MaTheLoaiCha)
             .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<DauSach>(static e =>
        {
            e.HasKey(static x => x.MaDauSach);
            e.Property(static x => x.TenDauSach).IsRequired().HasMaxLength(200);
            e.Property(static x => x.GiaBia).HasColumnType("decimal(18,2)");
            e.HasOne(static x => x.TheLoai)
             .WithMany(static x => x.DauSachs)
             .HasForeignKey(static x => x.MaTheLoai)
             .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<CuonSach>(static e =>
        {
            e.HasKey(static x => x.MaCuonSach);
            e.Property(static x => x.TrangThai).IsRequired().HasMaxLength(50);
            e.HasOne(static x => x.DauSach)
             .WithMany(static x => x.CuonSachs)
             .HasForeignKey(static x => x.MaDauSach)
             .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<DocGia>(static e =>
        {
            e.HasKey(static x => x.MaDocGia);
            e.Property(static x => x.HoTen).IsRequired().HasMaxLength(100);
            e.Property(static x => x.TienNoPhat).HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<PhieuMuon>(static e =>
        {
            e.HasKey(static x => x.MaPhieuMuon);
            e.HasOne(static x => x.DocGia)
             .WithMany(static x => x.PhieuMuons)
             .HasForeignKey(static x => x.MaDocGia)
             .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ChiTietPhieuMuon>(static e =>
        {
            e.HasKey(static x => x.Id);
            e.HasOne(static x => x.PhieuMuon)
             .WithMany(static x => x.ChiTietPhieuMuons)
             .HasForeignKey(static x => x.MaPhieuMuon)
             .OnDelete(DeleteBehavior.Restrict);
            e.HasOne(static x => x.CuonSach)
             .WithMany(static x => x.ChiTietPhieuMuons)
             .HasForeignKey(static x => x.MaCuonSach)
             .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<HoaDonPhat>(static e =>
        {
            e.HasKey(static x => x.MaHoaDon);
            e.Property(static x => x.TienPhatQuaHan).HasColumnType("decimal(18,2)");
            e.Property(static x => x.TienBoiThuong).HasColumnType("decimal(18,2)");
            e.HasOne(static x => x.DocGia)
             .WithMany(static x => x.HoaDonPhats)
             .HasForeignKey(static x => x.MaDocGia)
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