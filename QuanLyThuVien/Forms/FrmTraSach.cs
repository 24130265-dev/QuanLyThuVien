using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using QuanLyThuVien.Data;
using QuanLyThuVien.Entities;
namespace QuanLyThuVien.Forms;

public partial class FrmTraSach : Form
{
    public partial class FrmTraSach : Form
    {
        private AppDbContext _context;
        private const decimal PHI_PHAT_MOI_NGAY = 5000; // 5,000 VNĐ/ngày trễ

        public FrmTraSach()
        {
            InitializeComponent();
            _context = new AppDbContext();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string maPhieu = txtMaPhieuMuon.Text.Trim();
            if (string.IsNullOrEmpty(maPhieu))
            {
                MessageBox.Show("Vui lòng nhập mã phiếu mượn cần trả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LoadChiTietPhieuMuon(maPhieu);
        }

        private void LoadChiTietPhieuMuon(string maPhieu)
        {
            // Sử dụng LINQ để lấy danh sách sách chưa trả từ phiếu mượn
            var chiTietMuon = _context.ChiTietPhieuMuons
                .Where(ct => ct.MaPhieuMuon == maPhieu && ct.NgayTra == null)
                .Select(ct => new
                {
                    ct.MaCuonSach,
                    ct.CuonSach.DauSach.TenSach,
                    ct.NgayPhaiTra,
                    SoNgayQuaHan = (DateTime.Now > ct.NgayPhaiTra) ? (DateTime.Now - ct.NgayPhaiTra).Value.Days : 0,
                    TienPhat = (DateTime.Now > ct.NgayPhaiTra) ? ((DateTime.Now - ct.NgayPhaiTra).Value.Days * PHI_PHAT_MOI_NGAY) : 0
                })
                .ToList();

            dgvDanhSachTra.DataSource = chiTietMuon;

            if (chiTietMuon.Count == 0)
            {
                MessageBox.Show("Phiếu mượn này đã được trả hết hoặc không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblTongTienPhat.Text = "0 VNĐ";
            }
            else
            {
                decimal tongPhat = chiTietMuon.Sum(x => x.TienPhat);
                lblTongTienPhat.Text = $"{tongPhat:N0} VNĐ";
            }
        }

        private void btnXacNhanTra_Click(object sender, EventArgs e)
        {
            string maPhieu = txtMaPhieuMuon.Text.Trim();
            if (dgvDanhSachTra.Rows.Count == 0) return;

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    decimal tongTienPhat = 0;

                    foreach (DataGridViewRow row in dgvDanhSachTra.Rows)
                    {
                        string maCuonSach = row.Cells["MaCuonSach"].Value.ToString();
                        
                        // Cập nhật chi tiết phiếu mượn
                        var chiTiet = _context.ChiTietPhieuMuons
                            .FirstOrDefault(ct => ct.MaPhieuMuon == maPhieu && ct.MaCuonSach == maCuonSach);
                        
                        if (chiTiet != null)
                        {
                            chiTiet.NgayTra = DateTime.Now;
                            int soNgayTre = (DateTime.Now > chiTiet.NgayPhaiTra) ? (DateTime.Now - chiTiet.NgayPhaiTra).Value.Days : 0;
                            
                            if (soNgayTre > 0)
                            {
                                tongTienPhat += soNgayTre * PHI_PHAT_MOI_NGAY;
                            }

                            // Cập nhật trạng thái cuốn sách thành "Sẵn sàng"
                            var cuonSach = _context.CuonSachs.Find(maCuonSach);
                            if (cuonSach != null)
                            {
                                cuonSach.TrangThai = TrangThaiSach.SanSang;
                            }
                        }
                    }

                    // Nếu có phát sinh tiền phạt, tạo Hóa đơn phạt
                    if (tongTienPhat > 0)
                    {
                        var hoaDonPhat = new HoaDonPhat
                        {
                            MaHoaDon = Guid.NewGuid().ToString(),
                            MaPhieuMuon = maPhieu,
                            SoTienPhat = tongTienPhat,
                            LyDo = "Trễ hạn trả sách",
                            TrangThaiThanhToan = false
                        };
                        _context.HoaDonPhats.Add(hoaDonPhat);
                    }

                    _context.SaveChanges();
                    transaction.Commit();

                    MessageBox.Show("Trả sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadChiTietPhieuMuon(maPhieu); // Refresh lại grid
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"Đã xảy ra lỗi trong quá trình xử lý: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}