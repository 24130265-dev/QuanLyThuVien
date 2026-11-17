using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;

namespace QuanLyThuVien.Forms;

public partial class FrmMain : Form
{
    private Form? _formHienTai;

    public FrmMain()
    {
        InitializeComponent();

        FormClosing += FrmMain_FormClosing;
        timerDongHo.Tick += (s, e) => CapNhatDongHo();

        mnuQuanLySach.Click += (s, e) => MoForm(new FrmQuanLySach());
        mnuTraSach.Click += (s, e) => MoForm(new FrmTraSach());
        mnuBaoCao.Click += (s, e) => MoForm(new FrmBaoCao());

        mnuDocGia.Click += (s, e) => MoForm(new FrmDocGia());
        mnuMuonSach.Click += (s, e) => MoForm(new FrmMuonSach());
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
        if (e.CloseReason != CloseReason.UserClosing) return;

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

    private void lblDongHo_Click(object sender, EventArgs e)
    {

    }

    private void mnuThoat_Click(object sender, EventArgs e)
    {
        Close();
    }
    
    //Nút của trả sách và báo cáo
    private void btnTraSach_Click(object sender, EventArgs e)
    {
        using var frm = new FrmTraSach();
        frm.ShowDialog();
    }

    private void btnBaoCao_Click(object sender, EventArgs e)
    {
        using var frm = new FrmBaoCao();
        frm.ShowDialog();
    }
}