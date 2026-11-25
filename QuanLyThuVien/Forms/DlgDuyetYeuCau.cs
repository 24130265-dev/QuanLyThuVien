using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.UI;

namespace QuanLyThuVien.Forms;

/// <summary>
/// Hộp thoại Thủ thư duyệt / từ chối yêu cầu mượn online của độc giả.
/// FrmMuonSach đọc kết quả qua các thuộc tính MaDocGiaDuocChon, MaDauSachDuocChon, MaCuonSachGoiY.
/// </summary>
public partial class DlgDuyetYeuCau : Form
{
    // ===== Kết quả trả về cho FrmMuonSach (chỉ có giá trị khi DialogResult = OK) =====
    public string MaDocGiaDuocChon { get; private set; } = string.Empty;
    public string MaDauSachDuocChon { get; private set; } = string.Empty;
    public string? MaCuonSachGoiY { get; private set; }     // cuốn đang sẵn sàng của đầu sách đó

    private sealed record DongYeuCau(int MaYeuCau, string MaDocGia, string HoTen,
        string MaDauSach, string TenDauSach, string NgayGui, int SoCuonSanSang);

    public DlgDuyetYeuCau()
    {
        InitializeComponent();

        Theme.Apply(this);
        Theme.StyleButton(btnTuChoi, Theme.Danger);
        Theme.StyleButton(btnDong, Theme.Neutral);
        lblTieuDe.Font = Theme.FontTitle;
        lblTieuDe.ForeColor = Theme.PrimaryDark;
        lblDem.ForeColor = Theme.Neutral;

        CauHinhLuoi();
    }

    private static DataGridViewTextBoxColumn Cot(string prop, string tieuDe, float trongSo)
        => new()
        {
            Name = prop,
            DataPropertyName = prop,
            HeaderText = tieuDe,
            FillWeight = trongSo,
            MinimumWidth = 60,
            ReadOnly = true
        };

    private void CauHinhLuoi()
    {
        dgvYeuCau.AutoGenerateColumns = false;
        dgvYeuCau.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvYeuCau.ReadOnly = true;
        dgvYeuCau.MultiSelect = false;
        dgvYeuCau.AllowUserToAddRows = false;
        dgvYeuCau.AllowUserToDeleteRows = false;

        dgvYeuCau.Columns.AddRange(
            Cot("MaYeuCau", "Mã YC", 8),
            Cot("MaDocGia", "Mã độc giả", 12),
            Cot("HoTen", "Họ tên", 22),
            Cot("MaDauSach", "Mã đầu sách", 12),
            Cot("TenDauSach", "Tên đầu sách", 26),
            Cot("NgayGui", "Ngày gửi", 16),
            Cot("SoCuonSanSang", "Còn sẵn sàng", 12));
    }

    private async void DlgDuyetYeuCau_Load(object? sender, EventArgs e)
    {
        await NapDanhSachAsync();
    }

    private async Task NapDanhSachAsync()
    {
        using var db = new AppDbContext();
        var ds = await db.YeuCauMuons.AsNoTracking()
            .Include(y => y.DocGia)
            .Include(y => y.DauSach).ThenInclude(d => d!.CuonSachs)
            .Where(y => y.TrangThai == TrangThaiYeuCau.ChoDuyet)
            .OrderBy(y => y.NgayGui)
            .ToListAsync();

        var dong = ds.Select(y => new DongYeuCau(
                y.MaYeuCau,
                y.MaDocGia,
                y.DocGia?.HoTen ?? "",
                y.MaDauSach,
                y.DauSach?.TenDauSach ?? "",
                y.NgayGui.ToString("dd/MM/yyyy HH:mm"),
                y.DauSach?.CuonSachs.Count(c => c.TrangThai == TrangThaiSach.SanSang) ?? 0))
            .ToList();

        dgvYeuCau.DataSource = dong;
        lblDem.Text = $"Có {dong.Count} yêu cầu chờ duyệt";
        btnDuyet.Enabled = dong.Count > 0;
        btnTuChoi.Enabled = dong.Count > 0;
    }

    private void dgvYeuCau_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0) btnDuyet.PerformClick();    // nhấp đúp = duyệt nhanh
    }

    // ===== Duyệt =====
    private async void btnDuyet_Click(object? sender, EventArgs e)
    {
        if (dgvYeuCau.CurrentRow?.DataBoundItem is not DongYeuCau dong)
        {
            Toast.CanhBao(this, "Hãy chọn một yêu cầu cần duyệt.");
            return;
        }

        using var db = new AppDbContext();

        // Đọc lại từ CSDL: có thể yêu cầu đã được xử lý ở nơi khác
        var yc = await db.YeuCauMuons.Include(y => y.DocGia)
            .FirstOrDefaultAsync(y => y.MaYeuCau == dong.MaYeuCau);
        if (yc == null || yc.TrangThai != TrangThaiYeuCau.ChoDuyet)
        {
            Toast.CanhBao(this, "Yêu cầu này đã được xử lý trước đó.");
            await NapDanhSachAsync();
            return;
        }

        var docGia = yc.DocGia;
        if (docGia == null)
        {
            Toast.Loi(this, "Không tìm thấy độc giả của yêu cầu này.");
            return;
        }
        if (docGia.BiKhoaThe)
        {
            Toast.Loi(this, $"Thẻ của {docGia.HoTen} đang bị khóa, không thể duyệt. Hãy chọn Từ chối.");
            return;
        }

        bool conNoHoaDon = await db.HoaDonPhats.AnyAsync(h => h.MaDocGia == docGia.MaDocGia && !h.DaThanhToan);
        if (docGia.TienNoPhat > 0 || conNoHoaDon)
        {
            Toast.Loi(this, $"{docGia.HoTen} còn nợ phạt chưa thanh toán, không thể duyệt.");
            return;
        }

        var cuon = await db.CuonSachs
            .Where(c => c.MaDauSach == yc.MaDauSach && c.TrangThai == TrangThaiSach.SanSang)
            .OrderBy(c => c.MaCuonSach)
            .FirstOrDefaultAsync();
        if (cuon == null)
        {
            Toast.Loi(this, "Đầu sách này hiện không còn bản sẵn sàng.");
            await NapDanhSachAsync();
            return;
        }

        yc.TrangThai = TrangThaiYeuCau.DaDuyet;
        await db.SaveChangesAsync();

        MaDocGiaDuocChon = yc.MaDocGia;
        MaDauSachDuocChon = yc.MaDauSach;
        MaCuonSachGoiY = cuon.MaCuonSach;
        DialogResult = DialogResult.OK;       // đóng hộp thoại, trả kết quả cho FrmMuonSach
    }

    // ===== Từ chối =====
    private async void btnTuChoi_Click(object? sender, EventArgs e)
    {
        if (dgvYeuCau.CurrentRow?.DataBoundItem is not DongYeuCau dong)
        {
            Toast.CanhBao(this, "Hãy chọn một yêu cầu cần từ chối.");
            return;
        }

        var hoi = MessageBox.Show(
            $"Từ chối yêu cầu mượn \"{dong.TenDauSach}\" của {dong.HoTen}?",
            "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (hoi != DialogResult.Yes) return;

        using var db = new AppDbContext();
        var yc = await db.YeuCauMuons.FindAsync(dong.MaYeuCau);
        if (yc == null || yc.TrangThai != TrangThaiYeuCau.ChoDuyet)
        {
            Toast.CanhBao(this, "Yêu cầu này đã được xử lý trước đó.");
            await NapDanhSachAsync();
            return;
        }

        yc.TrangThai = TrangThaiYeuCau.TuChoi;
        await db.SaveChangesAsync();

        Toast.ThanhCong(this, $"Đã từ chối yêu cầu của {dong.HoTen}.");
        await NapDanhSachAsync();
    }
}