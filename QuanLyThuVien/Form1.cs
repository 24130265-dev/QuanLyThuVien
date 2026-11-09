namespace QuanLyThuVien
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent()
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

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Label label = new Label();
            title.Text = "Phan Mem Quan Ly Thu Vien";
            title.Font = new Font("Arial", 22, FontStyle.Bold);
            title.Location = new Point(10, 10);
            title.Size = new Size(500, 50);
            Controls.Add(title);

            Button btnSach = TaoButton("Quan Ly Sach", 100, 150);
            Button btnDocGia = TaoButton("Quan Ly Doc Gia", 350, 150);
            Button btnMuonTra = TaoButton("Quan Ly Muon Tra", 600, 150);

            Controls.Add(btnSach);
            Controls.Add(btnDocGia);
            Controls.Add(btnMuonTra);
           
           btnSach.Click += (s, e) => {
                MessageBox.Show("Quan ly sach");
            };
            btnDocGia.Click += (s, e) => {
                MessageBox.Show("Quan ly doc gia");
            };
            btnMuonTra.Click += (s, e) => {
                MessageBox.Show("Quan ly muon tra");
            };
        }

    }
}
