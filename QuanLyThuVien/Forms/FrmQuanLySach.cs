using System.ComponentModel;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.DTOs;
using QuanLyThuVien.Entities;

namespace QuanLyThuVien.Forms;

public partial class FrmQuanLySach : Form
{
    private readonly BindingList<DauSachDto> _dsDauSach = new();
    private readonly BindingSource _bsDauSach = new();

    private readonly BindingList<CuonSachDto> _dsCuonSach = new();
    private readonly BindingSource _bsCuonSach = new();


    private List<TheLoai> _tatCaTheLoai = new();

    public FrmQuanLySach()
    {
        InitializeComponent();

        _bsDauSach.DataSource = _dsDauSach;
        dgvDauSach.DataSource = _bsDauSach;

        _bsCuonSach.DataSource = _dsCuonSach;
        dgvCuonSach.DataSource = _bsCuonSach;

        
        dgvDauSach.SelectionChanged += DgvDauSach_SelectionChanged;
        dgvCuonSach.CellFormatting += DgvCuonSach_CellFormatting;
    }

   

    //Event

    private async void FrmQuanLySach_Load(object? sender, EventArgs e)
    {
        try
        {
            CauHinhLuoi();
            await NapCayTheLoaiAsync(null);
        }
        catch (Exception ex)
        {
            BaoLoi(ex);
        }
    }

    private async void tvTheLoai_AfterSelect(object? sender, TreeViewEventArgs e)
    {
        try
        {
            // Tag của nút gốc là null -> xem tất cả
            await NapDauSachAsync(e.Node?.Tag as string);
        }
        catch (Exception ex)
        {
            BaoLoi(ex);
        }
    }

    // (Master - Detail)
    private async void DgvDauSach_SelectionChanged(object? sender, EventArgs e)
    {
        try
        {
            if (_bsDauSach.Current is not DauSachDto d)
            {
                _dsCuonSach.Clear();
                LamMoiKhungNhapDauSach();
                return;
            }

            txtMaDauSach.Text = d.MaDauSach;
            txtTenDauSach.Text = d.TenDauSach;
            txtTacGia.Text = d.TacGia;
            nudNamXuatBan.Value = KeoVaoKhoang(d.NamXuatBan, nudNamXuatBan.Minimum, nudNamXuatBan.Maximum);
            nudGiaBia.Value = KeoVaoKhoang(d.GiaBia, nudGiaBia.Minimum, nudGiaBia.Maximum);
            cboTheLoai.SelectedValue = d.MaTheLoai;

            await NapCuonSachAsync(d.MaDauSach);
        }
        catch (Exception ex)
        {
            BaoLoi(ex);
        }
    }

    private async void btnThemTheLoai_Click(object? sender, EventArgs e)
    {
        try
        {
            // dat the loai cha = null
            string? maCha = tvTheLoai.SelectedNode?.Tag as string;
            string tenCha = tvTheLoai.SelectedNode?.Text ?? "Tất cả sách";

            string? ma = HoiChuoi("Thêm thể loại",
                $"Nhập MÃ thể loại mới (chữ HOA, số, dấu _; ví dụ TL_VANHOC).\nThể loại cha: {tenCha}");
            if (ma == null || ma == "") return;

            if (!Regex.IsMatch(ma, @"^[A-Z0-9_]{3,20}$"))
            {
                MessageBox.Show("Mã chỉ gồm chữ HOA, số, dấu _ và dài 3 đến 20 ký tự.",
                    "Sai định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string? ten = HoiChuoi("Thêm thể loại", "Nhập TÊN thể loại:");
            if (ten == null || ten == "") return;

            using var db = new AppDbContext();
            if (await db.TheLoais.AnyAsync(t => t.MaTheLoai == ma))
            {
                MessageBox.Show($"Mã \"{ma}\" đã tồn tại.", "Trùng mã",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            db.TheLoais.Add(new TheLoai { MaTheLoai = ma, TenTheLoai = ten, MaTheLoaiCha = maCha });
            await db.SaveChangesAsync();

            await NapCayTheLoaiAsync(ma);
        }
        catch (Exception ex)
        {
            BaoLoi(ex);
        }
    }

    private async void btnSuaTheLoai_Click(object? sender, EventArgs e)
    {
        try
        {
            if (tvTheLoai.SelectedNode?.Tag is not string ma)
            {
                MessageBox.Show("Hãy chọn một thể loại (không phải \"Tất cả sách\") để sửa.",
                    "Chưa chọn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string? tenMoi = HoiChuoi("Sửa thể loại",
                $"Nhập tên mới cho thể loại [{ma}]:", tvTheLoai.SelectedNode.Text);
            if (tenMoi == null || tenMoi == "") return;

            using var db = new AppDbContext();
            var tl = await db.TheLoais.FirstOrDefaultAsync(t => t.MaTheLoai == ma);
            if (tl == null) return;

            tl.TenTheLoai = tenMoi;
            await db.SaveChangesAsync();

            await NapCayTheLoaiAsync(ma);
        }
        catch (Exception ex)
        {
            BaoLoi(ex);
        }
    }

    private async void btnXoaTheLoai_Click(object? sender, EventArgs e)
    {
        try
        {
            if (tvTheLoai.SelectedNode?.Tag is not string ma)
            {
                MessageBox.Show("Hãy chọn một thể loại (không phải \"Tất cả sách\") để xóa.",
                    "Chưa chọn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var db = new AppDbContext();

            if (await db.TheLoais.AnyAsync(t => t.MaTheLoaiCha == ma))
            {
                MessageBox.Show("Không thể xóa: thể loại này còn thể loại con.",
                    "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (await db.DauSachs.AnyAsync(d => d.MaTheLoai == ma))
            {
                MessageBox.Show("Không thể xóa: thể loại này còn đầu sách.",
                    "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var kq = MessageBox.Show($"Xóa thể loại \"{tvTheLoai.SelectedNode.Text}\"?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq != DialogResult.Yes) return;

            var tl = await db.TheLoais.FirstOrDefaultAsync(t => t.MaTheLoai == ma);
            if (tl == null) return;

            string? maCha = tl.MaTheLoaiCha;
            db.TheLoais.Remove(tl);
            await db.SaveChangesAsync();

            await NapCayTheLoaiAsync(maCha);
        }
        catch (Exception ex)
        {
            BaoLoi(ex);
        }
    }

    //TREEVIEW


    private async Task NapCayTheLoaiAsync(string? maCanChon)
    {
        using (var db = new AppDbContext())
        {
            _tatCaTheLoai = await db.TheLoais.AsNoTracking().ToListAsync();
        }

        // Cập nhật ComboBox
        cboTheLoai.DataSource = null;
        cboTheLoai.DisplayMember = nameof(TheLoai.TenTheLoai);
        cboTheLoai.ValueMember = nameof(TheLoai.MaTheLoai);
        cboTheLoai.DataSource = _tatCaTheLoai.OrderBy(t => t.TenTheLoai).ToList();

        tvTheLoai.BeginUpdate();
        tvTheLoai.Nodes.Clear();

        var nutGoc = new TreeNode("Tất cả sách") { Tag = null };
        tvTheLoai.Nodes.Add(nutGoc);
        NapNutCon(nutGoc, null);

        tvTheLoai.ExpandAll();
        tvTheLoai.EndUpdate();

        // Chọn nút
        TreeNode? nutChon = maCanChon == null ? null : TimNut(nutGoc, maCanChon);
        tvTheLoai.SelectedNode = nutChon ?? nutGoc;
    }

    // ĐỆ QUY: tìm các thể loại con trực tiếp của maCha rồi gọi lại chính nó cho từng con
    private void NapNutCon(TreeNode nutCha, string? maCha)
    {
        var cacCon = _tatCaTheLoai
            .Where(t => t.MaTheLoaiCha == maCha)
            .OrderBy(t => t.TenTheLoai);

        foreach (var tl in cacCon)
        {
            var nutCon = new TreeNode(tl.TenTheLoai) { Tag = tl.MaTheLoai };
            nutCha.Nodes.Add(nutCon);
            NapNutCon(nutCon, tl.MaTheLoai);
        }
    }

    //de quy
    private static TreeNode? TimNut(TreeNode nut, string ma)
    {
        foreach (TreeNode con in nut.Nodes)
        {
            if (con.Tag as string == ma) return con;
            var kq = TimNut(con, ma);
            if (kq != null) return kq;
        }
        return null;
    }

    //layma
    private List<string> LayMaTheLoaiCon(string ma)
    {
        var kq = new List<string> { ma };
        foreach (var con in _tatCaTheLoai.Where(t => t.MaTheLoaiCha == ma))
            kq.AddRange(LayMaTheLoaiCon(con.MaTheLoai));
        return kq;
    }

    //tree

    private async Task NapDauSachAsync(string? maTheLoai)
    {
        using var db = new AppDbContext();

        IQueryable<DauSach> truyVan = db.DauSachs
            .AsNoTracking()
            .Include(d => d.CuonSachs)
            .Include(d => d.TheLoai);

        if (maTheLoai != null)
        {
            var dsMa = LayMaTheLoaiCon(maTheLoai);
            truyVan = truyVan.Where(d => dsMa.Contains(d.MaTheLoai));
        }

        var ds = await truyVan.OrderBy(d => d.MaDauSach).ToListAsync();

        _dsDauSach.RaiseListChangedEvents = false;
        _dsDauSach.Clear();
        foreach (var d in ds)
        {
            _dsDauSach.Add(new DauSachDto(
                d.MaDauSach,
                d.TenDauSach,
                d.TacGia,
                d.NamXuatBan,
                d.GiaBia,
                d.MaTheLoai,
                d.TheLoai?.TenTheLoai ?? "",
                d.CuonSachs.Count,
                d.CuonSachs.Count(c => c.TrangThai == TrangThaiSach.SanSang)));
        }
        _dsDauSach.RaiseListChangedEvents = true;
        _dsDauSach.ResetBindings();
    }

    private async Task NapCuonSachAsync(string maDauSach)
    {
        using var db = new AppDbContext();
        var ds = await db.CuonSachs
            .AsNoTracking()
            .Where(c => c.MaDauSach == maDauSach)
            .OrderBy(c => c.MaCuonSach.Length)
            .ThenBy(c => c.MaCuonSach)
            .ToListAsync();

        _dsCuonSach.Clear();
        foreach (var c in ds)
            _dsCuonSach.Add(new CuonSachDto(c.MaCuonSach, c.ViTriKe, c.TrangThai));
    }

    private void CauHinhLuoi()
    {
        var tieuDeDauSach = new Dictionary<string, string>
        {
            ["MaDauSach"] = "Mã đầu sách",
            ["TenDauSach"] = "Tên đầu sách",
            ["TacGia"] = "Tác giả",
            ["NamXuatBan"] = "Năm XB",
            ["GiaBia"] = "Giá bìa",
            ["TenTheLoai"] = "Thể loại",
            ["TongSoCuon"] = "Tổng số cuốn",
            ["SoCuonSanSang"] = "Sẵn sàng"
        };
        foreach (DataGridViewColumn cot in dgvDauSach.Columns)
        {
            if (tieuDeDauSach.TryGetValue(cot.Name, out var t))
                cot.HeaderText = t;
        }
        if (dgvDauSach.Columns["MaTheLoai"] is { } cotMaTheLoai)
            cotMaTheLoai.Visible = false;
        if (dgvDauSach.Columns["GiaBia"] is { } cotGia)
            cotGia.DefaultCellStyle.Format = "N0";

        var tieuDeCuon = new Dictionary<string, string>
        {
            ["MaCuonSach"] = "Mã cuốn sách",
            ["ViTriKe"] = "Vị trí kệ",
            ["TrangThai"] = "Trạng thái"
        };
        foreach (DataGridViewColumn cot in dgvCuonSach.Columns)
        {
            if (tieuDeCuon.TryGetValue(cot.Name, out var t))
                cot.HeaderText = t;
        }
    }


    //ham phu
    private void DgvCuonSach_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
        if (dgvCuonSach.Columns[e.ColumnIndex].Name != "TrangThai") return;
        if (e.Value is not string trangthai) return;

        e.CellStyle.ForeColor = trangthai switch
        {
            TrangThaiSach.SanSang => Color.Green,
            TrangThaiSach.DangMuon => Color.Orange,
            TrangThaiSach.Hong => Color.Firebrick,
            TrangThaiSach.Mat => Color.Firebrick,
            _ => e.CellStyle.ForeColor
        };

    }
    private void LamMoiKhungNhapDauSach()
    {
        txtMaDauSach.Clear();
        txtTenDauSach.Clear();
        txtTacGia.Clear();
        nudNamXuatBan.Value = 2024;
        nudGiaBia.Value = 0;
        cboTheLoai.SelectedIndex = -1;
    }

    private static decimal KeoVaoKhoang(decimal gt, decimal min, decimal max)
    {
        if (gt < min) return min;
        if (gt > max) return max;
        return gt;
    }

    //Hop thoai 

    private static string? HoiChuoi(string tieuDe, string cauHoi, string macDinh = "")
    {
        using var f = new Form
        {
            Text = tieuDe,
            StartPosition = FormStartPosition.CenterParent,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            MaximizeBox = false,
            MinimizeBox = false,
            ClientSize = new Size(420, 130)
        };
        var lbl = new Label { Text = cauHoi, Left = 12, Top = 12, Width = 396, Height = 40 };
        var txt = new TextBox { Left = 12, Top = 58, Width = 396, Text = macDinh };
        var ok = new Button { Text = "OK", Left = 232, Top = 92, Width = 80, DialogResult = DialogResult.OK };
        var huy = new Button { Text = "Hủy", Left = 328, Top = 92, Width = 80, DialogResult = DialogResult.Cancel };

        f.Controls.AddRange(new Control[] { lbl, txt, ok, huy });
        f.AcceptButton = ok;
        f.CancelButton = huy;

        return f.ShowDialog() == DialogResult.OK ? txt.Text.Trim() : null;
    }

    private static void BaoLoi(Exception ex)
    {
        MessageBox.Show($"Có lỗi xảy ra:\n{ex.Message}", "Lỗi",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    private void btnLamMoi_Click(object sender, EventArgs e)
    {
        dgvDauSach.ClearSelection();
        _dsCuonSach.Clear();
        LamMoiKhungNhapDauSach();
        txtTenDauSach.Focus();
    }
}