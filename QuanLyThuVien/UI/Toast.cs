using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace QuanLyThuVien.UI;

public enum ToastLoai { ThanhCong, Loi, CanhBao, ThongTin }

/// <summary>
/// Thông báo nhỏ trượt vào góc phải trên của cửa sổ chính rồi tự biến mất.
/// Cách dùng (trong bất kỳ Form nào):  Toast.ThanhCong(this, "Đã lưu thành công");
/// </summary>
public sealed class Toast : Form
{
    // ===== API dùng ở các Form =====
    private static Toast? _dangHien;

    public static void ThanhCong(Control nguon, string noiDung) => Hien(nguon, noiDung, ToastLoai.ThanhCong, 2800);
    public static void Loi(Control nguon, string noiDung) => Hien(nguon, noiDung, ToastLoai.Loi, 4000);
    public static void CanhBao(Control nguon, string noiDung) => Hien(nguon, noiDung, ToastLoai.CanhBao, 3500);
    public static void ThongTin(Control nguon, string noiDung) => Hien(nguon, noiDung, ToastLoai.ThongTin, 2800);

    public static void Hien(Control nguon, string noiDung, ToastLoai loai = ToastLoai.ThanhCong, int ms = 2800)
    {
        // Form con nhúng trong FrmMain (TopLevel = false) -> lấy cửa sổ chính làm nơi hiển thị
        var host = nguon.TopLevelControl as Form ?? nguon.FindForm();

        if (host == null || host.IsDisposed || !host.IsHandleCreated || host.WindowState == FormWindowState.Minimized)
        {
            // Dự phòng: không có cửa sổ chính để gắn thì dùng hộp thoại thường
            MessageBox.Show(noiDung, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        _dangHien?.DongNgay();                 // chỉ giữ 1 thông báo tại một thời điểm
        var t = new Toast(noiDung, loai, ms);
        _dangHien = t;
        t.HienTai(host);
    }

    // ===== Thiết lập nội bộ =====
    private const TextFormatFlags FlagsChu = TextFormatFlags.Left | TextFormatFlags.Top
        | TextFormatFlags.WordBreak | TextFormatFlags.NoPrefix | TextFormatFlags.NoPadding;

    private const int ThoiGianVao = 260;   // ms
    private const int ThoiGianRa = 220;    // ms

    private readonly string _noiDung;
    private readonly string _tieuDe;
    private readonly ToastLoai _loai;
    private readonly Color _mau;
    private readonly int _thoiGianHien;

    private readonly System.Windows.Forms.Timer _timer = new() { Interval = 15 };
    private readonly Stopwatch _dongHo = new();

    private int _giaiDoan;          // 0 = đang trượt vào, 1 = đang hiển thị, 2 = đang trượt ra
    private int _xDich;
    private int _khoangTruot;
    private double _tiLe = 1.0;     // phần thời gian còn lại, dùng vẽ thanh tiến trình

    private readonly int _xChu;
    private readonly int _rongChu;
    private readonly int _yTieuDe;
    private readonly int _yNoiDung;

    private int P(int v) => (int)Math.Round(v * DeviceDpi / 96.0);   // đổi theo độ phóng đại màn hình

    private Toast(string noiDung, ToastLoai loai, int thoiGianHien)
    {
        _noiDung = noiDung;
        _loai = loai;
        _thoiGianHien = Math.Max(800, thoiGianHien);

        (_mau, _tieuDe) = loai switch
        {
            ToastLoai.ThanhCong => (Color.FromArgb(22, 163, 74), "Thành công"),
            ToastLoai.Loi => (Theme.Danger, "Có lỗi xảy ra"),
            ToastLoai.CanhBao => (Color.FromArgb(217, 119, 6), "Lưu ý"),
            _ => (Theme.Secondary, "Thông tin")
        };

        FormBorderStyle = FormBorderStyle.None;
        StartPosition = FormStartPosition.Manual;
        ShowInTaskbar = false;
        BackColor = Color.White;
        DoubleBuffered = true;
        Opacity = 0;
        Cursor = Cursors.Hand;

        // Tính kích thước theo độ dài nội dung
        int rongToast = P(360);
        _xChu = P(64);
        _rongChu = rongToast - _xChu - P(16);
        _yTieuDe = P(12);
        _yNoiDung = _yTieuDe + Theme.FontBold.Height + P(2);

        var kich = TextRenderer.MeasureText(noiDung, Theme.FontNormal, new Size(_rongChu, int.MaxValue), FlagsChu);
        ClientSize = new Size(rongToast, Math.Max(P(68), _yNoiDung + kich.Height + P(16)));

        _timer.Tick += Tick;
        Click += (s, e) => BatDauDong();   // bấm vào thông báo để tắt ngay
    }

    // Không cướp focus của cửa sổ đang làm việc; không hiện trên thanh taskbar; có bóng đổ
    protected override bool ShowWithoutActivation => true;

    protected override CreateParams CreateParams
    {
        get
        {
            var cp = base.CreateParams;
            cp.ExStyle |= 0x08000000 | 0x00000080;   // WS_EX_NOACTIVATE | WS_EX_TOOLWINDOW
            cp.ClassStyle |= 0x00020000;             // CS_DROPSHADOW
            return cp;
        }
    }

    [DllImport("dwmapi.dll")]
    private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        try
        {
            // Windows 11: bo góc mượt cho cửa sổ không viền (Windows 10 bỏ qua, vẫn hoạt động bình thường)
            int bo = 2;   // DWMWCP_ROUND
            DwmSetWindowAttribute(Handle, 33, ref bo, sizeof(int));
        }
        catch { }
    }

    private void HienTai(Form host)
    {
        // Nếu host là hộp thoại nhỏ (như DlgDoiMatKhau) thì neo Toast theo cửa sổ cha
        var khung = (host.Owner != null && host.ClientSize.Width < Width + P(60))
            ? host.Owner
            : host;

        var goc = khung.PointToScreen(Point.Empty);
        _xDich = goc.X + khung.ClientSize.Width - Width - P(24);
        _khoangTruot = P(70);

        int leTren = khung == host ? P(76) : P(16);
        Location = new Point(_xDich + _khoangTruot, goc.Y + leTren);
        Show(host);

        _dongHo.Restart();
        _timer.Start();
    }

    // ===== Hiệu ứng =====
    private void Tick(object? sender, EventArgs e)
    {
        long t = _dongHo.ElapsedMilliseconds;

        if (_giaiDoan == 0)
        {
            double p = Math.Min(1.0, t / (double)ThoiGianVao);
            double ease = 1 - Math.Pow(1 - p, 3);            // chậm dần về cuối
            Left = _xDich + (int)((1 - ease) * _khoangTruot);
            Opacity = ease;
            if (p >= 1.0)
            {
                _giaiDoan = 1;
                _dongHo.Restart();
            }
        }
        else if (_giaiDoan == 1)
        {
            _tiLe = 1.0 - Math.Min(1.0, t / (double)_thoiGianHien);
            Invalidate(new Rectangle(0, Height - P(4), Width, P(4)));
            if (t >= _thoiGianHien) BatDauDong();
        }
        else
        {
            double p = Math.Min(1.0, t / (double)ThoiGianRa);
            double ease = p * p * p;                          // nhanh dần
            Left = _xDich + (int)(ease * _khoangTruot);
            Opacity = 1 - ease;
            if (p >= 1.0)
            {
                _timer.Stop();
                Close();
            }
        }
    }

    private void BatDauDong()
    {
        if (_giaiDoan == 2 || IsDisposed) return;
        _giaiDoan = 2;
        _tiLe = 0;
        _dongHo.Restart();
    }

    private void DongNgay()
    {
        if (IsDisposed) return;
        _timer.Stop();
        Close();
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        _timer.Stop();
        _timer.Dispose();
        if (ReferenceEquals(_dangHien, this)) _dangHien = null;
        base.OnFormClosed(e);
    }

    // ===== Vẽ =====
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;

        // Viền mảnh quanh thông báo
        using (var vien = new Pen(Theme.Border))
            g.DrawRectangle(vien, 0, 0, Width - 1, Height - 1);

        // Thanh màu bên trái
        using (var thanh = new SolidBrush(_mau))
            g.FillRectangle(thanh, 0, 0, P(5), Height);

        // Vòng tròn biểu tượng
        int d = P(30);
        var vongTron = new Rectangle(P(20), _yTieuDe, d, d);
        using (var nen = new SolidBrush(_mau))
            g.FillEllipse(nen, vongTron);
        VeKyHieu(g, vongTron);

        // Chữ
        TextRenderer.DrawText(g, _tieuDe, Theme.FontBold,
            new Rectangle(_xChu, _yTieuDe, _rongChu, Theme.FontBold.Height), Theme.TextDark,
            TextFormatFlags.Left | TextFormatFlags.Top | TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix);

        TextRenderer.DrawText(g, _noiDung, Theme.FontNormal,
            new Rectangle(_xChu, _yNoiDung, _rongChu, Height - _yNoiDung), Theme.Neutral, FlagsChu);

        // Thanh thời gian còn lại ở đáy
        if (_giaiDoan < 2 && _tiLe > 0)
        {
            using var tg = new SolidBrush(Color.FromArgb(140, _mau));
            g.FillRectangle(tg, 0, Height - P(3), (int)(Width * _tiLe), P(3));
        }
    }

    private void VeKyHieu(Graphics g, Rectangle r)
    {
        float x = r.X, y = r.Y, d = r.Width;
        using var but = new Pen(Color.White, Math.Max(2f, d / 10f))
        {
            StartCap = LineCap.Round,
            EndCap = LineCap.Round,
            LineJoin = LineJoin.Round
        };

        switch (_loai)
        {
            case ToastLoai.ThanhCong:      // dấu tích
                g.DrawLines(but, new[]
                {
                    new PointF(x + d * 0.28f, y + d * 0.53f),
                    new PointF(x + d * 0.44f, y + d * 0.69f),
                    new PointF(x + d * 0.73f, y + d * 0.34f)
                });
                break;

            case ToastLoai.Loi:            // dấu X
                g.DrawLine(but, x + d * 0.33f, y + d * 0.33f, x + d * 0.67f, y + d * 0.67f);
                g.DrawLine(but, x + d * 0.67f, y + d * 0.33f, x + d * 0.33f, y + d * 0.67f);
                break;

            default:                       // "!" hoặc "i"
                TextRenderer.DrawText(g, _loai == ToastLoai.CanhBao ? "!" : "i", Theme.FontBold, r, Color.White,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
                break;
        }
    }
}