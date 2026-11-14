namespace QuanLyThuVien.Forms
{
    partial class FrmQuanLySach
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            splMain = new SplitContainer();
            tvTheLoai = new TreeView();
            pnlNutTheLoai = new Panel();
            btnXoaTheLoai = new Button();
            btnSuaTheLoai = new Button();
            btnThemTheLoai = new Button();
            splPhai = new SplitContainer();
            dgvDauSach = new DataGridView();
            grpDauSach = new GroupBox();
            btnLamMoi = new Button();
            btnXoa = new Button();
            btnSua = new Button();
            btnThem = new Button();
            cboTheLoai = new ComboBox();
            nudGiaBia = new NumericUpDown();
            nudNamXuatBan = new NumericUpDown();
            txtTacGia = new TextBox();
            txtTenDauSach = new TextBox();
            txtMaDauSach = new TextBox();
            lblTheLoai = new Label();
            lblGiaBia = new Label();
            lblNamXuatBan = new Label();
            lblTacGia = new Label();
            lblTenDauSach = new Label();
            lblMaDauSach = new Label();
            dgvCuonSach = new DataGridView();
            pnlNutCuonSach = new Panel();
            btnDoiTrangThai = new Button();
            btnNhapThemBanSao = new Button();
            nudSoLuong = new NumericUpDown();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)splMain).BeginInit();
            splMain.Panel1.SuspendLayout();
            splMain.Panel2.SuspendLayout();
            splMain.SuspendLayout();
            pnlNutTheLoai.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splPhai).BeginInit();
            splPhai.Panel1.SuspendLayout();
            splPhai.Panel2.SuspendLayout();
            splPhai.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDauSach).BeginInit();
            grpDauSach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudGiaBia).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudNamXuatBan).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvCuonSach).BeginInit();
            pnlNutCuonSach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudSoLuong).BeginInit();
            SuspendLayout();
            // 
            // splMain
            // 
            splMain.Dock = DockStyle.Fill;
            splMain.Location = new Point(0, 0);
            splMain.Name = "splMain";
            // 
            // splMain.Panel1
            // 
            splMain.Panel1.Controls.Add(tvTheLoai);
            splMain.Panel1.Controls.Add(pnlNutTheLoai);
            // 
            // splMain.Panel2
            // 
            splMain.Panel2.Controls.Add(splPhai);
            splMain.Size = new Size(1082, 603);
            splMain.SplitterDistance = 324;
            splMain.TabIndex = 0;
            // 
            // tvTheLoai
            // 
            tvTheLoai.Dock = DockStyle.Fill;
            tvTheLoai.Location = new Point(0, 0);
            tvTheLoai.Name = "tvTheLoai";
            tvTheLoai.Size = new Size(324, 558);
            tvTheLoai.TabIndex = 1;
            tvTheLoai.AfterSelect += tvTheLoai_AfterSelect;
            // 
            // pnlNutTheLoai
            // 
            pnlNutTheLoai.Controls.Add(btnXoaTheLoai);
            pnlNutTheLoai.Controls.Add(btnSuaTheLoai);
            pnlNutTheLoai.Controls.Add(btnThemTheLoai);
            pnlNutTheLoai.Dock = DockStyle.Bottom;
            pnlNutTheLoai.Location = new Point(0, 558);
            pnlNutTheLoai.Name = "pnlNutTheLoai";
            pnlNutTheLoai.Size = new Size(324, 45);
            pnlNutTheLoai.TabIndex = 0;
            // 
            // btnXoaTheLoai
            // 
            btnXoaTheLoai.Location = new Point(242, 8);
            btnXoaTheLoai.Name = "btnXoaTheLoai";
            btnXoaTheLoai.Size = new Size(70, 29);
            btnXoaTheLoai.TabIndex = 2;
            btnXoaTheLoai.Text = "Xóa";
            btnXoaTheLoai.UseVisualStyleBackColor = true;
            btnXoaTheLoai.Click += btnXoaTheLoai_Click;
            // 
            // btnSuaTheLoai
            // 
            btnSuaTheLoai.Location = new Point(127, 8);
            btnSuaTheLoai.Name = "btnSuaTheLoai";
            btnSuaTheLoai.Size = new Size(70, 29);
            btnSuaTheLoai.TabIndex = 1;
            btnSuaTheLoai.Text = "Sửa";
            btnSuaTheLoai.UseVisualStyleBackColor = true;
            btnSuaTheLoai.Click += btnSuaTheLoai_Click;
            // 
            // btnThemTheLoai
            // 
            btnThemTheLoai.Location = new Point(12, 8);
            btnThemTheLoai.Name = "btnThemTheLoai";
            btnThemTheLoai.Size = new Size(70, 29);
            btnThemTheLoai.TabIndex = 0;
            btnThemTheLoai.Text = "Thêm";
            btnThemTheLoai.UseVisualStyleBackColor = true;
            btnThemTheLoai.Click += btnThemTheLoai_Click;
            // 
            // splPhai
            // 
            splPhai.Dock = DockStyle.Fill;
            splPhai.Location = new Point(0, 0);
            splPhai.Name = "splPhai";
            splPhai.Orientation = Orientation.Horizontal;
            // 
            // splPhai.Panel1
            // 
            splPhai.Panel1.Controls.Add(dgvDauSach);
            splPhai.Panel1.Controls.Add(grpDauSach);
            // 
            // splPhai.Panel2
            // 
            splPhai.Panel2.Controls.Add(dgvCuonSach);
            splPhai.Panel2.Controls.Add(pnlNutCuonSach);
            splPhai.Size = new Size(754, 603);
            splPhai.SplitterDistance = 360;
            splPhai.TabIndex = 0;
            // 
            // dgvDauSach
            // 
            dgvDauSach.AllowUserToAddRows = false;
            dgvDauSach.AllowUserToDeleteRows = false;
            dgvDauSach.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDauSach.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDauSach.Dock = DockStyle.Fill;
            dgvDauSach.Location = new Point(0, 175);
            dgvDauSach.MultiSelect = false;
            dgvDauSach.Name = "dgvDauSach";
            dgvDauSach.ReadOnly = true;
            dgvDauSach.RowHeadersWidth = 51;
            dgvDauSach.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDauSach.Size = new Size(754, 185);
            dgvDauSach.TabIndex = 1;
            // 
            // grpDauSach
            // 
            grpDauSach.Controls.Add(btnLamMoi);
            grpDauSach.Controls.Add(btnXoa);
            grpDauSach.Controls.Add(btnSua);
            grpDauSach.Controls.Add(btnThem);
            grpDauSach.Controls.Add(cboTheLoai);
            grpDauSach.Controls.Add(nudGiaBia);
            grpDauSach.Controls.Add(nudNamXuatBan);
            grpDauSach.Controls.Add(txtTacGia);
            grpDauSach.Controls.Add(txtTenDauSach);
            grpDauSach.Controls.Add(txtMaDauSach);
            grpDauSach.Controls.Add(lblTheLoai);
            grpDauSach.Controls.Add(lblGiaBia);
            grpDauSach.Controls.Add(lblNamXuatBan);
            grpDauSach.Controls.Add(lblTacGia);
            grpDauSach.Controls.Add(lblTenDauSach);
            grpDauSach.Controls.Add(lblMaDauSach);
            grpDauSach.Dock = DockStyle.Top;
            grpDauSach.Location = new Point(0, 0);
            grpDauSach.Name = "grpDauSach";
            grpDauSach.Size = new Size(754, 175);
            grpDauSach.TabIndex = 0;
            grpDauSach.TabStop = false;
            grpDauSach.Text = "Thông tin đầu sách";
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(529, 136);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(94, 29);
            btnLamMoi.TabIndex = 15;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(353, 136);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(94, 29);
            btnXoa.TabIndex = 14;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(202, 136);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(94, 29);
            btnSua.TabIndex = 13;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(55, 136);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(94, 29);
            btnThem.TabIndex = 12;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            // 
            // cboTheLoai
            // 
            cboTheLoai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTheLoai.FormattingEnabled = true;
            cboTheLoai.Location = new Point(430, 90);
            cboTheLoai.Name = "cboTheLoai";
            cboTheLoai.Size = new Size(150, 28);
            cboTheLoai.TabIndex = 11;
            // 
            // nudGiaBia
            // 
            nudGiaBia.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            nudGiaBia.Location = new Point(430, 57);
            nudGiaBia.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            nudGiaBia.Name = "nudGiaBia";
            nudGiaBia.Size = new Size(150, 27);
            nudGiaBia.TabIndex = 9;
            nudGiaBia.ThousandsSeparator = true;
            // 
            // nudNamXuatBan
            // 
            nudNamXuatBan.Location = new Point(430, 24);
            nudNamXuatBan.Maximum = new decimal(new int[] { 2100, 0, 0, 0 });
            nudNamXuatBan.Minimum = new decimal(new int[] { 1900, 0, 0, 0 });
            nudNamXuatBan.Name = "nudNamXuatBan";
            nudNamXuatBan.Size = new Size(150, 27);
            nudNamXuatBan.TabIndex = 7;
            nudNamXuatBan.Value = new decimal(new int[] { 2024, 0, 0, 0 });
            // 
            // txtTacGia
            // 
            txtTacGia.Location = new Point(114, 90);
            txtTacGia.Name = "txtTacGia";
            txtTacGia.Size = new Size(182, 27);
            txtTacGia.TabIndex = 5;
            // 
            // txtTenDauSach
            // 
            txtTenDauSach.Location = new Point(114, 57);
            txtTenDauSach.Name = "txtTenDauSach";
            txtTenDauSach.Size = new Size(182, 27);
            txtTenDauSach.TabIndex = 3;
            // 
            // txtMaDauSach
            // 
            txtMaDauSach.Location = new Point(114, 24);
            txtMaDauSach.MaxLength = 10;
            txtMaDauSach.Name = "txtMaDauSach";
            txtMaDauSach.Size = new Size(182, 27);
            txtMaDauSach.TabIndex = 1;
            // 
            // lblTheLoai
            // 
            lblTheLoai.AutoSize = true;
            lblTheLoai.Location = new Point(320, 93);
            lblTheLoai.Name = "lblTheLoai";
            lblTheLoai.Size = new Size(62, 20);
            lblTheLoai.TabIndex = 10;
            lblTheLoai.Text = "Thể loại";
            // 
            // lblGiaBia
            // 
            lblGiaBia.AutoSize = true;
            lblGiaBia.Location = new Point(320, 60);
            lblGiaBia.Name = "lblGiaBia";
            lblGiaBia.Size = new Size(56, 20);
            lblGiaBia.TabIndex = 8;
            lblGiaBia.Text = "Giá bìa";
            // 
            // lblNamXuatBan
            // 
            lblNamXuatBan.AutoSize = true;
            lblNamXuatBan.Location = new Point(320, 27);
            lblNamXuatBan.Name = "lblNamXuatBan";
            lblNamXuatBan.Size = new Size(102, 20);
            lblNamXuatBan.TabIndex = 6;
            lblNamXuatBan.Text = "Năm xuất bản";
            // 
            // lblTacGia
            // 
            lblTacGia.AutoSize = true;
            lblTacGia.Location = new Point(14, 93);
            lblTacGia.Name = "lblTacGia";
            lblTacGia.Size = new Size(55, 20);
            lblTacGia.TabIndex = 4;
            lblTacGia.Text = "Tác giả";
            // 
            // lblTenDauSach
            // 
            lblTenDauSach.AutoSize = true;
            lblTenDauSach.Location = new Point(14, 60);
            lblTenDauSach.Name = "lblTenDauSach";
            lblTenDauSach.Size = new Size(94, 20);
            lblTenDauSach.TabIndex = 2;
            lblTenDauSach.Text = "Tên đầu sách";
            // 
            // lblMaDauSach
            // 
            lblMaDauSach.AutoSize = true;
            lblMaDauSach.Location = new Point(14, 27);
            lblMaDauSach.Name = "lblMaDauSach";
            lblMaDauSach.Size = new Size(92, 20);
            lblMaDauSach.TabIndex = 0;
            lblMaDauSach.Text = "Mã đầu sách";
            // 
            // dgvCuonSach
            // 
            dgvCuonSach.AllowUserToAddRows = false;
            dgvCuonSach.AllowUserToDeleteRows = false;
            dgvCuonSach.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCuonSach.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCuonSach.Dock = DockStyle.Fill;
            dgvCuonSach.Location = new Point(0, 45);
            dgvCuonSach.MultiSelect = false;
            dgvCuonSach.Name = "dgvCuonSach";
            dgvCuonSach.ReadOnly = true;
            dgvCuonSach.RowHeadersVisible = false;
            dgvCuonSach.RowHeadersWidth = 51;
            dgvCuonSach.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCuonSach.Size = new Size(754, 194);
            dgvCuonSach.TabIndex = 1;
            // 
            // pnlNutCuonSach
            // 
            pnlNutCuonSach.Controls.Add(btnDoiTrangThai);
            pnlNutCuonSach.Controls.Add(btnNhapThemBanSao);
            pnlNutCuonSach.Controls.Add(nudSoLuong);
            pnlNutCuonSach.Controls.Add(label1);
            pnlNutCuonSach.Dock = DockStyle.Top;
            pnlNutCuonSach.Location = new Point(0, 0);
            pnlNutCuonSach.Name = "pnlNutCuonSach";
            pnlNutCuonSach.Size = new Size(754, 45);
            pnlNutCuonSach.TabIndex = 0;
            // 
            // btnDoiTrangThai
            // 
            btnDoiTrangThai.Location = new Point(529, 8);
            btnDoiTrangThai.Name = "btnDoiTrangThai";
            btnDoiTrangThai.Size = new Size(110, 29);
            btnDoiTrangThai.TabIndex = 3;
            btnDoiTrangThai.Text = "Đổi trạng thái";
            btnDoiTrangThai.UseVisualStyleBackColor = true;
            // 
            // btnNhapThemBanSao
            // 
            btnNhapThemBanSao.Location = new Point(320, 8);
            btnNhapThemBanSao.Name = "btnNhapThemBanSao";
            btnNhapThemBanSao.Size = new Size(153, 29);
            btnNhapThemBanSao.TabIndex = 2;
            btnNhapThemBanSao.Text = "Nhập thêm bản sao";
            btnNhapThemBanSao.UseVisualStyleBackColor = true;
            // 
            // nudSoLuong
            // 
            nudSoLuong.Location = new Point(114, 10);
            nudSoLuong.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudSoLuong.Name = "nudSoLuong";
            nudSoLuong.Size = new Size(150, 27);
            nudSoLuong.TabIndex = 1;
            nudSoLuong.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 10);
            label1.Name = "label1";
            label1.Size = new Size(82, 20);
            label1.TabIndex = 0;
            label1.Text = "Số bản sao";
            // 
            // FrmQuanLySach
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1082, 603);
            Controls.Add(splMain);
            Name = "FrmQuanLySach";
            Text = "Quản lý Sách";
            Load += FrmQuanLySach_Load;
            splMain.Panel1.ResumeLayout(false);
            splMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splMain).EndInit();
            splMain.ResumeLayout(false);
            pnlNutTheLoai.ResumeLayout(false);
            splPhai.Panel1.ResumeLayout(false);
            splPhai.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splPhai).EndInit();
            splPhai.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDauSach).EndInit();
            grpDauSach.ResumeLayout(false);
            grpDauSach.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudGiaBia).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudNamXuatBan).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvCuonSach).EndInit();
            pnlNutCuonSach.ResumeLayout(false);
            pnlNutCuonSach.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudSoLuong).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splMain;
        private SplitContainer splPhai;
        private Panel pnlNutTheLoai;
        private Button btnThemTheLoai;
        private Button btnSuaTheLoai;
        private Button btnXoaTheLoai;
        private TreeView tvTheLoai;
        private GroupBox grpDauSach;
        private Label lblMaDauSach;
        private Label lblTenDauSach;
        private Label lblTacGia;
        private Label lblNamXuatBan;
        private Label lblGiaBia;
        private Label lblTheLoai;
        private TextBox txtMaDauSach;
        private TextBox txtTenDauSach;
        private TextBox txtTacGia;
        private NumericUpDown nudNamXuatBan;
        private NumericUpDown nudGiaBia;
        private ComboBox cboTheLoai;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private DataGridView dgvDauSach;
        private Panel pnlNutCuonSach;
        private NumericUpDown nudSoLuong;
        private Label label1;
        private Button btnNhapThemBanSao;
        private Button btnDoiTrangThai;
        private DataGridView dgvCuonSach;
    }
}