#nullable enable

using System.ComponentModel;
using System.Drawing;

namespace QuanLyThuVien.Forms;

partial class FrmMuonSach
{
    private IContainer? components = null;
    private Label lblTieuDe = null!;
    private Label lblMaDocGia = null!;
    private TextBox txtMaDocGia = null!;
    private Button btnKiemTra = null!;

    private Button btnDuyetYeuCau = null!;
    private Label lblThongTinDocGia = null!;
    private GroupBox grpThongTinDocGia = null!;
    private GroupBox grpChonSach = null!;
    private Label lblMaCuonSach = null!;
    private TextBox txtMaCuonSach = null!;
    private Button btnThemVaoGio = null!;
    private Button btnXoaKhoiGio = null!;
    private DataGridView dgvGioMuon = null!;
    private Label lblSoLuong = null!;
    private Label lblNgayMuon = null!;
    private DateTimePicker dtpNgayMuon = null!;
    private Label lblNgayPhaiTra = null!;
    private DateTimePicker dtpNgayPhaiTra = null!;
    private Button btnLapPhieu = null!;
    private Button btnLamMoi = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lblTieuDe = new Label();
        lblMaDocGia = new Label();
        txtMaDocGia = new TextBox();
        btnKiemTra = new Button();
        btnDuyetYeuCau = new Button();
        lblThongTinDocGia = new Label();
        grpThongTinDocGia = new GroupBox();
        lblNgayMuon = new Label();
        dtpNgayMuon = new DateTimePicker();
        lblNgayPhaiTra = new Label();
        dtpNgayPhaiTra = new DateTimePicker();
        grpChonSach = new GroupBox();
        lblMaCuonSach = new Label();
        txtMaCuonSach = new TextBox();
        btnThemVaoGio = new Button();
        btnXoaKhoiGio = new Button();
        lblSoLuong = new Label();
        dgvGioMuon = new DataGridView();
        btnLapPhieu = new Button();
        btnLamMoi = new Button();
        grpThongTinDocGia.SuspendLayout();
        grpChonSach.SuspendLayout();
        ((ISupportInitialize)dgvGioMuon).BeginInit();
        SuspendLayout();
        // 
        // lblTieuDe
        // 
        lblTieuDe.AutoSize = true;
        lblTieuDe.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblTieuDe.Location = new Point(27, 24);
        lblTieuDe.Name = "lblTieuDe";
        lblTieuDe.Size = new Size(206, 41);
        lblTieuDe.TabIndex = 5;
        lblTieuDe.Text = "MƯỢN SÁCH";
        lblTieuDe.Visible = false;
        // 
        // lblMaDocGia
        // 
        lblMaDocGia.AutoSize = true;
        lblMaDocGia.Location = new Point(21, 51);
        lblMaDocGia.Name = "lblMaDocGia";
        lblMaDocGia.Size = new Size(87, 20);
        lblMaDocGia.TabIndex = 0;
        lblMaDocGia.Text = "Mã độc giả:";
        // 
        // txtMaDocGia
        // 
        txtMaDocGia.CharacterCasing = CharacterCasing.Upper;
        txtMaDocGia.Location = new Point(137, 45);
        txtMaDocGia.Margin = new Padding(3, 4, 3, 4);
        txtMaDocGia.MaxLength = 25;
        txtMaDocGia.Name = "txtMaDocGia";
        txtMaDocGia.Size = new Size(217, 27);
        txtMaDocGia.TabIndex = 1;
        // 
        // btnKiemTra
        // 
        btnKiemTra.Location = new Point(371, 41);
        btnKiemTra.Margin = new Padding(3, 4, 3, 4);
        btnKiemTra.Name = "btnKiemTra";
        btnKiemTra.Size = new Size(109, 45);
        btnKiemTra.TabIndex = 2;
        btnKiemTra.Text = "Kiểm tra";
        btnKiemTra.UseVisualStyleBackColor = true;
        // 
        // btnDuyetYeuCau
        // 
        btnDuyetYeuCau.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnDuyetYeuCau.Location = new Point(971, 101);
        btnDuyetYeuCau.Margin = new Padding(3, 4, 3, 4);
        btnDuyetYeuCau.Name = "btnDuyetYeuCau";
        btnDuyetYeuCau.Size = new Size(183, 45);
        btnDuyetYeuCau.TabIndex = 6;
        btnDuyetYeuCau.Text = "Duyệt yêu cầu";
        btnDuyetYeuCau.UseVisualStyleBackColor = true;
        btnDuyetYeuCau.Click += btnDuyetYeuCau_Click;
        // 
        // lblThongTinDocGia
        // 
        lblThongTinDocGia.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblThongTinDocGia.AutoEllipsis = true;
        lblThongTinDocGia.Location = new Point(503, 44);
        lblThongTinDocGia.Name = "lblThongTinDocGia";
        lblThongTinDocGia.Size = new Size(651, 40);
        lblThongTinDocGia.TabIndex = 3;
        lblThongTinDocGia.Text = "Nhập mã độc giả rồi bấm Kiểm tra.";
        lblThongTinDocGia.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // grpThongTinDocGia
        // 
        grpThongTinDocGia.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        grpThongTinDocGia.Controls.Add(lblMaDocGia);
        grpThongTinDocGia.Controls.Add(txtMaDocGia);
        grpThongTinDocGia.Controls.Add(btnKiemTra);
        grpThongTinDocGia.Controls.Add(lblThongTinDocGia);
        grpThongTinDocGia.Controls.Add(lblNgayMuon);
        grpThongTinDocGia.Controls.Add(dtpNgayMuon);
        grpThongTinDocGia.Controls.Add(lblNgayPhaiTra);
        grpThongTinDocGia.Controls.Add(dtpNgayPhaiTra);
        grpThongTinDocGia.Controls.Add(btnDuyetYeuCau);
        grpThongTinDocGia.Location = new Point(14, 13);
        grpThongTinDocGia.Margin = new Padding(3, 4, 3, 4);
        grpThongTinDocGia.Name = "grpThongTinDocGia";
        grpThongTinDocGia.Padding = new Padding(3, 4, 3, 4);
        grpThongTinDocGia.Size = new Size(1177, 173);
        grpThongTinDocGia.TabIndex = 0;
        grpThongTinDocGia.TabStop = false;
        grpThongTinDocGia.Text = "Thông tin Độc giả && Lịch mượn";
        // 
        // lblNgayMuon
        // 
        lblNgayMuon.AutoSize = true;
        lblNgayMuon.Location = new Point(21, 112);
        lblNgayMuon.Name = "lblNgayMuon";
        lblNgayMuon.Size = new Size(90, 20);
        lblNgayMuon.TabIndex = 4;
        lblNgayMuon.Text = "Ngày mượn:";
        // 
        // dtpNgayMuon
        // 
        dtpNgayMuon.Format = DateTimePickerFormat.Short;
        dtpNgayMuon.Location = new Point(137, 107);
        dtpNgayMuon.Margin = new Padding(3, 4, 3, 4);
        dtpNgayMuon.Name = "dtpNgayMuon";
        dtpNgayMuon.Size = new Size(217, 27);
        dtpNgayMuon.TabIndex = 3;
        // 
        // lblNgayPhaiTra
        // 
        lblNgayPhaiTra.AutoSize = true;
        lblNgayPhaiTra.Location = new Point(394, 112);
        lblNgayPhaiTra.Name = "lblNgayPhaiTra";
        lblNgayPhaiTra.Size = new Size(102, 20);
        lblNgayPhaiTra.TabIndex = 5;
        lblNgayPhaiTra.Text = "Ngày phải trả:";
        // 
        // dtpNgayPhaiTra
        // 
        dtpNgayPhaiTra.Format = DateTimePickerFormat.Short;
        dtpNgayPhaiTra.Location = new Point(509, 107);
        dtpNgayPhaiTra.Margin = new Padding(3, 4, 3, 4);
        dtpNgayPhaiTra.Name = "dtpNgayPhaiTra";
        dtpNgayPhaiTra.Size = new Size(217, 27);
        dtpNgayPhaiTra.TabIndex = 4;
        // 
        // grpChonSach
        // 
        grpChonSach.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        grpChonSach.Controls.Add(lblMaCuonSach);
        grpChonSach.Controls.Add(txtMaCuonSach);
        grpChonSach.Controls.Add(btnThemVaoGio);
        grpChonSach.Controls.Add(btnXoaKhoiGio);
        grpChonSach.Controls.Add(lblSoLuong);
        grpChonSach.Controls.Add(dgvGioMuon);
        grpChonSach.Enabled = false;
        grpChonSach.Location = new Point(14, 200);
        grpChonSach.Margin = new Padding(3, 4, 3, 4);
        grpChonSach.Name = "grpChonSach";
        grpChonSach.Padding = new Padding(3, 4, 3, 4);
        grpChonSach.Size = new Size(1177, 573);
        grpChonSach.TabIndex = 4;
        grpChonSach.TabStop = false;
        grpChonSach.Text = "Nhập sách cho mượn && Danh sách sách chọn mượn";
        // 
        // lblMaCuonSach
        // 
        lblMaCuonSach.AutoSize = true;
        lblMaCuonSach.Location = new Point(21, 51);
        lblMaCuonSach.Name = "lblMaCuonSach";
        lblMaCuonSach.Size = new Size(102, 20);
        lblMaCuonSach.TabIndex = 0;
        lblMaCuonSach.Text = "Mã cuốn sách:";
        // 
        // txtMaCuonSach
        // 
        txtMaCuonSach.CharacterCasing = CharacterCasing.Upper;
        txtMaCuonSach.Location = new Point(137, 45);
        txtMaCuonSach.Margin = new Padding(3, 4, 3, 4);
        txtMaCuonSach.MaxLength = 25;
        txtMaCuonSach.Name = "txtMaCuonSach";
        txtMaCuonSach.Size = new Size(285, 27);
        txtMaCuonSach.TabIndex = 1;
        // 
        // btnThemVaoGio
        // 
        btnThemVaoGio.Location = new Point(440, 41);
        btnThemVaoGio.Margin = new Padding(3, 4, 3, 4);
        btnThemVaoGio.Name = "btnThemVaoGio";
        btnThemVaoGio.Size = new Size(131, 45);
        btnThemVaoGio.TabIndex = 2;
        btnThemVaoGio.Text = "Thêm vào giỏ";
        btnThemVaoGio.UseVisualStyleBackColor = true;
        // 
        // btnXoaKhoiGio
        // 
        btnXoaKhoiGio.Location = new Point(583, 41);
        btnXoaKhoiGio.Margin = new Padding(3, 4, 3, 4);
        btnXoaKhoiGio.Name = "btnXoaKhoiGio";
        btnXoaKhoiGio.Size = new Size(143, 45);
        btnXoaKhoiGio.TabIndex = 3;
        btnXoaKhoiGio.Text = "Bỏ khỏi giỏ";
        btnXoaKhoiGio.UseVisualStyleBackColor = true;
        // 
        // lblSoLuong
        // 
        lblSoLuong.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblSoLuong.Location = new Point(737, 45);
        lblSoLuong.Name = "lblSoLuong";
        lblSoLuong.Size = new Size(417, 36);
        lblSoLuong.TabIndex = 4;
        lblSoLuong.Text = "Đang giữ: 0 | Trong giỏ: 0 | Tối đa: 3";
        lblSoLuong.TextAlign = ContentAlignment.MiddleRight;
        // 
        // dgvGioMuon
        // 
        dgvGioMuon.AllowUserToAddRows = false;
        dgvGioMuon.AllowUserToDeleteRows = false;
        dgvGioMuon.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvGioMuon.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvGioMuon.Location = new Point(23, 104);
        dgvGioMuon.Margin = new Padding(3, 4, 3, 4);
        dgvGioMuon.MultiSelect = false;
        dgvGioMuon.Name = "dgvGioMuon";
        dgvGioMuon.RowHeadersWidth = 51;
        dgvGioMuon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvGioMuon.Size = new Size(1131, 380);
        dgvGioMuon.TabIndex = 5;
        // 
        // btnLapPhieu
        // 
        btnLapPhieu.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnLapPhieu.Location = new Point(777, 803);
        btnLapPhieu.Margin = new Padding(3, 4, 3, 4);
        btnLapPhieu.Name = "btnLapPhieu";
        btnLapPhieu.Size = new Size(263, 53);
        btnLapPhieu.TabIndex = 2;
        btnLapPhieu.Text = "XÁC NHẬN CHO MƯỢN & LẬP PHIẾU";
        btnLapPhieu.UseVisualStyleBackColor = true;
        // 
        // btnLamMoi
        // 
        btnLamMoi.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnLamMoi.Location = new Point(1051, 803);
        btnLamMoi.Margin = new Padding(3, 4, 3, 4);
        btnLamMoi.Name = "btnLamMoi";
        btnLamMoi.Size = new Size(139, 53);
        btnLamMoi.TabIndex = 1;
        btnLamMoi.Text = "Hủy thao tác";
        btnLamMoi.UseVisualStyleBackColor = true;
        // 
        // FrmMuonSach
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1206, 887);
        Controls.Add(grpThongTinDocGia);
        Controls.Add(btnLamMoi);
        Controls.Add(btnLapPhieu);
        Controls.Add(grpChonSach);
        Controls.Add(lblTieuDe);
        Margin = new Padding(3, 4, 3, 4);
        MinimumSize = new Size(1026, 784);
        Name = "FrmMuonSach";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Mượn sách";
        grpThongTinDocGia.ResumeLayout(false);
        grpThongTinDocGia.PerformLayout();
        grpChonSach.ResumeLayout(false);
        grpChonSach.PerformLayout();
        ((ISupportInitialize)dgvGioMuon).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}