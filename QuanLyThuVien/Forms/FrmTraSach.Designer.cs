using System.ComponentModel;

namespace QuanLyThuVien.Forms;

partial class FrmTraSach
{
    private System.ComponentModel.IContainer components = null;
    
    private Panel pnlTopFilter;
    private Label lblTitle;
    private Label lblTimKiem;
    private TextBox txtTimKiem;
    private Button btnTimKiem;
    private ComboBox cboPhieuMuon;
    private Label lblChonPhieu;

    private SplitContainer splitMain;
    private GroupBox grpChiTietMuon;
    private DataGridView dgvChiTietMuon;
    private GroupBox grpThongTinPhat;

    private Label lblDonGiaTre;
    private NumericUpDown numDonGiaTre;
    private Label lblTienPhatHuHong;
    private NumericUpDown numTienPhatHuHong;
    private Label lblLyDoPhat;
    private TextBox txtLyDoPhat;
    private Label lblTongTienPhatTitle;
    private Label lblTongTienPhat;
    private CheckBox chkDaThanhToanNgay;
    private Button btnXacNhanTra;

    private GroupBox grpHoaDonPhat;
    private DataGridView dgvHoaDonPhat;
    private Button btnThanhToanHoaDon;
    
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }
    
    private void InitializeComponent()
    {
      this.pnlTopFilter = new Panel();
            this.lblTitle = new Label();
            this.lblTimKiem = new Label();
            this.txtTimKiem = new TextBox();
            this.btnTimKiem = new Button();
            this.lblChonPhieu = new Label();
            this.cboPhieuMuon = new ComboBox();

            this.splitMain = new SplitContainer();
            this.grpChiTietMuon = new GroupBox();
            this.dgvChiTietMuon = new DataGridView();
            this.grpThongTinPhat = new GroupBox();

            this.lblDonGiaTre = new Label();
            this.numDonGiaTre = new NumericUpDown();
            this.lblTienPhatHuHong = new Label();
            this.numTienPhatHuHong = new NumericUpDown();
            this.lblLyDoPhat = new Label();
            this.txtLyDoPhat = new TextBox();
            this.lblTongTienPhatTitle = new Label();
            this.lblTongTienPhat = new Label();
            this.chkDaThanhToanNgay = new CheckBox();
            this.btnXacNhanTra = new Button();

            this.grpHoaDonPhat = new GroupBox();
            this.dgvHoaDonPhat = new DataGridView();
            this.btnThanhToanHoaDon = new Button();

            this.pnlTopFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.grpChiTietMuon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietMuon)).BeginInit();
            this.grpThongTinPhat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDonGiaTre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTienPhatHuHong)).BeginInit();
            this.grpHoaDonPhat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDonPhat)).BeginInit();
            this.SuspendLayout();

            // 
            // pnlTopFilter
            // 
            this.pnlTopFilter.BackColor = Color.FromArgb(245, 247, 250);
            this.pnlTopFilter.Dock = DockStyle.Top;
            this.pnlTopFilter.Height = 95;
            this.pnlTopFilter.Padding = new Padding(15, 10, 15, 10);
            this.pnlTopFilter.Controls.AddRange(new Control[] {
                this.lblTitle, this.lblTimKiem, this.txtTimKiem, this.btnTimKiem, this.lblChonPhieu, this.cboPhieuMuon
            });

            this.lblTitle.Text = "QUẢN LÝ TRẢ SÁCH & XỬ LÝ TIỀN PHẠT";
            this.lblTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(30, 58, 138);
            this.lblTitle.Location = new Point(15, 10);
            this.lblTitle.AutoSize = true;

            this.lblTimKiem.Text = "Độc giả / Mã phiếu:";
            this.lblTimKiem.Font = new Font("Segoe UI", 9.5F);
            this.lblTimKiem.Location = new Point(15, 52);
            this.lblTimKiem.AutoSize = true;

            this.txtTimKiem.Font = new Font("Segoe UI", 9.5F);
            this.txtTimKiem.Location = new Point(145, 49);
            this.txtTimKiem.Size = new Size(220, 25);

            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.BackColor = Color.FromArgb(37, 99, 235);
            this.btnTimKiem.ForeColor = Color.White;
            this.btnTimKiem.FlatStyle = FlatStyle.Flat;
            this.btnTimKiem.Location = new Point(375, 48);
            this.btnTimKiem.Size = new Size(95, 27);
            this.btnTimKiem.Click += new EventHandler(this.btnTimKiem_Click);

            this.lblChonPhieu.Text = "Phiếu đang mượn:";
            this.lblChonPhieu.Font = new Font("Segoe UI", 9.5F);
            this.lblChonPhieu.Location = new Point(495, 52);
            this.lblChonPhieu.AutoSize = true;

            this.cboPhieuMuon.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboPhieuMuon.Font = new Font("Segoe UI", 9.5F);
            this.cboPhieuMuon.Location = new Point(620, 49);
            this.cboPhieuMuon.Size = new Size(360, 25);
            this.cboPhieuMuon.SelectedIndexChanged += new EventHandler(this.cboPhieuMuon_SelectedIndexChanged);

            // 
            // splitMain
            // 
            this.splitMain.Dock = DockStyle.Fill;
            this.splitMain.Orientation = Orientation.Horizontal;
            this.splitMain.SplitterDistance = 360;
            this.splitMain.Panel1.Padding = new Padding(15, 10, 15, 5);
            this.splitMain.Panel2.Padding = new Padding(15, 5, 15, 15);
            this.splitMain.Panel1.Controls.Add(this.grpChiTietMuon);
            this.splitMain.Panel1.Controls.Add(this.grpThongTinPhat);
            this.splitMain.Panel2.Controls.Add(this.grpHoaDonPhat);

            // 
            // grpThongTinPhat (Bên phải Panel1)
            // 
            this.grpThongTinPhat.Text = "Tính phí phạt & Xác nhận trả";
            this.grpThongTinPhat.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.grpThongTinPhat.Dock = DockStyle.Right;
            this.grpThongTinPhat.Width = 340;
            this.grpThongTinPhat.Controls.AddRange(new Control[] {
                this.lblDonGiaTre, this.numDonGiaTre,
                this.lblTienPhatHuHong, this.numTienPhatHuHong,
                this.lblLyDoPhat, this.txtLyDoPhat,
                this.lblTongTienPhatTitle, this.lblTongTienPhat,
                this.chkDaThanhToanNgay, this.btnXacNhanTra
            });

            this.lblDonGiaTre.Text = "Phạt trễ hạn (VNĐ/ngày):";
            this.lblDonGiaTre.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.lblDonGiaTre.Location = new Point(15, 32);
            this.lblDonGiaTre.AutoSize = true;

            this.numDonGiaTre.Font = new Font("Segoe UI", 9.5F);
            this.numDonGiaTre.Maximum = 1000000;
            this.numDonGiaTre.Increment = 1000;
            this.numDonGiaTre.Value = 5000;
            this.numDonGiaTre.ThousandsSeparator = true;
            this.numDonGiaTre.Location = new Point(185, 29);
            this.numDonGiaTre.Size = new Size(135, 25);
            this.numDonGiaTre.ValueChanged += (s, e) => TinhTongTienPhat();

            this.lblTienPhatHuHong.Text = "Phạt hư hỏng / mất (VNĐ):";
            this.lblTienPhatHuHong.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.lblTienPhatHuHong.Location = new Point(15, 68);
            this.lblTienPhatHuHong.AutoSize = true;

            this.numTienPhatHuHong.Font = new Font("Segoe UI", 9.5F);
            this.numTienPhatHuHong.Maximum = 50000000;
            this.numTienPhatHuHong.Increment = 5000;
            this.numTienPhatHuHong.ThousandsSeparator = true;
            this.numTienPhatHuHong.Location = new Point(185, 65);
            this.numTienPhatHuHong.Size = new Size(135, 25);
            this.numTienPhatHuHong.ValueChanged += (s, e) => TinhTongTienPhat();

            this.lblLyDoPhat.Text = "Ghi chú / Lý do phạt:";
            this.lblLyDoPhat.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.lblLyDoPhat.Location = new Point(15, 102);
            this.lblLyDoPhat.AutoSize = true;

            this.txtLyDoPhat.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.txtLyDoPhat.Multiline = true;
            this.txtLyDoPhat.Location = new Point(15, 124);
            this.txtLyDoPhat.Size = new Size(305, 52);

            this.lblTongTienPhatTitle.Text = "TỔNG TIỀN PHẠT:";
            this.lblTongTienPhatTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblTongTienPhatTitle.Location = new Point(15, 190);
            this.lblTongTienPhatTitle.AutoSize = true;

            this.lblTongTienPhat.Text = "0 VNĐ";
            this.lblTongTienPhat.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblTongTienPhat.ForeColor = Color.FromArgb(220, 38, 38);
            this.lblTongTienPhat.Location = new Point(155, 186);
            this.lblTongTienPhat.AutoSize = true;

            this.chkDaThanhToanNgay.Text = "Độc giả thanh toán phạt ngay";
            this.chkDaThanhToanNgay.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.chkDaThanhToanNgay.Checked = true;
            this.chkDaThanhToanNgay.Location = new Point(15, 222);
            this.chkDaThanhToanNgay.AutoSize = true;

            this.btnXacNhanTra.Text = "XÁC NHẬN TRẢ SÁCH";
            this.btnXacNhanTra.BackColor = Color.FromArgb(22, 163, 74);
            this.btnXacNhanTra.ForeColor = Color.White;
            this.btnXacNhanTra.FlatStyle = FlatStyle.Flat;
            this.btnXacNhanTra.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnXacNhanTra.Location = new Point(15, 255);
            this.btnXacNhanTra.Size = new Size(305, 42);
            this.btnXacNhanTra.Click += new EventHandler(this.btnXacNhanTra_Click);

            // 
            // grpChiTietMuon (Bên trái Panel1)
            // 
            this.grpChiTietMuon.Text = "Danh sách cuốn sách trong phiếu mượn (Tích chọn sách trả)";
            this.grpChiTietMuon.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.grpChiTietMuon.Dock = DockStyle.Fill;
            this.grpChiTietMuon.Controls.Add(this.dgvChiTietMuon);

            this.dgvChiTietMuon.Dock = DockStyle.Fill;
            this.dgvChiTietMuon.BackgroundColor = Color.White;
            this.dgvChiTietMuon.AllowUserToAddRows = false;
            this.dgvChiTietMuon.RowHeadersVisible = false;
            this.dgvChiTietMuon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvChiTietMuon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTietMuon.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.dgvChiTietMuon.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (dgvChiTietMuon.IsCurrentCellDirty)
                    dgvChiTietMuon.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };
            this.dgvChiTietMuon.CellValueChanged += (s, e) => TinhTongTienPhat();

            // 
            // grpHoaDonPhat (Panel2)
            // 
            this.grpHoaDonPhat.Text = "Danh sách Hóa đơn phạt của độc giả";
            this.grpHoaDonPhat.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.grpHoaDonPhat.Dock = DockStyle.Fill;
            this.grpHoaDonPhat.Controls.Add(this.dgvHoaDonPhat);
            this.grpHoaDonPhat.Controls.Add(this.btnThanhToanHoaDon);

            this.dgvHoaDonPhat.Dock = DockStyle.Fill;
            this.dgvHoaDonPhat.BackgroundColor = Color.White;
            this.dgvHoaDonPhat.AllowUserToAddRows = false;
            this.dgvHoaDonPhat.ReadOnly = true;
            this.dgvHoaDonPhat.RowHeadersVisible = false;
            this.dgvHoaDonPhat.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvHoaDonPhat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHoaDonPhat.Font = new Font("Segoe UI", 9F, FontStyle.Regular);

            this.btnThanhToanHoaDon.Text = "Xác nhận đã thu tiền hóa đơn đang chọn";
            this.btnThanhToanHoaDon.Dock = DockStyle.Bottom;
            this.btnThanhToanHoaDon.Height = 35;
            this.btnThanhToanHoaDon.BackColor = Color.FromArgb(234, 88, 12);
            this.btnThanhToanHoaDon.ForeColor = Color.White;
            this.btnThanhToanHoaDon.FlatStyle = FlatStyle.Flat;
            this.btnThanhToanHoaDon.Click += new EventHandler(this.btnThanhToanHoaDon_Click);

            // 
            // FrmTraSach
            // 
            this.ClientSize = new Size(1050, 680);
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.pnlTopFilter);
            this.Name = "FrmTraSach";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Trả sách & Xử lý phạt - Quản Lý Thư Viện";
            this.Load += new EventHandler(this.FrmTraSach_Load);

            this.pnlTopFilter.ResumeLayout(false);
            this.pnlTopFilter.PerformLayout();
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.grpChiTietMuon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietMuon)).EndInit();
            this.grpThongTinPhat.ResumeLayout(false);
            this.grpThongTinPhat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDonGiaTre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTienPhatHuHong)).EndInit();
            this.grpHoaDonPhat.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDonPhat)).EndInit();
            this.ResumeLayout(false);
        }
    }


