namespace QuanLyThuVien.Forms
{
    partial class DlgDangKy
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblTieuDe = new Label();
            lblGhiChu = new Label();
            lblMaSv = new Label();
            txtMaSv = new TextBox();
            lblHoTen = new Label();
            txtHoTen = new TextBox();
            lblSdt = new Label();
            txtSdt = new TextBox();
            lblMatKhau = new Label();
            txtMatKhau = new TextBox();
            lblXacNhan = new Label();
            txtXacNhan = new TextBox();
            chkHienMatKhau = new CheckBox();
            btnDangKy = new Button();
            btnHuy = new Button();
            SuspendLayout();
            // 
            // lblTieuDe
            // 
            lblTieuDe.AutoSize = true;
            lblTieuDe.Location = new Point(30, 20);
            lblTieuDe.Margin = new Padding(4, 0, 4, 0);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(216, 20);
            lblTieuDe.TabIndex = 0;
            lblTieuDe.Text = "ĐĂNG KÝ TÀI KHOẢN ĐỘC GIẢ";
            // 
            // lblGhiChu
            // 
            lblGhiChu.AutoSize = true;
            lblGhiChu.Location = new Point(30, 62);
            lblGhiChu.Margin = new Padding(4, 0, 4, 0);
            lblGhiChu.Name = "lblGhiChu";
            lblGhiChu.Size = new Size(303, 20);
            lblGhiChu.TabIndex = 1;
            lblGhiChu.Text = "Dành cho sinh viên lần đầu sử dụng thư viện.";
            // 
            // lblMaSv
            // 
            lblMaSv.AutoSize = true;
            lblMaSv.Location = new Point(30, 118);
            lblMaSv.Margin = new Padding(4, 0, 4, 0);
            lblMaSv.Name = "lblMaSv";
            lblMaSv.Size = new Size(113, 20);
            lblMaSv.TabIndex = 2;
            lblMaSv.Text = "Mã số sinh viên:";
            // 
            // txtMaSv
            // 
            txtMaSv.Location = new Point(250, 112);
            txtMaSv.Margin = new Padding(4, 4, 4, 4);
            txtMaSv.MaxLength = 8;
            txtMaSv.Name = "txtMaSv";
            txtMaSv.PlaceholderText = "8 chữ số, ví dụ 24130265";
            txtMaSv.Size = new Size(324, 27);
            txtMaSv.TabIndex = 3;
            // 
            // lblHoTen
            // 
            lblHoTen.AutoSize = true;
            lblHoTen.Location = new Point(30, 168);
            lblHoTen.Margin = new Padding(4, 0, 4, 0);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Size = new Size(76, 20);
            lblHoTen.TabIndex = 4;
            lblHoTen.Text = "Họ và tên:";
            // 
            // txtHoTen
            // 
            txtHoTen.Location = new Point(250, 162);
            txtHoTen.Margin = new Padding(4, 4, 4, 4);
            txtHoTen.MaxLength = 100;
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(324, 27);
            txtHoTen.TabIndex = 5;
            // 
            // lblSdt
            // 
            lblSdt.AutoSize = true;
            lblSdt.Location = new Point(30, 218);
            lblSdt.Margin = new Padding(4, 0, 4, 0);
            lblSdt.Name = "lblSdt";
            lblSdt.Size = new Size(100, 20);
            lblSdt.TabIndex = 6;
            lblSdt.Text = "Số điện thoại:";
            // 
            // txtSdt
            // 
            txtSdt.Location = new Point(250, 212);
            txtSdt.Margin = new Padding(4, 4, 4, 4);
            txtSdt.MaxLength = 10;
            txtSdt.Name = "txtSdt";
            txtSdt.Size = new Size(324, 27);
            txtSdt.TabIndex = 7;
            // 
            // lblMatKhau
            // 
            lblMatKhau.AutoSize = true;
            lblMatKhau.Location = new Point(30, 268);
            lblMatKhau.Margin = new Padding(4, 0, 4, 0);
            lblMatKhau.Name = "lblMatKhau";
            lblMatKhau.Size = new Size(73, 20);
            lblMatKhau.TabIndex = 8;
            lblMatKhau.Text = "Mật khẩu:";
            // 
            // txtMatKhau
            // 
            txtMatKhau.Location = new Point(250, 262);
            txtMatKhau.Margin = new Padding(4, 4, 4, 4);
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.Size = new Size(324, 27);
            txtMatKhau.TabIndex = 9;
            txtMatKhau.UseSystemPasswordChar = true;
            // 
            // lblXacNhan
            // 
            lblXacNhan.AutoSize = true;
            lblXacNhan.Location = new Point(30, 318);
            lblXacNhan.Margin = new Padding(4, 0, 4, 0);
            lblXacNhan.Name = "lblXacNhan";
            lblXacNhan.Size = new Size(133, 20);
            lblXacNhan.TabIndex = 10;
            lblXacNhan.Text = "Nhập lại mật khẩu:";
            // 
            // txtXacNhan
            // 
            txtXacNhan.Location = new Point(250, 312);
            txtXacNhan.Margin = new Padding(4, 4, 4, 4);
            txtXacNhan.Name = "txtXacNhan";
            txtXacNhan.Size = new Size(324, 27);
            txtXacNhan.TabIndex = 11;
            txtXacNhan.UseSystemPasswordChar = true;
            // 
            // chkHienMatKhau
            // 
            chkHienMatKhau.AutoSize = true;
            chkHienMatKhau.Location = new Point(250, 362);
            chkHienMatKhau.Margin = new Padding(4, 4, 4, 4);
            chkHienMatKhau.Name = "chkHienMatKhau";
            chkHienMatKhau.Size = new Size(127, 24);
            chkHienMatKhau.TabIndex = 12;
            chkHienMatKhau.Text = "Hiện mật khẩu";
            chkHienMatKhau.UseVisualStyleBackColor = true;
            chkHienMatKhau.CheckedChanged += chkHienMatKhau_CheckedChanged;
            // 
            // btnDangKy
            // 
            btnDangKy.Location = new Point(250, 415);
            btnDangKy.Margin = new Padding(4, 4, 4, 4);
            btnDangKy.Name = "btnDangKy";
            btnDangKy.Size = new Size(188, 50);
            btnDangKy.TabIndex = 13;
            btnDangKy.Text = "Đăng ký";
            btnDangKy.UseVisualStyleBackColor = true;
            btnDangKy.Click += btnDangKy_Click;
            // 
            // btnHuy
            // 
            btnHuy.DialogResult = DialogResult.Cancel;
            btnHuy.Location = new Point(450, 415);
            btnHuy.Margin = new Padding(4, 4, 4, 4);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(125, 50);
            btnHuy.TabIndex = 14;
            btnHuy.Text = "Hủy";
            btnHuy.UseVisualStyleBackColor = true;
            // 
            // DlgDangKy
            // 
            AcceptButton = btnDangKy;
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = btnHuy;
            ClientSize = new Size(612, 494);
            Controls.Add(lblTieuDe);
            Controls.Add(lblGhiChu);
            Controls.Add(lblMaSv);
            Controls.Add(txtMaSv);
            Controls.Add(lblHoTen);
            Controls.Add(txtHoTen);
            Controls.Add(lblSdt);
            Controls.Add(txtSdt);
            Controls.Add(lblMatKhau);
            Controls.Add(txtMatKhau);
            Controls.Add(lblXacNhan);
            Controls.Add(txtXacNhan);
            Controls.Add(chkHienMatKhau);
            Controls.Add(btnDangKy);
            Controls.Add(btnHuy);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 4, 4, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DlgDangKy";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Đăng ký tài khoản";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTieuDe;
        private Label lblGhiChu;
        private Label lblMaSv;
        private TextBox txtMaSv;
        private Label lblHoTen;
        private TextBox txtHoTen;
        private Label lblSdt;
        private TextBox txtSdt;
        private Label lblMatKhau;
        private TextBox txtMatKhau;
        private Label lblXacNhan;
        private TextBox txtXacNhan;
        private CheckBox chkHienMatKhau;
        private Button btnDangKy;
        private Button btnHuy;
    }
}