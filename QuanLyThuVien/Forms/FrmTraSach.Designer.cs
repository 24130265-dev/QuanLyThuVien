using System.ComponentModel;

namespace QuanLyThuVien.Forms;

partial class FrmTraSach
{
    /// <summary>
    /// Required designer variable.
    /// </summary>

    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.Label lblMaPhieuMuon;
    private System.Windows.Forms.TextBox txtMaPhieuMuon;
    private System.Windows.Forms.Button btnTimKiem;
    private System.Windows.Forms.DataGridView dgvDanhSachTra;
    private System.Windows.Forms.Button btnXacNhanTra;
    private System.Windows.Forms.Label lblTextTongPhat;
    private System.Windows.Forms.Label lblTongTienPhat;
    private System.Windows.Forms.GroupBox grpThongTinTra;
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
        this.lblMaPhieuMuon = new System.Windows.Forms.Label();
            this.txtMaPhieuMuon = new System.Windows.Forms.TextBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.dgvDanhSachTra = new System.Windows.Forms.DataGridView();
            this.btnXacNhanTra = new System.Windows.Forms.Button();
            this.lblTextTongPhat = new System.Windows.Forms.Label();
            this.lblTongTienPhat = new System.Windows.Forms.Label();
            this.grpThongTinTra = new System.Windows.Forms.GroupBox();
            
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachTra)).BeginInit();
            this.grpThongTinTra.SuspendLayout();
            this.SuspendLayout();

            // Lbl & Textbox Mã phiếu
            this.lblMaPhieuMuon.AutoSize = true;
            this.lblMaPhieuMuon.Location = new System.Drawing.Point(30, 35);
            this.lblMaPhieuMuon.Name = "lblMaPhieuMuon";
            this.lblMaPhieuMuon.Size = new System.Drawing.Size(110, 17);
            this.lblMaPhieuMuon.Text = "Mã phiếu mượn:";
            
            this.txtMaPhieuMuon.Location = new System.Drawing.Point(150, 32);
            this.txtMaPhieuMuon.Name = "txtMaPhieuMuon";
            this.txtMaPhieuMuon.Size = new System.Drawing.Size(200, 22);

            // Nút Tìm Kiếm
            this.btnTimKiem.Location = new System.Drawing.Point(370, 30);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(100, 27);
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);

            // GroupBox Thông tin mượn
            this.grpThongTinTra.Controls.Add(this.dgvDanhSachTra);
            this.grpThongTinTra.Location = new System.Drawing.Point(30, 80);
            this.grpThongTinTra.Name = "grpThongTinTra";
            this.grpThongTinTra.Size = new System.Drawing.Size(720, 250);
            this.grpThongTinTra.Text = "Danh sách sách cần trả";

            // DataGridView
            this.dgvDanhSachTra.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhSachTra.Location = new System.Drawing.Point(20, 30);
            this.dgvDanhSachTra.Name = "dgvDanhSachTra";
            this.dgvDanhSachTra.ReadOnly = true;
            this.dgvDanhSachTra.Size = new System.Drawing.Size(680, 200);
            this.dgvDanhSachTra.AllowUserToAddRows = false;
            this.dgvDanhSachTra.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // Nhãn Phạt
            this.lblTextTongPhat.AutoSize = true;
            this.lblTextTongPhat.Location = new System.Drawing.Point(30, 350);
            this.lblTextTongPhat.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblTextTongPhat.Text = "Tổng tiền phạt (ước tính):";

            this.lblTongTienPhat.AutoSize = true;
            this.lblTongTienPhat.Location = new System.Drawing.Point(220, 350);
            this.lblTongTienPhat.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblTongTienPhat.ForeColor = System.Drawing.Color.Red;
            this.lblTongTienPhat.Text = "0 VNĐ";

            // Nút Xác nhận trả
            this.btnXacNhanTra.Location = new System.Drawing.Point(600, 340);
            this.btnXacNhanTra.Name = "btnXacNhanTra";
            this.btnXacNhanTra.Size = new System.Drawing.Size(150, 40);
            this.btnXacNhanTra.Text = "Xác Nhận Trả Sách";
            this.btnXacNhanTra.BackColor = System.Drawing.Color.LightGreen;
            this.btnXacNhanTra.Click += new System.EventHandler(this.btnXacNhanTra_Click);

            // Form
            this.ClientSize = new System.Drawing.Size(780, 420);
            this.Controls.Add(this.lblMaPhieuMuon);
            this.Controls.Add(this.txtMaPhieuMuon);
            this.Controls.Add(this.btnTimKiem);
            this.Controls.Add(this.grpThongTinTra);
            this.Controls.Add(this.lblTextTongPhat);
            this.Controls.Add(this.lblTongTienPhat);
            this.Controls.Add(this.btnXacNhanTra);
            this.Name = "FrmTraSach";
            this.Text = "Xử Lý Trả Sách";
            
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachTra)).EndInit();
            this.grpThongTinTra.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }

    #endregion
