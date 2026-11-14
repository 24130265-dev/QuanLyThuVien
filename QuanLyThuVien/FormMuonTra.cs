
using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Entities;

namespace QuanLyThuVien
{
    public class FormMuonTra : Form
    {
        ComboBox cboDocGia = new ComboBox();
        ComboBox cboCuonSach = new ComboBox();
        ComboBox cboTinhTrang = new ComboBox();

        DateTimePicker dtpNgayTra = new DateTimePicker();

        DataGridView dgvMuonTra = new DataGridView();

        Button btnMuon = new Button();
        Button btnTra = new Button();
        Button btnLamMoi = new Button();

        public FormMuonTra()
        {
            TaoGiaoDien();
            LoadDuLieu();
        }

        private void TaoGiaoDien()
        {
            Text = "QUẢN LÝ MƯỢN TRẢ SÁCH";
            Size = new Size(1000, 650);
            StartPosition = FormStartPosition.CenterScreen;

            Label lblTieuDe = new Label();
            lblTieuDe.Text = "QUẢN LÝ MƯỢN TRẢ SÁCH";
            lblTieuDe.Font = new Font("Arial", 20, FontStyle.Bold);
            lblTieuDe.Location = new Point(280, 20);
            lblTieuDe.Size = new Size(500, 40);
            Controls.Add(lblTieuDe);

            // Nhóm mượn sách
            GroupBox grpMuon = new GroupBox();
            grpMuon.Text = "Thông tin mượn sách";
            grpMuon.Location = new Point(20, 80);
            grpMuon.Size = new Size(450, 190);
            Controls.Add(grpMuon);

            Label lblDocGia = new Label();
            lblDocGia.Text = "Độc giả:";
            lblDocGia.Location = new Point(20, 35);
            lblDocGia.AutoSize = true;
            grpMuon.Controls.Add(lblDocGia);

            cboDocGia.Location = new Point(130, 32);
            cboDocGia.Size = new Size(280, 30);
            cboDocGia.DropDownStyle = ComboBoxStyle.DropDownList;
            grpMuon.Controls.Add(cboDocGia);

            Label lblSach = new Label();
            lblSach.Text = "Cuốn sách:";
            lblSach.Location = new Point(20, 80);
            lblSach.AutoSize = true;
            grpMuon.Controls.Add(lblSach);

            cboCuonSach.Location = new Point(130, 77);
            cboCuonSach.Size = new Size(280, 30);
            cboCuonSach.DropDownStyle = ComboBoxStyle.DropDownList;
            grpMuon.Controls.Add(cboCuonSach);

            Label lblNgayTra = new Label();
            lblNgayTra.Text = "Ngày phải trả:";
            lblNgayTra.Location = new Point(20, 125);
            lblNgayTra.AutoSize = true;
            grpMuon.Controls.Add(lblNgayTra);

            dtpNgayTra.Location = new Point(130, 122);
            dtpNgayTra.Size = new Size(200, 30);
            dtpNgayTra.Format = DateTimePickerFormat.Short;
            dtpNgayTra.Value = DateTime.Today.AddDays(14);
            dtpNgayTra.MinDate = DateTime.Today.AddDays(1);
            grpMuon.Controls.Add(dtpNgayTra);

            btnMuon.Text = "MƯỢN SÁCH";
            btnMuon.Location = new Point(150, 155);
            btnMuon.Size = new Size(140, 30);
            btnMuon.Click += BtnMuon_Click;
            grpMuon.Controls.Add(btnMuon);

            // Nhóm trả sách
            GroupBox grpTra = new GroupBox();
            grpTra.Text = "Thông tin trả sách";
            grpTra.Location = new Point(500, 80);
            grpTra.Size = new Size(450, 190);
            Controls.Add(grpTra);

            Label lblTinhTrang = new Label();
            lblTinhTrang.Text = "Tình trạng sách:";
            lblTinhTrang.Location = new Point(20, 45);
            lblTinhTrang.AutoSize = true;
            grpTra.Controls.Add(lblTinhTrang);

            cboTinhTrang.Location = new Point(150, 42);
            cboTinhTrang.Size = new Size(250, 30);
            cboTinhTrang.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTinhTrang.Items.Add("Tốt");
            cboTinhTrang.Items.Add("Hỏng");
            cboTinhTrang.Items.Add("Mất");
            cboTinhTrang.SelectedIndex = 0;
            grpTra.Controls.Add(cboTinhTrang);

            btnTra.Text = "TRẢ SÁCH";
            btnTra.Location = new Point(150, 95);
            btnTra.Size = new Size(140, 40);
            btnTra.Click += BtnTra_Click;
            grpTra.Controls.Add(btnTra);

            // Bảng danh sách
            Label lblDanhSach = new Label();
            lblDanhSach.Text = "DANH SÁCH SÁCH ĐANG MƯỢN";
            lblDanhSach.Font = new Font("Arial", 12, FontStyle.Bold);
            lblDanhSach.Location = new Point(20, 290);
            lblDanhSach.AutoSize = true;
            Controls.Add(lblDanhSach);

            dgvMuonTra.Location = new Point(20, 325);
            dgvMuonTra.Size = new Size(930, 230);
            dgvMuonTra.ReadOnly = true;
            dgvMuonTra.AllowUserToAddRows = false;
            dgvMuonTra.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvMuonTra.MultiSelect = false;
            dgvMuonTra.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            Controls.Add(dgvMuonTra);

            btnLamMoi.Text = "LÀM MỚI";
            btnLamMoi.Location = new Point(820, 570);
            btnLamMoi.Size = new Size(130, 35);
            btnLamMoi.Click += (s, e) => LoadDuLieu();
            Controls.Add(btnLamMoi);
        }

        // Tải dữ liệu từ SQL Server
        private void LoadDuLieu()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    // Danh sách độc giả
                    var docGias = db.DocGias
                        .Where(static x => !x.BiKhoaThe)
                        .ToList();

                    cboDocGia.DataSource = docGias;
                    cboDocGia.DisplayMember = "HoTen";
                    cboDocGia.ValueMember = "MaDocGia";

                    // Danh sách cuốn sách sẵn sàng
                    var cuonSachs = db.CuonSachs
                        .Include(static x => x.DauSach)
                        .Where(static x => x.TrangThai == TrangThaiSach.SanSang)
                        .ToList()
                        .Select(static x => new
                        {
                            x.MaCuonSach,
                            TenHienThi = x.MaCuonSach + " - " +
                                (x.DauSach == null
                                    ? ""
                                    : x.DauSach.TenDauSach)
                        })
                        .ToList();

                    cboCuonSach.DataSource = cuonSachs;
                    cboCuonSach.DisplayMember = "TenHienThi";
                    cboCuonSach.ValueMember = "MaCuonSach";

                    // Danh sách sách đang mượn
                    var danhSach = db.ChiTietPhieuMuons
                        .Include(static x => x.PhieuMuon)
                            .ThenInclude(static x => x.DocGia)
                        .Include(static x => x.CuonSach)
                            .ThenInclude(static x => x.DauSach)
                        .Where(static x => x.NgayTraThucTe == null)
                        .ToList()
                        .Select(static x => new
                        {
                            x.Id,
                            MaPhieuMuon = x.MaPhieuMuon,
                            MaDocGia = x.PhieuMuon.MaDocGia,
                            TenDocGia = x.PhieuMuon.DocGia.HoTen,
                            MaCuonSach = x.MaCuonSach,
                            TenSach = x.CuonSach.DauSach.TenDauSach,
                            NgayMuon = x.PhieuMuon.NgayMuon,
                            NgayPhaiTra = x.PhieuMuon.NgayPhaiTra
                        })
                        .ToList();

                    dgvMuonTra.DataSource = danhSach;

                    if (dgvMuonTra.Columns["Id"] != null)
                        dgvMuonTra.Columns["Id"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        // Chức năng mượn sách
        private void BtnMuon_Click(object sender, EventArgs e)
        {
            if (cboDocGia.SelectedValue == null ||
                cboCuonSach.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn độc giả và cuốn sách!");
                return;
            }

            if (dtpNgayTra.Value.Date <= DateTime.Today)
            {
                MessageBox.Show("Ngày phải trả phải sau ngày mượn!");
                return;
            }

            try
            {
                using (var db = new AppDbContext())
                {
                    string maDocGia =
                        cboDocGia.SelectedValue.ToString();

                    string maCuonSach =
                        cboCuonSach.SelectedValue.ToString();

                    var docGia = db.DocGias
                        .FirstOrDefault(x => x.MaDocGia == maDocGia);

                    var cuonSach = db.CuonSachs
                        .FirstOrDefault(x =>
                            x.MaCuonSach == maCuonSach);

                    if (docGia == null || cuonSach == null)
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu!");
                        return;
                    }

                    if (docGia.BiKhoaThe)
                    {
                        MessageBox.Show("Thẻ độc giả đã bị khóa!");
                        return;
                    }

                    if (cuonSach.TrangThai != TrangThaiSach.SanSang)
                    {
                        MessageBox.Show("Cuốn sách hiện không sẵn sàng!");
                        return;
                    }

                    PhieuMuon phieu = new PhieuMuon
                    {
                        MaDocGia = maDocGia,
                        NgayMuon = DateTime.Today,
                        NgayPhaiTra = dtpNgayTra.Value.Date
                    };

                    ChiTietPhieuMuon chiTiet =
                        new ChiTietPhieuMuon
                        {
                            MaCuonSach = maCuonSach
                        };

                    phieu.ChiTietPhieuMuons.Add(chiTiet);

                    cuonSach.TrangThai = TrangThaiSach.DangMuon;

                    db.PhieuMuons.Add(phieu);
                    db.SaveChanges();
                }

                MessageBox.Show("Mượn sách thành công!");

                LoadDuLieu();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi mượn sách: " + ex.Message);
            }
        }

        // Chức năng trả sách
        private void BtnTra_Click(object sender, EventArgs e)
        {
            if (dgvMuonTra.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Vui lòng chọn sách cần trả trong danh sách!");
                return;
            }

            if (cboTinhTrang.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn tình trạng sách!");
                return;
            }

            try
            {
                int id = Convert.ToInt32(
                    dgvMuonTra.SelectedRows[0]
                    .Cells["Id"].Value);

                string tinhTrang =
                    cboTinhTrang.SelectedItem.ToString();

                int soNgayQuaHan = 0;

                using (var db = new AppDbContext())
                {
                    var chiTiet = db.ChiTietPhieuMuons
                        .Include(x => x.PhieuMuon)
                        .Include(x => x.CuonSach)
                        .FirstOrDefault(x => x.Id == id);

                    if (chiTiet == null ||
                        chiTiet.NgayTraThucTe != null)
                    {
                        MessageBox.Show(
                            "Phiếu mượn không hợp lệ hoặc sách đã được trả!");
                        return;
                    }

                    chiTiet.NgayTraThucTe = DateTime.Today;
                    chiTiet.TinhTrangKhiTra = tinhTrang;

                    if (chiTiet.CuonSach != null)
                    {
                        if (tinhTrang == "Tốt")
                            chiTiet.CuonSach.TrangThai =
                                TrangThaiSach.SanSang;

                        else if (tinhTrang == "Hỏng")
                            chiTiet.CuonSach.TrangThai =
                                TrangThaiSach.Hong;

                        else if (tinhTrang == "Mất")
                            chiTiet.CuonSach.TrangThai =
                                TrangThaiSach.Mat;
                    }

                    soNgayQuaHan = Math.Max(
                        0,
                        (DateTime.Today -
                         chiTiet.PhieuMuon.NgayPhaiTra.Date).Days);

                    db.SaveChanges();
                }

                string thongBao = "Trả sách thành công!";

                if (soNgayQuaHan > 0)
                {
                    thongBao += "\nSố ngày quá hạn: " +
                        soNgayQuaHan +
                        "\nCần xem xét lập hóa đơn phạt.";
                }

                MessageBox.Show(thongBao);

                LoadDuLieu();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi trả sách: " + ex.Message);
            }
        }
    }
}
