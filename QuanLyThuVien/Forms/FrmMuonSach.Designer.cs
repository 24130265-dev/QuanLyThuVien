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
        //
        // lblMaDocGia
        //
        lblMaDocGia.AutoSize = true;
        lblMaDocGia.Location = new Point(24, 77);
        lblMaDocGia.Name = "lblMaDocGia";
        lblMaDocGia.Size = new Size(78, 20);
        lblMaDocGia.Text = "Mã độc giả:";
        //
        // txtMaDocGia
        //
        txtMaDocGia.CharacterCasing = CharacterCasing.Upper;
        txtMaDocGia.Location = new Point(120, 73);
        txtMaDocGia.MaxLength = 10;
        txtMaDocGia.Name = "txtMaDocGia";
        txtMaDocGia.Size = new Size(180, 27);
        //
        // btnKiemTra
        //
        btnKiemTra.Location = new Point(312, 70);
        btnKiemTra.Name = "btnKiemTra";
        btnKiemTra.Size = new Size(115, 34);
        btnKiemTra.Text = "Kiểm tra";
        btnKiemTra.UseVisualStyleBackColor = true;
        //
        // lblThongTinDocGia
        //
        lblThongTinDocGia.AutoEllipsis = true;
        lblThongTinDocGia.Location = new Point(450, 74);
        lblThongTinDocGia.Name = "lblThongTinDocGia";
        lblThongTinDocGia.Size = new Size(580, 30);
        lblThongTinDocGia.Text = "Nhập mã độc giả rồi bấm Kiểm tra.";
        lblThongTinDocGia.TextAlign = ContentAlignment.MiddleLeft;
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
        grpChonSach.Controls.Add(lblNgayMuon);
        grpChonSach.Controls.Add(dtpNgayMuon);
        grpChonSach.Controls.Add(lblNgayPhaiTra);
        grpChonSach.Controls.Add(dtpNgayPhaiTra);
        grpChonSach.Enabled = false;
        grpChonSach.Location = new Point(24, 124);
        grpChonSach.Name = "grpChonSach";
        grpChonSach.Size = new Size(1006, 455);
        grpChonSach.TabIndex = 4;
        grpChonSach.TabStop = false;
        grpChonSach.Text = "Chọn sách";
        //
        // lblMaCuonSach
        //
        lblMaCuonSach.AutoSize = true;
        lblMaCuonSach.Location = new Point(18, 35);
        lblMaCuonSach.Name = "lblMaCuonSach";
        lblMaCuonSach.Size = new Size(89, 20);
        lblMaCuonSach.Text = "Mã cuốn sách:";
        //
        // txtMaCuonSach
        //
        txtMaCuonSach.CharacterCasing = CharacterCasing.Upper;
        txtMaCuonSach.Location = new Point(120, 31);
        txtMaCuonSach.Name = "txtMaCuonSach";
        txtMaCuonSach.Size = new Size(190, 27);
        //
        // btnThemVaoGio
        //
        btnThemVaoGio.Location = new Point(326, 28);
        btnThemVaoGio.Name = "btnThemVaoGio";
        btnThemVaoGio.Size = new Size(140, 34);
        btnThemVaoGio.Text = "Thêm vào giỏ";
        btnThemVaoGio.UseVisualStyleBackColor = true;
        //
        // btnXoaKhoiGio
        //
        btnXoaKhoiGio.Location = new Point(476, 28);
        btnXoaKhoiGio.Name = "btnXoaKhoiGio";
        btnXoaKhoiGio.Size = new Size(120, 34);
        btnXoaKhoiGio.Text = "Bỏ khỏi giỏ";
        btnXoaKhoiGio.UseVisualStyleBackColor = true;
        //
        // lblSoLuong
        //
        lblSoLuong.AutoSize = true;
        lblSoLuong.Location = new Point(620, 35);
        lblSoLuong.Name = "lblSoLuong";
        lblSoLuong.Size = new Size(213, 20);
        lblSoLuong.Text = "Đang giữ: 0 | Trong giỏ: 0 | Tối đa: 3";
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
        dgvGioMuon.Size = new Size(966, 270);
        dgvGioMuon.TabIndex = 5;
        //
        // lblNgayMuon
        //
        lblNgayMuon.AutoSize = true;
        lblNgayMuon.Location = new Point(20, 376);
        lblNgayMuon.Name = "lblNgayMuon";
        lblNgayMuon.Size = new Size(77, 20);
        lblNgayMuon.Text = "Ngày mượn:";
        //
        // dtpNgayMuon
        //
        dtpNgayMuon.Location = new Point(120, 372);
        dtpNgayMuon.Name = "dtpNgayMuon";
        dtpNgayMuon.Size = new Size(210, 27);
        dtpNgayMuon.TabIndex = 6;
        //
        // lblNgayPhaiTra
        //
        lblNgayPhaiTra.AutoSize = true;
        lblNgayPhaiTra.Location = new Point(370, 376);
        lblNgayPhaiTra.Name = "lblNgayPhaiTra";
        lblNgayPhaiTra.Size = new Size(88, 20);
        lblNgayPhaiTra.Text = "Ngày phải trả:";
        //
        // dtpNgayPhaiTra
        //
        dtpNgayPhaiTra.Location = new Point(480, 372);
        dtpNgayPhaiTra.Name = "dtpNgayPhaiTra";
        dtpNgayPhaiTra.Size = new Size(210, 27);
        dtpNgayPhaiTra.TabIndex = 7;
        //
        // btnLapPhieu
        //
        btnLapPhieu.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnLapPhieu.Location = new Point(758, 602);
        btnLapPhieu.Name = "btnLapPhieu";
        btnLapPhieu.Size = new Size(150, 40);
        btnLapPhieu.Text = "Lập phiếu mượn";
        btnLapPhieu.UseVisualStyleBackColor = true;
        //
        // btnLamMoi
        //
        btnLamMoi.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnLamMoi.Location = new Point(918, 602);
        btnLamMoi.Name = "btnLamMoi";
        btnLamMoi.Size = new Size(112, 40);
        btnLamMoi.Text = "Làm mới";
        btnLamMoi.UseVisualStyleBackColor = true;
        //
        // FrmMuonSach
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1055, 665);
        Controls.Add(btnLamMoi);
        Controls.Add(btnLapPhieu);
        Controls.Add(grpChonSach);
        Controls.Add(lblThongTinDocGia);
        Controls.Add(btnKiemTra);
        Controls.Add(txtMaDocGia);
        Controls.Add(lblMaDocGia);
        Controls.Add(lblTieuDe);
        MinimumSize = new Size(900, 600);
        Name = "FrmMuonSach";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Mượn sách";
        grpChonSach.ResumeLayout(false);
        grpChonSach.PerformLayout();
        ((ISupportInitialize)dgvGioMuon).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
