#nullable enable

using System.ComponentModel;
using System.Drawing;

namespace QuanLyThuVien.Forms;

partial class FrmDocGia
{
    private IContainer? components = null;
    private Label lblTieuDe = null!;
    private Label lblMaDocGia = null!;
    private Label lblHoTen = null!;
    private Label lblSoDienThoai = null!;
    private Label lblSoTienTra = null!;
    private TextBox txtMaDocGia = null!;
    private TextBox txtHoTen = null!;
    private TextBox txtSoDienThoai = null!;
    private Label lblTienNoPhat = null!;
    private NumericUpDown nudSoTienTra = null!;
    private Button btnThem = null!;
    private Button btnSua = null!;
    private Button btnKhoa = null!;
    private Button btnXoa = null!;
    private Button btnLamMoi = null!;
    private Button btnThanhToan = null!;
    private Button btnTraHet = null!;
    private DataGridView dgvDocGia = null!;
    private ErrorProvider errProvider = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new Container();
        lblTieuDe = new Label();
        lblMaDocGia = new Label();
        lblHoTen = new Label();
        lblSoDienThoai = new Label();
        lblSoTienTra = new Label();
        txtMaDocGia = new TextBox();
        txtHoTen = new TextBox();
        txtSoDienThoai = new TextBox();
        lblTienNoPhat = new Label();
        nudSoTienTra = new NumericUpDown();
        btnThem = new Button();
        btnSua = new Button();
        btnKhoa = new Button();
        btnXoa = new Button();
        btnLamMoi = new Button();
        btnThanhToan = new Button();
        btnTraHet = new Button();
        dgvDocGia = new DataGridView();
        ((ISupportInitialize)nudSoTienTra).BeginInit();
        ((ISupportInitialize)dgvDocGia).BeginInit();
        SuspendLayout();
        //
        // lblTieuDe
        //
        lblTieuDe.AutoSize = true;
        lblTieuDe.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblTieuDe.Location = new Point(24, 18);
        lblTieuDe.Name = "lblTieuDe";
        lblTieuDe.Size = new Size(252, 32);
        lblTieuDe.Text = "QUẢN LÝ ĐỘC GIẢ";
        //
        // lblMaDocGia
        //
        lblMaDocGia.AutoSize = true;
        lblMaDocGia.Location = new Point(24, 76);
        lblMaDocGia.Name = "lblMaDocGia";
        lblMaDocGia.Size = new Size(78, 20);
        lblMaDocGia.Text = "Mã độc giả:";
        //
        // txtMaDocGia
        //
        txtMaDocGia.Location = new Point(120, 72);
        txtMaDocGia.Name = "txtMaDocGia";
        txtMaDocGia.Size = new Size(170, 27);
        //
        // lblHoTen
        //
        lblHoTen.AutoSize = true;
        lblHoTen.Location = new Point(320, 76);
        lblHoTen.Name = "lblHoTen";
        lblHoTen.Size = new Size(54, 20);
        lblHoTen.Text = "Họ tên:";
        //
        // txtHoTen
        //
        txtHoTen.Location = new Point(390, 72);
        txtHoTen.Name = "txtHoTen";
        txtHoTen.Size = new Size(240, 27);
        //
        // lblSoDienThoai
        //
        lblSoDienThoai.AutoSize = true;
        lblSoDienThoai.Location = new Point(660, 76);
        lblSoDienThoai.Name = "lblSoDienThoai";
        lblSoDienThoai.Size = new Size(93, 20);
        lblSoDienThoai.Text = "Số điện thoại:";
        //
        // txtSoDienThoai
        //
        txtSoDienThoai.Location = new Point(770, 72);
        txtSoDienThoai.Name = "txtSoDienThoai";
        txtSoDienThoai.Size = new Size(250, 27);
        //
        // lblTienNoPhat
        //
        lblTienNoPhat.AutoSize = true;
        lblTienNoPhat.Location = new Point(24, 119);
        lblTienNoPhat.Name = "lblTienNoPhat";
        lblTienNoPhat.Size = new Size(91, 20);
        lblTienNoPhat.Text = "Nợ phạt: 0 VNĐ";
        //
        // lblSoTienTra
        //
        lblSoTienTra.AutoSize = true;
        lblSoTienTra.Location = new Point(320, 119);
        lblSoTienTra.Name = "lblSoTienTra";
        lblSoTienTra.Size = new Size(67, 20);
        lblSoTienTra.Text = "Số tiền trả:";
        //
        // nudSoTienTra
        //
        nudSoTienTra.Increment = 1000;
        nudSoTienTra.Location = new Point(410, 115);
        nudSoTienTra.Maximum = 100000000;
        nudSoTienTra.Name = "nudSoTienTra";
        nudSoTienTra.Size = new Size(180, 27);
        nudSoTienTra.ThousandsSeparator = true;
        //
        // btnThem
        //
        btnThem.Location = new Point(24, 158);
        btnThem.Name = "btnThem";
        btnThem.Size = new Size(100, 36);
        btnThem.Text = "Thêm";
        btnThem.UseVisualStyleBackColor = true;
        //
        // btnSua
        //
        btnSua.Location = new Point(132, 158);
        btnSua.Name = "btnSua";
        btnSua.Size = new Size(100, 36);
        btnSua.Text = "Sửa";
        btnSua.UseVisualStyleBackColor = true;
        //
        // btnKhoa
        //
        btnKhoa.Location = new Point(240, 158);
        btnKhoa.Name = "btnKhoa";
        btnKhoa.Size = new Size(120, 36);
        btnKhoa.Text = "Khóa thẻ";
        btnKhoa.UseVisualStyleBackColor = true;
        //
        // btnXoa
        //
        btnXoa.Location = new Point(368, 158);
        btnXoa.Name = "btnXoa";
        btnXoa.Size = new Size(100, 36);
        btnXoa.Text = "Xóa";
        btnXoa.UseVisualStyleBackColor = true;
        //
        // btnLamMoi
        //
        btnLamMoi.Location = new Point(476, 158);
        btnLamMoi.Name = "btnLamMoi";
        btnLamMoi.Size = new Size(110, 36);
        btnLamMoi.Text = "Làm mới";
        btnLamMoi.UseVisualStyleBackColor = true;
        //
        // btnThanhToan
        //
        btnThanhToan.Location = new Point(650, 158);
        btnThanhToan.Name = "btnThanhToan";
        btnThanhToan.Size = new Size(150, 36);
        btnThanhToan.Text = "Thanh toán";
        btnThanhToan.UseVisualStyleBackColor = true;
        //
        // btnTraHet
        //
        btnTraHet.Location = new Point(810, 158);
        btnTraHet.Name = "btnTraHet";
        btnTraHet.Size = new Size(130, 36);
        btnTraHet.Text = "Trả hết nợ";
        btnTraHet.UseVisualStyleBackColor = true;
        //
        // dgvDocGia
        //
        dgvDocGia.AllowUserToAddRows = false;
        dgvDocGia.AllowUserToDeleteRows = false;
        dgvDocGia.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvDocGia.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvDocGia.Location = new Point(24, 216);
        dgvDocGia.MultiSelect = false;
        dgvDocGia.Name = "dgvDocGia";
        dgvDocGia.RowHeadersWidth = 51;
        dgvDocGia.Size = new Size(1032, 440);
        dgvDocGia.TabIndex = 8;
        //
        // FrmDocGia
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1080, 680);
        Controls.Add(dgvDocGia);
        Controls.Add(btnTraHet);
        Controls.Add(btnThanhToan);
        Controls.Add(btnLamMoi);
        Controls.Add(btnXoa);
        Controls.Add(btnKhoa);
        Controls.Add(btnSua);
        Controls.Add(btnThem);
        Controls.Add(nudSoTienTra);
        Controls.Add(lblSoTienTra);
        Controls.Add(lblTienNoPhat);
        Controls.Add(txtSoDienThoai);
        Controls.Add(lblSoDienThoai);
        Controls.Add(txtHoTen);
        Controls.Add(lblHoTen);
        Controls.Add(txtMaDocGia);
        Controls.Add(lblMaDocGia);
        Controls.Add(lblTieuDe);
        MinimumSize = new Size(900, 600);
        Name = "FrmDocGia";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Quản lý độc giả";
        ((ISupportInitialize)nudSoTienTra).EndInit();
        ((ISupportInitialize)dgvDocGia).EndInit();
        ResumeLayout(false);
        PerformLayout();
        errProvider = new ErrorProvider(components);
        errProvider.ContainerControl = this;
    }
}
