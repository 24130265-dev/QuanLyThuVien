using System.ComponentModel;
using System;
using System.Drawing;
using System.Windows.Forms;
namespace QuanLyThuVien.Forms;

partial class FrmBaoCao
{
    
    private System.ComponentModel.IContainer components = null;

    private Panel pnlHeader;
    private Label lblTitle;
    private Label lblTuNgay;
    private DateTimePicker dtpTuNgay;
    private Label lblDenNgay;
    private DateTimePicker dtpDenNgay;
    private Button btnXemBaoCao;
    private Button btnXuatCsv;

    private TableLayoutPanel tblKpi;
    private Label lblKpiLuotMuon;
    private Label lblKpiDangMuon;
    private Label lblKpiQuaHan;
    private Label lblKpiTienPhat;

    private TabControl tabBaoCao;
    private TabPage tabMuonTra;
    private TabPage tabTopSach;
    private TabPage tabTienPhat;

    private DataGridView dgvMuonTra;
    private DataGridView dgvTopSach;
    private DataGridView dgvTienPhat;
    
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }
    
    private void InitializeComponent()
    {
        this.pnlHeader = new Panel();
            this.lblTitle = new Label();
            this.lblTuNgay = new Label();
            this.dtpTuNgay = new DateTimePicker();
            this.lblDenNgay = new Label();
            this.dtpDenNgay = new DateTimePicker();
            this.btnXemBaoCao = new Button();
            this.btnXuatCsv = new Button();

            this.tblKpi = new TableLayoutPanel();
            this.tabBaoCao = new TabControl();
            this.tabMuonTra = new TabPage();
            this.tabTopSach = new TabPage();
            this.tabTienPhat = new TabPage();

            this.dgvMuonTra = new DataGridView();
            this.dgvTopSach = new DataGridView();
            this.dgvTienPhat = new DataGridView();

            this.pnlHeader.SuspendLayout();
            this.tblKpi.SuspendLayout();
            this.tabBaoCao.SuspendLayout();
            this.tabMuonTra.SuspendLayout();
            this.tabTopSach.SuspendLayout();
            this.tabTienPhat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMuonTra)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopSach)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTienPhat)).BeginInit();
            this.SuspendLayout();

            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = Color.FromArgb(245, 247, 250);
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Height = 95;
            this.pnlHeader.Controls.AddRange(new Control[] {
                this.lblTitle, this.lblTuNgay, this.dtpTuNgay,
                this.lblDenNgay, this.dtpDenNgay, this.btnXemBaoCao, this.btnXuatCsv
            });

            this.lblTitle.Text = "BÁO CÁO & THỐNG KÊ HOẠT ĐỘNG THƯ VIỆN";
            this.lblTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(30, 58, 138);
            this.lblTitle.Location = new Point(15, 12);
            this.lblTitle.AutoSize = true;

            this.lblTuNgay.Text = "Từ ngày:";
            this.lblTuNgay.Font = new Font("Segoe UI", 9.5F);
            this.lblTuNgay.Location = new Point(15, 52);
            this.lblTuNgay.AutoSize = true;

            this.dtpTuNgay.Format = DateTimePickerFormat.Short;
            this.dtpTuNgay.Font = new Font("Segoe UI", 9.5F);
            this.dtpTuNgay.Location = new Point(80, 49);
            this.dtpTuNgay.Size = new Size(130, 25);

            this.lblDenNgay.Text = "Đến ngày:";
            this.lblDenNgay.Font = new Font("Segoe UI", 9.5F);
            this.lblDenNgay.Location = new Point(230, 52);
            this.lblDenNgay.AutoSize = true;

            this.dtpDenNgay.Format = DateTimePickerFormat.Short;
            this.dtpDenNgay.Font = new Font("Segoe UI", 9.5F);
            this.dtpDenNgay.Location = new Point(305, 49);
            this.dtpDenNgay.Size = new Size(130, 25);

            this.btnXemBaoCao.Text = "Thống kê";
            this.btnXemBaoCao.BackColor = Color.FromArgb(37, 99, 235);
            this.btnXemBaoCao.ForeColor = Color.White;
            this.btnXemBaoCao.FlatStyle = FlatStyle.Flat;
            this.btnXemBaoCao.Location = new Point(455, 48);
            this.btnXemBaoCao.Size = new Size(105, 28);
            this.btnXemBaoCao.Click += new EventHandler(this.btnXemBaoCao_Click);

            this.btnXuatCsv.Text = "Xuất CSV (Excel)";
            this.btnXuatCsv.BackColor = Color.FromArgb(22, 163, 74);
            this.btnXuatCsv.ForeColor = Color.White;
            this.btnXuatCsv.FlatStyle = FlatStyle.Flat;
            this.btnXuatCsv.Location = new Point(570, 48);
            this.btnXuatCsv.Size = new Size(125, 28);
            this.btnXuatCsv.Click += new EventHandler(this.btnXuatCsv_Click);

            // 
            // tblKpi (4 thẻ chỉ số tổng quan)
            // 
            this.tblKpi.Dock = DockStyle.Top;
            this.tblKpi.Height = 95;
            this.tblKpi.ColumnCount = 4;
            this.tblKpi.RowCount = 1;
            this.tblKpi.Padding = new Padding(10, 8, 10, 8);
            for (int i = 0; i < 4; i++)
                this.tblKpi.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));

            this.tblKpi.Controls.Add(TaoTheKpi("LƯỢT MƯỢN TRONG KỲ", Color.FromArgb(37, 99, 235), out this.lblKpiLuotMuon), 0, 0);
            this.tblKpi.Controls.Add(TaoTheKpi("SÁCH ĐANG MƯỢN", Color.FromArgb(13, 148, 136), out this.lblKpiDangMuon), 1, 0);
            this.tblKpi.Controls.Add(TaoTheKpi("SÁCH QUÁ HẠN", Color.FromArgb(220, 38, 38), out this.lblKpiQuaHan), 2, 0);
            this.tblKpi.Controls.Add(TaoTheKpi("TỔNG THU TIỀN PHẠT", Color.FromArgb(217, 119, 6), out this.lblKpiTienPhat), 3, 0);

            // 
            // tabBaoCao
            // 
            this.tabBaoCao.Dock = DockStyle.Fill;
            this.tabBaoCao.Font = new Font("Segoe UI", 9.5F);
            this.tabBaoCao.Controls.AddRange(new Control[] { this.tabMuonTra, this.tabTopSach, this.tabTienPhat });

            this.tabMuonTra.Text = "1. Tình hình Mượn - Trả & Quá hạn";
            this.tabMuonTra.Controls.Add(this.dgvMuonTra);
            CauHinhLuoi(this.dgvMuonTra);

            this.tabTopSach.Text = "2. Top Đầu Sách Mượn Nhiều";
            this.tabTopSach.Controls.Add(this.dgvTopSach);
            CauHinhLuoi(this.dgvTopSach);

            this.tabTienPhat.Text = "3. Thống Kê Hóa Đơn Phạt";
            this.tabTienPhat.Controls.Add(this.dgvTienPhat);
            CauHinhLuoi(this.dgvTienPhat);

            // 
            // FrmBaoCao
            // 
            this.ClientSize = new Size(1060, 680);
            this.Controls.Add(this.tabBaoCao);
            this.Controls.Add(this.tblKpi);
            this.Controls.Add(this.pnlHeader);
            this.Name = "FrmBaoCao";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Báo cáo & Thống kê - Quản Lý Thư Viện";
            this.Load += new EventHandler(this.FrmBaoCao_Load);

            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.tblKpi.ResumeLayout(false);
            this.tabBaoCao.ResumeLayout(false);
            this.tabMuonTra.ResumeLayout(false);
            this.tabTopSach.ResumeLayout(false);
            this.tabTienPhat.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMuonTra)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopSach)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTienPhat)).EndInit();
            this.ResumeLayout(false);
    }
    
    private Panel TaoTheKpi(string tieuDe, Color mauNhan, out Label lblGiaTri)
    {
        var card = new Panel
        {
            Dock = DockStyle.Fill,
            Margin = new Padding(6),
            BackColor = Color.White,
            BorderStyle = BorderStyle.FixedSingle
        };

        var lblHeader = new Label
        {
            Text = tieuDe,
            Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
            ForeColor = Color.Gray,
            Location = new Point(12, 10),
            AutoSize = true
        };

        lblGiaTri = new Label
        {
            Text = "0",
            Font = new Font("Segoe UI", 15F, FontStyle.Bold),
            ForeColor = mauNhan,
            Location = new Point(12, 34),
            AutoSize = true
        };

        card.Controls.Add(lblHeader);
        card.Controls.Add(lblGiaTri);
        return card;
    }

    private void CauHinhLuoi(DataGridView dgv)
    {
        dgv.Dock = DockStyle.Fill;
        dgv.BackgroundColor = Color.White;
        dgv.AllowUserToAddRows = false;
        dgv.ReadOnly = true;
        dgv.RowHeadersVisible = false;
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
    }
    public class BaoCaoMuonTraDto
    {
        public int MaPhieu { get; set; }
        public string TenDocGia { get; set; }
        public string TenDauSach { get; set; }
        public string MaCuonSach { get; set; }
        public DateTime NgayMuon { get; set; }
        public DateTime HanTra { get; set; }
        public DateTime? NgayTraThucTe { get; set; }
        public string TrangThai { get; set; }
    }

    public class TopDauSachDto
    {
        public string MaDauSach { get; set; }
        public string TenSach { get; set; }
        public string TheLoai { get; set; }
        public int SoLuotMuon { get; set; }
        public int TongSoCuon { get; set; }
    }

    public class BaoCaoTienPhatDto
    {
        public int MaHoaDon { get; set; }
        public string TenDocGia { get; set; }
        public DateTime NgayLap { get; set; }
        public decimal SoTien { get; set; }
        public string LyDo { get; set; }
        public string TrangThai { get; set; }
    }
}