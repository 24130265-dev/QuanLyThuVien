namespace QuanLyThuVien.Forms
{
    partial class FrmDangNhap
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
            pnlTrai = new Panel();
            lblHeThong = new Label();
            lblKhoa = new Label();
            lblTenTruong = new Label();
            lblLogo = new Label();
            grpDangNhap = new GroupBox();
            lblGhiChu = new Label();
            btnThoat = new Button();
            btnDangNhap = new Button();
            grpVaiTro = new GroupBox();
            rdoDocGia = new RadioButton();
            rdoThuThu = new RadioButton();
            txtMatKhau = new TextBox();
            chkHienMatKhau = new CheckBox();
            lblMatKhau = new Label();
            txtEmail = new TextBox();
            lblEmail = new Label();
            pnlTrai.SuspendLayout();
            grpDangNhap.SuspendLayout();
            grpVaiTro.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTrai
            // 
            pnlTrai.BackColor = Color.FromArgb(0, 102, 51);
            pnlTrai.Controls.Add(lblHeThong);
            pnlTrai.Controls.Add(lblKhoa);
            pnlTrai.Controls.Add(lblTenTruong);
            pnlTrai.Controls.Add(lblLogo);
            pnlTrai.Dock = DockStyle.Left;
            pnlTrai.Location = new Point(0, 0);
            pnlTrai.Name = "pnlTrai";
            pnlTrai.Size = new Size(359, 493);
            pnlTrai.TabIndex = 0;
            // 
            // lblHeThong
            // 
            lblHeThong.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblHeThong.ForeColor = Color.White;
            lblHeThong.Location = new Point(15, 290);
            lblHeThong.Name = "lblHeThong";
            lblHeThong.Size = new Size(330, 60);
            lblHeThong.TabIndex = 3;
            lblHeThong.Text = "HỆ THỐNG QUẢN LÝ MƯỢN / TRẢ SÁCH";
            lblHeThong.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblKhoa
            // 
            lblKhoa.Font = new Font("Segoe UI", 10F);
            lblKhoa.ForeColor = Color.White;
            lblKhoa.Location = new Point(15, 215);
            lblKhoa.Name = "lblKhoa";
            lblKhoa.Size = new Size(330, 30);
            lblKhoa.TabIndex = 2;
            lblKhoa.Text = "KHOA CÔNG NGHỆ THÔNG TIN";
            lblKhoa.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTenTruong
            // 
            lblTenTruong.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTenTruong.ForeColor = Color.White;
            lblTenTruong.Location = new Point(15, 150);
            lblTenTruong.Name = "lblTenTruong";
            lblTenTruong.Size = new Size(330, 60);
            lblTenTruong.TabIndex = 1;
            lblTenTruong.Text = "THƯ VIỆN ĐẠI HỌC NÔNG LÂM TP. HCM";
            lblTenTruong.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblLogo
            // 
            lblLogo.Font = new Font("Segoe UI", 42F, FontStyle.Bold);
            lblLogo.ForeColor = Color.White;
            lblLogo.Location = new Point(20, 45);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(330, 90);
            lblLogo.TabIndex = 0;
            lblLogo.Text = "📚";
            lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // grpDangNhap
            // 
            grpDangNhap.Controls.Add(lblGhiChu);
            grpDangNhap.Controls.Add(btnThoat);
            grpDangNhap.Controls.Add(btnDangNhap);
            grpDangNhap.Controls.Add(grpVaiTro);
            grpDangNhap.Controls.Add(txtMatKhau);
            grpDangNhap.Controls.Add(chkHienMatKhau);
            grpDangNhap.Controls.Add(lblMatKhau);
            grpDangNhap.Controls.Add(txtEmail);
            grpDangNhap.Controls.Add(lblEmail);
            grpDangNhap.Location = new Point(365, 35);
            grpDangNhap.Name = "grpDangNhap";
            grpDangNhap.Size = new Size(520, 430);
            grpDangNhap.TabIndex = 1;
            grpDangNhap.TabStop = false;
            grpDangNhap.Text = "ĐĂNG NHẬP HỆ THỐNG";
            // 
            // lblGhiChu
            // 
            lblGhiChu.AutoSize = true;
            lblGhiChu.Location = new Point(35, 382);
            lblGhiChu.Name = "lblGhiChu";
            lblGhiChu.Size = new Size(266, 20);
            lblGhiChu.TabIndex = 9;
            lblGhiChu.Text = "Mặc định: admin@hcmuaf.edu.vn / 123";
            // 
            // btnThoat
            // 
            btnThoat.Location = new Point(270, 322);
            btnThoat.Name = "btnThoat";
            btnThoat.Size = new Size(215, 42);
            btnThoat.TabIndex = 8;
            btnThoat.Text = "Thoát";
            btnThoat.UseVisualStyleBackColor = true;
            btnThoat.Click += btnThoat_Click;
            // 
            // btnDangNhap
            // 
            btnDangNhap.Location = new Point(35, 322);
            btnDangNhap.Name = "btnDangNhap";
            btnDangNhap.Size = new Size(220, 42);
            btnDangNhap.TabIndex = 7;
            btnDangNhap.Text = "Đăng nhập";
            btnDangNhap.UseVisualStyleBackColor = true;
            btnDangNhap.Click += btnDangNhap_Click;
            // 
            // grpVaiTro
            // 
            grpVaiTro.Controls.Add(rdoDocGia);
            grpVaiTro.Controls.Add(rdoThuThu);
            grpVaiTro.Location = new Point(35, 195);
            grpVaiTro.Name = "grpVaiTro";
            grpVaiTro.Size = new Size(450, 121);
            grpVaiTro.TabIndex = 6;
            grpVaiTro.TabStop = false;
            grpVaiTro.Text = "Vai trò đăng nhập";
            // 
            // rdoDocGia
            // 
            rdoDocGia.AutoSize = true;
            rdoDocGia.Location = new Point(235, 28);
            rdoDocGia.Name = "rdoDocGia";
            rdoDocGia.Size = new Size(155, 24);
            rdoDocGia.TabIndex = 1;
            rdoDocGia.Text = "Độc giả / Sinh viên";
            rdoDocGia.UseVisualStyleBackColor = true;
            // 
            // rdoThuThu
            // 
            rdoThuThu.AutoSize = true;
            rdoThuThu.Checked = true;
            rdoThuThu.Location = new Point(20, 26);
            rdoThuThu.Name = "rdoThuThu";
            rdoThuThu.Size = new Size(147, 24);
            rdoThuThu.TabIndex = 0;
            rdoThuThu.TabStop = true;
            rdoThuThu.Text = "Thủ thư / Quản trị";
            rdoThuThu.UseVisualStyleBackColor = true;
            // 
            // txtMatKhau
            // 
            txtMatKhau.Location = new Point(35, 130);
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.Size = new Size(125, 27);
            txtMatKhau.TabIndex = 5;
            // 
            // chkHienMatKhau
            // 
            chkHienMatKhau.AutoSize = true;
            chkHienMatKhau.Location = new Point(35, 165);
            chkHienMatKhau.Name = "chkHienMatKhau";
            chkHienMatKhau.Size = new Size(127, 24);
            chkHienMatKhau.TabIndex = 3;
            chkHienMatKhau.Text = "Hiện mật khẩu";
            chkHienMatKhau.UseVisualStyleBackColor = true;
            chkHienMatKhau.CheckedChanged += chkHienMatKhau_CheckedChanged;
            // 
            // lblMatKhau
            // 
            lblMatKhau.AutoSize = true;
            lblMatKhau.Location = new Point(35, 107);
            lblMatKhau.Name = "lblMatKhau";
            lblMatKhau.Size = new Size(73, 20);
            lblMatKhau.TabIndex = 2;
            lblMatKhau.Text = "Mật khẩu:";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(35, 65);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(125, 27);
            txtEmail.TabIndex = 1;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(35, 40);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(210, 20);
            lblEmail.TabIndex = 0;
            lblEmail.Text = "Email trường / Tên đăng nhập:";
            // 
            // FrmDangNhap
            // 
            AcceptButton = btnDangNhap;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnThoat;
            ClientSize = new Size(922, 493);
            Controls.Add(grpDangNhap);
            Controls.Add(pnlTrai);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmDangNhap";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng nhập - Hệ thống Quản lý Thư viện NLU";
            pnlTrai.ResumeLayout(false);
            grpDangNhap.ResumeLayout(false);
            grpDangNhap.PerformLayout();
            grpVaiTro.ResumeLayout(false);
            grpVaiTro.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlTrai;
        private Label lblHeThong;
        private Label lblKhoa;
        private Label lblTenTruong;
        private Label lblLogo;
        private GroupBox grpDangNhap;
        private Label lblMatKhau;
        private TextBox txtEmail;
        private Label lblEmail;
        private CheckBox chkHienMatKhau;
        private TextBox txtMatKhau;
        private Button btnDangNhap;
        private GroupBox grpVaiTro;
        private Button btnThoat;
        private Label lblGhiChu;
        private RadioButton rdoThuThu;
        private RadioButton rdoDocGia;
    }
}