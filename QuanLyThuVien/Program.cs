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

        // Báo lỗi
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += (s, e) =>
            MessageBox.Show($"Lỗi giao diện: {e.Exception.Message}", "Cảnh báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            MessageBox.Show($"Lỗi hệ thống: {(e.ExceptionObject as Exception)?.Message}", "Lỗi nghiêm trọng",
                MessageBoxButtons.OK, MessageBoxIcon.Error);

        // Tự động tạo CSDL và nạp dữ liệu tài khoản Thủ thư / Sinh viên
        try
        {
            using var db = new AppDbContext();
            db.Database.EnsureCreated();
            PhienDangNhap.KhoiTaoDuLieuTaiKhoan(db);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Không thể khởi tạo CSDL:\n{ex.Message}", "Lỗi CSDL",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        // Màn hình đăng nhập
        while (true)
        {
            using (var frmLogin = new FrmDangNhap())
            {
                if (frmLogin.ShowDialog() != DialogResult.OK)
                    return;
            }

            if (PhienDangNhap.LaThuThu)
            {
                var frmMain = new FrmMain();
                Application.Run(frmMain);
                if (!frmMain.DaDangXuat) return;   // bấm X thì thoát hẳn
            }
            else
            {
                MessageBox.Show("Cổng thông tin độc giả đang được phát triển.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                PhienDangNhap.DangXuat();
            }
        }
    }
}