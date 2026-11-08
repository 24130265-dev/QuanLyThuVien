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

        }
    }
}
