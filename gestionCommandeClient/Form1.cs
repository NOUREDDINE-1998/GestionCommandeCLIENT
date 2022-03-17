using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gestionCommandeClient
{
    public partial class Form1 : Form
    {
        public static Form1 mdiobj;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            deconnexionToolStripMenuItem.Enabled = false;
            gestionToolStripMenuItem.Enabled = false;
        }

        private void gestionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void gestionDesProduitsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5(global.User);
            f5.ShowDialog();
        }

        private void connexionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            Form3 f3 = new Form3();
            f3.MdiParent = this;
            f2.MdiParent = this;
            f2.Show();
            mdiobj = this;
         
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gestionDesClientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 fr3 = new Form3();
            fr3.ShowDialog();
        }

        private void gestionDesCommandesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.Show();

        }

        private void consulationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 f6 = new Form6();
            f6.ShowDialog();
        }

        private void deconnexionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deconnexionToolStripMenuItem.Enabled = false;
            connexionToolStripMenuItem.Enabled = true;
            gestionToolStripMenuItem.Enabled = false;
        }
    }
}
