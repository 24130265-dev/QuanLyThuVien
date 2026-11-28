using QuanLyThuVien.Data;
using QuanLyThuVien.Forms;

namespace QuanLyThuVien;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        // Báo lỗi: ghi chi tiết vào error.log, người dùng chỉ thấy thông báo chung
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += static (s, e) =>
        {
            NhatKy.Ghi(e.Exception);
            MessageBox.Show(MoTaLoi("Đã xảy ra lỗi giao diện.", e.Exception), "Cảnh báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        };
        AppDomain.CurrentDomain.UnhandledException += static (s, e) =>
        {
            var ex = e.ExceptionObject as Exception;
            NhatKy.Ghi(ex);
            MessageBox.Show(MoTaLoi("Đã xảy ra lỗi hệ thống.", ex), "Lỗi nghiêm trọng",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        };

        // Tự động tạo CSDL, nạp tài khoản Thủ thư / Sinh viên (chỉ chạy một lần khi mở app)
        try
        {
            using var db = new AppDbContext();
            db.Database.EnsureCreated();
            PhienDangNhap.KhoiTaoDuLieuTaiKhoan(db);
        }
        catch (Exception ex)
        {
            NhatKy.Ghi(ex);
            MessageBox.Show(MoTaLoi("Không thể khởi tạo CSDL.", ex), "Lỗi CSDL",
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
                var portal = new FrmDocGiaPortal();
                Application.Run(portal);
                if (!portal.DaDangXuat) return;
            }
        }
    }


    private static string MoTaLoi(string thongBao, Exception? ex)
    {
#if DEBUG
        return $"{thongBao}\n\n{ex?.Message}";
#else
        return $"{thongBao}\nChi tiết đã được ghi vào file error.log.";
#endif
    }
}