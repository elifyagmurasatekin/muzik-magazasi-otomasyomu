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
    public partial class Satis : Form
    {
        public Satis()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MuzikShpDb;Integrated Security=True");
        private void uyeler()
        {
            baglanti.Open();
            string query = "select EnsAd,EnsUcret from EnstrumanTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, baglanti);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BilgiDGV.DataSource = ds.Tables[0];
            baglanti.Close();
        }
        private void Filter()
        {
            baglanti.Open();
            string query = "select EnsAd ,EnsUcret from EnstrumanTbl where EnsMarka='" + FilterCb.SelectedItem.ToString() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, baglanti);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BilgiDGV.DataSource = ds.Tables[0];
            baglanti.Close();
        }
        private void Satis_Load(object sender, EventArgs e)
        {
            uyeler();
        }

        private void BilgiDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EnsAdTb.Text = BilgiDGV.SelectedRows[0].Cells[0].Value.ToString();
            EnsUcretTb.Text = BilgiDGV.SelectedRows[0].Cells[1].Value.ToString();
        }

        private void FilterCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Filter();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            uyeler();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Login log=new Login();
            log.Show();
            this.Hide();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            EnsAdTb.Text = "";
            EnsMiktarTb.Text = "";
            EnsUcretTb.Text = "";
        }
        private void insertFatura()
        {
            string Bugun;
            Bugun = DateTime.Today.Date.ToString();
            try
            {
                baglanti.Open();
                string query = "insert into FaturaTbl values("+TutarToplam+",'"+Bugun+"')";
                SqlCommand komut = new SqlCommand(query, baglanti);
                komut.ExecuteNonQuery();
                MessageBox.Show("Fatura Başarıyla Eklendi");
                baglanti.Close();
                uyeler();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        int n = 0,TutarToplam=0;

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
            if(printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
            insertFatura();
        }
        int ensid, ensmiktar, ensucret, topplam, pos = 60;

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        string ensad;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Nota Ve Ötesi Müzik Mağazası", new Font("Times New Roman", 12, FontStyle.Bold), Brushes.DarkCyan, new Point(30));
            e.Graphics.DrawString("ID ENSTRÜMAN ÜCRET MİKTAR TOPLAM", new Font("Times New Roman", 10, FontStyle.Regular), Brushes.DarkCyan, new Point(5,20));
            foreach(DataGridViewRow row in SatisDGV.Rows)
            {
                ensid = Convert.ToInt32(row.Cells["Column1"].Value);
                ensad = "" + row.Cells["Column2"].Value;
                ensucret = Convert.ToInt32(row.Cells["Column3"].Value);
                ensmiktar = Convert.ToInt32(row.Cells["Column4"].Value);
                topplam = Convert.ToInt32(row.Cells["Column5"].Value);
                e.Graphics.DrawString(""+ensid, new Font("Times New Roman", 10, FontStyle.Regular), Brushes.DarkOrange, new Point(5, pos));
                e.Graphics.DrawString(""+ensad, new Font("Times New Roman", 10, FontStyle.Regular), Brushes.DarkCyan, new Point(25,pos));
                e.Graphics.DrawString("" + ensucret, new Font("Times New Roman", 10, FontStyle.Regular), Brushes.DarkCyan, new Point(120,pos));
                e.Graphics.DrawString("" + ensmiktar, new Font("Times New Roman", 10, FontStyle.Regular), Brushes.DarkCyan, new Point(175,pos));
                e.Graphics.DrawString("" + topplam, new Font("Times New Roman", 10, FontStyle.Regular), Brushes.DarkCyan, new Point(225,pos));
                pos = pos + 20;
            }
            e.Graphics.DrawString("Toplam Tutar: "+TutarToplam, new Font("Times New Roman", 12, FontStyle.Bold), Brushes.DarkCyan, new Point(60, 200));
            e.Graphics.DrawString("*** Müzik Enstrüman Satış Mağazası ***", new Font("Times New Roman", 8, FontStyle.Bold), Brushes.DarkCyan, new Point(50,230));
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            if (EnsAdTb.Text == "" || EnsMiktarTb.Text == "" || EnsUcretTb.Text == "")
            {
                MessageBox.Show("Enstrüman seçiniz ve miktarını giriniz");
            }
            else
            {
                int toplam = Convert.ToInt32(EnsMiktarTb.Text) * Convert.ToInt32(EnsUcretTb.Text);
                DataGridViewRow newRow=new DataGridViewRow();
                newRow.CreateCells(SatisDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = EnsAdTb.Text;
                newRow.Cells[2].Value = EnsUcretTb.Text;
                newRow.Cells[3].Value = EnsMiktarTb.Text;
                newRow.Cells[4].Value = toplam;
                SatisDGV.Rows.Add(newRow);
                n++;
                TutarToplam = TutarToplam + toplam;
                UcretLbl.Text = "" + TutarToplam;
            }
        }
    }
}
