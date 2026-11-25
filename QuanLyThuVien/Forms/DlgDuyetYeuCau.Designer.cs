namespace QuanLyThuVien.Forms
{
    partial class DlgDuyetYeuCau
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
            lblDem = new Label();
            dgvYeuCau = new DataGridView();
            btnDuyet = new Button();
            btnTuChoi = new Button();
            btnDong = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvYeuCau).BeginInit();
            SuspendLayout();
            // 
            // lblTieuDe
            // 
            lblTieuDe.AutoSize = true;
            lblTieuDe.Location = new Point(20, 16);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(300, 20);
            lblTieuDe.TabIndex = 0;
            lblTieuDe.Text = "YÊU CẦU MƯỢN SÁCH ĐANG CHỜ DUYỆT";
            // 
            // lblDem
            // 
            lblDem.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblDem.Location = new Point(640, 18);
            lblDem.Name = "lblDem";
            lblDem.Size = new Size(220, 24);
            lblDem.TabIndex = 1;
            lblDem.Text = "Có 0 yêu cầu";
            lblDem.TextAlign = ContentAlignment.MiddleRight;
            // 
            // dgvYeuCau
            // 
            dgvYeuCau.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvYeuCau.Location = new Point(20, 56);
            dgvYeuCau.Name = "dgvYeuCau";
            dgvYeuCau.RowHeadersWidth = 51;
            dgvYeuCau.Size = new Size(840, 340);
            dgvYeuCau.TabIndex = 2;
            dgvYeuCau.CellDoubleClick += dgvYeuCau_CellDoubleClick;
            // 
            // btnDuyet
            // 
            btnDuyet.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDuyet.Location = new Point(20, 412);
            btnDuyet.Name = "btnDuyet";
            btnDuyet.Size = new Size(170, 40);
            btnDuyet.TabIndex = 3;
            btnDuyet.Text = "Duyệt yêu cầu";
            btnDuyet.UseVisualStyleBackColor = true;
            btnDuyet.Click += btnDuyet_Click;
            // 
            // btnTuChoi
            // 
            btnTuChoi.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnTuChoi.Location = new Point(200, 412);
            btnTuChoi.Name = "btnTuChoi";
            btnTuChoi.Size = new Size(150, 40);
            btnTuChoi.TabIndex = 4;
            btnTuChoi.Text = "Từ chối";
            btnTuChoi.UseVisualStyleBackColor = true;
            btnTuChoi.Click += btnTuChoi_Click;
            // 
            // btnDong
            // 
            btnDong.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnDong.DialogResult = DialogResult.Cancel;
            btnDong.Location = new Point(710, 412);
            btnDong.Name = "btnDong";
            btnDong.Size = new Size(150, 40);
            btnDong.TabIndex = 5;
            btnDong.Text = "Đóng";
            btnDong.UseVisualStyleBackColor = true;
            // 
            // DlgDuyetYeuCau
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = btnDong;
            ClientSize = new Size(880, 470);
            Controls.Add(lblTieuDe);
            Controls.Add(lblDem);
            Controls.Add(dgvYeuCau);
            Controls.Add(btnDuyet);
            Controls.Add(btnTuChoi);
            Controls.Add(btnDong);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DlgDuyetYeuCau";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Duyệt yêu cầu mượn sách";
            Load += DlgDuyetYeuCau_Load;
            ((System.ComponentModel.ISupportInitialize)dgvYeuCau).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTieuDe;
        private Label lblDem;
        private DataGridView dgvYeuCau;
        private Button btnDuyet;
        private Button btnTuChoi;
        private Button btnDong;
    }
}