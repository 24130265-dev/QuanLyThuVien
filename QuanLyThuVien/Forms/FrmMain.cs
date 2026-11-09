using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;

namespace QuanLyThuVien.Forms;

public partial class FrmMain : Form
{
    private readonly MenuStrip _menu = new();
    private readonly StatusStrip _status = new();
    private readonly ToolStripStatusLabel lblKetNoi = new() { Spring = true, TextAlign = ContentAlignment.MiddleLeft };
    private readonly ToolStripStatusLabel lblDongHo = new();
    private readonly Panel pnlContent = new() { Dock = DockStyle.Fill };
    private readonly System.Windows.Forms.Timer timerDongHo = new() { Interval = 1000 };

    private readonly ToolStripMenuItem mnuQuanLySach = new("Quản lý Sách");
    private readonly ToolStripMenuItem mnuDocGia = new("Quản lý Độc giả");
    private readonly ToolStripMenuItem mnuMuonSach = new("Mượn sách");
    private readonly ToolStripMenuItem mnuTraSach = new("Trả sách && Phạt");
    private readonly ToolStripMenuItem mnuBaoCao = new("Báo cáo thống kê");

    private Form? _formHienTai;

    public FrmMain()
    {
        InitializeComponent();

        Text = "Quản lý Thư viện";
        StartPosition = FormStartPosition.CenterScreen;
        WindowState = FormWindowState.Maximized;

        // Thứ tự Add quan trọng: panel (Fill) trước, rồi status, rồi menu
        _menu.Items.AddRange(new ToolStripItem[] { mnuQuanLySach, mnuDocGia, mnuMuonSach, mnuTraSach, mnuBaoCao });
        _status.Items.AddRange(new ToolStripItem[] { lblKetNoi, lblDongHo });
        Controls.Add(pnlContent);
        Controls.Add(_status);
        Controls.Add(_menu);
        MainMenuStrip = _menu;

        Load += FrmMain_Load;
        FormClosing += FrmMain_FormClosing;
        timerDongHo.Tick += (s, e) => CapNhatDongHo();

        mnuQuanLySach.Click += (s, e) => MoForm(new FrmQuanLySach());
        mnuTraSach.Click += (s, e) => MoForm(new FrmTraSach());
        mnuBaoCao.Click += (s, e) => MoForm(new FrmBaoCao());

        // Hai form của TV2 sẽ nối sau khi TV2 đẩy code lên main (Ngày 11)
        mnuDocGia.Click += (s, e) => ChuaCo("Quản lý Độc giả");
        mnuMuonSach.Click += (s, e) => ChuaCo("Mượn sách");
    }

    private async void FrmMain_Load(object? sender, EventArgs e)
    {
        CapNhatDongHo();
        timerDongHo.Start();
        await KiemTraKetNoiAsync();
        MoForm(new FrmQuanLySach());
    }

    private void FrmMain_FormClosing(object? sender, FormClosingEventArgs e)
    {
        var kq = MessageBox.Show("Bạn có chắc muốn thoát chương trình?", "Xác nhận",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (kq == DialogResult.No)
            e.Cancel = true;
    }

    private void CapNhatDongHo()
    {
        lblDongHo.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
    }

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