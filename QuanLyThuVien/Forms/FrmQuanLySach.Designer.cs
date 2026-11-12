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
            splPhai = new SplitContainer();
            pnlNutTheLoai = new Panel();
            btnThemTheLoai = new Button();
            btnSuaTheLoai = new Button();
            btnXoaTheLoai = new Button();
            tvTheLoai = new TreeView();
            grpDauSach = new GroupBox();
            txtMaDauSach = new Label();
            txtTenDauSach = new Label();
            txtTacGia = new Label();
            nudNamXuatBan = new Label();
            nudGiaBia = new Label();
            cboTheLoai = new Label();
            ((System.ComponentModel.ISupportInitialize)splMain).BeginInit();
            splMain.Panel1.SuspendLayout();
            splMain.Panel2.SuspendLayout();
            splMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splPhai).BeginInit();
            splPhai.Panel1.SuspendLayout();
            splPhai.SuspendLayout();
            pnlNutTheLoai.SuspendLayout();
            grpDauSach.SuspendLayout();
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
            // splPhai
            // 
            splPhai.Dock = DockStyle.Fill;
            splPhai.Location = new Point(0, 0);
            splPhai.Name = "splPhai";
            splPhai.Orientation = Orientation.Horizontal;
            // 
            // splPhai.Panel1
            // 
            splPhai.Panel1.Controls.Add(grpDauSach);
            splPhai.Size = new Size(754, 603);
            splPhai.SplitterDistance = 442;
            splPhai.TabIndex = 0;
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
            pnlNutTheLoai.Paint += pnlNutTheLoai_Paint;
            // 
            // btnThemTheLoai
            // 
            btnThemTheLoai.Location = new Point(12, 6);
            btnThemTheLoai.Name = "btnThemTheLoai";
            btnThemTheLoai.Size = new Size(70, 29);
            btnThemTheLoai.TabIndex = 0;
            btnThemTheLoai.Text = "Thêm";
            btnThemTheLoai.UseVisualStyleBackColor = true;
            // 
            // btnSuaTheLoai
            // 
            btnSuaTheLoai.Location = new Point(131, 6);
            btnSuaTheLoai.Name = "btnSuaTheLoai";
            btnSuaTheLoai.Size = new Size(70, 29);
            btnSuaTheLoai.TabIndex = 1;
            btnSuaTheLoai.Text = "Sửa";
            btnSuaTheLoai.UseVisualStyleBackColor = true;
            btnSuaTheLoai.Click += button1_Click;
            // 
            // btnXoaTheLoai
            // 
            btnXoaTheLoai.Location = new Point(251, 6);
            btnXoaTheLoai.Name = "btnXoaTheLoai";
            btnXoaTheLoai.Size = new Size(70, 29);
            btnXoaTheLoai.TabIndex = 2;
            btnXoaTheLoai.Text = "Xóa";
            btnXoaTheLoai.UseVisualStyleBackColor = true;
            btnXoaTheLoai.Click += btnXoaTheLoai_Click;
            // 
            // tvTheLoai
            // 
            tvTheLoai.Dock = DockStyle.Fill;
            tvTheLoai.Location = new Point(0, 0);
            tvTheLoai.Name = "tvTheLoai";
            tvTheLoai.Size = new Size(324, 558);
            tvTheLoai.TabIndex = 1;
            // 
            // grpDauSach
            // 
            grpDauSach.Controls.Add(cboTheLoai);
            grpDauSach.Controls.Add(nudGiaBia);
            grpDauSach.Controls.Add(nudNamXuatBan);
            grpDauSach.Controls.Add(txtTacGia);
            grpDauSach.Controls.Add(txtTenDauSach);
            grpDauSach.Controls.Add(txtMaDauSach);
            grpDauSach.Dock = DockStyle.Top;
            grpDauSach.Location = new Point(0, 0);
            grpDauSach.Name = "grpDauSach";
            grpDauSach.Size = new Size(754, 150);
            grpDauSach.TabIndex = 0;
            grpDauSach.TabStop = false;
            grpDauSach.Text = "Thông tin đầu sách";
            grpDauSach.Enter += grpDauSach_Enter;
            // 
            // txtMaDauSach
            // 
            txtMaDauSach.AutoSize = true;
            txtMaDauSach.Location = new Point(6, 23);
            txtMaDauSach.Name = "txtMaDauSach";
            txtMaDauSach.Padding = new Padding(10, 0, 0, 0);
            txtMaDauSach.Size = new Size(102, 20);
            txtMaDauSach.TabIndex = 0;
            txtMaDauSach.Text = "Mã đầu sách";
            // 
            // txtTenDauSach
            // 
            txtTenDauSach.AutoSize = true;
            txtTenDauSach.Location = new Point(114, 23);
            txtTenDauSach.Name = "txtTenDauSach";
            txtTenDauSach.Size = new Size(94, 20);
            txtTenDauSach.TabIndex = 1;
            txtTenDauSach.Text = "Tên đầu sách";
            // 
            // txtTacGia
            // 
            txtTacGia.AutoSize = true;
            txtTacGia.Location = new Point(214, 23);
            txtTacGia.Name = "txtTacGia";
            txtTacGia.Size = new Size(55, 20);
            txtTacGia.TabIndex = 2;
            txtTacGia.Text = "Tác giả";
            // 
            // nudNamXuatBan
            // 
            nudNamXuatBan.AutoSize = true;
            nudNamXuatBan.Location = new Point(275, 23);
            nudNamXuatBan.Name = "nudNamXuatBan";
            nudNamXuatBan.Size = new Size(102, 20);
            nudNamXuatBan.TabIndex = 3;
            nudNamXuatBan.Text = "Năm xuất bản";
            // 
            // nudGiaBia
            // 
            nudGiaBia.AutoSize = true;
            nudGiaBia.Location = new Point(383, 23);
            nudGiaBia.Name = "nudGiaBia";
            nudGiaBia.Size = new Size(56, 20);
            nudGiaBia.TabIndex = 4;
            nudGiaBia.Text = "Giá bìa";
            // 
            // cboTheLoai
            // 
            cboTheLoai.AutoSize = true;
            cboTheLoai.Location = new Point(445, 23);
            cboTheLoai.Name = "cboTheLoai";
            cboTheLoai.Size = new Size(62, 20);
            cboTheLoai.TabIndex = 5;
            cboTheLoai.Text = "Thể loại";
            // 
            // FrmQuanLySach
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1082, 603);
            Controls.Add(splMain);
            Name = "FrmQuanLySach";
            Text = "Quản lý Sách";
            splMain.Panel1.ResumeLayout(false);
            splMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splMain).EndInit();
            splMain.ResumeLayout(false);
            splPhai.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splPhai).EndInit();
            splPhai.ResumeLayout(false);
            pnlNutTheLoai.ResumeLayout(false);
            grpDauSach.ResumeLayout(false);
            grpDauSach.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splMain;
        private SplitContainer splPhai;
        private Button button2;
        private Panel pnlNutTheLoai;
        private Button btnThemTheLoai;
        private Button btnSuaTheLoai;
        private Button btnXoaTheLoai;
        private TreeView tvTheLoai;
        private GroupBox grpDauSach;
        private Label txtMaDauSach;
        private Label nudNamXuatBan;
        private Label txtTacGia;
        private Label txtTenDauSach;
        private Label cboTheLoai;
        private Label nudGiaBia;
        private TreeView treeView1;
    }
}