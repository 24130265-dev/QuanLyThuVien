using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace QuanLyThuVien.UI;

public static class Theme
{
    // =====================================================================
    // 1. BẢNG MÀU DÙNG CHUNG (giữ nguyên tên cũ để các Form khác không bị lỗi)
    // =====================================================================
    public static readonly Color Primary = Color.FromArgb(15, 118, 110);
    public static readonly Color PrimaryDark = Color.FromArgb(19, 78, 74);
    public static readonly Color Secondary = Color.FromArgb(37, 99, 235);
    public static readonly Color Danger = Color.FromArgb(220, 38, 38);
    public static readonly Color Neutral = Color.FromArgb(100, 116, 139);
    public static readonly Color BgMain = Color.FromArgb(241, 245, 249);
    public static readonly Color CardBg = Color.White;
    public static readonly Color TextDark = Color.FromArgb(30, 41, 59);
    public static readonly Color GridAltRow = Color.FromArgb(240, 253, 250);
    public static readonly Color GridSelect = Color.FromArgb(153, 246, 228);   // đậm hơn bản cũ để dễ thấy dòng đang chọn

    // Màu mới cho hiệu ứng
    public static readonly Color GridHover = Color.FromArgb(209, 247, 240);    // dòng bảng khi rê chuột
    public static readonly Color Border = Color.FromArgb(203, 213, 225);       // viền thẻ / khung
    public static readonly Color FocusBg = Color.FromArgb(240, 253, 250);      // ô nhập đang gõ
    public static readonly Color Accent = Color.FromArgb(94, 234, 212);        // vạch nhấn trên sidebar

    // Bật/tắt biểu tượng tự động trên nút (Thêm, Sửa, Xóa...). Đặt false nếu máy không có font icon.
    public static bool DungIcon = true;

    // =====================================================================
    // 2. PHÔNG CHỮ
    // =====================================================================
    public static readonly Font FontNormal = new("Segoe UI", 10F, FontStyle.Regular);
    public static readonly Font FontBold = new("Segoe UI", 10F, FontStyle.Bold);
    public static readonly Font FontTitle = new("Segoe UI", 13F, FontStyle.Bold);
    public static readonly Font FontSmall = new("Segoe UI", 9F, FontStyle.Regular);

    // Font biểu tượng có sẵn trên Windows 10/11 (Segoe Fluent Icons hoặc Segoe MDL2 Assets)
    private static readonly Font? FontIcon = TaoFontIcon();

    private static Font? TaoFontIcon()
    {
        foreach (var ten in new[] { "Segoe Fluent Icons", "Segoe MDL2 Assets" })
        {
            if (FontFamily.Families.Any(f => f.Name == ten))
                return new Font(ten, 11F, FontStyle.Regular);
        }
        return null;
    }

    // =====================================================================
    // 3. ÁP THEME CHO CẢ FORM:  Theme.Apply(this);
    // =====================================================================
    public static void Apply(Control root)
    {
        if (root is Form form)
        {
            form.BackColor = BgMain;
            form.Font = FontNormal;
            form.ForeColor = TextDark;
        }

        ApDungDeQuy(root);
    }

    private static void ApDungDeQuy(Control parent)
    {
        foreach (Control c in parent.Controls)
        {
            switch (c)
            {
                case DataGridView dgv:
                    StyleGrid(dgv);
                    break;

                case Button btn:
                    StyleButtonAuto(btn);
                    break;

                case GroupBox grp:
                    StyleGroup(grp);
                    break;

                case TreeView tv:
                    StyleTree(tv);
                    break;

                case TextBox tb:
                    StyleTextBox(tb);
                    break;

                case SplitContainer spl:
                    StyleSplit(spl);
                    break;

                case ComboBox or NumericUpDown or DateTimePicker:
                    c.Font = FontNormal;
                    c.ForeColor = TextDark;
                    break;

                case CheckBox or RadioButton:
                    // GroupBox dùng chữ đậm, nên phải đặt lại để không "thừa kế" chữ đậm
                    c.Font = FontNormal;
                    c.ForeColor = TextDark;
                    break;

                case Label lbl:
                    lbl.Font = FontNormal;
                    lbl.ForeColor = TextDark;
                    break;
            }

            // Không đi sâu vào các control có control con nội bộ (ô gõ bên trong NumericUpDown, thanh cuộn...)
            if (c.HasChildren && c is not (NumericUpDown or ComboBox or DateTimePicker or DataGridView or TreeView or ListView))
            {
                ApDungDeQuy(c);
            }
        }
    }

    // Đăng ký sự kiện chỉ 1 lần cho mỗi control (tránh gắn trùng khi gọi Apply nhiều lần)
    private static readonly ConditionalWeakTable<Control, object> _daDangKy = new();

    private static bool DangKyMotLan(Control c)
    {
        if (_daDangKy.TryGetValue(c, out _)) return false;
        _daDangKy.Add(c, new object());
        return true;
    }

    // Bật DoubleBuffered (thuộc tính protected) để vẽ mượt, không nháy
    private static void BatDoubleBuffer(Control c)
    {
        typeof(Control).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)
            ?.SetValue(c, true);
    }

    // =====================================================================
    // 4. BẢNG DATAGRIDVIEW
    // =====================================================================
    public static void StyleGrid(DataGridView dgv)
    {
        dgv.BackgroundColor = CardBg;
        dgv.BorderStyle = BorderStyle.None;
        dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        dgv.GridColor = Color.FromArgb(226, 232, 240);
        dgv.RowHeadersVisible = false;               // bỏ cột mũi tên xanh bên trái
        dgv.AllowUserToResizeRows = false;
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        // Tiêu đề cột
        dgv.EnableHeadersVisualStyles = false;
        dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgv.ColumnHeadersHeight = 40;
        var th = dgv.ColumnHeadersDefaultCellStyle;
        th.BackColor = Primary;
        th.ForeColor = Color.White;
        th.Font = FontBold;
        th.SelectionBackColor = Primary;             // tránh tiêu đề đổi sang xanh dương mặc định
        th.SelectionForeColor = Color.White;
        th.Padding = new Padding(10, 0, 10, 0);
        th.Alignment = DataGridViewContentAlignment.MiddleLeft;
        th.WrapMode = DataGridViewTriState.False;    // không xuống dòng tiêu đề

        // Dòng dữ liệu
        dgv.RowTemplate.Height = 36;
        var ds = dgv.DefaultCellStyle;
        ds.BackColor = CardBg;
        ds.ForeColor = TextDark;
        ds.Font = FontNormal;
        ds.SelectionBackColor = GridSelect;
        ds.SelectionForeColor = TextDark;
        ds.Padding = new Padding(10, 0, 10, 0);
        dgv.AlternatingRowsDefaultCellStyle.BackColor = GridAltRow;

        if (DangKyMotLan(dgv))
        {
            BatDoubleBuffer(dgv);

            // Hiệu ứng rê chuột: tô sáng dòng đang trỏ tới
            dgv.CellMouseEnter += (s, e) =>
            {
                if (e.RowIndex >= 0 && e.RowIndex < dgv.Rows.Count && !dgv.Rows[e.RowIndex].Selected)
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = GridHover;
            };
            dgv.CellMouseLeave += (s, e) =>
            {
                if (e.RowIndex >= 0 && e.RowIndex < dgv.Rows.Count)
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Empty;
            };
        }
    }

    // =====================================================================
    // 5. NÚT BẤM: tự vẽ bo góc + hiệu ứng rê chuột / nhấn / biểu tượng
    // =====================================================================
    private sealed class NutState
    {
        public Color Mau = Color.Empty;
        public bool Hover, Down, Sidebar, Active;
        public string Icon = "";
    }

    private static readonly ConditionalWeakTable<Button, NutState> _nut = new();

    private static NutState LayTrangThai(Button btn)
    {
        if (_nut.TryGetValue(btn, out var st)) return st;

        st = new NutState();
        _nut.Add(btn, st);
        var state = st;

        btn.MouseEnter += (s, e) => { state.Hover = true; btn.Invalidate(); };
        btn.MouseLeave += (s, e) => { state.Hover = false; state.Down = false; btn.Invalidate(); };
        btn.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) { state.Down = true; btn.Invalidate(); } };
        btn.MouseUp += (s, e) => { state.Down = false; btn.Invalidate(); };
        btn.EnabledChanged += (s, e) => btn.Invalidate();
        btn.Paint += (s, e) => VeNut(btn, state, e.Graphics);
        return st;
    }

    // Tự nhận diện tên nút để chọn màu
    public static void StyleButtonAuto(Button btn)
    {
        // Nút trên sidebar FrmMain dùng StyleSidebarButton riêng
        if (btn.Parent?.Name == "pnlSidebar" || btn.Parent?.Name == "flpMenu")
            return;

        string key = (btn.Name + " " + btn.Text).ToLowerInvariant();

        if (key.Contains("xoa") || key.Contains("xóa"))
            StyleButton(btn, Danger);
        else if (key.Contains("sua") || key.Contains("sửa") || key.Contains("doi") || key.Contains("đổi"))
            StyleButton(btn, Secondary);
        else if (key.Contains("lammoi") || key.Contains("làm mới") || key.Contains("huy") || key.Contains("hủy"))
            StyleButton(btn, Neutral);
        else
            StyleButton(btn, Primary);
    }

    public static void StyleButton(Button btn, Color backColor)
    {
        btn.FlatStyle = FlatStyle.Flat;
        btn.FlatAppearance.BorderSize = 0;
        btn.UseVisualStyleBackColor = false;
        btn.BackColor = backColor;
        btn.ForeColor = Color.White;
        btn.Font = FontBold;
        btn.Cursor = Cursors.Hand;

        var st = LayTrangThai(btn);
        st.Mau = backColor;
        st.Sidebar = false;
        st.Icon = TimIcon((btn.Name + " " + btn.Text).ToLowerInvariant());
        btn.Invalidate();
    }

    // Nút điều hướng bên trái FrmMain
    public static void StyleSidebarButton(Button btn, bool isActive)
    {
        btn.FlatStyle = FlatStyle.Flat;
        btn.FlatAppearance.BorderSize = 0;
        btn.UseVisualStyleBackColor = false;
        btn.BackColor = isActive ? Primary : PrimaryDark;
        btn.ForeColor = Color.White;
        btn.Font = FontBold;
        btn.TextAlign = ContentAlignment.MiddleLeft;
        btn.Padding = new Padding(12, 0, 0, 0);
        btn.Cursor = Cursors.Hand;

        var st = LayTrangThai(btn);
        st.Sidebar = true;
        st.Active = isActive;
        btn.Invalidate();
    }

    // Mã ký tự trong font Segoe MDL2 Assets / Segoe Fluent Icons
    private static string TimIcon(string key)
    {
        if (key.Contains("đăng xuất") || key.Contains("dangxuat")) return "\uE7E8";
        if (key.Contains("xóa") || key.Contains("xoa")) return "\uE74D";
        if (key.Contains("sửa") || key.Contains("sua")) return "\uE70F";
        if (key.Contains("làm mới") || key.Contains("lammoi")) return "\uE72C";
        if (key.Contains("đổi") || key.Contains("doi")) return "\uE895";
        if (key.Contains("tìm") || key.Contains("tim")) return "\uE721";
        if (key.Contains("xuất") || key.Contains("xuat")) return "\uE896";
        if (key.Contains("lưu") || key.Contains("luu")) return "\uE74E";
        if (key.Contains("xác nhận") || key.Contains("xacnhan") || key.Contains("duyệt") || key.Contains("duyet")) return "\uE73E";
        if (key.Contains("hủy") || key.Contains("huy") || key.Contains("từ chối") || key.Contains("tuchoi")) return "\uE711";
        if (key.Contains("thêm") || key.Contains("them")) return "\uE710";
        return "";
    }

    private static Color Mix(Color a, Color b, float t) =>
        Color.FromArgb(a.R + (int)((b.R - a.R) * t), a.G + (int)((b.G - a.G) * t), a.B + (int)((b.B - a.B) * t));

    private static GraphicsPath TaoBoGoc(Rectangle r, int bk)
    {
        var p = new GraphicsPath();
        if (bk <= 0)
        {
            p.AddRectangle(r);
            return p;
        }
        int d = bk * 2;
        p.AddArc(r.X, r.Y, d, d, 180, 90);
        p.AddArc(r.Right - d, r.Y, d, d, 270, 90);
        p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
        p.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
        p.CloseFigure();
        return p;
    }

    private static void VeNut(Button btn, NutState st, Graphics g)
    {
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.Clear(btn.Parent?.BackColor ?? BgMain);   // tô nền góc bo bằng màu của vùng chứa

        Color chu = btn.Enabled ? btn.ForeColor : Color.FromArgb(148, 163, 184);
        Color nen;
        if (!btn.Enabled) nen = Color.FromArgb(226, 232, 240);
        else if (st.Sidebar) nen = st.Active ? Primary : (st.Hover ? Mix(PrimaryDark, Color.White, 0.12f) : PrimaryDark);
        else if (st.Down) nen = Mix(st.Mau, Color.Black, 0.18f);
        else if (st.Hover) nen = Mix(st.Mau, Color.White, 0.16f);
        else nen = st.Mau;

        Rectangle khung = st.Sidebar
            ? new Rectangle(0, 0, btn.Width, btn.Height)
            : new Rectangle(0, 0, btn.Width - 1, btn.Height - 1);

        using (var path = TaoBoGoc(khung, st.Sidebar ? 0 : 8))
        using (var brush = new SolidBrush(nen))
        {
            g.FillPath(brush, path);
        }

        var flags = TextFormatFlags.NoPadding | TextFormatFlags.HidePrefix
                  | TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter;

        // --- Nút sidebar: chữ canh trái + vạch nhấn khi đang chọn
        if (st.Sidebar)
        {
            if (st.Active)
            {
                using var vach = new SolidBrush(Accent);
                g.FillRectangle(vach, 0, 8, 4, btn.Height - 16);
            }
            var r = new Rectangle(20, 0, btn.Width - 28, btn.Height);
            TextRenderer.DrawText(g, btn.Text, btn.Font, r, chu, flags | TextFormatFlags.Left | TextFormatFlags.EndEllipsis);
            return;
        }

        // --- Nút thường: biểu tượng + chữ, canh giữa
        bool coIcon = DungIcon && FontIcon != null && st.Icon.Length > 0;
        int rongIcon = coIcon ? 24 : 0;
        int rongChu = TextRenderer.MeasureText(g, btn.Text, btn.Font, new Size(int.MaxValue, int.MaxValue), flags).Width;
        int kha = btn.Width - 16;
        if (coIcon && rongIcon + rongChu > kha)
        {
            // Không đủ chỗ cho cả icon lẫn chữ: bỏ icon để giữ nguyên chữ
            coIcon = false;
            rongIcon = 0;
        }
        int tong = rongIcon + rongChu;
        if (tong > kha)
        {
            rongChu = Math.Max(0, kha);
            tong = kha;
        }
        int x = (btn.Width - tong) / 2;

        if (coIcon)
        {
            TextRenderer.DrawText(g, st.Icon, FontIcon!, new Rectangle(x, 0, rongIcon, btn.Height),
                chu, flags | TextFormatFlags.HorizontalCenter);
        }
        TextRenderer.DrawText(g, btn.Text, btn.Font, new Rectangle(x + rongIcon, 0, rongChu + 2, btn.Height),
            chu, flags | TextFormatFlags.Left | TextFormatFlags.EndEllipsis);
    }

    // =====================================================================
    // 6. GROUPBOX: thẻ bo góc, tiêu đề cắt ngang viền
    // =====================================================================
    private static void StyleGroup(GroupBox grp)
    {
        grp.BackColor = CardBg;
        grp.ForeColor = PrimaryDark;
        grp.Font = FontBold;

        if (DangKyMotLan(grp))
        {
            BatDoubleBuffer(grp);
            grp.Paint += VeGroupBox;
        }
    }

    private static void VeGroupBox(object? sender, PaintEventArgs e)
    {
        if (sender is not GroupBox grp) return;

        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        Color cha = grp.Parent?.BackColor ?? BgMain;
        g.Clear(cha);

        int th = grp.Font.Height;
        var khung = new Rectangle(0, th / 2, grp.Width - 1, grp.Height - th / 2 - 1);
        using (var path = TaoBoGoc(khung, 8))
        using (var nen = new SolidBrush(CardBg))
        using (var vien = new Pen(Border))
        {
            g.FillPath(nen, path);
            g.DrawPath(vien, path);
        }

        if (!string.IsNullOrEmpty(grp.Text))
        {
            var flags = TextFormatFlags.NoPadding | TextFormatFlags.HidePrefix | TextFormatFlags.SingleLine;
            var size = TextRenderer.MeasureText(g, grp.Text, grp.Font, new Size(int.MaxValue, int.MaxValue), flags);
            using (var xoaVien = new SolidBrush(cha))
                g.FillRectangle(xoaVien, 10, 0, size.Width + 8, th);
            TextRenderer.DrawText(g, grp.Text, grp.Font, new Point(14, 0), grp.ForeColor, flags);
        }
    }

    // =====================================================================
    // 7. Ô NHẬP: viền mảnh + đổi nền nhẹ khi đang gõ
    // =====================================================================
    private static void StyleTextBox(TextBox tb)
    {
        tb.Font = FontNormal;
        tb.ForeColor = TextDark;
        tb.BorderStyle = BorderStyle.FixedSingle;

        if (DangKyMotLan(tb))
        {
            tb.Enter += (s, e) => { if (!tb.ReadOnly) tb.BackColor = FocusBg; };
            tb.Leave += (s, e) => tb.ResetBackColor();   // trả về mặc định (ô ReadOnly tự xám)
        }
    }

    // =====================================================================
    // 8. SPLITCONTAINER: thanh chia mảnh, nền hai bên đồng nhất
    // =====================================================================
    private static void StyleSplit(SplitContainer spl)
    {
        spl.BackColor = Color.FromArgb(226, 232, 240);
        spl.Panel1.BackColor = BgMain;
        spl.Panel2.BackColor = BgMain;
        spl.SplitterWidth = 6;
    }

    // =====================================================================
    // 9. TREEVIEW: mũi tên kiểu Explorer, nút chọn bo góc, rê chuột sáng lên
    // =====================================================================
    [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
    private static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string? pszSubIdList);

    private sealed class CayState { public TreeNode? Node; }
    private static readonly ConditionalWeakTable<TreeView, CayState> _cayHover = new();

    public static void StyleTree(TreeView tv)
    {
        tv.BackColor = CardBg;
        tv.ForeColor = TextDark;
        tv.Font = FontNormal;
        tv.BorderStyle = BorderStyle.None;
        tv.ShowLines = false;
        tv.ItemHeight = 32;
        tv.Indent = 22;
        tv.HideSelection = false;
        tv.DrawMode = TreeViewDrawMode.OwnerDrawText;

        if (DangKyMotLan(tv))
        {
            tv.HandleCreated += (s, e) => SetWindowTheme(tv.Handle, "explorer", null);
            if (tv.IsHandleCreated) SetWindowTheme(tv.Handle, "explorer", null);

            tv.DrawNode += VeNutCay;

            tv.MouseMove += (s, e) =>
            {
                var moi = tv.GetNodeAt(e.X, e.Y);
                var st = _cayHover.GetOrCreateValue(tv);
                if (ReferenceEquals(st.Node, moi)) return;

                var cu = st.Node;
                st.Node = moi;
                if (cu != null && cu.TreeView == tv) tv.Invalidate(cu.Bounds);
                if (moi != null) tv.Invalidate(moi.Bounds);
            };
            tv.MouseLeave += (s, e) =>
            {
                var st = _cayHover.GetOrCreateValue(tv);
                var cu = st.Node;
                st.Node = null;
                if (cu != null && cu.TreeView == tv) tv.Invalidate(cu.Bounds);
            };
        }
    }

    private static void VeNutCay(object? sender, DrawTreeNodeEventArgs e)
    {
        if (sender is not TreeView tv || e.Node == null || e.Bounds.Width <= 0) return;

        bool chon = (e.State & TreeNodeStates.Selected) != 0;
        bool ditro = _cayHover.TryGetValue(tv, out var st) && ReferenceEquals(st.Node, e.Node);

        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        var r = e.Bounds;

        using (var xoa = new SolidBrush(tv.BackColor))
            g.FillRectangle(xoa, r);

        if (chon || ditro)
        {
            var vien = new Rectangle(r.X, r.Y + 1, r.Width, r.Height - 2);
            using var path = TaoBoGoc(vien, 6);
            using var br = new SolidBrush(chon ? Primary : GridHover);
            g.FillPath(br, path);
        }

        TextRenderer.DrawText(g, e.Node.Text, e.Node.NodeFont ?? tv.Font, r,
            chon ? Color.White : TextDark,
            TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter
            | TextFormatFlags.NoPrefix | TextFormatFlags.EndEllipsis);
    }

    // =====================================================================
    // 10. HIỆU ỨNG MỜ DẦN KHI MỞ FORM (chỉ dùng cho Form cấp cao nhất)
    //     Cách dùng: Theme.FadeIn(this); trong constructor
    // =====================================================================
    public static void FadeIn(Form f, int ms = 250)
    {
        if (!f.TopLevel) return;

        f.Opacity = 0;
        var timer = new System.Windows.Forms.Timer { Interval = 15 };
        double buoc = 1.0 / Math.Max(1, ms / 15);

        timer.Tick += (s, e) =>
        {
            if (f.IsDisposed)
            {
                timer.Stop();
                timer.Dispose();
                return;
            }
            f.Opacity = Math.Min(1.0, f.Opacity + buoc);
            if (f.Opacity >= 1.0)
            {
                timer.Stop();
                timer.Dispose();
            }
        };
        f.Shown += (s, e) => timer.Start();
    }

    // =====================================================================
    // 11. THANH TRẠNG THÁI (StatusStrip): nền trắng, vạch kẻ mảnh phía trên
    //     Cách dùng: Theme.StyleStatusStrip(statusStrip1);
    // =====================================================================
    private sealed class StatusRenderer : ToolStripProfessionalRenderer
    {
        public StatusRenderer() : base(new ProfessionalColorTable())
        {
            RoundedEdges = false;
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            using var b = new SolidBrush(e.BackColor);
            e.Graphics.FillRectangle(b, e.AffectedBounds);
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            using var pen = new Pen(Border);
            e.Graphics.DrawLine(pen, 0, 0, e.ToolStrip.Width, 0);
        }
    }

    public static void StyleStatusStrip(StatusStrip ss)
    {
        ss.SizingGrip = false;
        ss.BackColor = CardBg;
        ss.ForeColor = Neutral;
        ss.Font = FontSmall;
        ss.Renderer = new StatusRenderer();
        foreach (ToolStripItem item in ss.Items)
            item.ForeColor = Neutral;
    }
}