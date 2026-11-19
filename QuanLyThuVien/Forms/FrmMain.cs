using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.UI;

namespace QuanLyThuVien.Forms;

public partial class FrmMain : Form
{
    private Form? _formHienTai;
    private bool _dangXuat;
    public bool DaDangXuat => _dangXuat;

    public FrmMain()
    {
        InitializeComponent();

        // 1. Áp Theme chung, sau đó tùy biến riêng phần khung chính
        Theme.Apply(this);
        Theme.FadeIn(this);

        // Mở rộng sidebar để chữ không bị cắt
        pnlSidebar.Width = 250;
        foreach (var b in flpMenu.Controls.OfType<Button>())
        {
            b.Width = 250;
            b.Height = 52;
        }

        // 2. Thanh tiêu đề trên
        pnlHeader.BackColor = Theme.CardBg;
        lblTieuDe.Font = Theme.FontTitle;
        lblTieuDe.ForeColor = Theme.PrimaryDark;
        lblPhanHe.ForeColor = Theme.Neutral;
        lblXinChao.Text = $"Xin chào: {PhienDangNhap.HoTen} (Thủ thư)";
        Theme.StyleButton(btnDangXuat, Theme.Neutral);

        // 3. Sidebar trái
        pnlSidebar.BackColor = Theme.PrimaryDark;
        flpMenu.BackColor = Theme.PrimaryDark;
        lblLogo.BackColor = Theme.PrimaryDark;
        lblLogo.ForeColor = Color.White;
        lblLogo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        SetActive(null);

    }

    private void btnQuanLySach_Click(object? sender, EventArgs e)
  => DieuHuong(btnQuanLySach, "Quản lý Sách", new FrmQuanLySach());

    private void btnTraSach_Click(object? sender, EventArgs e)
        => DieuHuong(btnTraSach, "Trả sách & Phạt", new FrmTraSach());

    private void btnBaoCao_Click(object? sender, EventArgs e)
        => DieuHuong(btnBaoCao, "Báo cáo & Tổng quan", new FrmBaoCao());

    private void btnDocGia_Click(object? sender, EventArgs e)
        => ChuaCo("Quản lý Độc giả");

    private void btnMuonSach_Click(object? sender, EventArgs e)
        => ChuaCo("Mượn sách");

    private void DieuHuong(Button nut, string tenManHinh, Form form)
    {
        SetActive(nut);
        lblPhanHe.Text = "Màn hình: " + tenManHinh;
        MoForm(form);
    }

    private void SetActive(Button? nutDangChon)
    {
        foreach (Button b in flpMenu.Controls.OfType<Button>())
            Theme.StyleSidebarButton(b, b == nutDangChon);
    }

    private void btnDangXuat_Click(object? sender, EventArgs e)
    {
        var kq = MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Xác nhận",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (kq != DialogResult.Yes) return;

        _dangXuat = true;
        PhienDangNhap.DangXuat();
        Close();
    }


    private async void FrmMain_Load(object? sender, EventArgs e)
    {
        CapNhatDongHo();
        timerDongHo.Start();
        await KiemTraKetNoiAsync();
        DieuHuong(btnQuanLySach, "Quản lý Sách", new FrmQuanLySach());
    }

    private void FrmMain_FormClosing(object? sender, FormClosingEventArgs e)
    {
        if (_dangXuat || e.CloseReason != CloseReason.UserClosing) return;

        var kq = MessageBox.Show("Bạn có chắc muốn thoát chương trình?", "Xác nhận",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (kq == DialogResult.No)
            e.Cancel = true;
    }

    private void CapNhatDongHo()
    {
        lblDongHo.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
    }
    private void timerDongHo_Tick(object? sender, EventArgs e) => CapNhatDongHo();

    private async Task KiemTraKetNoiAsync()
    {
        try
        {
            using var db = new AppDbContext();
            bool ok = await db.Database.CanConnectAsync();
            lblKetNoi.Text = ok ? "CSDL SQLite: Đã kết nối" : "CSDL SQLite: Mất kết nối";
        }
        catch
        {
            lblKetNoi.Text = "CSDL SQLite: Mất kết nối";
        }
    }

    // Nhúng form con vào pnlContent
    public void MoForm(Form f)
    {
        _formHienTai?.Close();
        _formHienTai = f;

        f.TopLevel = false;
        f.FormBorderStyle = FormBorderStyle.None;
        f.Dock = DockStyle.Fill;

        pnlContent.Controls.Clear();
        pnlContent.Controls.Add(f);
        f.Show();
    }

    private static void ChuaCo(string ten)
    {
        MessageBox.Show($"Màn hình \"{ten}\" sẽ được nối sau khi ghép code của nhóm.",
            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }



   

    
}