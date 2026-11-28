using System.ComponentModel;
using QuanLyThuVien.UI;

namespace QuanLyThuVien.Forms;

partial class FrmTraSach
{
    private System.ComponentModel.IContainer components = null;

    private Panel pnlTopFilter;
    private Label lblTitle;
    private Label lblTimKiem;
    private TextBox txtTimKiem;
    private Button btnTimKiem;
    private ComboBox cboPhieuMuon;
    private Label lblChonPhieu;

    private SplitContainer splitMain;
    private GroupBox grpChiTietMuon;
    private DataGridView dgvChiTietMuon;
    private GroupBox grpThongTinPhat;

    private Label lblDonGiaTre;
    private NumericUpDown numDonGiaTre;
    private Label lblTienPhatHuHong;
    private NumericUpDown numTienPhatHuHong;
    private Label lblLyDoPhat;
    private TextBox txtLyDoPhat;
    private Label lblTongTienPhatTitle;
    private Label lblTongTienPhat;
    private CheckBox chkDaThanhToanNgay;
    private Button btnXacNhanTra;

    private GroupBox grpHoaDonPhat;
    private DataGridView dgvHoaDonPhat;
    private Button btnThanhToanHoaDon;
    
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
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
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
        pnlTopFilter = new System.Windows.Forms.Panel();
        lblTitle = new System.Windows.Forms.Label();
        lblTimKiem = new System.Windows.Forms.Label();
        txtTimKiem = new System.Windows.Forms.TextBox();
        btnTimKiem = new System.Windows.Forms.Button();
        lblChonPhieu = new System.Windows.Forms.Label();
        cboPhieuMuon = new System.Windows.Forms.ComboBox();
        splitMain = new System.Windows.Forms.SplitContainer();
        grpChiTietMuon = new System.Windows.Forms.GroupBox();
        dgvChiTietMuon = new System.Windows.Forms.DataGridView();
        grpThongTinPhat = new System.Windows.Forms.GroupBox();
        lblDonGiaTre = new System.Windows.Forms.Label();
        numDonGiaTre = new System.Windows.Forms.NumericUpDown();
        lblTienPhatHuHong = new System.Windows.Forms.Label();
        numTienPhatHuHong = new System.Windows.Forms.NumericUpDown();
        lblLyDoPhat = new System.Windows.Forms.Label();
        txtLyDoPhat = new System.Windows.Forms.TextBox();
        lblTongTienPhatTitle = new System.Windows.Forms.Label();
        lblTongTienPhat = new System.Windows.Forms.Label();
        chkDaThanhToanNgay = new System.Windows.Forms.CheckBox();
        btnXacNhanTra = new System.Windows.Forms.Button();
        grpHoaDonPhat = new System.Windows.Forms.GroupBox();
        dgvHoaDonPhat = new System.Windows.Forms.DataGridView();
        btnThanhToanHoaDon = new System.Windows.Forms.Button();
        pnlTopFilter.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
        splitMain.Panel1.SuspendLayout();
        splitMain.Panel2.SuspendLayout();
        splitMain.SuspendLayout();
        grpChiTietMuon.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvChiTietMuon).BeginInit();
        grpThongTinPhat.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)numDonGiaTre).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numTienPhatHuHong).BeginInit();
        grpHoaDonPhat.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvHoaDonPhat).BeginInit();
        SuspendLayout();
        // 
        // pnlTopFilter
        // 
        pnlTopFilter.BackColor = System.Drawing.Color.White;
        pnlTopFilter.Controls.Add(lblTitle);
        pnlTopFilter.Controls.Add(lblTimKiem);
        pnlTopFilter.Controls.Add(txtTimKiem);
        pnlTopFilter.Controls.Add(btnTimKiem);
        pnlTopFilter.Controls.Add(lblChonPhieu);
        pnlTopFilter.Controls.Add(cboPhieuMuon);
        pnlTopFilter.Dock = System.Windows.Forms.DockStyle.Top;
        pnlTopFilter.Location = new System.Drawing.Point(0, 0);
        pnlTopFilter.Name = "pnlTopFilter";
        pnlTopFilter.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
        pnlTopFilter.Size = new System.Drawing.Size(1050, 95);
        pnlTopFilter.TabIndex = 1;
        // 
        // lblTitle
        // 
        lblTitle.AutoSize = true;
        lblTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
        lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)19)), ((int)((byte)78)), ((int)((byte)74)));
        lblTitle.Location = new System.Drawing.Point(15, 10);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new System.Drawing.Size(359, 25);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "QUẢN LÝ TRẢ SÁCH && XỬ LÝ TIỀN PHẠT";
        // 
        // lblTimKiem
        // 
        lblTimKiem.AutoSize = true;
        lblTimKiem.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        lblTimKiem.Location = new System.Drawing.Point(15, 52);
        lblTimKiem.Name = "lblTimKiem";
        lblTimKiem.Size = new System.Drawing.Size(124, 17);
        lblTimKiem.TabIndex = 1;
        lblTimKiem.Text = "Độc giả / Mã phiếu:";
        // 
        // txtTimKiem
        // 
        txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        txtTimKiem.Location = new System.Drawing.Point(155, 55);
        txtTimKiem.Name = "txtTimKiem";
        txtTimKiem.Size = new System.Drawing.Size(170, 24);
        txtTimKiem.TabIndex = 2;
        // 
        // btnTimKiem
        // 
        btnTimKiem.BackColor = System.Drawing.Color.FromArgb(((int)((byte)37)), ((int)((byte)99)), ((int)((byte)235)));
        btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btnTimKiem.ForeColor = System.Drawing.Color.White;
        btnTimKiem.Location = new System.Drawing.Point(335, 54);
        btnTimKiem.Name = "btnTimKiem";
        btnTimKiem.Size = new System.Drawing.Size(95, 27);
        btnTimKiem.TabIndex = 3;
        btnTimKiem.Text = "Tìm kiếm";
        btnTimKiem.UseVisualStyleBackColor = false;
        btnTimKiem.Click += btnTimKiem_Click;
        // 
        // lblChonPhieu
        // 
        lblChonPhieu.AutoSize = true;
        lblChonPhieu.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        lblChonPhieu.Location = new System.Drawing.Point(440, 58);
        lblChonPhieu.Name = "lblChonPhieu";
        lblChonPhieu.Size = new System.Drawing.Size(114, 17);
        lblChonPhieu.TabIndex = 4;
        lblChonPhieu.Text = "Phiếu đang mượn:";
        // 
        // cboPhieuMuon
        // 
        cboPhieuMuon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        cboPhieuMuon.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        cboPhieuMuon.Location = new System.Drawing.Point(565, 55);
        cboPhieuMuon.Name = "cboPhieuMuon";
        cboPhieuMuon.Size = new System.Drawing.Size(340, 25);
        cboPhieuMuon.TabIndex = 5;
        cboPhieuMuon.SelectedIndexChanged += cboPhieuMuon_SelectedIndexChanged;
        // 
        // splitMain
        // 
        splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
        splitMain.Location = new System.Drawing.Point(0, 95);
        splitMain.Name = "splitMain";
        splitMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
        // 
        // splitMain.Panel1
        // 
        splitMain.Panel1.Controls.Add(grpChiTietMuon);
        splitMain.Panel1.Controls.Add(grpThongTinPhat);
        splitMain.Panel1.Padding = new System.Windows.Forms.Padding(15, 10, 15, 5);
        // 
        // splitMain.Panel2
        // 
        splitMain.Panel2.Controls.Add(grpHoaDonPhat);
        splitMain.Panel2.Padding = new System.Windows.Forms.Padding(15, 5, 15, 15);
        splitMain.Size = new System.Drawing.Size(1050, 585);
        splitMain.SplitterDistance = 415;
        splitMain.TabIndex = 0;
        // 
        // grpChiTietMuon
        // 
        grpChiTietMuon.Controls.Add(dgvChiTietMuon);
        grpChiTietMuon.Dock = System.Windows.Forms.DockStyle.Fill;
        grpChiTietMuon.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
        grpChiTietMuon.Location = new System.Drawing.Point(15, 10);
        grpChiTietMuon.Name = "grpChiTietMuon";
        grpChiTietMuon.Size = new System.Drawing.Size(680, 400);
        grpChiTietMuon.TabIndex = 0;
        grpChiTietMuon.TabStop = false;
        grpChiTietMuon.Text = "Danh sách cuốn sách trong phiếu mượn (Tích chọn sách trả)";
        // 
        // dgvChiTietMuon
        // 
        dgvChiTietMuon.AllowUserToAddRows = false;
        dgvChiTietMuon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        dgvChiTietMuon.BackgroundColor = System.Drawing.Color.White;
        dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
        dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F);
        dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
        dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        dgvChiTietMuon.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
        dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
        dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
        dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
        dgvChiTietMuon.DefaultCellStyle = dataGridViewCellStyle2;
        dgvChiTietMuon.Dock = System.Windows.Forms.DockStyle.Fill;
        dgvChiTietMuon.Font = new System.Drawing.Font("Segoe UI", 9F);
        dgvChiTietMuon.Location = new System.Drawing.Point(3, 20);
        dgvChiTietMuon.Name = "dgvChiTietMuon";
        dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
        dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
        dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
        dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        dgvChiTietMuon.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
        dgvChiTietMuon.RowHeadersVisible = false;
        dgvChiTietMuon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        dgvChiTietMuon.Size = new System.Drawing.Size(674, 377);
        dgvChiTietMuon.TabIndex = 0;
        // 
        // grpThongTinPhat
        // 
        grpThongTinPhat.Controls.Add(lblDonGiaTre);
        grpThongTinPhat.Controls.Add(numDonGiaTre);
        grpThongTinPhat.Controls.Add(lblTienPhatHuHong);
        grpThongTinPhat.Controls.Add(numTienPhatHuHong);
        grpThongTinPhat.Controls.Add(lblLyDoPhat);
        grpThongTinPhat.Controls.Add(txtLyDoPhat);
        grpThongTinPhat.Controls.Add(lblTongTienPhatTitle);
        grpThongTinPhat.Controls.Add(lblTongTienPhat);
        grpThongTinPhat.Controls.Add(chkDaThanhToanNgay);
        grpThongTinPhat.Controls.Add(btnXacNhanTra);
        grpThongTinPhat.Dock = System.Windows.Forms.DockStyle.Right;
        grpThongTinPhat.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
        grpThongTinPhat.Location = new System.Drawing.Point(695, 10);
        grpThongTinPhat.Name = "grpThongTinPhat";
        grpThongTinPhat.Size = new System.Drawing.Size(340, 400);
        grpThongTinPhat.TabIndex = 1;
        grpThongTinPhat.TabStop = false;
        grpThongTinPhat.Text = "Tính phí phạt && Xác nhận trả";
        // 
        // lblDonGiaTre
        // 
        lblDonGiaTre.AutoSize = true;
        lblDonGiaTre.Font = new System.Drawing.Font("Segoe UI", 9F);
        lblDonGiaTre.Location = new System.Drawing.Point(15, 42);
        lblDonGiaTre.Name = "lblDonGiaTre";
        lblDonGiaTre.Size = new System.Drawing.Size(140, 15);
        lblDonGiaTre.TabIndex = 0;
        lblDonGiaTre.Text = "Phạt trễ hạn (VNĐ/ngày):";
        // 
        // numDonGiaTre
        // 
        numDonGiaTre.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        numDonGiaTre.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
        numDonGiaTre.Location = new System.Drawing.Point(200, 42);
        numDonGiaTre.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
        numDonGiaTre.Name = "numDonGiaTre";
        numDonGiaTre.Size = new System.Drawing.Size(105, 24);
        numDonGiaTre.TabIndex = 1;
        numDonGiaTre.ThousandsSeparator = true;
        numDonGiaTre.Value = new decimal(new int[] { 5000, 0, 0, 0 });
        // 
        // lblTienPhatHuHong
        // 
        lblTienPhatHuHong.AutoSize = true;
        lblTienPhatHuHong.Font = new System.Drawing.Font("Segoe UI", 9F);
        lblTienPhatHuHong.Location = new System.Drawing.Point(15, 78);
        lblTienPhatHuHong.Name = "lblTienPhatHuHong";
        lblTienPhatHuHong.Size = new System.Drawing.Size(149, 15);
        lblTienPhatHuHong.TabIndex = 2;
        lblTienPhatHuHong.Text = "Phạt hư hỏng / mất (VNĐ):";
        // 
        // numTienPhatHuHong
        // 
        numTienPhatHuHong.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        numTienPhatHuHong.Increment = new decimal(new int[] { 5000, 0, 0, 0 });
        numTienPhatHuHong.Location = new System.Drawing.Point(200, 78);
        numTienPhatHuHong.Maximum = new decimal(new int[] { 50000000, 0, 0, 0 });
        numTienPhatHuHong.Name = "numTienPhatHuHong";
        numTienPhatHuHong.Size = new System.Drawing.Size(105, 24);
        numTienPhatHuHong.TabIndex = 3;
        numTienPhatHuHong.ThousandsSeparator = true;
        // 
        // lblLyDoPhat
        // 
        lblLyDoPhat.AutoSize = true;
        lblLyDoPhat.Font = new System.Drawing.Font("Segoe UI", 9F);
        lblLyDoPhat.Location = new System.Drawing.Point(15, 102);
        lblLyDoPhat.Name = "lblLyDoPhat";
        lblLyDoPhat.Size = new System.Drawing.Size(117, 15);
        lblLyDoPhat.TabIndex = 4;
        lblLyDoPhat.Text = "Ghi chú / Lý do phạt:";
        // 
        // txtLyDoPhat
        // 
        txtLyDoPhat.Font = new System.Drawing.Font("Segoe UI", 9F);
        txtLyDoPhat.Location = new System.Drawing.Point(15, 124);
        txtLyDoPhat.Multiline = true;
        txtLyDoPhat.Name = "txtLyDoPhat";
        txtLyDoPhat.Size = new System.Drawing.Size(305, 52);
        txtLyDoPhat.TabIndex = 5;
        // 
        // lblTongTienPhatTitle
        // 
        lblTongTienPhatTitle.AutoSize = true;
        lblTongTienPhatTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        lblTongTienPhatTitle.Location = new System.Drawing.Point(15, 190);
        lblTongTienPhatTitle.Name = "lblTongTienPhatTitle";
        lblTongTienPhatTitle.Size = new System.Drawing.Size(129, 19);
        lblTongTienPhatTitle.TabIndex = 6;
        lblTongTienPhatTitle.Text = "TỔNG TIỀN PHẠT:";
        // 
        // lblTongTienPhat
        // 
        lblTongTienPhat.AutoSize = true;
        lblTongTienPhat.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
        lblTongTienPhat.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)220)), ((int)((byte)38)), ((int)((byte)38)));
        lblTongTienPhat.Location = new System.Drawing.Point(155, 186);
        lblTongTienPhat.Name = "lblTongTienPhat";
        lblTongTienPhat.Size = new System.Drawing.Size(59, 21);
        lblTongTienPhat.TabIndex = 7;
        lblTongTienPhat.Text = "0 VNĐ";
        // 
        // chkDaThanhToanNgay
        // 
        chkDaThanhToanNgay.AutoSize = true;
        chkDaThanhToanNgay.Checked = true;
        chkDaThanhToanNgay.CheckState = System.Windows.Forms.CheckState.Checked;
        chkDaThanhToanNgay.Font = new System.Drawing.Font("Segoe UI", 9F);
        chkDaThanhToanNgay.Location = new System.Drawing.Point(15, 222);
        chkDaThanhToanNgay.Name = "chkDaThanhToanNgay";
        chkDaThanhToanNgay.Size = new System.Drawing.Size(183, 19);
        chkDaThanhToanNgay.TabIndex = 8;
        chkDaThanhToanNgay.Text = "Độc giả thanh toán phạt ngay";
        // 
        // btnXacNhanTra
        // 
        btnXacNhanTra.BackColor = System.Drawing.Color.FromArgb(((int)((byte)22)), ((int)((byte)163)), ((int)((byte)74)));
        btnXacNhanTra.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btnXacNhanTra.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        btnXacNhanTra.ForeColor = System.Drawing.Color.White;
        btnXacNhanTra.Location = new System.Drawing.Point(15, 255);
        btnXacNhanTra.Name = "btnXacNhanTra";
        btnXacNhanTra.Size = new System.Drawing.Size(305, 42);
        btnXacNhanTra.TabIndex = 9;
        btnXacNhanTra.Text = "XÁC NHẬN TRẢ SÁCH";
        btnXacNhanTra.UseVisualStyleBackColor = false;
        btnXacNhanTra.Click += btnXacNhanTra_Click;
        // 
        // grpHoaDonPhat
        // 
        grpHoaDonPhat.Controls.Add(dgvHoaDonPhat);
        grpHoaDonPhat.Controls.Add(btnThanhToanHoaDon);
        grpHoaDonPhat.Dock = System.Windows.Forms.DockStyle.Fill;
        grpHoaDonPhat.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
        grpHoaDonPhat.Location = new System.Drawing.Point(15, 5);
        grpHoaDonPhat.Name = "grpHoaDonPhat";
        grpHoaDonPhat.Size = new System.Drawing.Size(1020, 146);
        grpHoaDonPhat.TabIndex = 0;
        grpHoaDonPhat.TabStop = false;
        grpHoaDonPhat.Text = "Danh sách Hóa đơn phạt của độc giả";
        // 
        // dgvHoaDonPhat
        // 
        dgvHoaDonPhat.AllowUserToAddRows = false;
        dgvHoaDonPhat.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        dgvHoaDonPhat.BackgroundColor = System.Drawing.Color.White;
        dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
        dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F);
        dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
        dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        dgvHoaDonPhat.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
        dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
        dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9F);
        dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
        dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
        dgvHoaDonPhat.DefaultCellStyle = dataGridViewCellStyle5;
        dgvHoaDonPhat.Dock = System.Windows.Forms.DockStyle.Fill;
        dgvHoaDonPhat.Font = new System.Drawing.Font("Segoe UI", 9F);
        dgvHoaDonPhat.Location = new System.Drawing.Point(3, 20);
        dgvHoaDonPhat.Name = "dgvHoaDonPhat";
        dgvHoaDonPhat.ReadOnly = true;
        dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
        dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F);
        dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
        dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        dgvHoaDonPhat.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
        dgvHoaDonPhat.RowHeadersVisible = false;
        dgvHoaDonPhat.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        dgvHoaDonPhat.Size = new System.Drawing.Size(1014, 88);
        dgvHoaDonPhat.TabIndex = 0;
        // 
        // btnThanhToanHoaDon
        // 
        btnThanhToanHoaDon.BackColor = System.Drawing.Color.FromArgb(((int)((byte)234)), ((int)((byte)88)), ((int)((byte)12)));
        btnThanhToanHoaDon.Dock = System.Windows.Forms.DockStyle.Bottom;
        btnThanhToanHoaDon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btnThanhToanHoaDon.ForeColor = System.Drawing.Color.White;
        btnThanhToanHoaDon.Location = new System.Drawing.Point(3, 108);
        btnThanhToanHoaDon.Name = "btnThanhToanHoaDon";
        btnThanhToanHoaDon.Size = new System.Drawing.Size(1014, 35);
        btnThanhToanHoaDon.TabIndex = 1;
        btnThanhToanHoaDon.Text = "Xác nhận đã thu tiền hóa đơn đang chọn";
        btnThanhToanHoaDon.UseVisualStyleBackColor = false;
        btnThanhToanHoaDon.Click += btnThanhToanHoaDon_Click;
        // 
        // FrmTraSach
        // 
        ClientSize = new System.Drawing.Size(1050, 680);
        Controls.Add(splitMain);
        Controls.Add(pnlTopFilter);
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "Trả sách & Xử lý phạt - Quản Lý Thư Viện";
        Load += FrmTraSach_Load;
        pnlTopFilter.ResumeLayout(false);
        pnlTopFilter.PerformLayout();
        splitMain.Panel1.ResumeLayout(false);
        splitMain.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
        splitMain.ResumeLayout(false);
        grpChiTietMuon.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvChiTietMuon).EndInit();
        grpThongTinPhat.ResumeLayout(false);
        grpThongTinPhat.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)numDonGiaTre).EndInit();
        ((System.ComponentModel.ISupportInitialize)numTienPhatHuHong).EndInit();
        grpHoaDonPhat.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvHoaDonPhat).EndInit();
        ResumeLayout(false);
    }
    }


