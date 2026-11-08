using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;

namespace QuanLyThuVien;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        // Báo Lỗi
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += (s, e) =>
            MessageBox.Show($"Lỗi giao diện: {e.Exception.Message}", "Cảnh báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            MessageBox.Show($"Lỗi hệ thống: {(e.ExceptionObject as Exception)?.Message}", "Lỗi nghiêm trọng",
                MessageBoxButtons.OK, MessageBoxIcon.Error);

        // Tự động tạo CSDL và nạp dữ liệu mẫu nếu chưa có
        try
        {
            using var db = new AppDbContext();
            db.Database.EnsureCreated();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Không thể khởi tạo CSDL:\n{ex.Message}", "Lỗi CSDL",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        Application.Run(new Form1());
    }
}