using System.Diagnostics;

namespace QuanLyThuVien
{
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.Text = "QUẢN LÝ THƯ VIỆN";
            this.Size = new Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            TaoDaoDien();
        }

        private void TaoDaoDien()
        {
            Label title = new Label();
            title.Text ="Phan Mem Quan Ly Thu Vien";
            title.Font =  new Font("Arial", 22, FontStyle.Bold);
            title.Location = new Point(250, 30);
            title.Size = new Size(550, 50);
            Controls.Add(title);

            Button btnSach = TaoButton("Quan Ly Sach", 100, 150);
            Button btnDocGia = TaoButton("Quan Ly Doc Gia", 350, 150);
            Button btnMuonTra = TaoButton("Quan Ly Muon Tra", 600, 150);

            Controls.Add(btnSach);
            Controls.Add(btnDocGia);
            Controls.Add(btnMuonTra);
           
           btnSach.Click += (s, e) => 
           {
            MessageBox.Show(
              "Chuc nang quan ly sach duoc chon",
              "Thong bao",
              MessageBoxButtons.OK,
              MessageBoxIcon.Information);
            };
            btnSach.Click += (s, e) => 
           {
            MessageBox.Show(
              "Chuc nang quan doc gia duoc chon",
              "Thong bao",
              MessageBoxButtons.OK,
              MessageBoxIcon.Information);
            };
            btnSach.Click += (s, e) =>
            {
                using(FormMuonTra form = new FormMuonTra())
                {
                    FormMuonTra.ShowDialog();
                }
            }


        private Button TaoButton(string text, int x, int y)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Location = new Point(x, y);
            btn.Size = new Size(200, 80);
            btn.Font = new Font("Arial", 12, FontStyle.Bold);
            return btn;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // Code to execute when the form loads
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // Code to execute when the checkbox state changes
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
    }
}
