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
    }
}