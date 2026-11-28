using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Entities;
using QuanLyThuVien.UI;

namespace QuanLyThuVien.Forms;

public partial class FrmDangNhap : Form
{

    public FrmDangNhap()
    {
        InitializeComponent();

        // 1. Áp dụng bộ giao diện phẳng dùng chung
        Theme.Apply(this);

        

        txtMatKhau.UseSystemPasswordChar = true;

        // 2. Giữ lại màu nền xanh đậm và chữ trắng cho cột thương hiệu bên trái (pnlTrai)
        pnlTrai.BackColor = Theme.PrimaryDark;
        lblLogo.ForeColor = Color.White;
        lblLogo.Font = new Font("Segoe UI", 40F, FontStyle.Bold);
        lblTenTruong.ForeColor = Color.White;
        lblTenTruong.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        lblKhoa.ForeColor = Color.FromArgb(204, 251, 241);
        lblHeThong.ForeColor = Color.White;
        lblHeThong.Font = new Font("Segoe UI", 11F, FontStyle.Bold);

        // Tô màu xám cho nút Thoát
        Theme.StyleButton(btnThoat, Theme.Neutral);
#if !DEBUG
        lblGhiChu.Visible = false;   // bản phát hành: không lộ mật khẩu mặc định
#endif

        // Liên kết mở form đăng ký (tạo bằng code, không cần sửa Designer)
        var lnkDangKy = new LinkLabel
        {
            Text = "Sinh viên mới? Đăng ký",
            AutoSize = true,
            Location = new Point(235, 167),
            Font = Theme.FontNormal,
            LinkColor = Theme.Secondary
        };
        lnkDangKy.LinkClicked += LnkDangKy_LinkClicked;
        grpDangNhap.Controls.Add(lnkDangKy);

        // Tự động gợi ý nhảy nút RadioButton khi gõ đuôi email trường
        txtEmail.TextChanged += TxtEmail_TextChanged;

        // Tự động điền lại tài khoản đã đăng nhập lần trước
        txtEmail.Text = PhienDangNhap.DocTaiKhoanLanCuoi();
    }

    private void TxtEmail_TextChanged(object? sender, EventArgs e)
    {
        string email = txtEmail.Text.Trim().ToLowerInvariant();
        if (email.EndsWith("@st.hcmuaf.edu.vn") || (email.Length > 0 && char.IsDigit(email[0])))
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
        string nhap = taiKhoan.ToLowerInvariant();

        
        if (rdoDocGia.Checked)
            PhienDangNhap.DongBoTaiKhoanDocGia(db);

        TaiKhoan? tkTimThay;

        // TRƯỜNG HỢP 1: Đăng nhập vai trò Thủ thư / Quản trị
        if (rdoThuThu.Checked)
        {
            string emailThuThu = nhap.Contains('@') ? nhap : $"{nhap}@hcmuaf.edu.vn";

            tkTimThay = await db.TaiKhoans.FirstOrDefaultAsync(t =>
                    t.VaiTro == LoaiTaiKhoan.ThuThu &&
                    (t.TenDangNhap.ToLower() == nhap || t.TenDangNhap.ToLower() == emailThuThu));
        }
        // TRƯỜNG HỢP 2: Đăng nhập vai trò Độc giả / Sinh viên
        else
        {
            bool laEmail = nhap.Contains('@');
            string maDocGia = nhap.ToUpperInvariant();

            tkTimThay = await db.TaiKhoans.FirstOrDefaultAsync(t =>
                    t.VaiTro == LoaiTaiKhoan.DocGia &&
                    (laEmail
                        ? t.TenDangNhap.ToLower() == nhap
                        : t.MaDocGia == maDocGia));
        }


        // Nếu không tìm thấy tài khoản HOẶC mật khẩu nhập vào không khớp
        if (tkTimThay == null || !MatKhau.Kiem(matKhau, tkTimThay.MatKhauHash))
        {

            string thongBao = "Sai tài khoản, mật khẩu hoặc chọn sai vai trò!";
#if DEBUG
            string goiY = rdoThuThu.Checked
                ? "Tài khoản Thủ thư mẫu: admin@hcmuaf.edu.vn / Mật khẩu: 123"
                : "Tài khoản Sinh viên mẫu: 24130265@st.hcmuaf.edu.vn (hoặc 24130265) / Mật khẩu: 123";
            thongBao += $"\n\n({goiY})";
#endif
            MessageBox.Show(thongBao, "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

            // Tự động nâng cấp mật khẩu từ chuẩn SHA256 cũ lên PBKDF2 mới
            if (MatKhau.CanNangCap(tkTimThay.MatKhauHash))
        {
            tkTimThay.MatKhauHash = MatKhau.Bam(matKhau);
            await db.SaveChangesAsync();
        }
        if (tkTimThay.BatBuocDoiMatKhau)
        {
            using var dlg = new DlgDoiMatKhau(tkTimThay.MaTaiKhoan);
            if (dlg.ShowDialog(this) != DialogResult.OK)
                return;                                   

            await db.Entry(tkTimThay).ReloadAsync();  // Tải lại dữ liệu tài khoản từ CSDL sau khi đổi mật khẩu
        }

        // Lưu phiên đăng nhập vào PhienDangNhap để FrmMain sử dụng
        PhienDangNhap.LuuPhien(tkTimThay);

        DialogResult = DialogResult.OK;
        Close();
    }
    private void LnkDangKy_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
    {
        using var dlg = new DlgDangKy();
        if (dlg.ShowDialog(this) != DialogResult.OK) return;

        rdoDocGia.Checked = true;
        txtEmail.Text = dlg.MaSinhVien;
        txtMatKhau.Clear();
        txtMatKhau.Focus();
        Toast.ThanhCong(this, "Đăng ký thành công. Hãy đăng nhập bằng mật khẩu vừa đặt.");
    }

    private void btnThoat_Click(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}