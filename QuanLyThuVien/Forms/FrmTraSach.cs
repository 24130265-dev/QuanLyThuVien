using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Entities;
namespace QuanLyThuVien.Forms;

public partial class FrmTraSach : Form
{
    private readonly AppDbContext _context;
    
        public FrmTraSach()
        {
            InitializeComponent();
            _context = new AppDbContext();
        }
        
        private void FrmTraSach_Load(object sender, EventArgs e)
        {
            CauHinhCotLuoiChiTiet();
            TaiDanhSachPhieuDangMuon();
        }
        
        private void CauHinhCotLuoiChiTiet()
        {
            dgvChiTietMuon.Columns.Clear();

            var colChon = new DataGridViewCheckBoxColumn
            {
                Name = "ChonTra",
                HeaderText = "Trả",
                Width = 45
            };
            dgvChiTietMuon.Columns.Add(colChon);

            dgvChiTietMuon.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ChiTietId",
                HeaderText = "Mã CT",
                ReadOnly = true,
                Visible = false
            });

            dgvChiTietMuon.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaCuonSach",
                HeaderText = "Mã Cuốn Sách",
                ReadOnly = true
            });

            dgvChiTietMuon.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenDauSach",
                HeaderText = "Tên Đầu Sách",
                ReadOnly = true
            });

            dgvChiTietMuon.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "HanTra",
                HeaderText = "Hạn Trả",
                ReadOnly = true
            });

            dgvChiTietMuon.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SoNgayTre",
                HeaderText = "Số Ngày Trễ",
                ReadOnly = true
            });

            var colTinhTrang = new DataGridViewComboBoxColumn
            {
                Name = "TinhTrangTra",
                HeaderText = "Tình Trạng Sách Trả",
                DataSource = new[] { "Bình thường", "Hư hỏng", "Mất sách" }
            };
            dgvChiTietMuon.Columns.Add(colTinhTrang);
        }
        
        private void TaiDanhSachPhieuDangMuon(string tuKhoa = "")
        {
            var query = _context.PhieuMuons
                .Include(p => p.DocGia)
                .Include(p => p.ChiTietPhieuMuons)
                .Where(p => p.ChiTietPhieuMuons.Any(ct => ct.NgayTraThucTe == null))
                .AsQueryable();
            
            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                tuKhoa = tuKhoa.Trim().ToLower();
                query = query.Where(p =>
                    p.MaPhieuMuon.ToString().Contains(tuKhoa) ||
                    (p.DocGia != null && p.DocGia.HoTen.ToLower().Contains(tuKhoa)));
            }
            var danhSach = query
                .OrderByDescending(p => p.NgayMuon)
                .Select(p => new
                {
                    Id = p.MaPhieuMuon,
                    p.MaDocGia,
                    HienThi = $"Phiếu #{p.MaPhieuMuon} - {p.DocGia.HoTen} (Mượn: {p.NgayMuon:dd/MM/yyyy})"
                })
                .ToList();
            cboPhieuMuon.DisplayMember = "HienThi";
            cboPhieuMuon.ValueMember = "Id";
            cboPhieuMuon.DataSource = danhSach;

            if (danhSach.Count == 0)
            {
                dgvChiTietMuon.Rows.Clear();
                dgvHoaDonPhat.DataSource = null;
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            TaiDanhSachPhieuDangMuon(txtTimKiem.Text);
        }

        private void cboPhieuMuon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPhieuMuon.SelectedValue == null || !(cboPhieuMuon.SelectedValue is int phieuMuonId))
                return;
            
            var phieu = _context.PhieuMuons
                .Include(p => p.ChiTietPhieuMuons)
                .ThenInclude(ct => ct.CuonSach)
                .ThenInclude(cs => cs.DauSach)
                .FirstOrDefault(p => p.MaPhieuMuon == phieuMuonId);
            
            if (phieu == null) return;
            
            dgvChiTietMuon.Rows.Clear();
            DateTime homNay = DateTime.Today;

            foreach (var ct in phieu.ChiTietPhieuMuons.Where(x => x.NgayTraThucTe == null))
            {
                int soNgayTre = homNay > phieu.NgayPhaiTra.Date
                    ? (homNay - phieu.NgayPhaiTra.Date).Days
                    : 0;
                dgvChiTietMuon.Rows.Add(
                    true,
                    ct.Id,
                    ct.MaCuonSach.ToString(),
                    ct.CuonSach?.DauSach?.TenDauSach ?? "Không rõ tên sách",
                    phieu.NgayPhaiTra.ToString("dd/MM/yyyy"),
                    soNgayTre,
                    "Bình thường"
                );
            }
            
            TinhTongTienPhat();
            TaiHoaDonPhatCuaDocGia(phieu.MaDocGia);
        }

        private void TinhTongTienPhat()
        {
            decimal donGiaTre = numDonGiaTre.Value;
            decimal phatHuHong = numTienPhatHuHong.Value;
            int tongNgayTre = 0;
            var lyDoList = new List<string>();
            foreach (DataGridViewRow row in dgvChiTietMuon.Rows)
            {
                bool duocChon = Convert.ToBoolean(row.Cells["ChonTra"].Value ?? false);
                if (!duocChon) continue;

                int ngayTre = Convert.ToInt32(row.Cells["SoNgayTre"].Value ?? 0);
                string tinhTrang = row.Cells["TinhTrangTra"].Value?.ToString() ?? "Bình thường";
                string tenSach = row.Cells["TenDauSach"].Value?.ToString() ?? "";

                if (ngayTre > 0)
                {
                    tongNgayTre += ngayTre;
                    lyDoList.Add($"Trễ {ngayTre} ngày ({tenSach})");
                }

                if (tinhTrang != "Bình thường")
                {
                    lyDoList.Add($"{tinhTrang} ({tenSach})");
                }
            }

            decimal tienPhatTre = tongNgayTre * donGiaTre;
            decimal tongPhat = tienPhatTre + phatHuHong;

            lblTongTienPhat.Text = $"{tongPhat:N0} VNĐ";
            txtLyDoPhat.Text = string.Join("; ", lyDoList);
        }

        private void btnXacNhanTra_Click(object sender, EventArgs e)
        {
            if (cboPhieuMuon.SelectedValue == null || !(cboPhieuMuon.SelectedValue is int phieuMuonId))
            {
                MessageBox.Show("Vui lòng chọn phiếu mượn cần trả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var chiTietIdsDuocChon = new Dictionary<int, string>();
            foreach (DataGridViewRow row in dgvChiTietMuon.Rows)
            {
                if (Convert.ToBoolean(row.Cells["ChonTra"].Value ?? false))
                {
                    int ctId = Convert.ToInt32(row.Cells["ChiTietId"].Value);
                    string tinhTrang = row.Cells["TinhTrangTra"].Value?.ToString() ?? "Bình thường";
                    chiTietIdsDuocChon[ctId] = tinhTrang;
                }
            }
            if (chiTietIdsDuocChon.Count == 0)
            {
                MessageBox.Show("Vui lòng tích chọn ít nhất một cuốn sách để trả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var phieu = _context.PhieuMuons
                .Include(p => p.ChiTietPhieuMuons)
                .ThenInclude(ct => ct.CuonSach)
                .FirstOrDefault(p => p.MaPhieuMuon == phieuMuonId);

            if (phieu == null) return;

            // 1. Cập nhật từng chi tiết phiếu mượn & trạng thái cuốn sách
            foreach (var ct in phieu.ChiTietPhieuMuons)
            {
                if (!chiTietIdsDuocChon.ContainsKey(ct.Id)) continue;

                ct.NgayTraThucTe = DateTime.Now;
                string tinhTrang = chiTietIdsDuocChon[ct.Id];

                if (ct.CuonSach != null)
                {
                    // Đối chiếu enum/hằng số trong Data/TrangThaiSach.cs của bạn
                    if (tinhTrang == "Bình thường")
                        ct.CuonSach.TrangThai = TrangThaiSach.SanSang;
                    else if (tinhTrang == "Hỏng")
                        ct.CuonSach.TrangThai = TrangThaiSach.Hong;
                    else
                        ct.CuonSach.TrangThai = TrangThaiSach.Mat;
                }
            }

            // 2. Nếu có phát sinh tiền phạt -> Tạo HoaDonPhat
            decimal tongTienPhat = 0;
            decimal.TryParse(lblTongTienPhat.Text.Replace("VNĐ", "").Replace(",", "").Replace(".", "").Trim(), out tongTienPhat);

            if (tongTienPhat > 0)
            {
                var hoaDonPhat = new HoaDonPhat
                {
                    MaDocGia = phieu.MaDocGia,
                    MaPhieuMuon = phieu.MaPhieuMuon,
                    NgayLap = DateTime.Now,
                    TienPhatQuaHan = tongTienPhat,
                    LyDo = string.IsNullOrWhiteSpace(txtLyDoPhat.Text) ? "Phạt vi phạm mượn trả" : txtLyDoPhat.Text.Trim(),
                    DaThanhToan = chkDaThanhToanNgay.Checked
                };
                _context.HoaDonPhats.Add(hoaDonPhat);
            }

            _context.SaveChanges();

            MessageBox.Show("Đã ghi nhận trả sách thành công!", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);

            numTienPhatHuHong.Value = 0;
            TaiDanhSachPhieuDangMuon(txtTimKiem.Text);
        }

        private void TaiHoaDonPhatCuaDocGia(String MaDocGia)
        {
            var hoaDons = _context.HoaDonPhats
                .Where(h => h.MaDocGia == MaDocGia)
                .OrderByDescending(h => h.NgayLap)
                .Select(h => new
                {
                    MaHoaDon = h.MaHoaDon,
                    MaPhieuMuon = h.MaPhieuMuon,
                    NgayLap = h.NgayLap.ToString("dd/MM/yyyy HH:mm"),
                    SoTien = h.TienPhatQuaHan.ToString("N0") + " VNĐ",
                    h.LyDo,
                    TrangThai = h.DaThanhToan ? "Đã thanh toán" : "Chưa thanh toán"
                })
                .ToList();

            dgvHoaDonPhat.DataSource = hoaDons;
        }

        private void btnThanhToanHoaDon_Click(object sender, EventArgs e)
        {
            if (dgvHoaDonPhat.CurrentRow == null) return;

            int maHoaDon = Convert.ToInt32(dgvHoaDonPhat.CurrentRow.Cells["MaHoaDon"].Value);
            var hd = _context.HoaDonPhats.FirstOrDefault(x => x.MaHoaDon == maHoaDon);

            if (hd == null) return;
            if (hd.DaThanhToan)
            {
                MessageBox.Show("Hóa đơn này đã được thanh toán trước đó.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            hd.DaThanhToan = true;
            _context.SaveChanges();

            MessageBox.Show($"Đã thu tiền hóa đơn phạt #{hd.MaHoaDon}!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            TaiHoaDonPhatCuaDocGia(hd.MaDocGia);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _context?.Dispose();
            base.OnFormClosed(e);
        }
}
    