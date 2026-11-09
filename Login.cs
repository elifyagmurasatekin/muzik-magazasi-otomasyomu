using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MuzikEnstrumanSatisProjesi
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MuzikShpDb;Integrated Security=True");
        private void label7_Click(object sender, EventArgs e)
        {
            Satis sat= new Satis();
            sat.Show();
            this.Hide();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            KullaniciTb.Text = "";
            SifreTb.Text = "";

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlDataAdapter sda=new SqlDataAdapter("select count (*) from AdminTbl where AdminAdSoyad='"+KullaniciTb.Text+"' and AdminSifre='"+SifreTb.Text+"'",baglanti);
            DataTable dt= new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                Bilgi bil=new Bilgi();
                bil.Show();
                this.Hide();
                baglanti.Close();
            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı ya da Şifre");
            }
            baglanti.Close();
        }
    }
}
