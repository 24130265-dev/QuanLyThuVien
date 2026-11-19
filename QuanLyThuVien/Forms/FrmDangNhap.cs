using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.UI;

namespace QuanLyThuVien.Forms;

public partial class FrmDangNhap : Form
{

    public FrmDangNhap()
    {
        InitializeComponent();

        // 1. Áp dụng bộ giao diện phẳng dùng chung
        Theme.Apply(this);

        // 2. Giữ lại màu nền xanh đậm và chữ trắng cho cột thương hiệu bên trái (pnlTrai)
        pnlTrai.BackColor = Theme.PrimaryDark;
        lblLogo.ForeColor = Color.White;
        lblLogo.Font = new Font("Segoe UI", 40F, FontStyle.Bold);
        lblTenTruong.ForeColor = Color.White;
        lblTenTruong.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblKhoa.ForeColor = Color.FromArgb(204, 251, 241);
        lblHeThong.ForeColor = Color.White;
        lblHeThong.Font = new Font("Segoe UI", 11F, FontStyle.Bold);

        // Tô màu xám cho nút Thoát
        Theme.StyleButton(btnThoat, Theme.Neutral);

        // Tự động điền lại tài khoản đã đăng nhập lần trước
        txtEmail.Text = PhienDangNhap.DocTaiKhoanLanCuoi();

        // Tự động gợi ý nhảy nút RadioButton khi gõ đuôi email trường
        txtEmail.TextChanged += TxtEmail_TextChanged;
    }

    private void TxtEmail_TextChanged(object? sender, EventArgs e)
    {
        string email = txtEmail.Text.Trim().ToLowerInvariant();
        if (email.EndsWith("@st.hcmuaf.edu.vn") || email.StartsWith("dg"))
            rdoDocGia.Checked = true;
        else if (email.EndsWith("@hcmuaf.edu.vn") || email == "admin")
            rdoThuThu.Checked = true;
    }

    // Bật / tắt hiển thị mật khẩu
    private void chkHienMatKhau_CheckedChanged(object? sender, EventArgs e)
    {
        txtMatKhau.UseSystemPasswordChar = !chkHienMatKhau.Checked;
    }

    // Xử lý khi bấm nút Đăng nhập
    private async void btnDangNhap_Click(object? sender, EventArgs e)
    {
        string taiKhoan = txtEmail.Text.Trim();
        string matKhau = txtMatKhau.Text;

        if (string.IsNullOrEmpty(taiKhoan) || string.IsNullOrEmpty(matKhau))
        {
            MessageBox.Show("Vui lòng nhập đầy đủ Email/Mã và Mật khẩu!",
                "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtEmail.Focus();
            return;
        }

        using var db = new AppDbContext();
        // Đảm bảo bảng TaiKhoans đã đồng bộ tài khoản cho các Độc giả mới
        PhienDangNhap.KhoiTaoDuLieuTaiKhoan(db);

        // Băm mật khẩu người dùng vừa nhập để so sánh với cột MatKhauHash trong CSDL
        string nhap = taiKhoan.ToLowerInvariant();
        string matKhauBam = PhienDangNhap.BamMatKhau(matKhau);
        TaiKhoan? tkTimThay;

        // TRƯỜNG HỢP 1: Đăng nhập vai trò Thủ thư / Quản trị
        if (rdoThuThu.Checked)
        {
            string emailThuThu = nhap.Contains('@') ? nhap : $"{nhap}@hcmuaf.edu.vn";

            tkTimThay = await db.TaiKhoans
                .AsNoTracking()
                .FirstOrDefaultAsync(t =>
                    t.VaiTro == "ThuThu" &&
                    (t.TenDangNhap.ToLower() == nhap || t.TenDangNhap.ToLower() == emailThuThu) &&
                    t.MatKhauHash == matKhauBam);
        }
        // TRƯỜNG HỢP 2: Đăng nhập vai trò Độc giả / Sinh viên
        else
        {
            string emailSv = nhap.Contains('@') ? nhap : $"{nhap}@st.hcmuaf.edu.vn";
            string maDocGia = nhap.Split('@')[0].ToUpperInvariant();

            tkTimThay = await db.TaiKhoans
                .AsNoTracking()
                .FirstOrDefaultAsync(t =>
                    t.VaiTro == "DocGia" &&
                    (t.TenDangNhap.ToLower() == nhap ||
                     t.TenDangNhap.ToLower() == emailSv ||
                     t.MaDocGia == maDocGia) &&
                    t.MatKhauHash == matKhauBam);
        }

        // Nếu không tìm thấy tài khoản khớp vai trò và mật khẩu
        if (tkTimThay == null)
        {
            string goiY = rdoThuThu.Checked
                ? "Tài khoản Thủ thư mẫu: admin@hcmuaf.edu.vn / Mật khẩu: 123"
                : "Tài khoản Sinh viên mẫu: dg01@st.hcmuaf.edu.vn (hoặc DG01) / Mật khẩu: 123";

            MessageBox.Show($"Sai tài khoản, mật khẩu hoặc chọn sai vai trò!\n\n({goiY})",
                "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        // Lưu phiên đăng nhập vào PhienDangNhap để FrmMain sử dụng
        PhienDangNhap.LuuPhien(tkTimThay);

        DialogResult = DialogResult.OK;
        Close();
    }
    private void btnThoat_Click(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}