namespace QuanLyThuVien.Forms
{
    partial class FrmMain
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
            statusStrip1 = new StatusStrip();
            lblKetNoi = new ToolStripStatusLabel();
            lblDongHo = new ToolStripStatusLabel();
            timerDongHo = new System.Windows.Forms.Timer(components);
            pnlContent = new Panel();
            pnlHeader = new Panel();
            btnDangXuat = new Button();
            lblXinChao = new Label();
            lblPhanHe = new Label();
            lblTieuDe = new Label();
            pnlSidebar = new Panel();
            flpMenu = new FlowLayoutPanel();
            btnQuanLySach = new Button();
            btnDocGia = new Button();
            btnMuonSach = new Button();
            btnTraSach = new Button();
            btnBaoCao = new Button();
            lblLogo = new Label();
            statusStrip1.SuspendLayout();
            pnlHeader.SuspendLayout();
            pnlSidebar.SuspendLayout();
            flpMenu.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblKetNoi, lblDongHo });
            statusStrip1.Location = new Point(0, 638);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 16, 0);
            statusStrip1.Size = new Size(1098, 26);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblKetNoi
            // 
            lblKetNoi.AutoSize = false;
            lblKetNoi.Name = "lblKetNoi";
            lblKetNoi.Size = new Size(1037, 20);
            lblKetNoi.Spring = true;
            lblKetNoi.Text = "Đang kiểm tra...";
            lblKetNoi.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDongHo
            // 
            lblDongHo.Name = "lblDongHo";
            lblDongHo.Size = new Size(44, 20);
            lblDongHo.Text = "timer";
            // 
            // timerDongHo
            // 
            timerDongHo.Interval = 1000;
            timerDongHo.Tick += timerDongHo_Tick;
            // 
            // pnlContent
            // 
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(220, 64);
            pnlContent.Margin = new Padding(3, 4, 3, 4);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(878, 574);
            pnlContent.TabIndex = 2;
            // 
            // pnlHeader
            // 
            pnlHeader.Controls.Add(btnDangXuat);
            pnlHeader.Controls.Add(lblXinChao);
            pnlHeader.Controls.Add(lblPhanHe);
            pnlHeader.Controls.Add(lblTieuDe);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1098, 64);
            pnlHeader.TabIndex = 0;
            // 
            // btnDangXuat
            // 
            btnDangXuat.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDangXuat.Location = new Point(990, 15);
            btnDangXuat.Name = "btnDangXuat";
            btnDangXuat.Size = new Size(100, 34);
            btnDangXuat.TabIndex = 3;
            btnDangXuat.Text = "Đăng xuất";
            btnDangXuat.UseVisualStyleBackColor = true;
            btnDangXuat.Click += btnDangXuat_Click;
            // 
            // lblXinChao
            // 
            lblXinChao.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblXinChao.Location = new Point(560, 20);
            lblXinChao.Name = "lblXinChao";
            lblXinChao.Size = new Size(420, 24);
            lblXinChao.TabIndex = 2;
            lblXinChao.Text = "Xin chào:";
            lblXinChao.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblPhanHe
            // 
            lblPhanHe.AutoSize = true;
            lblPhanHe.Location = new Point(17, 38);
            lblPhanHe.Name = "lblPhanHe";
            lblPhanHe.Size = new Size(73, 20);
            lblPhanHe.TabIndex = 1;
            lblPhanHe.Text = "Màn hình:";
            // 
            // lblTieuDe
            // 
            lblTieuDe.AutoSize = true;
            lblTieuDe.Location = new Point(15, 8);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(394, 20);
            lblTieuDe.TabIndex = 0;
            lblTieuDe.Text = "HỆ THỐNG QUẢN LÝ THƯ VIỆN - ĐH NÔNG LÂM TP. HCM";
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = Color.FromArgb(0, 102, 51);
            pnlSidebar.Controls.Add(flpMenu);
            pnlSidebar.Controls.Add(lblLogo);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 64);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(220, 574);
            pnlSidebar.TabIndex = 3;
            // 
            // flpMenu
            // 
            flpMenu.Controls.Add(btnQuanLySach);
            flpMenu.Controls.Add(btnDocGia);
            flpMenu.Controls.Add(btnMuonSach);
            flpMenu.Controls.Add(btnTraSach);
            flpMenu.Controls.Add(btnBaoCao);
            flpMenu.Dock = DockStyle.Fill;
            flpMenu.FlowDirection = FlowDirection.TopDown;
            flpMenu.Location = new Point(0, 70);
            flpMenu.Name = "flpMenu";
            flpMenu.Size = new Size(220, 504);
            flpMenu.TabIndex = 1;
            flpMenu.WrapContents = false;
            // 
            // btnQuanLySach
            // 
            btnQuanLySach.Location = new Point(0, 0);
            btnQuanLySach.Margin = new Padding(0);
            btnQuanLySach.Name = "btnQuanLySach";
            btnQuanLySach.Size = new Size(220, 50);
            btnQuanLySach.TabIndex = 0;
            btnQuanLySach.Text = "📚 Quản lý Sách";
            btnQuanLySach.UseVisualStyleBackColor = true;
            btnQuanLySach.Click += btnQuanLySach_Click;
            // 
            // btnDocGia
            // 
            btnDocGia.Location = new Point(0, 50);
            btnDocGia.Margin = new Padding(0);
            btnDocGia.Name = "btnDocGia";
            btnDocGia.Size = new Size(220, 50);
            btnDocGia.TabIndex = 1;
            btnDocGia.Text = "👤 Quản lý Độc giả";
            btnDocGia.UseVisualStyleBackColor = true;
            btnDocGia.Click += btnDocGia_Click;
            // 
            // btnMuonSach
            // 
            btnMuonSach.Location = new Point(0, 100);
            btnMuonSach.Margin = new Padding(0);
            btnMuonSach.Name = "btnMuonSach";
            btnMuonSach.Size = new Size(220, 50);
            btnMuonSach.TabIndex = 2;
            btnMuonSach.Text = "📥 Mượn sách";
            btnMuonSach.UseVisualStyleBackColor = true;
            btnMuonSach.Click += btnMuonSach_Click;
            // 
            // btnTraSach
            // 
            btnTraSach.Location = new Point(0, 150);
            btnTraSach.Margin = new Padding(0);
            btnTraSach.Name = "btnTraSach";
            btnTraSach.Size = new Size(220, 50);
            btnTraSach.TabIndex = 3;
            btnTraSach.Text = "📤 Trả sách && Phạt";
            btnTraSach.UseVisualStyleBackColor = true;
            btnTraSach.Click += btnTraSach_Click;
            // 
            // btnBaoCao
            // 
            btnBaoCao.Location = new Point(0, 200);
            btnBaoCao.Margin = new Padding(0);
            btnBaoCao.Name = "btnBaoCao";
            btnBaoCao.Size = new Size(220, 50);
            btnBaoCao.TabIndex = 4;
            btnBaoCao.Text = "📊 Báo cáo && Tổng quan";
            btnBaoCao.UseVisualStyleBackColor = true;
            btnBaoCao.Click += btnBaoCao_Click;
            // 
            // lblLogo
            // 
            lblLogo.Dock = DockStyle.Top;
            lblLogo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblLogo.ForeColor = Color.White;
            lblLogo.Location = new Point(0, 0);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(220, 70);
            lblLogo.TabIndex = 0;
            lblLogo.Text = "[📖] THƯ VIỆN NLU";
            lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1098, 664);
            Controls.Add(pnlContent);
            Controls.Add(pnlSidebar);
            Controls.Add(pnlHeader);
            Controls.Add(statusStrip1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FrmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý Thư viện";
            WindowState = FormWindowState.Maximized;
            FormClosing += FrmMain_FormClosing;
            Load += FrmMain_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlSidebar.ResumeLayout(false);
            flpMenu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblKetNoi;
        private ToolStripStatusLabel lblDongHo;
        private System.Windows.Forms.Timer timerDongHo;
        private Panel pnlContent;
        private Panel pnlSidebar;
        private Label lblLogo;
        private FlowLayoutPanel flpMenu;
        private Button btnQuanLySach;
        private Button btnDocGia;
        private Button btnMuonSach;
        private Button btnTraSach;
        private Button btnBaoCao;
        private Panel pnlHeader;
        private Button btnDangXuat;
        private Label lblXinChao;
        private Label lblPhanHe;
        private Label lblTieuDe;
    }
}