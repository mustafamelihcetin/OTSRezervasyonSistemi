using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Otomobil_Test_Sürüşü_Rezervasyon_Sistemi
{
    public partial class Form5 : Form
    {
        private readonly SqlConnection baglanti = new SqlConnection("Data source=DPCM;Initial Catalog=testsurusuVB;Integrated Security=SSPI");

        public Form5()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox3.Text))
                {
                    MessageBox.Show("Lütfen boş alan bırakmayınız!");
                    return;
                }

                baglanti.Open();
                string giris = "INSERT INTO araclar(aracismi) VALUES(@aracismi)";
                SqlCommand kmtgiris = new SqlCommand(giris, baglanti);
                kmtgiris.Parameters.AddWithValue("@aracismi", textBox3.Text);
                SqlDataReader bilgigetir = kmtgiris.ExecuteReader();
                while (bilgigetir.Read())
                {
                    if (bilgigetir.GetString(1) == textBox3.Text)
                    {
                        MessageBox.Show("Bu kayıt zaten var!");
                        return;
                    }
                }

                baglanti.Close();
                DialogResult secenek = MessageBox.Show("Kayıt işlemi başarıyla gerçekleşti!");

                if (secenek == DialogResult.OK)
                {
                    Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata meydana geldi: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = checkBox1.Checked;
            button1.Enabled = checkBox1.Checked;
        }
        private void Form5_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                string giris = "DELETE FROM araclar WHERE aracismi = @aracismi";
                SqlCommand kmtgiris = new SqlCommand(giris, baglanti);
                kmtgiris.Parameters.AddWithValue("@aracismi", textBox1.Text);
                kmtgiris.ExecuteNonQuery();
                DialogResult secenek = MessageBox.Show("Kayıt başarıyla silindi!");

                if (secenek == DialogResult.OK)
                {
                    Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata meydana geldi: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }
        }
    }
}
