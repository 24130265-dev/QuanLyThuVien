namespace QuanLyThuVien.Forms
{
    partial class FrmDocGiaPortal
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
            components = new System.ComponentModel.Container();
            pnlHeader = new Panel();
            lblTieuDe = new Label();
            lblThongTin = new Label();
            lblTienNoPhat = new Label();
            btnDoiMatKhau = new Button();
            btnDangXuat = new Button();
            tabControl1 = new TabControl();
            tabTraCuu = new TabPage();
            grpDanhSach = new GroupBox();
            dgvSachThuVien = new DataGridView();
            grpAction = new GroupBox();
            lblSachChon = new Label();
            lblGhiChu = new Label();
            btnGoiYeuCau = new Button();
            grpBoLoc = new GroupBox();
            lblTimKiem = new Label();
            txtTimKiem = new TextBox();
            lblTheLoai = new Label();
            cboTheLoai = new ComboBox();
            btnTimKiem = new Button();
            tabLichSu = new TabPage();
            tlpLichSu = new TableLayoutPanel();
            grpLichSuMuon = new GroupBox();
            dgvLichSuMuon = new DataGridView();
            grpYeuCau = new GroupBox();
            dgvYeuCauCuaToi = new DataGridView();
            grpHoaDon = new GroupBox();
            dgvHoaDon = new DataGridView();
            statusStrip1 = new StatusStrip();
            lblKetNoi = new ToolStripStatusLabel();
            lblDongHo = new ToolStripStatusLabel();
            timerDongHo = new System.Windows.Forms.Timer(components);
            pnlHeader.SuspendLayout();
            tabControl1.SuspendLayout();
            tabTraCuu.SuspendLayout();
            grpDanhSach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSachThuVien).BeginInit();
            grpAction.SuspendLayout();
            grpBoLoc.SuspendLayout();
            tabLichSu.SuspendLayout();
            tlpLichSu.SuspendLayout();
            grpLichSuMuon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLichSuMuon).BeginInit();
            grpYeuCau.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvYeuCauCuaToi).BeginInit();
            grpHoaDon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHoaDon).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.Controls.Add(lblTieuDe);
            pnlHeader.Controls.Add(lblThongTin);
            pnlHeader.Controls.Add(lblTienNoPhat);
            pnlHeader.Controls.Add(btnDoiMatKhau);
            pnlHeader.Controls.Add(btnDangXuat);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1200, 64);
            pnlHeader.TabIndex = 1;
            // 
            // lblTieuDe
            // 
            lblTieuDe.AutoSize = true;
            lblTieuDe.Location = new Point(15, 8);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(300, 20);
            lblTieuDe.TabIndex = 0;
            lblTieuDe.Text = "HỆ THỐNG THƯ VIỆN NLU - CỔNG ĐỘC GIẢ";
            // 
            // lblThongTin
            // 
            lblThongTin.AutoSize = true;
            lblThongTin.Location = new Point(17, 38);
            lblThongTin.Name = "lblThongTin";
            lblThongTin.Size = new Size(70, 20);
            lblThongTin.TabIndex = 1;
            lblThongTin.Text = "Độc giả:";
            // 
            // lblTienNoPhat
            // 
            lblTienNoPhat.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTienNoPhat.Location = new Point(585, 20);
            lblTienNoPhat.Name = "lblTienNoPhat";
            lblTienNoPhat.Size = new Size(310, 24);
            lblTienNoPhat.TabIndex = 2;
            lblTienNoPhat.Text = "Tiền nợ phạt: 0 đ";
            lblTienNoPhat.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnDoiMatKhau
            // 
            btnDoiMatKhau.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDoiMatKhau.Location = new Point(905, 15);
            btnDoiMatKhau.Name = "btnDoiMatKhau";
            btnDoiMatKhau.Size = new Size(150, 34);
            btnDoiMatKhau.TabIndex = 3;
            btnDoiMatKhau.Text = "Đổi mật khẩu";
            btnDoiMatKhau.UseVisualStyleBackColor = true;
            btnDoiMatKhau.Click += btnDoiMatKhau_Click;
            // 
            // btnDangXuat
            // 
            btnDangXuat.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDangXuat.Location = new Point(1065, 15);
            btnDangXuat.Name = "btnDangXuat";
            btnDangXuat.Size = new Size(125, 34);
            btnDangXuat.TabIndex = 4;
            btnDangXuat.Text = "Đăng xuất";
            btnDangXuat.UseVisualStyleBackColor = true;
            btnDangXuat.Click += btnDangXuat_Click;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabTraCuu);
            tabControl1.Controls.Add(tabLichSu);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 64);
            tabControl1.Name = "tabControl1";
            tabControl1.Padding = new Point(20, 8);
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1200, 634);
            tabControl1.TabIndex = 0;
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            // 
            // tabTraCuu
            // 
            tabTraCuu.Controls.Add(grpDanhSach);
            tabTraCuu.Controls.Add(grpAction);
            tabTraCuu.Controls.Add(grpBoLoc);
            tabTraCuu.Location = new Point(4, 40);
            tabTraCuu.Name = "tabTraCuu";
            tabTraCuu.Padding = new Padding(12);
            tabTraCuu.Size = new Size(1192, 590);
            tabTraCuu.TabIndex = 0;
            tabTraCuu.Text = "Tra cứu && Yêu cầu mượn";
            tabTraCuu.UseVisualStyleBackColor = true;
            // 
            // grpDanhSach
            // 
            grpDanhSach.Controls.Add(dgvSachThuVien);
            grpDanhSach.Dock = DockStyle.Fill;
            grpDanhSach.Location = new Point(12, 92);
            grpDanhSach.Name = "grpDanhSach";
            grpDanhSach.Size = new Size(1168, 386);
            grpDanhSach.TabIndex = 2;
            grpDanhSach.TabStop = false;
            grpDanhSach.Text = "DANH SÁCH ĐẦU SÁCH SẴN CÓ TRÊN KỆ";
            // 
            // dgvSachThuVien
            // 
            dgvSachThuVien.Dock = DockStyle.Fill;
            dgvSachThuVien.Location = new Point(3, 23);
            dgvSachThuVien.Name = "dgvSachThuVien";
            dgvSachThuVien.RowHeadersWidth = 51;
            dgvSachThuVien.Size = new Size(1162, 360);
            dgvSachThuVien.TabIndex = 0;
            dgvSachThuVien.SelectionChanged += dgvSachThuVien_SelectionChanged;
            // 
            // grpAction
            // 
            grpAction.Controls.Add(lblSachChon);
            grpAction.Controls.Add(lblGhiChu);
            grpAction.Controls.Add(btnGoiYeuCau);
            grpAction.Dock = DockStyle.Bottom;
            grpAction.Location = new Point(12, 478);
            grpAction.Name = "grpAction";
            grpAction.Size = new Size(1168, 100);
            grpAction.TabIndex = 1;
            grpAction.TabStop = false;
            grpAction.Text = "Gửi yêu cầu mượn";
            // 
            // lblSachChon
            // 
            lblSachChon.AutoSize = true;
            lblSachChon.Location = new Point(20, 34);
            lblSachChon.Name = "lblSachChon";
            lblSachChon.Size = new Size(300, 20);
            lblSachChon.TabIndex = 0;
            lblSachChon.Text = "Đầu sách đang chọn: (chưa chọn)";
            // 
            // lblGhiChu
            // 
            lblGhiChu.AutoSize = true;
            lblGhiChu.Location = new Point(20, 62);
            lblGhiChu.Name = "lblGhiChu";
            lblGhiChu.Size = new Size(600, 20);
            lblGhiChu.TabIndex = 1;
            lblGhiChu.Text = "Yêu cầu của bạn sẽ ở trạng thái \"Chờ duyệt\". Hãy nhận sách trực tiếp tại quầy Thủ thư khi được duyệt.";
            // 
            // btnGoiYeuCau
            // 
            btnGoiYeuCau.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnGoiYeuCau.Location = new Point(800, 30);
            btnGoiYeuCau.Name = "btnGoiYeuCau";
            btnGoiYeuCau.Size = new Size(340, 44);
            btnGoiYeuCau.TabIndex = 2;
            btnGoiYeuCau.Text = "GỬI YÊU CẦU MƯỢN ĐẦU SÁCH ĐANG CHỌN";
            btnGoiYeuCau.UseVisualStyleBackColor = true;
            btnGoiYeuCau.Click += btnGoiYeuCau_Click;
            // 
            // grpBoLoc
            // 
            grpBoLoc.Controls.Add(lblTimKiem);
            grpBoLoc.Controls.Add(txtTimKiem);
            grpBoLoc.Controls.Add(lblTheLoai);
            grpBoLoc.Controls.Add(cboTheLoai);
            grpBoLoc.Controls.Add(btnTimKiem);
            grpBoLoc.Dock = DockStyle.Top;
            grpBoLoc.Location = new Point(12, 12);
            grpBoLoc.Name = "grpBoLoc";
            grpBoLoc.Size = new Size(1168, 80);
            grpBoLoc.TabIndex = 0;
            grpBoLoc.TabStop = false;
            grpBoLoc.Text = "Bộ lọc && Tìm kiếm sách";
            // 
            // lblTimKiem
            // 
            lblTimKiem.AutoSize = true;
            lblTimKiem.Location = new Point(20, 38);
            lblTimKiem.Name = "lblTimKiem";
            lblTimKiem.Size = new Size(170, 20);
            lblTimKiem.TabIndex = 0;
            lblTimKiem.Text = "Tìm tên sách / tác giả:";
            // 
            // txtTimKiem
            // 
            txtTimKiem.Location = new Point(215, 34);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.PlaceholderText = "Nhập tên sách, tác giả...";
            txtTimKiem.Size = new Size(260, 27);
            txtTimKiem.TabIndex = 1;
            txtTimKiem.KeyDown += txtTimKiem_KeyDown;
            // 
            // lblTheLoai
            // 
            lblTheLoai.AutoSize = true;
            lblTheLoai.Location = new Point(505, 38);
            lblTheLoai.Name = "lblTheLoai";
            lblTheLoai.Size = new Size(70, 20);
            lblTheLoai.TabIndex = 2;
            lblTheLoai.Text = "Thể loại:";
            // 
            // cboTheLoai
            // 
            cboTheLoai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTheLoai.FormattingEnabled = true;
            cboTheLoai.Location = new Point(590, 34);
            cboTheLoai.Name = "cboTheLoai";
            cboTheLoai.Size = new Size(260, 28);
            cboTheLoai.TabIndex = 3;
            cboTheLoai.SelectedIndexChanged += cboTheLoai_SelectedIndexChanged;
            // 
            // btnTimKiem
            // 
            btnTimKiem.Location = new Point(875, 30);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new Size(140, 36);
            btnTimKiem.TabIndex = 4;
            btnTimKiem.Text = "Tìm kiếm";
            btnTimKiem.UseVisualStyleBackColor = true;
            btnTimKiem.Click += btnTimKiem_Click;
            // 
            // tabLichSu
            // 
            tabLichSu.Controls.Add(tlpLichSu);
            tabLichSu.Location = new Point(4, 40);
            tabLichSu.Name = "tabLichSu";
            tabLichSu.Padding = new Padding(12);
            tabLichSu.Size = new Size(1192, 590);
            tabLichSu.TabIndex = 1;
            tabLichSu.Text = "Lịch sử mượn && Nợ phạt";
            tabLichSu.UseVisualStyleBackColor = true;
            // 
            // tlpLichSu
            // 
            tlpLichSu.ColumnCount = 1;
            tlpLichSu.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpLichSu.Controls.Add(grpLichSuMuon, 0, 0);
            tlpLichSu.Controls.Add(grpYeuCau, 0, 1);
            tlpLichSu.Controls.Add(grpHoaDon, 0, 2);
            tlpLichSu.Dock = DockStyle.Fill;
            tlpLichSu.Location = new Point(12, 12);
            tlpLichSu.Name = "tlpLichSu";
            tlpLichSu.RowCount = 3;
            tlpLichSu.RowStyles.Add(new RowStyle(SizeType.Percent, 42F));
            tlpLichSu.RowStyles.Add(new RowStyle(SizeType.Percent, 24F));
            tlpLichSu.RowStyles.Add(new RowStyle(SizeType.Percent, 34F));
            tlpLichSu.Size = new Size(1168, 566);
            tlpLichSu.TabIndex = 0;
            // 
            // grpLichSuMuon
            // 
            grpLichSuMuon.Controls.Add(dgvLichSuMuon);
            grpLichSuMuon.Dock = DockStyle.Fill;
            grpLichSuMuon.Location = new Point(3, 3);
            grpLichSuMuon.Name = "grpLichSuMuon";
            grpLichSuMuon.Size = new Size(1162, 232);
            grpLichSuMuon.TabIndex = 0;
            grpLichSuMuon.TabStop = false;
            grpLichSuMuon.Text = "LỊCH SỬ MƯỢN SÁCH";
            // 
            // dgvLichSuMuon
            // 
            dgvLichSuMuon.Dock = DockStyle.Fill;
            dgvLichSuMuon.Location = new Point(3, 23);
            dgvLichSuMuon.Name = "dgvLichSuMuon";
            dgvLichSuMuon.RowHeadersWidth = 51;
            dgvLichSuMuon.Size = new Size(1156, 206);
            dgvLichSuMuon.TabIndex = 0;
            // 
            // grpYeuCau
            // 
            grpYeuCau.Controls.Add(dgvYeuCauCuaToi);
            grpYeuCau.Dock = DockStyle.Fill;
            grpYeuCau.Location = new Point(3, 241);
            grpYeuCau.Name = "grpYeuCau";
            grpYeuCau.Size = new Size(1162, 129);
            grpYeuCau.TabIndex = 1;
            grpYeuCau.TabStop = false;
            grpYeuCau.Text = "YÊU CẦU MƯỢN ĐÃ GỬI";
            // 
            // dgvYeuCauCuaToi
            // 
            dgvYeuCauCuaToi.Dock = DockStyle.Fill;
            dgvYeuCauCuaToi.Location = new Point(3, 23);
            dgvYeuCauCuaToi.Name = "dgvYeuCauCuaToi";
            dgvYeuCauCuaToi.RowHeadersWidth = 51;
            dgvYeuCauCuaToi.Size = new Size(1156, 103);
            dgvYeuCauCuaToi.TabIndex = 0;
            // 
            // grpHoaDon
            // 
            grpHoaDon.Controls.Add(dgvHoaDon);
            grpHoaDon.Dock = DockStyle.Fill;
            grpHoaDon.Location = new Point(3, 376);
            grpHoaDon.Name = "grpHoaDon";
            grpHoaDon.Size = new Size(1162, 187);
            grpHoaDon.TabIndex = 2;
            grpHoaDon.TabStop = false;
            grpHoaDon.Text = "HÓA ĐƠN PHẠT CỦA TÔI";
            // 
            // dgvHoaDon
            // 
            dgvHoaDon.Dock = DockStyle.Fill;
            dgvHoaDon.Location = new Point(3, 23);
            dgvHoaDon.Name = "dgvHoaDon";
            dgvHoaDon.RowHeadersWidth = 51;
            dgvHoaDon.Size = new Size(1156, 161);
            dgvHoaDon.TabIndex = 0;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblKetNoi, lblDongHo });
            statusStrip1.Location = new Point(0, 698);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1200, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblKetNoi
            // 
            lblKetNoi.Name = "lblKetNoi";
            lblKetNoi.Size = new Size(1100, 16);
            lblKetNoi.Spring = true;
            lblKetNoi.Text = "Đang kiểm tra...";
            lblKetNoi.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDongHo
            // 
            lblDongHo.Name = "lblDongHo";
            lblDongHo.Size = new Size(35, 16);
            lblDongHo.Text = "timer";
            // 
            // timerDongHo
            // 
            timerDongHo.Interval = 1000;
            timerDongHo.Tick += timerDongHo_Tick;
            // 
            // FrmDocGiaPortal
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1200, 720);
            Controls.Add(tabControl1);
            Controls.Add(pnlHeader);
            Controls.Add(statusStrip1);
            MinimumSize = new Size(1000, 640);
            Name = "FrmDocGiaPortal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cổng thông tin Độc giả - Thư viện NLU";
            FormClosing += FrmDocGiaPortal_FormClosing;
            Load += FrmDocGiaPortal_Load;
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabTraCuu.ResumeLayout(false);
            grpDanhSach.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvSachThuVien).EndInit();
            grpAction.ResumeLayout(false);
            grpAction.PerformLayout();
            grpBoLoc.ResumeLayout(false);
            grpBoLoc.PerformLayout();
            tabLichSu.ResumeLayout(false);
            tlpLichSu.ResumeLayout(false);
            grpLichSuMuon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvLichSuMuon).EndInit();
            grpYeuCau.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvYeuCauCuaToi).EndInit();
            grpHoaDon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvHoaDon).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlHeader;
        private Label lblTieuDe;
        private Label lblThongTin;
        private Label lblTienNoPhat;
        private Button btnDoiMatKhau;
        private Button btnDangXuat;
        private TabControl tabControl1;
        private TabPage tabTraCuu;
        private TabPage tabLichSu;
        private GroupBox grpBoLoc;
        private Label lblTimKiem;
        private TextBox txtTimKiem;
        private Label lblTheLoai;
        private ComboBox cboTheLoai;
        private Button btnTimKiem;
        private GroupBox grpDanhSach;
        private DataGridView dgvSachThuVien;
        private GroupBox grpAction;
        private Label lblSachChon;
        private Label lblGhiChu;
        private Button btnGoiYeuCau;
        private TableLayoutPanel tlpLichSu;
        private GroupBox grpLichSuMuon;
        private DataGridView dgvLichSuMuon;
        private GroupBox grpYeuCau;
        private DataGridView dgvYeuCauCuaToi;
        private GroupBox grpHoaDon;
        private DataGridView dgvHoaDon;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblKetNoi;
        private ToolStripStatusLabel lblDongHo;
        private System.Windows.Forms.Timer timerDongHo;
    }
}