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

    private static class TinhTrang
    {
        public const string BinhThuong = "Bình thường";
        public const string HuHong = "Hư hỏng";
        public const string MatSach = "Mất sách";
    }

    private decimal _tongTienPhat;
    private decimal _tienPhatTre;
    private decimal _tienPhatHuHong;
    private bool _coSachTre;

    public FrmTraSach()
    {
        InitializeComponent();
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
            DataSource = new[] { TinhTrang.BinhThuong, TinhTrang.HuHong, TinhTrang.MatSach }
        };
        dgvChiTietMuon.Columns.Add(colTinhTrang);
    }

    private void TaiDanhSachPhieuDangMuon(string tuKhoa = "")
    {
        using var db = new AppDbContext();

        var query = db.PhieuMuons
            .AsNoTracking()
            .Where(p => p.ChiTietPhieuMuons.Any(ct => ct.NgayTraThucTe == null));

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
                TenDocGia = p.DocGia != null ? p.DocGia.HoTen : "",
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
            TinhTongTienPhat();
            TaiHoaDonPhatCuaDocGia();
        }
    }

    private void btnTimKiem_Click(object sender, EventArgs e)
    {
        TaiDanhSachPhieuDangMuon(txtTimKiem.Text);
    }

    private void cboPhieuMuon_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (cboPhieuMuon.SelectedValue is not int phieuMuonId)
            return;

        using var db = new AppDbContext();
        var phieu = db.PhieuMuons
            .AsNoTracking()
            .Include(p => p.ChiTietPhieuMuons)
                .ThenInclude(ct => ct.CuonSach)
                    .ThenInclude(cs => cs!.DauSach)
            .FirstOrDefault(p => p.MaPhieuMuon == phieuMuonId);

        if (phieu == null)
        {
            Toast.CanhBao(this, "Không tìm thấy phiếu mượn!");
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
                ct.MaCuonSach,
                ct.CuonSach?.DauSach?.TenDauSach ?? "Không rõ tên sách",
                phieu.NgayPhaiTra.ToString("dd/MM/yyyy"),
                soNgayTre,
                TinhTrang.BinhThuong
            );
        }

        TinhTongTienPhat();
        TaiHoaDonPhatCuaDocGia(phieu.MaDocGia);
    }

    private void TinhTongTienPhat()
    {
        decimal donGiaTre = numDonGiaTre.Value;
        decimal mucPhatHuHong = numTienPhatHuHong.Value;

        int tongNgayTre = 0;
        decimal phatHuHong = 0;
        var lyDoList = new List<string>();

        foreach (DataGridViewRow row in dgvChiTietMuon.Rows)
        {
            bool duocChon = Convert.ToBoolean(row.Cells["ChonTra"].Value ?? false);
            if (!duocChon) continue;

            int ngayTre = Convert.ToInt32(row.Cells["SoNgayTre"].Value ?? 0);
            string tinhTrang = row.Cells["TinhTrangTra"].Value?.ToString() ?? TinhTrang.BinhThuong;
            string tenSach = row.Cells["TenDauSach"].Value?.ToString() ?? "";

            if (ngayTre > 0)
            {
                tongNgayTre += ngayTre;
                lyDoList.Add($"Trễ {ngayTre} ngày ({tenSach})");
            }

            if (tinhTrang == TinhTrang.HuHong || tinhTrang == TinhTrang.MatSach)
            {
                phatHuHong += mucPhatHuHong;
                lyDoList.Add($"{tinhTrang} ({tenSach})");
            }
        }

        _coSachTre = tongNgayTre > 0;
        _tienPhatTre = tongNgayTre * donGiaTre;
        _tienPhatHuHong = phatHuHong;
        _tongTienPhat = _tienPhatTre + _tienPhatHuHong;

        lblTongTienPhat.Text = $"{_tongTienPhat:N0} VNĐ";
        txtLyDoPhat.Text = string.Join("; ", lyDoList);
    }

    private async void btnXacNhanTra_Click(object? sender, EventArgs e)
    {
        try
        {
            if (cboPhieuMuon.SelectedValue is not int phieuMuonId)
            {
                Toast.CanhBao(this, "Vui lòng chọn phiếu mượn cần trả!");
                return;
            }

            var chiTietDuocChon = new Dictionary<int, string>();
            foreach (DataGridViewRow row in dgvChiTietMuon.Rows)
            {
                if (!Convert.ToBoolean(row.Cells["ChonTra"].Value ?? false)) continue;
                int ctId = Convert.ToInt32(row.Cells["ChiTietId"].Value);
                chiTietDuocChon[ctId] = row.Cells["TinhTrangTra"].Value?.ToString() ?? TinhTrang.BinhThuong;
            }

            if (chiTietDuocChon.Count == 0)
            {
                Toast.CanhBao(this, "Vui lòng tích chọn ít nhất một cuốn sách để trả!");
                return;
            }

            // Tính lại từ lưới ngay lúc xác nhận, không dùng số cũ
            TinhTongTienPhat();

            if (_coSachTre && numDonGiaTre.Value == 0)
            {
                var hoi = MessageBox.Show(
                    "Có sách trả trễ hạn nhưng \"Đơn giá phạt trễ\" đang là 0, nên sẽ không bị phạt.\n" +
                    "Vẫn tiếp tục xác nhận trả sách?",
                    "Chưa tính phạt trễ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (hoi != DialogResult.Yes) { numDonGiaTre.Focus(); return; }
            }

            using var db = new AppDbContext();
            var phieu = await db.PhieuMuons
                .Include(p => p.ChiTietPhieuMuons)
                    .ThenInclude(ct => ct.CuonSach)
                .FirstOrDefaultAsync(p => p.MaPhieuMuon == phieuMuonId);

            if (phieu == null)
            {
                Toast.CanhBao(this, "Không tìm thấy thông tin phiếu mượn!");
                return;
            }

            int soCuonTra = 0;
            foreach (var ct in phieu.ChiTietPhieuMuons)
            {
                // Bỏ qua cuốn không được chọn, hoặc đã được trả ở nơi khác
                if (!chiTietDuocChon.TryGetValue(ct.Id, out string? tinhTrang) || ct.NgayTraThucTe != null)
                    continue;

                ct.NgayTraThucTe = DateTime.Now;
                ct.TinhTrangKhiTra = tinhTrang;
                soCuonTra++;

                if (ct.CuonSach != null)
                {
                    ct.CuonSach.TrangThai = tinhTrang switch
                    {
                        TinhTrang.HuHong => TrangThaiSach.Hong,
                        TinhTrang.MatSach => TrangThaiSach.Mat,
                        _ => TrangThaiSach.SanSang
                    };
                }
            }

            if (soCuonTra == 0)
            {
                Toast.CanhBao(this, "Các cuốn đã chọn đã được trả trước đó.");
                TaiDanhSachPhieuDangMuon(txtTimKiem.Text);
                return;
            }

            // Hóa đơn + nợ phạt đi qua PhatService, lưu chung một lần với việc trả sách
            await PhatService.TaoHoaDonAsync(db, phieu.MaDocGia, phieu.MaPhieuMuon,
                _tienPhatTre, _tienPhatHuHong, txtLyDoPhat.Text, chkDaThanhToanNgay.Checked);

            await db.SaveChangesAsync();

            Toast.ThanhCong(this, "Đã ghi nhận trả sách thành công!");
            TaiDanhSachPhieuDangMuon(txtTimKiem.Text);
        }
        catch (Exception ex)
        {
            NhatKy.Ghi(ex);
            Toast.Loi(this, "Không thể ghi nhận trả sách. Vui lòng thử lại.");
        }
    }

    private void TaiHoaDonPhatCuaDocGia(string? _ = null)
    {
        using var db = new AppDbContext();
        var hoaDons = db.HoaDonPhats
            .AsNoTracking()
            .OrderByDescending(h => h.NgayLap)
            .ThenByDescending(h => h.MaHoaDon)
            .Select(h => new
            {
                MaHoaDon = h.MaHoaDon,
                MaPhieuMuon = h.MaPhieuMuon,
                DocGia = h.DocGia != null ? h.DocGia.HoTen : "",
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
            dgvHoaDonPhat.Columns["DocGia"]!.HeaderText = "Độc Giả";
            dgvHoaDonPhat.Columns["NgayLap"]!.HeaderText = "Ngày Lập";
            dgvHoaDonPhat.Columns["SoTien"]!.HeaderText = "Số Tiền Phạt";
            dgvHoaDonPhat.Columns["LyDo"]!.HeaderText = "Lý Do Phạt";
            dgvHoaDonPhat.Columns["TrangThai"]!.HeaderText = "Trạng Thái";
        }
    }

    private async void btnThanhToanHoaDon_Click(object? sender, EventArgs e)
    {
        try
        {
            if (dgvHoaDonPhat.CurrentRow == null)
            {
                Toast.CanhBao(this, "Hãy chọn một hóa đơn cần thu tiền.");
                return;
            }

            int maHoaDon = Convert.ToInt32(dgvHoaDonPhat.CurrentRow.Cells["MaHoaDon"].Value);

            using var db = new AppDbContext();
            bool daThu = await PhatService.ThuHoaDonAsync(db, maHoaDon);
            if (!daThu)
            {
                Toast.CanhBao(this, "Hóa đơn không tồn tại hoặc đã được thanh toán trước đó.");
                TaiHoaDonPhatCuaDocGia();
                return;
            }

            await db.SaveChangesAsync();
            Toast.ThanhCong(this, $"Đã thu tiền hóa đơn phạt #{maHoaDon}!");
            TaiHoaDonPhatCuaDocGia();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
            Toast.Loi(this, "Không thể thu tiền hóa đơn. Vui lòng thử lại.");
        }
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

        grpHoaDonPhat.Text = "Danh sách hóa đơn phạt (tất cả độc giả)";

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
        
        grpChiTietMuon.Padding = new Padding(12, 8, 12, 12);
        grpHoaDonPhat.Padding = new Padding(12, 8, 12, 12);

        // Đặt nút Xác nhận thu tiền gọn gàng ở góc dưới bên phải khung Hóa đơn phạt
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

    
}