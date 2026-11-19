using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.UI;

namespace QuanLyThuVien.Forms;

public partial class FrmMain : Form
{
    private Form? _formHienTai;
    private readonly Panel _pnlSidebar = new();
    private readonly Panel _pnlHeader = new();
    private readonly Label _lblTieuDeManHinh = new();
    private readonly List<Button> _nutDieuHuong = new();

    public FrmMain()
    {
        InitializeComponent();
        TaoKhungChinh();

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

    private void TaoKhungChinh()
    {
        menuStrip1.Visible = false;
        Controls.Clear();

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 3,
            BackColor = Theme.BgMain,
            Margin = Padding.Empty
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 190F));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));

        pnlContent.BackColor = Theme.BgMain;
        pnlContent.Padding = new Padding(12);
        pnlContent.Dock = DockStyle.Fill;
        pnlContent.Margin = Padding.Empty;

        _pnlSidebar.Dock = DockStyle.Fill;
        _pnlSidebar.BackColor = Theme.PrimaryDark;
        _pnlSidebar.Padding = new Padding(12);
        _pnlSidebar.Margin = Padding.Empty;

        var lblThuongHieu = new Label
        {
            Text = "THƯ VIỆN NLU",
            Font = new Font("Segoe UI", 13F, FontStyle.Bold),
            ForeColor = Color.White,
            Location = new Point(14, 20),
            Size = new Size(158, 32),
            TextAlign = ContentAlignment.MiddleCenter
        };
        var lblMoTa = new Label
        {
            Text = "HỆ THỐNG QUẢN LÝ THƯ VIỆN",
            Font = new Font("Segoe UI", 8F, FontStyle.Regular),
            ForeColor = Color.FromArgb(204, 251, 241),
            Location = new Point(10, 52),
            Size = new Size(166, 35),
            TextAlign = ContentAlignment.MiddleCenter
        };
        _pnlSidebar.Controls.Add(lblThuongHieu);
        _pnlSidebar.Controls.Add(lblMoTa);

        ThemNutDieuHuong("Quản lý Sách", typeof(FrmQuanLySach), () => MoForm(new FrmQuanLySach()));
        ThemNutDieuHuong("Quản lý Độc giả", typeof(FrmDocGia), () => MoForm(new FrmDocGia()));
        ThemNutDieuHuong("Mượn sách", typeof(FrmMuonSach), () => MoForm(new FrmMuonSach()));
        ThemNutDieuHuong("Trả sách && Phạt", typeof(FrmTraSach), () => MoForm(new FrmTraSach()));
        ThemNutDieuHuong("Báo cáo && Tổng quan", typeof(FrmBaoCao), () => MoForm(new FrmBaoCao()));

        _pnlHeader.Dock = DockStyle.Fill;
        _pnlHeader.BackColor = Color.White;
        _pnlHeader.Padding = new Padding(18, 8, 16, 8);
        _pnlHeader.Margin = Padding.Empty;

        var lblHeThong = new Label
        {
            Text = "HỆ THỐNG QUẢN LÝ THƯ VIỆN - ĐH NÔNG LÂM TP. HCM",
            Font = new Font("Segoe UI", 11F, FontStyle.Bold),
            ForeColor = Theme.TextDark,
            Location = new Point(18, 8),
            Size = new Size(800, 27),
            Anchor = AnchorStyles.Top | AnchorStyles.Left,
            AutoEllipsis = true
        };
        _lblTieuDeManHinh.Location = new Point(18, 37);
        _lblTieuDeManHinh.Size = new Size(800, 23);
        _lblTieuDeManHinh.ForeColor = Theme.Neutral;
        _lblTieuDeManHinh.Font = Theme.FontNormal;
        _lblTieuDeManHinh.AutoEllipsis = true;

        var lblNguoiDung = new Label
        {
            Text = $"Xin chào: {PhienDangNhap.HoTen} ({(PhienDangNhap.LaThuThu ? "Thủ thư" : "Độc giả")})",
            Font = Theme.FontNormal,
            ForeColor = Theme.TextDark,
            TextAlign = ContentAlignment.MiddleRight,
            Size = new Size(280, 34),
            Location = new Point(0, 18),
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            AutoEllipsis = true
        };
        var btnThoat = new Button
        {
            Text = "Đăng xuất",
            Size = new Size(100, 34),
            Location = new Point(0, 18),
            Anchor = AnchorStyles.Top | AnchorStyles.Right
        };
        Theme.StyleButton(btnThoat, Theme.Neutral);
        btnThoat.Click += (_, _) => mnuThoat_Click(this, EventArgs.Empty);
        _pnlHeader.Controls.AddRange(new Control[] { lblHeThong, _lblTieuDeManHinh, lblNguoiDung, btnThoat });
        _pnlHeader.Resize += (s, e) =>
        {
            btnThoat.Location = new Point(_pnlHeader.ClientSize.Width - btnThoat.Width - 16, 18);
            lblNguoiDung.Location = new Point(btnThoat.Left - lblNguoiDung.Width - 10, 18);
            int width = Math.Max(0, lblNguoiDung.Left - lblHeThong.Left - 18);
            lblHeThong.Width = width;
            _lblTieuDeManHinh.Width = width;
        };

        statusStrip1.Dock = DockStyle.Fill;
        statusStrip1.Margin = Padding.Empty;
        layout.Controls.Add(_pnlSidebar, 0, 0);
        layout.SetRowSpan(_pnlSidebar, 2);
        layout.Controls.Add(_pnlHeader, 1, 0);
        layout.Controls.Add(pnlContent, 1, 1);
        layout.Controls.Add(statusStrip1, 0, 2);
        layout.SetColumnSpan(statusStrip1, 2);
        Controls.Add(layout);
    }

    private void ThemNutDieuHuong(string tieuDe, Type loaiForm, Action moForm)
    {
        var nut = new Button
        {
            Text = tieuDe,
            Name = "btn" + _nutDieuHuong.Count,
            Location = new Point(10, 105 + _nutDieuHuong.Count * 50),
            Size = new Size(166, 42),
            Tag = loaiForm,
            FlatStyle = FlatStyle.Flat,
            BackColor = Theme.PrimaryDark,
            ForeColor = Color.White,
            Font = Theme.FontBold,
            TextAlign = ContentAlignment.MiddleLeft,
            Padding = new Padding(10, 0, 0, 0),
            Cursor = Cursors.Hand
        };
        nut.FlatAppearance.BorderColor = Color.FromArgb(63, 107, 102);
        nut.Click += (_, _) => moForm();
        _nutDieuHuong.Add(nut);
        _pnlSidebar.Controls.Add(nut);
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
        _lblTieuDeManHinh.Text = $"Phân hệ: {f.Text.Replace("&", "&&")}";

        foreach (var nut in _nutDieuHuong)
        {
            bool dangChon = nut.Tag is Type loaiForm && loaiForm == f.GetType();
            nut.BackColor = dangChon ? Theme.Primary : Theme.PrimaryDark;
            nut.FlatAppearance.BorderColor = dangChon ? Color.White : Color.FromArgb(63, 107, 102);
        }

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