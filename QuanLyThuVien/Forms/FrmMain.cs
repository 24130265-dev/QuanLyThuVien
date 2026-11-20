using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;
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

        Theme.StyleStatusStrip(statusStrip1);
        ThemDuongBong();

    }

    private void btnQuanLySach_Click(object? sender, EventArgs e)
  => DieuHuong(btnQuanLySach, "Quản lý Sách", new FrmQuanLySach());

    private void btnTraSach_Click(object? sender, EventArgs e)
        => DieuHuong(btnTraSach, "Trả sách & Phạt", new FrmTraSach());

    private void btnBaoCao_Click(object? sender, EventArgs e)
        => DieuHuong(btnBaoCao, "Báo cáo & Tổng quan", new FrmBaoCao());

    private void btnDocGia_Click(object? sender, EventArgs e)
        => DieuHuong(btnDocGia, "Quản lý Độc giả", new FrmDocGia());

    private void btnMuonSach_Click(object? sender, EventArgs e)
        => DieuHuong(btnMuonSach, "Mượn sách", new FrmMuonSach());

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
            lblKetNoi.Text = ok ? "●  CSDL SQLite: Đã kết nối" : "●  CSDL SQLite: Mất kết nối";
            lblKetNoi.ForeColor = ok ? Color.FromArgb(22, 163, 74) : Theme.Danger;
        }
        catch
        {
            lblKetNoi.Text = "●  CSDL SQLite: Mất kết nối";
            lblKetNoi.ForeColor = Theme.Danger;
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
    // Đường kẻ mảnh + bóng đổ nhẹ ngay dưới thanh tiêu đề
    private void ThemDuongBong()
    {
        var pnlBong = new Panel { Name = "pnlBong", Dock = DockStyle.Top, Height = 6 };

        pnlBong.Paint += (s, e) =>
        {
            var g = e.Graphics;
            int w = pnlBong.Width, h = pnlBong.Height;
            int xs = pnlSidebar.Width;

            
            using (var nenSidebar = new SolidBrush(Theme.PrimaryDark))
                g.FillRectangle(nenSidebar, 0, 0, xs, h);
            using (var nenNoiDung = new SolidBrush(Theme.BgMain))
                g.FillRectangle(nenNoiDung, xs, 0, Math.Max(0, w - xs), h);

            using (var duongKe = new Pen(Theme.Border))
                g.DrawLine(duongKe, xs, 0, w, 0);

            if (w - xs > 0)
            {
                var vung = new Rectangle(xs, 1, w - xs, h - 1);
                using var br = new LinearGradientBrush(vung,
                    Color.FromArgb(40, 15, 23, 42), Color.FromArgb(0, 15, 23, 42),
                    LinearGradientMode.Vertical);
                g.FillRectangle(br, vung);
            }
        };
        pnlBong.Resize += (s, e) => pnlBong.Invalidate();

        Controls.Add(pnlBong);
        // Phải nằm giữa sidebar và header trong thứ tự dock
        Controls.SetChildIndex(pnlBong, Controls.GetChildIndex(pnlSidebar) + 1);
    }







}