
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Entities;

namespace QuanLyThuVien
{
    public class FormMuonTra : Form
    {
        private readonly ComboBox cboDocGia = new();
        private readonly ComboBox cboCuonSach = new();
        private readonly ComboBox cboTinhTrang = new();
        private readonly DateTimePicker dtpNgayTra = new();
        private readonly DataGridView dgvMuonTra = new();
        private readonly Button btnMuon = new();
        private readonly Button btnTra = new();
        private readonly Button btnLamMoi = new();

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
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;

            var lblTieuDe = new Label
            {
                Text = "QUẢN LÝ MƯỢN TRẢ SÁCH",
                Font = new Font("Arial", 20, FontStyle.Bold),
                Location = new Point(280, 20),
                Size = new Size(500, 40),
                TextAlign = ContentAlignment.MiddleCenter
            };
            Controls.Add(lblTieuDe);

            var grpMuon = new GroupBox
            {
                Text = "Thông tin mượn sách",
                Location = new Point(20, 80),
                Size = new Size(450, 190)
            };
            Controls.Add(grpMuon);

            var lblDocGia = new Label { Text = "Độc giả:", Location = new Point(20, 35), AutoSize = true };
            grpMuon.Controls.Add(lblDocGia);

            cboDocGia.Location = new Point(130, 32);
            cboDocGia.Size = new Size(280, 30);
            cboDocGia.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDocGia.SelectedIndexChanged += (_, __) => CapNhatTrangThaiNut();
            grpMuon.Controls.Add(cboDocGia);

            var lblSach = new Label { Text = "Cuốn sách:", Location = new Point(20, 80), AutoSize = true };
            grpMuon.Controls.Add(lblSach);

            cboCuonSach.Location = new Point(130, 77);
            cboCuonSach.Size = new Size(280, 30);
            cboCuonSach.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCuonSach.SelectedIndexChanged += (_, __) => CapNhatTrangThaiNut();
            grpMuon.Controls.Add(cboCuonSach);

            var lblNgayTra = new Label { Text = "Ngày phải trả:", Location = new Point(20, 125), AutoSize = true };
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
            btnMuon.Enabled = false;
            btnMuon.Click += BtnMuon_Click;
            grpMuon.Controls.Add(btnMuon);

            var grpTra = new GroupBox
            {
                Text = "Thông tin trả sách",
                Location = new Point(500, 80),
                Size = new Size(450, 190)
            };
            Controls.Add(grpTra);

            var lblTinhTrang = new Label { Text = "Tình trạng sách:", Location = new Point(20, 45), AutoSize = true };
            grpTra.Controls.Add(lblTinhTrang);

            cboTinhTrang.Location = new Point(150, 42);
            cboTinhTrang.Size = new Size(250, 30);
            cboTinhTrang.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTinhTrang.Items.Add("Tốt");
            cboTinhTrang.Items.Add("Hỏng");
            cboTinhTrang.Items.Add("Mất");
            cboTinhTrang.SelectedIndex = 0;
            cboTinhTrang.Enabled = false;
            grpTra.Controls.Add(cboTinhTrang);

            btnTra.Text = "TRẢ SÁCH";
            btnTra.Location = new Point(150, 95);
            btnTra.Size = new Size(140, 40);
            btnTra.Enabled = false;
            btnTra.Click += BtnTra_Click;
            grpTra.Controls.Add(btnTra);

            var lblDanhSach = new Label
            {
                Text = "DANH SÁCH SÁCH ĐANG MƯỢN",
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(20, 290),
                AutoSize = true
            };
            Controls.Add(lblDanhSach);

            dgvMuonTra.Location = new Point(20, 325);
            dgvMuonTra.Size = new Size(930, 230);
            dgvMuonTra.ReadOnly = true;
            dgvMuonTra.AllowUserToAddRows = false;
            dgvMuonTra.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMuonTra.MultiSelect = false;
            dgvMuonTra.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMuonTra.SelectionChanged += (_, __) => CapNhatTrangThaiNut();
            Controls.Add(dgvMuonTra);

            btnLamMoi.Text = "LÀM MỚI";
            btnLamMoi.Location = new Point(820, 570);
            btnLamMoi.Size = new Size(130, 35);
            btnLamMoi.Click += (_, __) => LoadDuLieu();
            Controls.Add(btnLamMoi);
        }

        private void CapNhatTrangThaiNut()
        {
            var coDocGia = cboDocGia.SelectedIndex >= 0 && cboDocGia.SelectedValue is string maDocGia && !string.IsNullOrWhiteSpace(maDocGia);
            var coSach = cboCuonSach.SelectedIndex >= 0 && cboCuonSach.SelectedValue is string maCuonSach && !string.IsNullOrWhiteSpace(maCuonSach);
            var coDongDuocChon = dgvMuonTra.SelectedRows.Count > 0;

            btnMuon.Enabled = coDocGia && coSach;
            btnTra.Enabled = coDongDuocChon;
            cboTinhTrang.Enabled = coDongDuocChon;
        }

        private void LoadDuLieu()
        {
            try
            {
                using var db = new AppDbContext();

                var docGias = db.DocGias
                    .AsNoTracking()
                    .Where(x => !x.BiKhoaThe)
                    .OrderBy(x => x.HoTen)
                    .ToList();

                cboDocGia.DataSource = null;
                cboDocGia.Items.Clear();
                cboDocGia.DisplayMember = nameof(DocGia.HoTen);
                cboDocGia.ValueMember = nameof(DocGia.MaDocGia);
                cboDocGia.DataSource = docGias;
                cboDocGia.SelectedIndex = -1;

                var cuonSachs = db.CuonSachs
                    .AsNoTracking()
                    .Include(x => x.DauSach)
                    .Where(x => x.TrangThai == TrangThaiSach.SanSang)
                    .OrderBy(x => x.MaCuonSach)
                    .ToList()
                    .Select(x => new
                    {
                        x.MaCuonSach,
                        TenHienThi = x.MaCuonSach + " - " + (x.DauSach == null ? "" : x.DauSach.TenDauSach)
                    })
                    .ToList();

                cboCuonSach.DataSource = null;
                cboCuonSach.Items.Clear();
                cboCuonSach.DisplayMember = "TenHienThi";
                cboCuonSach.ValueMember = "MaCuonSach";
                cboCuonSach.DataSource = cuonSachs;
                cboCuonSach.SelectedIndex = -1;

                var danhSach = db.ChiTietPhieuMuons
                    .AsNoTracking()
                    .Include(x => x.PhieuMuon)
                        .ThenInclude(x => x!.DocGia)
                    .Include(x => x.CuonSach)
                        .ThenInclude(x => x!.DauSach)
                    .Where(x => x.NgayTraThucTe == null)
                    .OrderByDescending(x => x.MaPhieuMuon)
                    .Select(x => new
                    {
                        x.Id,
                        MaPhieuMuon = x.MaPhieuMuon,
                        MaDocGia = x.PhieuMuon != null ? x.PhieuMuon.MaDocGia : null,
                        TenDocGia = x.PhieuMuon != null && x.PhieuMuon.DocGia != null ? x.PhieuMuon.DocGia.HoTen : null,
                        MaCuonSach = x.MaCuonSach,
                        TenSach = x.CuonSach != null && x.CuonSach.DauSach != null ? x.CuonSach.DauSach.TenDauSach : null,
                        NgayMuon = x.PhieuMuon != null ? (DateTime?)x.PhieuMuon.NgayMuon : null,
                        NgayPhaiTra = x.PhieuMuon != null ? (DateTime?)x.PhieuMuon.NgayPhaiTra : null
                    })
                    .ToList();

                dgvMuonTra.DataSource = danhSach;

                if (dgvMuonTra.Columns.Contains("Id"))
                    dgvMuonTra.Columns["Id"].Visible = false;

                if (dgvMuonTra.Columns.Contains("MaPhieuMuon"))
                    dgvMuonTra.Columns["MaPhieuMuon"].HeaderText = "Mã phiếu";

                if (dgvMuonTra.Columns.Contains("MaDocGia"))
                    dgvMuonTra.Columns["MaDocGia"].HeaderText = "Mã độc giả";

                if (dgvMuonTra.Columns.Contains("TenDocGia"))
                    dgvMuonTra.Columns["TenDocGia"].HeaderText = "Tên độc giả";

                if (dgvMuonTra.Columns.Contains("MaCuonSach"))
                    dgvMuonTra.Columns["MaCuonSach"].HeaderText = "Mã cuốn";

                if (dgvMuonTra.Columns.Contains("TenSach"))
                    dgvMuonTra.Columns["TenSach"].HeaderText = "Tên sách";

                if (dgvMuonTra.Columns.Contains("NgayMuon"))
                    dgvMuonTra.Columns["NgayMuon"].HeaderText = "Ngày mượn";

                if (dgvMuonTra.Columns.Contains("NgayPhaiTra"))
                    dgvMuonTra.Columns["NgayPhaiTra"].HeaderText = "Ngày phải trả";

                dgvMuonTra.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CapNhatTrangThaiNut();
            }
        }

        private void BtnMuon_Click(object? sender, EventArgs e)
        {
            if (cboDocGia.SelectedValue is not string maDocGia || string.IsNullOrWhiteSpace(maDocGia))
            {
                MessageBox.Show("Vui lòng chọn độc giả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboCuonSach.SelectedValue is not string maCuonSach || string.IsNullOrWhiteSpace(maCuonSach))
            {
                MessageBox.Show("Vui lòng chọn cuốn sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtpNgayTra.Value.Date <= DateTime.Today)
            {
                MessageBox.Show("Ngày phải trả phải sau ngày mượn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var db = new AppDbContext();

                var docGia = db.DocGias.FirstOrDefault(x => x.MaDocGia == maDocGia);
                var cuonSach = db.CuonSachs.FirstOrDefault(x => x.MaCuonSach == maCuonSach);

                if (docGia == null || cuonSach == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu độc giả hoặc cuốn sách!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (docGia.BiKhoaThe)
                {
                    MessageBox.Show("Thẻ độc giả đã bị khóa, không thể mượn sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cuonSach.TrangThai != TrangThaiSach.SanSang)
                {
                    MessageBox.Show("Cuốn sách hiện không sẵn sàng để mượn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var phieuMuon = new PhieuMuon
                {
                    MaDocGia = maDocGia,
                    NgayMuon = DateTime.Today,
                    NgayPhaiTra = dtpNgayTra.Value.Date
                };

                phieuMuon.ChiTietPhieuMuons.Add(new ChiTietPhieuMuon
                {
                    MaCuonSach = maCuonSach
                });

                cuonSach.TrangThai = TrangThaiSach.DangMuon;
                db.PhieuMuons.Add(phieuMuon);
                db.SaveChanges();

                MessageBox.Show("Mượn sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDuLieu();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mượn sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnTra_Click(object? sender, EventArgs e)
        {
            if (dgvMuonTra.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sách cần trả trong danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboTinhTrang.SelectedItem is not string tinhTrang || string.IsNullOrWhiteSpace(tinhTrang))
            {
                MessageBox.Show("Vui lòng chọn tình trạng sách khi trả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var id = Convert.ToInt32(dgvMuonTra.SelectedRows[0].Cells["Id"].Value);
                int soNgayQuaHan = 0;

                using var db = new AppDbContext();

                var chiTiet = db.ChiTietPhieuMuons
                    .Include(x => x.PhieuMuon)
                    .Include(x => x.CuonSach)
                    .FirstOrDefault(x => x.Id == id);

                if (chiTiet == null || chiTiet.NgayTraThucTe != null || chiTiet.PhieuMuon == null || chiTiet.CuonSach == null)
                {
                    MessageBox.Show("Phiếu mượn không hợp lệ hoặc sách đã được trả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                chiTiet.NgayTraThucTe = DateTime.Today;
                chiTiet.TinhTrangKhiTra = tinhTrang;

                if (tinhTrang == "Tốt")
                    chiTiet.CuonSach.TrangThai = TrangThaiSach.SanSang;
                else if (tinhTrang == "Hỏng")
                    chiTiet.CuonSach.TrangThai = TrangThaiSach.Hong;
                else if (tinhTrang == "Mất")
                    chiTiet.CuonSach.TrangThai = TrangThaiSach.Mat;

                soNgayQuaHan = Math.Max(0, (DateTime.Today - chiTiet.PhieuMuon.NgayPhaiTra.Date).Days);
                db.SaveChanges();

                string thongBao = "Trả sách thành công!";
                if (soNgayQuaHan > 0)
                {
                    thongBao += "\nSố ngày quá hạn: " + soNgayQuaHan + "\nCần xem xét lập hóa đơn phạt.";
                }

                MessageBox.Show(thongBao, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDuLieu();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi trả sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
