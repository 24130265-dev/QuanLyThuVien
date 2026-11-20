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
        lblThongTinDocGia = new Label();
        grpThongTinDocGia = new GroupBox();
        grpChonSach = new GroupBox();
        lblMaCuonSach = new Label();
        txtMaCuonSach = new TextBox();
        btnThemVaoGio = new Button();
        btnXoaKhoiGio = new Button();
        dgvGioMuon = new DataGridView();
        lblSoLuong = new Label();
        lblNgayMuon = new Label();
        dtpNgayMuon = new DateTimePicker();
        lblNgayPhaiTra = new Label();
        dtpNgayPhaiTra = new DateTimePicker();
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
        lblTieuDe.Location = new Point(24, 18);
        lblTieuDe.Name = "lblTieuDe";
        lblTieuDe.Size = new Size(242, 32);
        lblTieuDe.Text = "MƯỢN SÁCH";
        lblTieuDe.Visible = false;
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
        grpThongTinDocGia.Location = new Point(12, 10);
        grpThongTinDocGia.Name = "grpThongTinDocGia";
        grpThongTinDocGia.Size = new Size(1030, 130);
        grpThongTinDocGia.TabStop = false;
        grpThongTinDocGia.Text = "Thông tin Độc giả && Lịch mượn";
        //
        // lblMaDocGia
        //
        lblMaDocGia.AutoSize = true;
        lblMaDocGia.Location = new Point(18, 38);
        lblMaDocGia.Name = "lblMaDocGia";
        lblMaDocGia.Size = new Size(78, 20);
        lblMaDocGia.Text = "Mã độc giả:";
        //
        // txtMaDocGia
        //
        txtMaDocGia.CharacterCasing = CharacterCasing.Upper;
        txtMaDocGia.Location = new Point(120, 34);
        txtMaDocGia.MaxLength = 25;
        txtMaDocGia.Name = "txtMaDocGia";
        txtMaDocGia.Size = new Size(190, 27);
        //
        // btnKiemTra
        //
        btnKiemTra.Location = new Point(325, 31);
        btnKiemTra.Name = "btnKiemTra";
        btnKiemTra.Size = new Size(95, 34);
        btnKiemTra.Text = "Kiểm tra";
        btnKiemTra.UseVisualStyleBackColor = true;
        //
        // lblThongTinDocGia
        //
        lblThongTinDocGia.AutoEllipsis = true;
        lblThongTinDocGia.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblThongTinDocGia.Location = new Point(440, 33);
        lblThongTinDocGia.Name = "lblThongTinDocGia";
        lblThongTinDocGia.Size = new Size(570, 30);
        lblThongTinDocGia.Text = "Nhập mã độc giả rồi bấm Kiểm tra.";
        lblThongTinDocGia.TextAlign = ContentAlignment.MiddleLeft;
        //
        // lblNgayMuon
        //
        lblNgayMuon.AutoSize = true;
        lblNgayMuon.Location = new Point(18, 84);
        lblNgayMuon.Name = "lblNgayMuon";
        lblNgayMuon.Size = new Size(77, 20);
        lblNgayMuon.Text = "Ngày mượn:";
        //
        // dtpNgayMuon
        //
        dtpNgayMuon.Format = DateTimePickerFormat.Short;
        dtpNgayMuon.Location = new Point(120, 80);
        dtpNgayMuon.Name = "dtpNgayMuon";
        dtpNgayMuon.Size = new Size(190, 27);
        dtpNgayMuon.TabIndex = 3;
        //
        // lblNgayPhaiTra
        //
        lblNgayPhaiTra.AutoSize = true;
        lblNgayPhaiTra.Location = new Point(345, 84);
        lblNgayPhaiTra.Name = "lblNgayPhaiTra";
        lblNgayPhaiTra.Size = new Size(88, 20);
        lblNgayPhaiTra.Text = "Ngày phải trả:";
        //
        // dtpNgayPhaiTra
        //
        dtpNgayPhaiTra.Format = DateTimePickerFormat.Short;
        dtpNgayPhaiTra.Location = new Point(445, 80);
        dtpNgayPhaiTra.Name = "dtpNgayPhaiTra";
        dtpNgayPhaiTra.Size = new Size(190, 27);
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
        grpChonSach.Location = new Point(12, 150);
        grpChonSach.Name = "grpChonSach";
        grpChonSach.Size = new Size(1030, 430);
        grpChonSach.TabIndex = 4;
        grpChonSach.TabStop = false;
        grpChonSach.Text = "Nhập sách cho mượn && Danh sách sách chọn mượn";
        //
        // lblMaCuonSach
        //
        lblMaCuonSach.AutoSize = true;
        lblMaCuonSach.Location = new Point(18, 38);
        lblMaCuonSach.Name = "lblMaCuonSach";
        lblMaCuonSach.Size = new Size(89, 20);
        lblMaCuonSach.Text = "Mã cuốn sách:";
        //
        // txtMaCuonSach
        //
        txtMaCuonSach.CharacterCasing = CharacterCasing.Upper;
        txtMaCuonSach.Location = new Point(120, 34);
        txtMaCuonSach.MaxLength = 25;
        txtMaCuonSach.Name = "txtMaCuonSach";
        txtMaCuonSach.Size = new Size(250, 27);
        //
        // btnThemVaoGio
        //
        btnThemVaoGio.Location = new Point(385, 31);
        btnThemVaoGio.Name = "btnThemVaoGio";
        btnThemVaoGio.Size = new Size(115, 34);
        btnThemVaoGio.Text = "Thêm vào giỏ";
        btnThemVaoGio.UseVisualStyleBackColor = true;
        //
        // btnXoaKhoiGio
        //
        btnXoaKhoiGio.Location = new Point(510, 31);
        btnXoaKhoiGio.Name = "btnXoaKhoiGio";
        btnXoaKhoiGio.Size = new Size(125, 34);
        btnXoaKhoiGio.Text = "Bỏ khỏi giỏ";
        btnXoaKhoiGio.UseVisualStyleBackColor = true;
        //
        // lblSoLuong
        //
        lblSoLuong.AutoSize = false;
        lblSoLuong.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblSoLuong.Location = new Point(645, 34);
        lblSoLuong.Name = "lblSoLuong";
        lblSoLuong.Size = new Size(365, 27);
        lblSoLuong.Text = "Đang giữ: 0 | Trong giỏ: 0 | Tối đa: 3";
        lblSoLuong.TextAlign = ContentAlignment.MiddleRight;
        //
        // dgvGioMuon
        //
        dgvGioMuon.AllowUserToAddRows = false;
        dgvGioMuon.AllowUserToDeleteRows = false;
        dgvGioMuon.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvGioMuon.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvGioMuon.Location = new Point(20, 78);
        dgvGioMuon.MultiSelect = false;
        dgvGioMuon.Name = "dgvGioMuon";
        dgvGioMuon.RowHeadersWidth = 51;
        dgvGioMuon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvGioMuon.Size = new Size(990, 285);
        dgvGioMuon.TabIndex = 5;
        //
        // btnLapPhieu
        //
        btnLapPhieu.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnLapPhieu.Location = new Point(680, 602);
        btnLapPhieu.Name = "btnLapPhieu";
        btnLapPhieu.Size = new Size(230, 40);
        btnLapPhieu.Text = "XÁC NHẬN CHO MƯỢN & LẬP PHIẾU";
        btnLapPhieu.UseVisualStyleBackColor = true;
        //
        // btnLamMoi
        //
        btnLamMoi.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnLamMoi.Location = new Point(920, 602);
        btnLamMoi.Name = "btnLamMoi";
        btnLamMoi.Size = new Size(122, 40);
        btnLamMoi.Text = "Hủy thao tác";
        btnLamMoi.UseVisualStyleBackColor = true;
        //
        // FrmMuonSach
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1055, 665);
        Controls.Add(grpThongTinDocGia);
        Controls.Add(btnLamMoi);
        Controls.Add(btnLapPhieu);
        Controls.Add(grpChonSach);
        Controls.Add(lblTieuDe);
        MinimumSize = new Size(900, 600);
        Name = "FrmMuonSach";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Mượn sách";
        grpChonSach.ResumeLayout(false);
        grpChonSach.PerformLayout();
        grpThongTinDocGia.ResumeLayout(false);
        grpThongTinDocGia.PerformLayout();
        ((ISupportInitialize)dgvGioMuon).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}