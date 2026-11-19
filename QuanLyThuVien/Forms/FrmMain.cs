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

        Theme.Apply(this);
        Theme.FadeIn(this);

        pnlSidebar.Width = 250;
        foreach (var button in flpMenu.Controls.OfType<Button>())
        {
            button.Width = 250;
            button.Height = 52;
        }

        pnlHeader.BackColor = Theme.CardBg;
        lblTieuDe.Font = Theme.FontTitle;
        lblTieuDe.ForeColor = Theme.PrimaryDark;
        lblPhanHe.ForeColor = Theme.Neutral;
        lblXinChao.Text = $"Xin chào: {PhienDangNhap.HoTen} ({(PhienDangNhap.LaThuThu ? "Thủ thư" : "Độc giả")})";
        Theme.StyleButton(btnDangXuat, Theme.Neutral);

        pnlSidebar.BackColor = Theme.PrimaryDark;
        flpMenu.BackColor = Theme.PrimaryDark;
        lblLogo.BackColor = Theme.PrimaryDark;
        lblLogo.ForeColor = Color.White;
        lblLogo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        SetActive(null);
    }

    private void btnQuanLySach_Click(object? sender, EventArgs e)
        => DieuHuong(btnQuanLySach, "Quản lý Sách", new FrmQuanLySach());

    private void btnDocGia_Click(object? sender, EventArgs e)
        => DieuHuong(btnDocGia, "Quản lý Độc giả", new FrmDocGia());

    private void btnMuonSach_Click(object? sender, EventArgs e)
        => DieuHuong(btnMuonSach, "Mượn sách", new FrmMuonSach());

    private void btnTraSach_Click(object? sender, EventArgs e)
        => DieuHuong(btnTraSach, "Trả sách & Phạt", new FrmTraSach());

    private void btnBaoCao_Click(object? sender, EventArgs e)
        => DieuHuong(btnBaoCao, "Báo cáo & Tổng quan", new FrmBaoCao());

    private void DieuHuong(Button nut, string tenManHinh, Form form)
    {
        SetActive(nut);
        lblPhanHe.Text = $"Màn hình: {tenManHinh}";
        MoForm(form);
    }

    private void SetActive(Button? nutDangChon)
    {
        foreach (Button button in flpMenu.Controls.OfType<Button>())
            Theme.StyleSidebarButton(button, button == nutDangChon);
    }

    private void btnDangXuat_Click(object? sender, EventArgs e)
    {
        var ketQua = MessageBox.Show(
            "Bạn có chắc muốn đăng xuất?",
            "Xác nhận",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);
        if (ketQua != DialogResult.Yes)
            return;

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
        if (_dangXuat || e.CloseReason != CloseReason.UserClosing)
            return;

        var ketQua = MessageBox.Show(
            "Bạn có chắc muốn thoát chương trình?",
            "Xác nhận",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);
        if (ketQua == DialogResult.No)
            e.Cancel = true;
    }

    private void timerDongHo_Tick(object? sender, EventArgs e)
        => CapNhatDongHo();

    private void CapNhatDongHo()
    {
        lblDongHo.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
    }

    private async Task KiemTraKetNoiAsync()
    {
        try
        {
            using var db = new AppDbContext();
            bool daKetNoi = await db.Database.CanConnectAsync();
            lblKetNoi.Text = daKetNoi
                ? "CSDL SQLite: Đã kết nối"
                : "CSDL SQLite: Mất kết nối";
        }
        catch
        {
            lblKetNoi.Text = "CSDL SQLite: Mất kết nối";
        }
    }

    public void MoForm(Form form)
    {
        _formHienTai?.Close();
        _formHienTai = form;

        form.TopLevel = false;
        form.FormBorderStyle = FormBorderStyle.None;
        form.Dock = DockStyle.Fill;

        pnlContent.Controls.Clear();
        pnlContent.Controls.Add(form);
        form.Show();
    }
}
