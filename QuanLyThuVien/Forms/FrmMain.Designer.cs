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
            menuStrip1 = new MenuStrip();
            mnuQuanLySach = new ToolStripMenuItem();
            mnuDocGia = new ToolStripMenuItem();
            mnuMuonSach = new ToolStripMenuItem();
            mnuTraSach = new ToolStripMenuItem();
            mnuBaoCao = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            lblKetNoi = new ToolStripStatusLabel();
            lblDongHo = new ToolStripStatusLabel();
            timerDongHo = new System.Windows.Forms.Timer(components);
            pnlContent = new Panel();
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { mnuQuanLySach, mnuDocGia, mnuMuonSach, mnuTraSach, mnuBaoCao });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // mnuQuanLySach
            // 
            mnuQuanLySach.Name = "mnuQuanLySach";
            mnuQuanLySach.Size = new Size(88, 20);
            mnuQuanLySach.Text = "Quản lý Sách";
            // 
            // mnuDocGia
            // 
            mnuDocGia.Name = "mnuDocGia";
            mnuDocGia.Size = new Size(103, 20);
            mnuDocGia.Text = "Quản lý Độc giả";
            // 
            // mnuMuonSach
            // 
            mnuMuonSach.Name = "mnuMuonSach";
            mnuMuonSach.Size = new Size(78, 20);
            mnuMuonSach.Text = "Mượn sách";
            // 
            // mnuTraSach
            // 
            mnuTraSach.Name = "mnuTraSach";
            mnuTraSach.Size = new Size(102, 20);
            mnuTraSach.Text = "Trả sách && Phạt";
            // 
            // mnuBaoCao
            // 
            mnuBaoCao.Name = "mnuBaoCao";
            mnuBaoCao.Size = new Size(111, 20);
            mnuBaoCao.Text = "Báo cáo thống kê";
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblKetNoi, lblDongHo });
            statusStrip1.Location = new Point(0, 428);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(800, 22);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblKetNoi
            // 
            lblKetNoi.Name = "lblKetNoi";
            lblKetNoi.Size = new Size(636, 17);
            lblKetNoi.Spring = true;
            lblKetNoi.Text = "Đang kiểm tra...";
            lblKetNoi.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDongHo
            // 
            lblDongHo.Name = "lblDongHo";
            lblDongHo.Size = new Size(118, 17);
            lblDongHo.Text = "toolStripStatusLabel2";
            // 
            // timerDongHo
            // 
            timerDongHo.Interval = 1000;
            // 
            // pnlContent
            // 
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 24);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(800, 404);
            pnlContent.TabIndex = 2;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pnlContent);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "FrmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý Thư viện";
            WindowState = FormWindowState.Maximized;
            Load += FrmMain_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem mnuQuanLySach;
        private ToolStripMenuItem mnuDocGia;
        private ToolStripMenuItem mnuMuonSach;
        private ToolStripMenuItem mnuTraSach;
        private ToolStripMenuItem mnuBaoCao;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblKetNoi;
        private ToolStripStatusLabel lblDongHo;
        private System.Windows.Forms.Timer timerDongHo;
        private Panel pnlContent;
    }
}