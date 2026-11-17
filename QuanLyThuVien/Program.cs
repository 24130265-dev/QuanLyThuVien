using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Forms;

namespace QuanLyThuVien;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        // Báo Lỗi
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += static (s, e) =>
            MessageBox.Show($"Lỗi giao diện: {e.Exception.Message}", "Cảnh báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        AppDomain.CurrentDomain.UnhandledException += static (s, e) =>
            MessageBox.Show($"Lỗi hệ thống: {(e.ExceptionObject as Exception)?.Message}", "Lỗi nghiêm trọng",
                MessageBoxButtons.OK, MessageBoxIcon.Error);

        // Tự động tạo CSDL và nạp dữ liệu tài khoản Thủ thư / Sinh viên
        try
        {
            using var db = new AppDbContext();
            db.Database.EnsureCreated();
            PhienDangNhap.KhoiTaoDuLieuTaiKhoan(db); //Tự tạo bảng TaiKhoans & tài khoản mẫu
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Không thể khởi tạo CSDL:\n{ex.Message}", "Lỗi CSDL",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        // Mở màn hình Đăng nhập
        using (var frmLogin = new Forms.FrmDangNhap())
        {
            if (frmLogin.ShowDialog() != DialogResult.OK)
                return;
        }

        Application.Run(new Forms.FrmMain());
    }
}