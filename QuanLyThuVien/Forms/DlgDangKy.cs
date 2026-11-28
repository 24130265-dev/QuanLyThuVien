using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Entities;
using QuanLyThuVien.UI;

namespace QuanLyThuVien.Forms;

/// <summary>
/// Sinh viên tự đăng ký lần đầu: tạo cùng lúc 1 DocGia và 1 TaiKhoan.
/// FrmDangNhap đọc MaSinhVien để điền sẵn ô đăng nhập.
/// </summary>
public partial class DlgDangKy : Form
{
    public string MaSinhVien { get; private set; } = string.Empty;

    public DlgDangKy()
    {
        InitializeComponent();
        Theme.Apply(this);

        lblTieuDe.Font = Theme.FontTitle;
        lblTieuDe.ForeColor = Theme.PrimaryDark;
        lblGhiChu.ForeColor = Theme.Neutral;
        Theme.StyleButton(btnHuy, Theme.Neutral);
        txtMatKhau.MaxLength = MatKhau.DoDaiToiDa;
        txtXacNhan.MaxLength = MatKhau.DoDaiToiDa;
    }

    private void chkHienMatKhau_CheckedChanged(object? sender, EventArgs e)
    {
        bool hien = chkHienMatKhau.Checked;
        txtMatKhau.UseSystemPasswordChar = !hien;
        txtXacNhan.UseSystemPasswordChar = !hien;
    }

    private void BaoLoi(string noiDung, Control oCanSua)
    {
        Toast.CanhBao(this, noiDung);
        oCanSua.Focus();
    }

    private async void btnDangKy_Click(object? sender, EventArgs e)
    {
        string ma = txtMaSv.Text.Trim();
        string hoTen = Regex.Replace(txtHoTen.Text.Trim(), @"\s+", " ");
        string sdt = txtSdt.Text.Trim();
        string matKhau = txtMatKhau.Text;

        // ===== Kiểm tra dữ liệu nhập =====
        if (!Regex.IsMatch(ma, @"^\d{8}$"))
        { BaoLoi("Mã số sinh viên gồm đúng 8 chữ số (ví dụ 24130265).", txtMaSv); return; }
        if (hoTen.Split(' ').Length < 2)
        { BaoLoi("Hãy nhập đầy đủ họ và tên (ít nhất 2 từ).", txtHoTen); return; }
        if (!Regex.IsMatch(sdt, @"^0\d{9}$"))
        { BaoLoi("Số điện thoại gồm 10 chữ số và bắt đầu bằng 0.", txtSdt); return; }

        string? loiMatKhau = MatKhau.KiemTraDoManh(matKhau, ma);
        if (loiMatKhau != null)
        { BaoLoi(loiMatKhau, txtMatKhau); return; }
        if (matKhau != txtXacNhan.Text)
        { BaoLoi("Hai mật khẩu không khớp.", txtXacNhan); return; }

        btnDangKy.Enabled = false;
        try
        {
            using var db = new AppDbContext();
            string email = $"{ma}@st.hcmuaf.edu.vn";

            // Mã đã có (do thủ thư tạo hoặc đã đăng ký) thì không cho đăng ký lại, 
            bool daCo = await db.DocGias.AnyAsync(d => d.MaDocGia == ma)
                     || await db.TaiKhoans.AnyAsync(t => t.MaDocGia == ma || t.TenDangNhap == email);
            if (daCo)
            {
                BaoLoi("Mã số sinh viên này đã có trong hệ thống. Hãy đăng nhập, hoặc liên hệ thủ thư nếu bạn quên mật khẩu.", txtMaSv);
                return;
            }

            // Lưu cả hai bản ghi trong một lần SaveChanges để không bị tạo dở dang
            db.DocGias.Add(new DocGia
            {
                MaDocGia = ma,
                HoTen = hoTen,
                SoDienThoai = sdt,
                BiKhoaThe = false,
                TienNoPhat = 0
            });
            db.TaiKhoans.Add(new TaiKhoan
            {
                TenDangNhap = email,
                MatKhauHash = MatKhau.Bam(matKhau),   // luôn băm, không lưu mật khẩu thô
                HoTen = hoTen,
                VaiTro = LoaiTaiKhoan.DocGia,
                MaDocGia = ma,
                BatBuocDoiMatKhau = false             // đã tự đặt mật khẩu nên không bắt đổi nữa
            });
            await db.SaveChangesAsync();

            MaSinhVien = ma;
            DialogResult = DialogResult.OK;
        }
        catch (DbUpdateException ex)
        {
            NhatKy.Ghi(ex);
            BaoLoi("Mã số sinh viên này vừa được đăng ký. Hãy thử đăng nhập lại.", txtMaSv);
        }
        catch (Exception ex)
        {
            NhatKy.Ghi(ex);
            Toast.Loi(this, "Không thể đăng ký. Vui lòng thử lại.");
        }
        finally
        {
            btnDangKy.Enabled = true;
        }
    }
}