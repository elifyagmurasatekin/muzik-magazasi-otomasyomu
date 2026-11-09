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

namespace MuzikEnstrumanSatisProjesi
{
    public partial class Bilgi : Form
    {
        public Bilgi()
        {
            InitializeComponent();
        }
        SqlConnection baglanti=new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MuzikShpDb;Integrated Security=True");

        private void Bilgi_Load(object sender, EventArgs e)
        {
            uyeler();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (EnsAdTb.Text == "" || EnsUcretTb.Text == "" || EnsMiktarTb.Text == "")
            {
                MessageBox.Show("Eksik Bilgi");
            }
            else
            {
                try
                {
                    baglanti.Open();
                    string query = "insert into EnstrumanTbl values ('" + EnsAdTb.Text + "','" + EnsMarkaCb.SelectedItem.ToString() + "'," + EnsMiktarTb.Text + "," + EnsUcretTb.Text + ",'" + EnsKateCb.SelectedItem.ToString() + "')";
                    SqlCommand komut = new SqlCommand(query,baglanti);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Enstrüman Başarıyla Eklendi");
                    baglanti.Close();
                    uyeler();
                }
                catch(Exception Ex) 
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void uyeler()
        {
            baglanti.Open();
            string query = "select * from EnstrumanTbl";
            SqlDataAdapter sda= new SqlDataAdapter(query,baglanti);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BilgiDGV.DataSource = ds.Tables[0];
            baglanti.Close();
        }
        private void Filter()
        {
            baglanti.Open();
            string query = "select * from EnstrumanTbl where EnsMarka='"+FilterCb.SelectedItem.ToString()+"'";
            SqlDataAdapter sda = new SqlDataAdapter(query, baglanti);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BilgiDGV.DataSource = ds.Tables[0];
            baglanti.Close();
        }
        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        int EnsKey;
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (EnsAdTb.Text == "")
            {
                MessageBox.Show("Silinecek Enstrümanı Seçiniz");
            }
            else
            {
                EnsKey = Convert.ToInt32(BilgiDGV.SelectedRows[0].Cells[0].Value.ToString());
                try
                {
                    baglanti.Open();
                    string query = "delete from EnstrumanTbl where EnsId="+EnsKey+"";
                    SqlCommand komut = new SqlCommand(query, baglanti);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Enstrüman Başarıyla Silindi");
                    baglanti.Close();
                    uyeler();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void BilgiDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EnsKey = Convert.ToInt32(BilgiDGV.SelectedRows[0].Cells[0].Value.ToString());
            EnsAdTb.Text = BilgiDGV.SelectedRows[0].Cells[1].Value.ToString();
            EnsMarkaCb.Text = BilgiDGV.SelectedRows[0].Cells[2].Value.ToString();
            EnsMiktarTb.Text = BilgiDGV.SelectedRows[0].Cells[3].Value.ToString();
            EnsUcretTb.Text = BilgiDGV.SelectedRows[0].Cells[4].Value.ToString();
            EnsKateCb.Text = BilgiDGV.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (EnsAdTb.Text == "" || EnsUcretTb.Text == "" || EnsMiktarTb.Text == "")
            {
                MessageBox.Show("Güncellenecek Enstrümanı Seçiniz");
            }
            else
            {
                try
                {
                    baglanti.Open();
                    string query = "update EnstrumanTbl set EnsAd='" + EnsAdTb.Text + "',EnsMarka='" + EnsMarkaCb.SelectedItem.ToString() + "',EnsMiktar=" + EnsMiktarTb.Text + ",EnsUcret=" + EnsUcretTb.Text + ",EnsKategori='" + EnsKateCb.Text + "' where EnsId=" + EnsKey + ";";
                    SqlCommand komut=new SqlCommand(query,baglanti);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Enstrüman Başarıyla Güncellendi");
                    baglanti.Close();
                    uyeler();
                }
                catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void FilterCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Filter();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            uyeler();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            if (BilgiSifreTb.Text == "")
            {
                MessageBox.Show("Güncellenecek Enstrümanı Seçiniz");
            }
            else
            {
                try
                {
                    baglanti.Open();
                    string query = "update AdminTbl set AdminSifre='"+BilgiSifreTb.Text+"' where AdminId="+1+"; ";
                    SqlCommand komut = new SqlCommand(query, baglanti);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Şifre Güncellendi");
                    baglanti.Close();
                    Login log=new Login();
                    log.Show(); 
                    this.Hide();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Login log=new Login();
            log.Show();
            this.Hide();
        }
    }
}
