using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MuzikEnstrumanSatisProjesi
{
    public partial class Gecis : Form
    {
        public Gecis()
        {
            InitializeComponent();
        }
        int baslangic = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            baslangic += 1;
            gunaBar.Value=baslangic;
            YuzdeLbl.Text = baslangic + "%";
            if (gunaBar.Value == 100)
            {
                gunaBar.Value = 0;
                timer1.Stop();
                Login log=new Login();
                log.Show();
                this.Hide();
                log.Show();
            }
        }

        private void Gecis_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
