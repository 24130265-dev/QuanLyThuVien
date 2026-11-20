namespace QuanLyThuVien.Forms
{
    partial class DlgDoiMatKhau
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
            lblMatKhauMoi = new Label();
            lblXacNhan = new Label();
            txtMatKhauMoi = new TextBox();
            txtXacNhanMatKhau = new TextBox();
            btnLuu = new Button();
            btnHuy = new Button();
            SuspendLayout();
            // 
            // lblMatKhauMoi
            // 
            lblMatKhauMoi.AutoSize = true;
            lblMatKhauMoi.Location = new Point(20, 24);
            lblMatKhauMoi.Name = "lblMatKhauMoi";
            lblMatKhauMoi.Size = new Size(103, 20);
            lblMatKhauMoi.TabIndex = 0;
            lblMatKhauMoi.Text = "Mật khẩu mới:";
            // 
            // lblXacNhan
            // 
            lblXacNhan.AutoSize = true;
            lblXacNhan.Location = new Point(20, 70);
            lblXacNhan.Name = "lblXacNhan";
            lblXacNhan.Size = new Size(137, 20);
            lblXacNhan.TabIndex = 1;
            lblXacNhan.Text = "Xác nhận mật khẩu:";
            // 
            // txtMatKhauMoi
            // 
            txtMatKhauMoi.Location = new Point(195, 20);
            txtMatKhauMoi.Name = "txtMatKhauMoi";
            txtMatKhauMoi.Size = new Size(240, 27);
            txtMatKhauMoi.TabIndex = 2;
            txtMatKhauMoi.UseSystemPasswordChar = true;
            // 
            // txtXacNhanMatKhau
            // 
            txtXacNhanMatKhau.Location = new Point(195, 66);
            txtXacNhanMatKhau.Name = "txtXacNhanMatKhau";
            txtXacNhanMatKhau.Size = new Size(240, 27);
            txtXacNhanMatKhau.TabIndex = 3;
            txtXacNhanMatKhau.UseSystemPasswordChar = true;
            // 
            // btnLuu
            // 
            btnLuu.Location = new Point(195, 120);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(100, 36);
            btnLuu.TabIndex = 4;
            btnLuu.Text = "Lưu";
            btnLuu.UseVisualStyleBackColor = true;
            btnLuu.Click += btnLuu_Click;
            // 
            // btnHuy
            // 
            btnHuy.DialogResult = DialogResult.Cancel;
            btnHuy.Location = new Point(335, 120);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(100, 36);
            btnHuy.TabIndex = 5;
            btnHuy.Text = "Hủy";
            btnHuy.UseVisualStyleBackColor = true;
            // 
            // DlgDoiMatKhau
            // 
            AcceptButton = btnLuu;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnHuy;
            ClientSize = new Size(458, 177);
            Controls.Add(btnHuy);
            Controls.Add(btnLuu);
            Controls.Add(txtXacNhanMatKhau);
            Controls.Add(txtMatKhauMoi);
            Controls.Add(lblXacNhan);
            Controls.Add(lblMatKhauMoi);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DlgDoiMatKhau";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Đổi mật khẩu";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblMatKhauMoi;
        private Label lblXacNhan;
        private TextBox txtMatKhauMoi;
        private TextBox txtXacNhanMatKhau;
        private Button btnLuu;
        private Button btnHuy;
    }
}