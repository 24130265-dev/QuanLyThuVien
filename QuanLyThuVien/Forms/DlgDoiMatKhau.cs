using QuanLyThuVien.Data;
using QuanLyThuVien.UI;

namespace QuanLyThuVien.Forms;

public partial class DlgDoiMatKhau : Form
{
    private readonly int _maTaiKhoan;

    public DlgDoiMatKhau(int maTaiKhoan)
    {
        InitializeComponent();
        _maTaiKhoan = maTaiKhoan;

        Theme.Apply(this);

        txtMatKhauMoi.MaxLength = MatKhau.DoDaiToiDa;
        txtXacNhanMatKhau.MaxLength = MatKhau.DoDaiToiDa;
    }

    private async void btnLuu_Click(object? sender, EventArgs e)
    {
        btnLuu.Enabled = false;
        try
        {
            string moi = txtMatKhauMoi.Text;

            string? loi = MatKhau.KiemTraDoManh(moi, PhienDangNhap.MaDocGia);
            if (loi != null)
            {
                Toast.CanhBao(this, loi);
                txtMatKhauMoi.Focus();
                return;
            }
            if (moi != txtXacNhanMatKhau.Text)
            {
                Toast.CanhBao(this, "Hai mật khẩu không khớp.");
                txtXacNhanMatKhau.Focus();
                return;
            }

            using var db = new AppDbContext();
            var tk = await db.TaiKhoans.FindAsync(_maTaiKhoan);
            if (tk == null)
            {
                Toast.Loi(this, "Không tìm thấy tài khoản để đổi mật khẩu.");
                return;
            }

            tk.MatKhauHash = MatKhau.Bam(moi);
            tk.BatBuocDoiMatKhau = false;
            await db.SaveChangesAsync();

            DialogResult = DialogResult.OK;
        }
        catch (Exception ex)
        {
            NhatKy.Ghi(ex);
            Toast.Loi(this, "Không thể đổi mật khẩu. Vui lòng thử lại.");
        }
        finally
        {
            btnLuu.Enabled = true;
        }
    }
}