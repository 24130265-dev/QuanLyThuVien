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
    }

    private async void btnLuu_Click(object? sender, EventArgs e)
    {
        string moi = txtMatKhauMoi.Text;

        if (moi.Length < 6)
        {
            Toast.CanhBao(this, "Mật khẩu mới cần ít nhất 6 ký tự.");
            txtMatKhauMoi.Focus();
            return;
        }
        if (moi == "123")
        {
            Toast.CanhBao(this, "Không được dùng lại mật khẩu mặc định.");
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
        if (tk == null) return;

        tk.MatKhauHash = MatKhau.Bam(moi);
        tk.BatBuocDoiMatKhau = false;
        await db.SaveChangesAsync();

        DialogResult = DialogResult.OK;
    }
}