using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyThuVien.Forms;

partial class FrmBaoCao
{
    private IContainer? components = null;
    private ToolTip toolTip = null!;

    private Panel pnlHeader = null!;
    private Label lblTitle = null!;
    private FlowLayoutPanel flpBoLoc = null!;
    private Label lblTuNgay = null!;
    private DateTimePicker dtpTuNgay = null!;
    private Label lblDenNgay = null!;
    private DateTimePicker dtpDenNgay = null!;
    private System.Windows.Forms.Button btnXemBaoCao;
    private System.Windows.Forms.Button btnXuatCsv;
    private System.Windows.Forms.Button btnThangNay;
    private System.Windows.Forms.Button btn30Ngay;
    private System.Windows.Forms.Button btnNamNay;

    private System.Windows.Forms.TableLayoutPanel tblKpi;
    private Label lblKpiLuotMuon = null!;
    private Label lblKpiDangMuon = null!;
    private Label lblKpiQuaHan = null!;
    private Label lblKpiTienPhat = null!;

    private TabControl tabBaoCao = null!;
    private TabPage tabMuonTra = null!;
    private TabPage tabTopSach = null!;
    private TabPage tabTienPhat = null!;
    private TabPage tabQuaHan = null!;

    private TableLayoutPanel tblChiTietMuonTra = null!;
    private Panel pnlBieuDoMuon = null!;
    private DataGridView dgvMuonTra = null!;
    private DataGridView dgvTopSach = null!;
    private DataGridView dgvTienPhat = null!;
    private DataGridView dgvQuaHan = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        toolTip = new System.Windows.Forms.ToolTip(components);
        btnXuatCsv = new System.Windows.Forms.Button();
        pnlHeader = new System.Windows.Forms.Panel();
        flpBoLoc = new System.Windows.Forms.FlowLayoutPanel();
        lblTuNgay = new System.Windows.Forms.Label();
        dtpTuNgay = new System.Windows.Forms.DateTimePicker();
        lblDenNgay = new System.Windows.Forms.Label();
        dtpDenNgay = new System.Windows.Forms.DateTimePicker();
        btnXemBaoCao = new System.Windows.Forms.Button();
        btnThangNay = new System.Windows.Forms.Button();
        btn30Ngay = new System.Windows.Forms.Button();
        btnNamNay = new System.Windows.Forms.Button();
        lblTitle = new System.Windows.Forms.Label();
        tblKpi = new System.Windows.Forms.TableLayoutPanel();
        tabBaoCao = new System.Windows.Forms.TabControl();
        tabMuonTra = new System.Windows.Forms.TabPage();
        tblChiTietMuonTra = new System.Windows.Forms.TableLayoutPanel();
        dgvMuonTra = new System.Windows.Forms.DataGridView();
        pnlBieuDoMuon = new System.Windows.Forms.Panel();
        tabTopSach = new System.Windows.Forms.TabPage();
        dgvTopSach = new System.Windows.Forms.DataGridView();
        tabTienPhat = new System.Windows.Forms.TabPage();
        dgvTienPhat = new System.Windows.Forms.DataGridView();
        tabQuaHan = new System.Windows.Forms.TabPage();
        dgvQuaHan = new System.Windows.Forms.DataGridView();
        pnlHeader.SuspendLayout();
        flpBoLoc.SuspendLayout();
        tabBaoCao.SuspendLayout();
        tabMuonTra.SuspendLayout();
        tblChiTietMuonTra.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvMuonTra).BeginInit();
        tabTopSach.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvTopSach).BeginInit();
        tabTienPhat.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvTienPhat).BeginInit();
        tabQuaHan.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvQuaHan).BeginInit();
        SuspendLayout();
        // 
        // btnXuatCsv
        // 
        btnXuatCsv.Location = new System.Drawing.Point(538, 7);
        btnXuatCsv.Margin = new System.Windows.Forms.Padding(0, 1, 14, 4);
        btnXuatCsv.Name = "btnXuatCsv";
        btnXuatCsv.Size = new System.Drawing.Size(124, 32);
        btnXuatCsv.TabIndex = 5;
        btnXuatCsv.Text = "Xuất CSV";
        toolTip.SetToolTip(btnXuatCsv, "Xuất dữ liệu của tab đang mở ra file CSV");
        btnXuatCsv.Click += btnXuatCsv_Click;
        // 
        // pnlHeader
        // 
        pnlHeader.BackColor = System.Drawing.Color.White;
        pnlHeader.Controls.Add(flpBoLoc);
        pnlHeader.Controls.Add(lblTitle);
        pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
        pnlHeader.Location = new System.Drawing.Point(0, 0);
        pnlHeader.Name = "pnlHeader";
        pnlHeader.Padding = new System.Windows.Forms.Padding(14, 10, 14, 8);
        pnlHeader.Size = new System.Drawing.Size(1060, 96);
        pnlHeader.TabIndex = 2;
        // 
        // flpBoLoc
        // 
        flpBoLoc.AutoSize = true;
        flpBoLoc.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        flpBoLoc.BackColor = System.Drawing.Color.Transparent;
        flpBoLoc.Controls.Add(lblTuNgay);
        flpBoLoc.Controls.Add(dtpTuNgay);
        flpBoLoc.Controls.Add(lblDenNgay);
        flpBoLoc.Controls.Add(dtpDenNgay);
        flpBoLoc.Controls.Add(btnXemBaoCao);
        flpBoLoc.Controls.Add(btnXuatCsv);
        flpBoLoc.Controls.Add(btnThangNay);
        flpBoLoc.Controls.Add(btn30Ngay);
        flpBoLoc.Controls.Add(btnNamNay);
        flpBoLoc.Dock = System.Windows.Forms.DockStyle.Top;
        flpBoLoc.Location = new System.Drawing.Point(14, 42);
        flpBoLoc.Margin = new System.Windows.Forms.Padding(0);
        flpBoLoc.Name = "flpBoLoc";
        flpBoLoc.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
        flpBoLoc.Size = new System.Drawing.Size(1032, 43);
        flpBoLoc.TabIndex = 0;
        // 
        // lblTuNgay
        // 
        lblTuNgay.AutoSize = true;
        lblTuNgay.Location = new System.Drawing.Point(0, 13);
        lblTuNgay.Margin = new System.Windows.Forms.Padding(0, 7, 6, 4);
        lblTuNgay.Name = "lblTuNgay";
        lblTuNgay.Size = new System.Drawing.Size(65, 20);
        lblTuNgay.TabIndex = 0;
        lblTuNgay.Text = "Từ ngày:";
        // 
        // dtpTuNgay
        // 
        dtpTuNgay.CustomFormat = "dd/MM/yyyy";
        dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
        dtpTuNgay.Location = new System.Drawing.Point(71, 9);
        dtpTuNgay.Margin = new System.Windows.Forms.Padding(0, 3, 12, 4);
        dtpTuNgay.Name = "dtpTuNgay";
        dtpTuNgay.Size = new System.Drawing.Size(116, 27);
        dtpTuNgay.TabIndex = 1;
        // 
        // lblDenNgay
        // 
        lblDenNgay.AutoSize = true;
        lblDenNgay.Location = new System.Drawing.Point(199, 13);
        lblDenNgay.Margin = new System.Windows.Forms.Padding(0, 7, 6, 4);
        lblDenNgay.Name = "lblDenNgay";
        lblDenNgay.Size = new System.Drawing.Size(75, 20);
        lblDenNgay.TabIndex = 2;
        lblDenNgay.Text = "Đến ngày:";
        // 
        // dtpDenNgay
        // 
        dtpDenNgay.CustomFormat = "dd/MM/yyyy";
        dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
        dtpDenNgay.Location = new System.Drawing.Point(280, 9);
        dtpDenNgay.Margin = new System.Windows.Forms.Padding(0, 3, 12, 4);
        dtpDenNgay.Name = "dtpDenNgay";
        dtpDenNgay.Size = new System.Drawing.Size(116, 27);
        dtpDenNgay.TabIndex = 3;
        // 
        // btnXemBaoCao
        // 
        btnXemBaoCao.Location = new System.Drawing.Point(408, 7);
        btnXemBaoCao.Margin = new System.Windows.Forms.Padding(0, 1, 8, 4);
        btnXemBaoCao.Name = "btnXemBaoCao";
        btnXemBaoCao.Size = new System.Drawing.Size(122, 32);
        btnXemBaoCao.TabIndex = 4;
        btnXemBaoCao.Text = "Xem báo cáo";
        btnXemBaoCao.Click += btnXemBaoCao_Click;
        // 
        // btnThangNay
        // 
        btnThangNay.Location = new System.Drawing.Point(676, 8);
        btnThangNay.Margin = new System.Windows.Forms.Padding(0, 2, 6, 4);
        btnThangNay.Name = "btnThangNay";
        btnThangNay.Size = new System.Drawing.Size(92, 30);
        btnThangNay.TabIndex = 6;
        btnThangNay.Text = "Tháng này";
        btnThangNay.Click += btnThangNay_Click;
        // 
        // btn30Ngay
        // 
        btn30Ngay.Location = new System.Drawing.Point(774, 8);
        btn30Ngay.Margin = new System.Windows.Forms.Padding(0, 2, 6, 4);
        btn30Ngay.Name = "btn30Ngay";
        btn30Ngay.Size = new System.Drawing.Size(80, 30);
        btn30Ngay.TabIndex = 7;
        btn30Ngay.Text = "30 ngày";
        btn30Ngay.Click += btn30Ngay_Click;
        // 
        // btnNamNay
        // 
        btnNamNay.Location = new System.Drawing.Point(860, 8);
        btnNamNay.Margin = new System.Windows.Forms.Padding(0, 2, 0, 4);
        btnNamNay.Name = "btnNamNay";
        btnNamNay.Size = new System.Drawing.Size(84, 30);
        btnNamNay.TabIndex = 8;
        btnNamNay.Text = "Năm nay";
        btnNamNay.Click += btnNamNay_Click;
        // 
        // lblTitle
        // 
        lblTitle.AutoEllipsis = true;
        lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
        lblTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
        lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)19)), ((int)((byte)78)), ((int)((byte)74)));
        lblTitle.Location = new System.Drawing.Point(14, 10);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new System.Drawing.Size(1032, 32);
        lblTitle.TabIndex = 1;
        lblTitle.Text = "BÁO CÁO & THỐNG KÊ HOẠT ĐỘNG THƯ VIỆN";
        lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        lblTitle.UseMnemonic = false;
        // 
        // tblKpi
        // 
        tblKpi.ColumnCount = 4;
        tblKpi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
        tblKpi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
        tblKpi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
        tblKpi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
        tblKpi.Dock = System.Windows.Forms.DockStyle.Top;
        tblKpi.Location = new System.Drawing.Point(0, 96);
        tblKpi.Name = "tblKpi";
        tblKpi.Padding = new System.Windows.Forms.Padding(6, 4, 6, 4);
        tblKpi.RowCount = 1;
        tblKpi.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
        tblKpi.Size = new System.Drawing.Size(1060, 114);
        tblKpi.TabIndex = 1;
        // 
        // tabBaoCao
        // 
        tabBaoCao.Controls.Add(tabMuonTra);
        tabBaoCao.Controls.Add(tabTopSach);
        tabBaoCao.Controls.Add(tabTienPhat);
        tabBaoCao.Controls.Add(tabQuaHan);
        tabBaoCao.Dock = System.Windows.Forms.DockStyle.Fill;
        tabBaoCao.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        tabBaoCao.Location = new System.Drawing.Point(0, 210);
        tabBaoCao.Name = "tabBaoCao";
        tabBaoCao.SelectedIndex = 0;
        tabBaoCao.Size = new System.Drawing.Size(1060, 470);
        tabBaoCao.TabIndex = 0;
        // 
        // tabMuonTra
        // 
        tabMuonTra.Controls.Add(tblChiTietMuonTra);
        tabMuonTra.Location = new System.Drawing.Point(4, 30);
        tabMuonTra.Name = "tabMuonTra";
        tabMuonTra.Padding = new System.Windows.Forms.Padding(8, 10, 8, 8);
        tabMuonTra.Size = new System.Drawing.Size(1052, 436);
        tabMuonTra.TabIndex = 0;
        tabMuonTra.Text = "1. Tình hình Mượn - Trả & Quá hạn";
        // 
        // tblChiTietMuonTra
        // 
        tblChiTietMuonTra.ColumnCount = 2;
        tblChiTietMuonTra.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
        tblChiTietMuonTra.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
        tblChiTietMuonTra.Controls.Add(dgvMuonTra, 0, 0);
        tblChiTietMuonTra.Controls.Add(pnlBieuDoMuon, 1, 0);
        tblChiTietMuonTra.Dock = System.Windows.Forms.DockStyle.Fill;
        tblChiTietMuonTra.Location = new System.Drawing.Point(8, 10);
        tblChiTietMuonTra.Name = "tblChiTietMuonTra";
        tblChiTietMuonTra.RowCount = 1;
        tblChiTietMuonTra.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tblChiTietMuonTra.Size = new System.Drawing.Size(1036, 418);
        tblChiTietMuonTra.TabIndex = 0;
        // 
        // dgvMuonTra
        // 
        dgvMuonTra.ColumnHeadersHeight = 29;
        dgvMuonTra.Dock = System.Windows.Forms.DockStyle.Fill;
        dgvMuonTra.Location = new System.Drawing.Point(0, 0);
        dgvMuonTra.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
        dgvMuonTra.Name = "dgvMuonTra";
        dgvMuonTra.RowHeadersWidth = 51;
        dgvMuonTra.Size = new System.Drawing.Size(665, 418);
        dgvMuonTra.TabIndex = 0;
        // 
        // pnlBieuDoMuon
        // 
        pnlBieuDoMuon.BackColor = System.Drawing.Color.White;
        pnlBieuDoMuon.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlBieuDoMuon.Location = new System.Drawing.Point(673, 0);
        pnlBieuDoMuon.Margin = new System.Windows.Forms.Padding(0);
        pnlBieuDoMuon.Name = "pnlBieuDoMuon";
        pnlBieuDoMuon.Size = new System.Drawing.Size(363, 418);
        pnlBieuDoMuon.TabIndex = 1;
        pnlBieuDoMuon.Paint += pnlBieuDoMuon_Paint;
        // 
        // tabTopSach
        // 
        tabTopSach.Controls.Add(dgvTopSach);
        tabTopSach.Location = new System.Drawing.Point(4, 30);
        tabTopSach.Name = "tabTopSach";
        tabTopSach.Padding = new System.Windows.Forms.Padding(8, 10, 8, 8);
        tabTopSach.Size = new System.Drawing.Size(192, 66);
        tabTopSach.TabIndex = 1;
        tabTopSach.Text = "2. Top Đầu Sách Mượn Nhiều";
        // 
        // dgvTopSach
        // 
        dgvTopSach.ColumnHeadersHeight = 29;
        dgvTopSach.Dock = System.Windows.Forms.DockStyle.Fill;
        dgvTopSach.Location = new System.Drawing.Point(8, 10);
        dgvTopSach.Name = "dgvTopSach";
        dgvTopSach.RowHeadersWidth = 51;
        dgvTopSach.Size = new System.Drawing.Size(176, 48);
        dgvTopSach.TabIndex = 0;
        // 
        // tabTienPhat
        // 
        tabTienPhat.Controls.Add(dgvTienPhat);
        tabTienPhat.Location = new System.Drawing.Point(4, 30);
        tabTienPhat.Name = "tabTienPhat";
        tabTienPhat.Padding = new System.Windows.Forms.Padding(8, 10, 8, 8);
        tabTienPhat.Size = new System.Drawing.Size(192, 66);
        tabTienPhat.TabIndex = 2;
        tabTienPhat.Text = "3. Thống Kê Hóa Đơn Phạt";
        // 
        // dgvTienPhat
        // 
        dgvTienPhat.ColumnHeadersHeight = 29;
        dgvTienPhat.Dock = System.Windows.Forms.DockStyle.Fill;
        dgvTienPhat.Location = new System.Drawing.Point(8, 10);
        dgvTienPhat.Name = "dgvTienPhat";
        dgvTienPhat.RowHeadersWidth = 51;
        dgvTienPhat.Size = new System.Drawing.Size(176, 48);
        dgvTienPhat.TabIndex = 0;
        // 
        // tabQuaHan
        // 
        tabQuaHan.Controls.Add(dgvQuaHan);
        tabQuaHan.Location = new System.Drawing.Point(4, 30);
        tabQuaHan.Name = "tabQuaHan";
        tabQuaHan.Padding = new System.Windows.Forms.Padding(8, 10, 8, 8);
        tabQuaHan.Size = new System.Drawing.Size(192, 66);
        tabQuaHan.TabIndex = 3;
        tabQuaHan.Text = "4. Độc giả quá hạn chưa trả";
        // 
        // dgvQuaHan
        // 
        dgvQuaHan.ColumnHeadersHeight = 29;
        dgvQuaHan.Dock = System.Windows.Forms.DockStyle.Fill;
        dgvQuaHan.Location = new System.Drawing.Point(8, 10);
        dgvQuaHan.Name = "dgvQuaHan";
        dgvQuaHan.RowHeadersWidth = 51;
        dgvQuaHan.Size = new System.Drawing.Size(176, 48);
        dgvQuaHan.TabIndex = 0;
        // 
        // FrmBaoCao
        // 
        ClientSize = new System.Drawing.Size(1060, 680);
        Controls.Add(tabBaoCao);
        Controls.Add(tblKpi);
        Controls.Add(pnlHeader);
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "Báo cáo & Thống kê - Quản Lý Thư Viện";
        Load += FrmBaoCao_Load;
        pnlHeader.ResumeLayout(false);
        pnlHeader.PerformLayout();
        flpBoLoc.ResumeLayout(false);
        flpBoLoc.PerformLayout();
        tabBaoCao.ResumeLayout(false);
        tabMuonTra.ResumeLayout(false);
        tblChiTietMuonTra.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvMuonTra).EndInit();
        tabTopSach.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvTopSach).EndInit();
        tabTienPhat.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvTienPhat).EndInit();
        tabQuaHan.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvQuaHan).EndInit();
        ResumeLayout(false);
    }
}