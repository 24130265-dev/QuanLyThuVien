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
            components = new System.ComponentModel.Container();
            splMain = new System.Windows.Forms.SplitContainer();
            tvTheLoai = new System.Windows.Forms.TreeView();
            pnlNutTheLoai = new System.Windows.Forms.Panel();
            btnXoaTheLoai = new System.Windows.Forms.Button();
            btnSuaTheLoai = new System.Windows.Forms.Button();
            btnThemTheLoai = new System.Windows.Forms.Button();
            splPhai = new System.Windows.Forms.SplitContainer();
            dgvDauSach = new System.Windows.Forms.DataGridView();
            grpDauSach = new System.Windows.Forms.GroupBox();
            btnLamMoi = new System.Windows.Forms.Button();
            btnXoa = new System.Windows.Forms.Button();
            btnSua = new System.Windows.Forms.Button();
            btnThem = new System.Windows.Forms.Button();
            cboTheLoai = new System.Windows.Forms.ComboBox();
            nudGiaBia = new System.Windows.Forms.NumericUpDown();
            nudNamXuatBan = new System.Windows.Forms.NumericUpDown();
            txtTacGia = new System.Windows.Forms.TextBox();
            txtTenDauSach = new System.Windows.Forms.TextBox();
            txtMaDauSach = new System.Windows.Forms.TextBox();
            lblTheLoai = new System.Windows.Forms.Label();
            lblGiaBia = new System.Windows.Forms.Label();
            lblNamXuatBan = new System.Windows.Forms.Label();
            lblTacGia = new System.Windows.Forms.Label();
            lblTenDauSach = new System.Windows.Forms.Label();
            lblMaDauSach = new System.Windows.Forms.Label();
            dgvCuonSach = new System.Windows.Forms.DataGridView();
            pnlNutCuonSach = new System.Windows.Forms.Panel();
            btnXoaCuon = new System.Windows.Forms.Button();
            btnDoiTrangThai = new System.Windows.Forms.Button();
            btnNhapThemBanSao = new System.Windows.Forms.Button();
            nudSoLuong = new System.Windows.Forms.NumericUpDown();
            label1 = new System.Windows.Forms.Label();
            errProvider = new System.Windows.Forms.ErrorProvider(components);
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
            ((System.ComponentModel.ISupportInitialize)errProvider).BeginInit();
            SuspendLayout();
            // 
            // splMain
            // 
            splMain.Dock = System.Windows.Forms.DockStyle.Fill;
            splMain.Location = new System.Drawing.Point(0, 0);
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
            splMain.Size = new System.Drawing.Size(1248, 603);
            splMain.SplitterDistance = 345;
            splMain.TabIndex = 0;
            // 
            // tvTheLoai
            // 
            tvTheLoai.Dock = System.Windows.Forms.DockStyle.Fill;
            tvTheLoai.Location = new System.Drawing.Point(0, 0);
            tvTheLoai.Name = "tvTheLoai";
            tvTheLoai.Size = new System.Drawing.Size(345, 551);
            tvTheLoai.TabIndex = 1;
            tvTheLoai.AfterSelect += tvTheLoai_AfterSelect;
            // 
            // pnlNutTheLoai
            // 
            pnlNutTheLoai.Controls.Add(btnXoaTheLoai);
            pnlNutTheLoai.Controls.Add(btnSuaTheLoai);
            pnlNutTheLoai.Controls.Add(btnThemTheLoai);
            pnlNutTheLoai.Dock = System.Windows.Forms.DockStyle.Bottom;
            pnlNutTheLoai.Location = new System.Drawing.Point(0, 551);
            pnlNutTheLoai.Name = "pnlNutTheLoai";
            pnlNutTheLoai.Size = new System.Drawing.Size(345, 52);
            pnlNutTheLoai.TabIndex = 0;
            // 
            // btnXoaTheLoai
            // 
            btnXoaTheLoai.Location = new System.Drawing.Point(232, 8);
            btnXoaTheLoai.Name = "btnXoaTheLoai";
            btnXoaTheLoai.Size = new System.Drawing.Size(100, 36);
            btnXoaTheLoai.TabIndex = 2;
            btnXoaTheLoai.Text = "Xóa";
            btnXoaTheLoai.UseVisualStyleBackColor = true;
            btnXoaTheLoai.Click += btnXoaTheLoai_Click;
            // 
            // btnSuaTheLoai
            // 
            btnSuaTheLoai.Location = new System.Drawing.Point(122, 8);
            btnSuaTheLoai.Name = "btnSuaTheLoai";
            btnSuaTheLoai.Size = new System.Drawing.Size(100, 36);
            btnSuaTheLoai.TabIndex = 1;
            btnSuaTheLoai.Text = "Sửa";
            btnSuaTheLoai.UseVisualStyleBackColor = true;
            btnSuaTheLoai.Click += btnSuaTheLoai_Click;
            // 
            // btnThemTheLoai
            // 
            btnThemTheLoai.Location = new System.Drawing.Point(12, 8);
            btnThemTheLoai.Name = "btnThemTheLoai";
            btnThemTheLoai.Size = new System.Drawing.Size(100, 36);
            btnThemTheLoai.TabIndex = 0;
            btnThemTheLoai.Text = "Thêm";
            btnThemTheLoai.UseVisualStyleBackColor = true;
            btnThemTheLoai.Click += btnThemTheLoai_Click;
            // 
            // splPhai
            // 
            splPhai.Dock = System.Windows.Forms.DockStyle.Fill;
            splPhai.Location = new System.Drawing.Point(0, 0);
            splPhai.Name = "splPhai";
            splPhai.Orientation = System.Windows.Forms.Orientation.Horizontal;
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
            splPhai.Size = new System.Drawing.Size(899, 603);
            splPhai.SplitterDistance = 360;
            splPhai.TabIndex = 0;
            // 
            // dgvDauSach
            // 
            dgvDauSach.AllowUserToAddRows = false;
            dgvDauSach.AllowUserToDeleteRows = false;
            dgvDauSach.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvDauSach.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDauSach.Dock = System.Windows.Forms.DockStyle.Fill;
            dgvDauSach.Location = new System.Drawing.Point(0, 195);
            dgvDauSach.MultiSelect = false;
            dgvDauSach.Name = "dgvDauSach";
            dgvDauSach.ReadOnly = true;
            dgvDauSach.RowHeadersWidth = 51;
            dgvDauSach.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvDauSach.Size = new System.Drawing.Size(899, 165);
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
            grpDauSach.Dock = System.Windows.Forms.DockStyle.Top;
            grpDauSach.Location = new System.Drawing.Point(0, 0);
            grpDauSach.Name = "grpDauSach";
            grpDauSach.Size = new System.Drawing.Size(899, 195);
            grpDauSach.TabIndex = 0;
            grpDauSach.TabStop = false;
            grpDauSach.Text = "Thông tin đầu sách";
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new System.Drawing.Point(380, 148);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new System.Drawing.Size(130, 36);
            btnLamMoi.TabIndex = 15;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            
            // 
            // btnXoa
            // 
            btnXoa.Location = new System.Drawing.Point(260, 148);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new System.Drawing.Size(110, 36);
            btnXoa.TabIndex = 14;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnSua
            // 
            btnSua.Location = new System.Drawing.Point(140, 148);
            btnSua.Name = "btnSua";
            btnSua.Size = new System.Drawing.Size(110, 36);
            btnSua.TabIndex = 13;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            // 
            // btnThem
            // 
            btnThem.Location = new System.Drawing.Point(20, 148);
            btnThem.Name = "btnThem";
            btnThem.Size = new System.Drawing.Size(110, 36);
            btnThem.TabIndex = 12;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            // 
            // cboTheLoai
            // 
            cboTheLoai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboTheLoai.FormattingEnabled = true;
            cboTheLoai.Location = new System.Drawing.Point(560, 106);
            cboTheLoai.Name = "cboTheLoai";
            cboTheLoai.Size = new System.Drawing.Size(210, 23);
            cboTheLoai.TabIndex = 11;
            // 
            // nudGiaBia
            // 
            nudGiaBia.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            nudGiaBia.Location = new System.Drawing.Point(560, 68);
            nudGiaBia.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            nudGiaBia.Name = "nudGiaBia";
            nudGiaBia.Size = new System.Drawing.Size(210, 23);
            nudGiaBia.TabIndex = 9;
            nudGiaBia.ThousandsSeparator = true;
            // 
            // nudNamXuatBan
            // 
            nudNamXuatBan.Location = new System.Drawing.Point(560, 30);
            nudNamXuatBan.Maximum = new decimal(new int[] { 2100, 0, 0, 0 });
            nudNamXuatBan.Minimum = new decimal(new int[] { 1900, 0, 0, 0 });
            nudNamXuatBan.Name = "nudNamXuatBan";
            nudNamXuatBan.Size = new System.Drawing.Size(210, 23);
            nudNamXuatBan.TabIndex = 7;
            nudNamXuatBan.Value = new decimal(new int[] { 2024, 0, 0, 0 });
            // 
            // txtTacGia
            // 
            txtTacGia.Location = new System.Drawing.Point(140, 106);
            txtTacGia.Name = "txtTacGia";
            txtTacGia.Size = new System.Drawing.Size(240, 23);
            txtTacGia.TabIndex = 5;
            txtTacGia.Validating += txtTacGia_Validating;
            // 
            // txtTenDauSach
            // 
            txtTenDauSach.Location = new System.Drawing.Point(140, 68);
            txtTenDauSach.Name = "txtTenDauSach";
            txtTenDauSach.Size = new System.Drawing.Size(240, 23);
            txtTenDauSach.TabIndex = 3;
            txtTenDauSach.Validating += txtTenDauSach_Validating;
            // 
            // txtMaDauSach
            // 
            txtMaDauSach.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            txtMaDauSach.Location = new System.Drawing.Point(140, 30);
            txtMaDauSach.MaxLength = 10;
            txtMaDauSach.Name = "txtMaDauSach";
            txtMaDauSach.Size = new System.Drawing.Size(240, 23);
            txtMaDauSach.TabIndex = 1;
            txtMaDauSach.Validating += txtMaDauSach_Validating;
            // 
            // lblTheLoai
            // 
            lblTheLoai.AutoSize = true;
            lblTheLoai.Location = new System.Drawing.Point(430, 110);
            lblTheLoai.Name = "lblTheLoai";
            lblTheLoai.Size = new System.Drawing.Size(48, 15);
            lblTheLoai.TabIndex = 10;
            lblTheLoai.Text = "Thể loại";
            // 
            // lblGiaBia
            // 
            lblGiaBia.AutoSize = true;
            lblGiaBia.Location = new System.Drawing.Point(430, 72);
            lblGiaBia.Name = "lblGiaBia";
            lblGiaBia.Size = new System.Drawing.Size(43, 15);
            lblGiaBia.TabIndex = 8;
            lblGiaBia.Text = "Giá bìa";
            // 
            // lblNamXuatBan
            // 
            lblNamXuatBan.AutoSize = true;
            lblNamXuatBan.Location = new System.Drawing.Point(430, 34);
            lblNamXuatBan.Name = "lblNamXuatBan";
            lblNamXuatBan.Size = new System.Drawing.Size(82, 15);
            lblNamXuatBan.TabIndex = 6;
            lblNamXuatBan.Text = "Năm xuất bản";
            // 
            // lblTacGia
            // 
            lblTacGia.AutoSize = true;
            lblTacGia.Location = new System.Drawing.Point(20, 110);
            lblTacGia.Name = "lblTacGia";
            lblTacGia.Size = new System.Drawing.Size(43, 15);
            lblTacGia.TabIndex = 4;
            lblTacGia.Text = "Tác giả";
            // 
            // lblTenDauSach
            // 
            lblTenDauSach.AutoSize = true;
            lblTenDauSach.Location = new System.Drawing.Point(20, 72);
            lblTenDauSach.Name = "lblTenDauSach";
            lblTenDauSach.Size = new System.Drawing.Size(75, 15);
            lblTenDauSach.TabIndex = 2;
            lblTenDauSach.Text = "Tên đầu sách";
            // 
            // lblMaDauSach
            // 
            lblMaDauSach.AutoSize = true;
            lblMaDauSach.Location = new System.Drawing.Point(20, 34);
            lblMaDauSach.Name = "lblMaDauSach";
            lblMaDauSach.Size = new System.Drawing.Size(74, 15);
            lblMaDauSach.TabIndex = 0;
            lblMaDauSach.Text = "Mã đầu sách";
            // 
            // dgvCuonSach
            // 
            dgvCuonSach.AllowUserToAddRows = false;
            dgvCuonSach.AllowUserToDeleteRows = false;
            dgvCuonSach.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvCuonSach.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCuonSach.Dock = System.Windows.Forms.DockStyle.Fill;
            dgvCuonSach.Location = new System.Drawing.Point(0, 52);
            dgvCuonSach.MultiSelect = false;
            dgvCuonSach.Name = "dgvCuonSach";
            dgvCuonSach.ReadOnly = true;
            dgvCuonSach.RowHeadersVisible = false;
            dgvCuonSach.RowHeadersWidth = 51;
            dgvCuonSach.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvCuonSach.Size = new System.Drawing.Size(899, 187);
            dgvCuonSach.TabIndex = 1;
            // 
            // pnlNutCuonSach
            // 
            pnlNutCuonSach.Controls.Add(btnXoaCuon);
            pnlNutCuonSach.Controls.Add(btnDoiTrangThai);
            pnlNutCuonSach.Controls.Add(btnNhapThemBanSao);
            pnlNutCuonSach.Controls.Add(nudSoLuong);
            pnlNutCuonSach.Controls.Add(label1);
            pnlNutCuonSach.Dock = System.Windows.Forms.DockStyle.Top;
            pnlNutCuonSach.Location = new System.Drawing.Point(0, 0);
            pnlNutCuonSach.Name = "pnlNutCuonSach";
            pnlNutCuonSach.Size = new System.Drawing.Size(899, 52);
            pnlNutCuonSach.TabIndex = 0;
            // 
            // btnXoaCuon
            // 
            btnXoaCuon.Location = new System.Drawing.Point(590, 8);
            btnXoaCuon.Name = "btnXoaCuon";
            btnXoaCuon.Size = new System.Drawing.Size(130, 36);
            btnXoaCuon.TabIndex = 4;
            btnXoaCuon.Text = "Xóa cuốn";
            btnXoaCuon.UseVisualStyleBackColor = true;
            btnXoaCuon.Click += btnXoaCuon_Click;
            // 
            // btnDoiTrangThai
            // 
            btnDoiTrangThai.Location = new System.Drawing.Point(420, 8);
            btnDoiTrangThai.Name = "btnDoiTrangThai";
            btnDoiTrangThai.Size = new System.Drawing.Size(160, 36);
            btnDoiTrangThai.TabIndex = 3;
            btnDoiTrangThai.Text = "Đổi trạng thái";
            btnDoiTrangThai.UseVisualStyleBackColor = true;
            btnDoiTrangThai.Click += btnDoiTrangThai_Click;
            // 
            // btnNhapThemBanSao
            // 
            btnNhapThemBanSao.Location = new System.Drawing.Point(210, 8);
            btnNhapThemBanSao.Name = "btnNhapThemBanSao";
            btnNhapThemBanSao.Size = new System.Drawing.Size(200, 36);
            btnNhapThemBanSao.TabIndex = 2;
            btnNhapThemBanSao.Text = "Nhập thêm bản sao";
            btnNhapThemBanSao.UseVisualStyleBackColor = true;
            btnNhapThemBanSao.Click += btnNhapThemBanSao_Click;
            // 
            // nudSoLuong
            // 
            nudSoLuong.Location = new System.Drawing.Point(110, 11);
            nudSoLuong.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudSoLuong.Name = "nudSoLuong";
            nudSoLuong.Size = new System.Drawing.Size(80, 23);
            nudSoLuong.TabIndex = 1;
            nudSoLuong.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(16, 16);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(64, 15);
            label1.TabIndex = 0;
            label1.Text = "Số bản sao";
            // 
            // errProvider
            // 
            errProvider.ContainerControl = this;
            // 
            // FrmQuanLySach
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1248, 603);
            Controls.Add(splMain);
            Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            ((System.ComponentModel.ISupportInitialize)errProvider).EndInit();
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
        private ErrorProvider errProvider;
        private Button btnXoaCuon;
    }
}