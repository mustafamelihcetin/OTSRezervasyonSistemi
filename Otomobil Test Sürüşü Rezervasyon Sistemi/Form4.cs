using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Otomobil_Test_Sürüşü_Rezervasyon_Sistemi
{
    public partial class Form4 : Form
    {
        private readonly SqlConnection baglanti = new SqlConnection("Data Source=DPCM;Initial Catalog=testsurusuVB;Integrated Security=SSPI;");

        public Form4()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            KayitEkle();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            IlleriComboBoxDoldur();
            AraclariComboBoxDoldur();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.TextLength == 3)
                textBox4.Focus();
        }

        private void IlleriComboBoxDoldur()
        {
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM iller", baglanti);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                foreach (DataRow dr in dataSet.Tables[0].Rows)
                {
                    comboBox2.Items.Add(dr["name"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("İller yüklenirken bir hata oluştu: " + ex.Message);
            }
        }

        private void AraclariComboBoxDoldur()
        {
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM araclar", baglanti);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                foreach (DataRow dr in dataSet.Tables[0].Rows)
                {
                    comboBox3.Items.Add(dr["aracismi"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Araçlar yüklenirken bir hata oluştu: " + ex.Message);
            }
        }

        private void KayitEkle()
        {
            try
            {
                baglanti.Open();
                string query = "INSERT INTO kayitlar(ad, soyad, dtarihi, eposta, telefon, aracisim, sehir, bayi) VALUES(@ad, @soyad, @dtarihi, @eposta, @telefon, @aracisim, @sehir, @bayi)";
                SqlCommand command = new SqlCommand(query, baglanti);
                command.Parameters.AddWithValue("@ad", textBox1.Text);
                command.Parameters.AddWithValue("@soyad", textBox2.Text);
                command.Parameters.AddWithValue("@dtarihi", dateTimePicker1.Text);
                command.Parameters.AddWithValue("@eposta", textBox3.Text);
                command.Parameters.AddWithValue("@telefon", textBox5.Text + textBox4.Text);
                command.Parameters.AddWithValue("@aracisim", comboBox3.Text);
                command.Parameters.AddWithValue("@sehir", comboBox2.Text);
                command.Parameters.AddWithValue("@bayi", comboBox4.Text);

                command.ExecuteNonQuery();
                MessageBox.Show("Kayıt işlemi başarıyla gerçekleşti!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kayıt ekleme sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
                Hide();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                comboBox4.Enabled = true;
                comboBox4.SelectedItem = null;
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM bayiler WHERE bayiler LIKE '" + comboBox2.Text + "%'", baglanti);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                foreach (DataRow dr in dataSet.Tables[0].Rows)
                {
                    comboBox4.Items.Add(dr["bayiler"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bayi bilgileri yüklenirken bir hata oluştu: " + ex.Message);
            }
        }
    }
}
