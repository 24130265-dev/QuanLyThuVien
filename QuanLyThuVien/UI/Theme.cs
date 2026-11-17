using System.Drawing;
using System.Windows.Forms;

namespace QuanLyThuVien.UI;

public static class Theme
{
    // 1. BẢNG MÀU DÙNG CHUNG TOÀN HỆ THỐNG (Chủ đề Xanh Thư viện NLU)
    public static readonly Color Primary = Color.FromArgb(15, 118, 110);       // Xanh ngọc đậm (Nút chính, Header bảng)
    public static readonly Color PrimaryDark = Color.FromArgb(19, 78, 74);     // Xanh đậm cho thanh Sidebar trái
    public static readonly Color Secondary = Color.FromArgb(37, 99, 235);      // Xanh dương (Nút Sửa / Đổi trạng thái)
    public static readonly Color Danger = Color.FromArgb(220, 38, 38);         // Đỏ (Nút Xóa)
    public static readonly Color Neutral = Color.FromArgb(100, 116, 139);      // Xám đậm (Nút Làm mới / Hủy)
    public static readonly Color BgMain = Color.FromArgb(241, 245, 249);       // Nền xám sáng dịu mắt cho Form
    public static readonly Color CardBg = Color.White;                         // Nền trắng cho GroupBox / Bảng
    public static readonly Color TextDark = Color.FromArgb(30, 41, 59);        // Chữ đen xám dễ đọc
    public static readonly Color GridAltRow = Color.FromArgb(240, 253, 250);   // Màu dòng xen kẽ xanh rất nhạt
    public static readonly Color GridSelect = Color.FromArgb(204, 251, 241);   // Màu khi bấm chọn 1 dòng trên bảng

    // 2. PHÔNG CHỮ CHUẨN
    public static readonly Font FontNormal = new("Segoe UI", 10F, FontStyle.Regular);
    public static readonly Font FontBold = new("Segoe UI", 10F, FontStyle.Bold);
    public static readonly Font FontTitle = new("Segoe UI", 13F, FontStyle.Bold);

    // 3. HÀM TỰ ĐỘNG LÀM ĐẸP TOÀN BỘ FORM (Chỉ cần gọi Theme.Apply(this) ở mỗi Form)
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
                    grp.BackColor = CardBg;
                    grp.ForeColor = PrimaryDark;
                    grp.Font = FontBold;
                    break;

                case TreeView tv:
                    tv.BackColor = CardBg;
                    tv.ForeColor = TextDark;
                    tv.Font = FontNormal;
                    tv.BorderStyle = BorderStyle.FixedSingle;
                    tv.ItemHeight = 26; // Giãn khoảng cách các nhánh cây cho thoáng
                    break;

                case TextBox or ComboBox or NumericUpDown or DateTimePicker:
                    c.Font = FontNormal;
                    break;

                case Label lbl:
                    lbl.Font = FontNormal;
                    lbl.ForeColor = TextDark;
                    break;
            }

            // Nếu bên trong Control này còn chứa các Control con (như SplitContainer, Panel, GroupBox, TabControl) thì duyệt tiếp
            if (c.HasChildren)
            {
                ApDungDeQuy(c);
            }
        }
    }

    // 4. HÀM LÀM ĐẸP BẢNG DATAGRIDVIEW (Xóa bỏ kiểu bảng tính Excel cũ)
    public static void StyleGrid(DataGridView dgv)
    {
        dgv.BackgroundColor = CardBg;
        dgv.BorderStyle = BorderStyle.None;
        dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        dgv.GridColor = Color.FromArgb(226, 232, 240);

        // Bắt buộc tắt VisualStyles mặc định thì tiêu đề cột mới nhận màu xanh
        dgv.EnableHeadersVisualStyles = false;
        dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        dgv.ColumnHeadersHeight = 36;
        dgv.ColumnHeadersDefaultCellStyle.BackColor = Primary;
        dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        dgv.ColumnHeadersDefaultCellStyle.Font = FontBold;

        // Dòng dữ liệu và dòng xen kẽ
        dgv.RowTemplate.Height = 30;
        dgv.DefaultCellStyle.BackColor = CardBg;
        dgv.DefaultCellStyle.ForeColor = TextDark;
        dgv.DefaultCellStyle.Font = FontNormal;
        dgv.DefaultCellStyle.SelectionBackColor = GridSelect;
        dgv.DefaultCellStyle.SelectionForeColor = TextDark;
        dgv.AlternatingRowsDefaultCellStyle.BackColor = GridAltRow;
    }

    // 5. HÀM TỰ ĐỘNG NHẬN DIỆN TÊN NÚT ĐỂ TÔ MÀU PHÙ HỢP
    public static void StyleButtonAuto(Button btn)
    {
        // Bỏ qua các nút nằm trên thanh Sidebar trái của FrmMain (để dùng hàm StyleSidebarButton riêng)
        if (btn.Parent?.Name == "pnlSidebar" || btn.Parent?.Name == "flpMenu")
            return;

        string key = (btn.Name + " " + btn.Text).ToLowerInvariant();

        if (key.Contains("xoa") || key.Contains("xóa"))
            StyleButton(btn, Danger);      // Nút Xóa -> Màu đỏ
        else if (key.Contains("sua") || key.Contains("sửa") || key.Contains("doi") || key.Contains("đổi"))
            StyleButton(btn, Secondary);   // Nút Sửa / Đổi trạng thái -> Màu xanh dương
        else if (key.Contains("lammoi") || key.Contains("làm mới") || key.Contains("huy") || key.Contains("hủy"))
            StyleButton(btn, Neutral);     // Nút Làm mới / Hủy -> Màu xám đậm
        else
            StyleButton(btn, Primary);     // Nút Thêm / Tìm kiếm / Xác nhận -> Màu xanh chính
    }

    public static void StyleButton(Button btn, Color backColor)
    {
        btn.FlatStyle = FlatStyle.Flat;
        btn.FlatAppearance.BorderSize = 0;
        btn.BackColor = backColor;
        btn.ForeColor = Color.White;
        btn.Font = FontBold;
        btn.Cursor = Cursors.Hand;
    }

    // 6. button FRMMAIN
    public static void StyleSidebarButton(Button btn, bool isActive)
    {
        btn.FlatStyle = FlatStyle.Flat;
        btn.FlatAppearance.BorderSize = 0;
        btn.BackColor = isActive ? Primary : PrimaryDark;
        btn.ForeColor = Color.White;
        btn.Font = FontBold;
        btn.TextAlign = ContentAlignment.MiddleLeft;
        btn.Padding = new Padding(12, 0, 0, 0);
        btn.Cursor = Cursors.Hand;
    }
}