using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Otomobil_Test_Sürüşü_Rezervasyon_Sistemi
{
    public partial class Form2 : Form
    {
        private readonly SqlConnection baglanti = new SqlConnection("Data Source=DPCM;Initial Catalog=testsurusuVB;Integrated Security=SSPI;");

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var frm = new Form1();
            frm.Show();
            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var frm = new Form4();
            frm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                var kmt = new SqlCommand("SELECT * FROM kayitlar WHERE telefon='" + textBox1.Text + "'", baglanti);
                var da = new SqlDataAdapter(kmt);
                var dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                var kmt = new SqlCommand("SELECT * FROM kayitlar", baglanti);
                var da = new SqlDataAdapter(kmt);
                var dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult cevap = MessageBox.Show("Silinecek olan kaydın telefon numarasına tıklayıp siliniz. Silme işlemini onaylıyor musunuz?", "Silme İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap == DialogResult.Yes)
            {
                try
                {
                    baglanti.Open();
                    var kmt = new SqlCommand("DELETE FROM kayitlar WHERE telefon='" + dataGridView1.CurrentRow?.Cells["telefon"].Value?.ToString() + "'", baglanti);
                    kmt.ExecuteNonQuery();
                    MessageBox.Show("Kayıt başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata oluştu: " + ex.Message);
                }
                finally
                {
                    baglanti.Close();
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var frm = new Form3();
            frm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var frm = new Form5();
            frm.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            var frm = new Form3();
            try
            {
                baglanti.Open();
                var kmt = new SqlCommand("SELECT * FROM kayitlar", baglanti);
                var da = new SqlDataAdapter(kmt);
                var dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }
        }
    }
}
