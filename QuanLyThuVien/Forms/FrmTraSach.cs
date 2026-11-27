using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Entities;
using QuanLyThuVien.UI;


namespace QuanLyThuVien.Forms;

public partial class FrmTraSach : Form
{
    private readonly AppDbContext _context;
    private decimal _tongTienPhat = 0;
    public FrmTraSach()
    {
        InitializeComponent();
        _context = new AppDbContext();
        CauHinhGiaoDien();
    }

    private void FrmTraSach_Load(object sender, EventArgs e)
    {
        CauHinhCotLuoiChiTiet();
        KhoiTaoCotLuoiHoaDonPhatMacDinh();
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
                (p.DocGia != null && p.DocGia.HoTen != null && p.DocGia.HoTen.ToLower().Contains(tuKhoa)));
        }

        var danhSach = query
            .OrderByDescending(p => p.NgayMuon)
            .Select(p => new
            {
                Id = p.MaPhieuMuon,
                p.MaDocGia,
                TenDocGia = p.DocGia != null ? (p.DocGia.HoTen ?? "") : "",
                p.NgayMuon
            })
            .ToList()
            .Select(p => new
            {
                p.Id,
                p.MaDocGia,
                HienThi = $"Phiếu #{p.Id} - {p.TenDocGia} (Mượn: {p.NgayMuon:dd/MM/yyyy})"
            })
            .ToList();

        cboPhieuMuon.DisplayMember = "HienThi";
        cboPhieuMuon.ValueMember = "Id";
        cboPhieuMuon.DataSource = danhSach;

        if (danhSach.Count == 0)
        {
            dgvChiTietMuon.Rows.Clear();
            dgvHoaDonPhat.DataSource = null;
            TinhTongTienPhat();
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
                    .ThenInclude(cs => cs!.DauSach)
            .FirstOrDefault(p => p.MaPhieuMuon == phieuMuonId);

        if (phieu == null)
        {
            MessageBox.Show("Không tìm thấy phiếu mượn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

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
        _tongTienPhat = tienPhatTre + phatHuHong;

        lblTongTienPhat.Text = $"{_tongTienPhat:N0} VNĐ";
        txtLyDoPhat.Text = string.Join("; ", lyDoList);
    }

    private void btnXacNhanTra_Click(object sender, EventArgs e)
    {
        _context.ChangeTracker.Clear();

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

        if (phieu == null)
        {
            MessageBox.Show("Không tìm thấy thông tin phiếu mượn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // 1. Cập nhật từng chi tiết phiếu mượn & trạng thái cuốn sách
        foreach (var ct in phieu.ChiTietPhieuMuons)
        {
            if (!chiTietIdsDuocChon.ContainsKey(ct.Id)) continue;

            ct.NgayTraThucTe = DateTime.Now;
            string tinhTrang = chiTietIdsDuocChon[ct.Id];

            if (ct.CuonSach != null)
            {
                if (tinhTrang == "Bình thường")
                    ct.CuonSach.TrangThai = TrangThaiSach.SanSang;
                else if (tinhTrang == "Hư hỏng")
                    ct.CuonSach.TrangThai = TrangThaiSach.Hong;
                else
                    ct.CuonSach.TrangThai = TrangThaiSach.Mat;
            }
        }

        // 2. Nếu có phát sinh tiền phạt -> Tạo HoaDonPhat
        if (_tongTienPhat > 0)
        {
            var hoaDonPhat = new HoaDonPhat
            {
                MaDocGia = phieu.MaDocGia,
                MaPhieuMuon = phieu.MaPhieuMuon,
                NgayLap = DateTime.Now,
                TienPhatQuaHan = _tongTienPhat - numTienPhatHuHong.Value,
                TienBoiThuong = numTienPhatHuHong.Value,
                LyDo = string.IsNullOrWhiteSpace(txtLyDoPhat.Text) ? "Phạt vi phạm mượn trả" : txtLyDoPhat.Text,
                DaThanhToan = chkDaThanhToanNgay.Checked
            };
            _context.HoaDonPhats.Add(hoaDonPhat);

            // Chỉ cộng nợ khi phát sinh phạt và chưa thanh toán ngay
            if (!chkDaThanhToanNgay.Checked)
            {
                var dg = _context.DocGias.FirstOrDefault(d => d.MaDocGia == phieu.MaDocGia);
                if (dg != null)
                {
                    dg.TienNoPhat += _tongTienPhat;
                }
            }
        }

        _context.SaveChanges();


        Toast.ThanhCong(this, "Đã ghi nhận trả sách thành công!");
        numTienPhatHuHong.Value = 0;
        TaiDanhSachPhieuDangMuon(txtTimKiem.Text);
    }

    private void TaiHoaDonPhatCuaDocGia(string maDocGia)
    {
        var hoaDons = _context.HoaDonPhats
            .Where(h => h.MaDocGia == maDocGia)
            .OrderByDescending(h => h.NgayLap)
            .Select(h => new
            {
                MaHoaDon = h.MaHoaDon,
                MaPhieuMuon = h.MaPhieuMuon,
                NgayLap = h.NgayLap.ToString("dd/MM/yyyy HH:mm"),
                SoTien = (h.TienPhatQuaHan + h.TienBoiThuong).ToString("N0") + " VNĐ",
                LyDo = h.LyDo ?? "",
                TrangThai = h.DaThanhToan ? "Đã thanh toán" : "Chưa thanh toán"
            })
            .ToList();

        dgvHoaDonPhat.Columns.Clear();
        dgvHoaDonPhat.DataSource = hoaDons;

        if (dgvHoaDonPhat.Columns.Count > 0)
        {
            dgvHoaDonPhat.Columns["MaHoaDon"]!.HeaderText = "Mã Hóa Đơn";
            dgvHoaDonPhat.Columns["MaPhieuMuon"]!.HeaderText = "Mã Phiếu Mượn";
            dgvHoaDonPhat.Columns["NgayLap"]!.HeaderText = "Ngày Lập";
            dgvHoaDonPhat.Columns["SoTien"]!.HeaderText = "Số Tiền Phạt";
            dgvHoaDonPhat.Columns["LyDo"]!.HeaderText = "Lý Do Phạt";
            dgvHoaDonPhat.Columns["TrangThai"]!.HeaderText = "Trạng Thái";
        }
    }

    private void btnThanhToanHoaDon_Click(object sender, EventArgs e)
    {
        _context.ChangeTracker.Clear();

        if (dgvHoaDonPhat.CurrentRow == null) return;

        int maHoaDon = Convert.ToInt32(dgvHoaDonPhat.CurrentRow.Cells["MaHoaDon"].Value);
        var hd = _context.HoaDonPhats.FirstOrDefault(x => x.MaHoaDon == maHoaDon);

        if (hd == null)
        {
            MessageBox.Show("Không tìm thấy hóa đơn phạt!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (hd.DaThanhToan)
        {
            MessageBox.Show("Hóa đơn này đã được thanh toán trước đó.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        hd.DaThanhToan = true;
        var docGia = _context.DocGias.First(d => d.MaDocGia == hd.MaDocGia);
        docGia.TienNoPhat = Math.Max(0, docGia.TienNoPhat - (hd.TienPhatQuaHan + hd.TienBoiThuong));
        _context.SaveChanges();

        Toast.ThanhCong(this, $"Đã thu tiền hóa đơn phạt #{hd.MaHoaDon}!");
        TaiHoaDonPhatCuaDocGia(hd.MaDocGia);
    }

    private void KhoiTaoCotLuoiHoaDonPhatMacDinh()
    {
        if (dgvHoaDonPhat.Columns.Count > 0) return;

        dgvHoaDonPhat.Columns.Add("MaHoaDon", "Mã Hóa Đơn");
        dgvHoaDonPhat.Columns.Add("MaPhieuMuon", "Mã Phiếu Mượn");
        dgvHoaDonPhat.Columns.Add("NgayLap", "Ngày Lập");
        dgvHoaDonPhat.Columns.Add("SoTien", "Số Tiền Phạt");
        dgvHoaDonPhat.Columns.Add("LyDo", "Lý Do Phạt");
        dgvHoaDonPhat.Columns.Add("TrangThai", "Trạng Thái");
    }

    private void CauHinhGiaoDien()
    {
        // 0. Đăng ký các sự kiện tính tiền phạt (chuyển từ Designer.cs sang)
        numDonGiaTre.ValueChanged += (s, e) => TinhTongTienPhat();
        numTienPhatHuHong.ValueChanged += (s, e) => TinhTongTienPhat();
        dgvChiTietMuon.CurrentCellDirtyStateChanged += (s, e) =>
        {
            if (dgvChiTietMuon.IsCurrentCellDirty)
                dgvChiTietMuon.CommitEdit(DataGridViewDataErrorContexts.Commit);
        };
        dgvChiTietMuon.CellValueChanged += (s, e) => TinhTongTienPhat();

        // 1. Áp dụng Theme
        Theme.Apply(this);

        Theme.StyleButton(btnTimKiem, Theme.Secondary);
        Theme.StyleButton(btnXacNhanTra, Theme.Primary);
        Theme.StyleButton(btnThanhToanHoaDon, Color.FromArgb(234, 88, 12));

        pnlTopFilter.BackColor = Theme.CardBg;
        lblTitle.Font = Theme.FontTitle;
        lblTitle.ForeColor = Theme.PrimaryDark;
        lblTongTienPhatTitle.Font = Theme.FontBold;
        lblTongTienPhat.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblTongTienPhat.ForeColor = Theme.Danger;

        // 2. Tự động nối tiếp tọa độ theo đúng độ rộng thực tế của chữ -> không bao giờ dính chữ!
        pnlTopFilter.PerformLayout();
        lblTimKiem.Location = new Point(15, 56);
        txtTimKiem.Location = new Point(lblTimKiem.Right + 10, 53);
        txtTimKiem.Size = new Size(175, 28);

        btnTimKiem.Location = new Point(txtTimKiem.Right + 12, 51);
        btnTimKiem.Size = new Size(115, 32);

        lblChonPhieu.Location = new Point(btnTimKiem.Right + 24, 56);
        cboPhieuMuon.Location = new Point(lblChonPhieu.Right + 10, 53);
        cboPhieuMuon.Size = new Size(360, 28);

        // 3. Tạo khe hở 12px giữa 2 khung trên
        var splitterGap = new Panel
        {
            Dock = DockStyle.Right,
            Width = 12,
            BackColor = Theme.BgMain
        };
        splitMain.Panel1.Controls.Add(splitterGap);
        splitterGap.SendToBack();
        grpThongTinPhat.SendToBack();

        // 4. Tinh chỉnh khung Tính phí phạt bên phải
        grpThongTinPhat.Width = 385;
        grpThongTinPhat.PerformLayout();

        lblDonGiaTre.Location = new Point(15, 38);
        numDonGiaTre.Location = new Point(240, 35);
        numDonGiaTre.Size = new Size(130, 28);

        lblTienPhatHuHong.Location = new Point(15, 76);
        numTienPhatHuHong.Location = new Point(240, 73);
        numTienPhatHuHong.Size = new Size(130, 28);

        lblLyDoPhat.Location = new Point(15, 112);
        txtLyDoPhat.Location = new Point(15, 136);
        txtLyDoPhat.Size = new Size(355, 54);

        lblTongTienPhatTitle.Location = new Point(15, 204);
        lblTongTienPhat.Location = new Point(170, 200);
        chkDaThanhToanNgay.Location = new Point(15, 236);

        btnXacNhanTra.Location = new Point(15, 270);
        btnXacNhanTra.Size = new Size(355, 40);

        // 5. Thu hẹp khoảng hở thừa phía trên 2 bảng DataGridView (mũi tên đỏ 2 chiều)
        // GroupBox mặc định đã chừa ~22px cho tiêu đề nên chỉ cần Padding.Top = 8 là vừa khít đẹp
        grpChiTietMuon.Padding = new Padding(12, 8, 12, 12);
        grpHoaDonPhat.Padding = new Padding(12, 8, 12, 12);

        // 6. Đặt nút Xác nhận thu tiền gọn gàng ở góc dưới bên phải khung Hóa đơn phạt
        var pnlActionBottom = new Panel
        {
            Dock = DockStyle.Bottom,
            Height = 44,
            BackColor = Theme.CardBg
        };

        grpHoaDonPhat.Controls.Remove(btnThanhToanHoaDon);
        btnThanhToanHoaDon.Dock = DockStyle.None;
        btnThanhToanHoaDon.Size = new Size(340, 36);
        btnThanhToanHoaDon.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnThanhToanHoaDon.Location = new Point(pnlActionBottom.Width - btnThanhToanHoaDon.Width, 6);

        pnlActionBottom.Controls.Add(btnThanhToanHoaDon);
        grpHoaDonPhat.Controls.Add(pnlActionBottom);
        dgvHoaDonPhat.BringToFront();
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        _context?.Dispose();
        base.OnFormClosed(e);
    }
}