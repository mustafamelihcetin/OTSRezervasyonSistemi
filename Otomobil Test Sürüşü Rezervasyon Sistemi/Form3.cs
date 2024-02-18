using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Otomobil_Test_Sürüşü_Rezervasyon_Sistemi
{
    public partial class Form3 : Form
    {
        
        public Form3()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data source=DPCM;Initial Catalog=testsurusuVB;Integrated Security=SSPI;");

        private void Form3_Load(object sender, EventArgs e)
        {
            baglanti.Open();                    
            Form2 frm2 = new Form2();

            int secili = frm2.dataGridView1.CurrentRow.Index;
            textBox1.Text = frm2.dataGridView1.Rows[secili].Cells[0].Value.ToString();

            baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
