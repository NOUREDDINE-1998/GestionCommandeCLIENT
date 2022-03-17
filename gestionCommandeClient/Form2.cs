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

namespace gestionCommandeClient
{
    public partial class Form2 : Form
    {
        private SqlConnection cnx;
        private SqlCommand cmd;
        private SqlDataReader dr;
        public Form2()
        {
            InitializeComponent();
        }

        private void okbtn_Click(object sender, EventArgs e)
        {
            
            cnx = new SqlConnection("Data Source=DESKTOP-6D9ML6R;Initial Catalog=GestionCommandeClient;Integrated Security=True");
            string query = "select login,pass from admin where login='"+textBox3.Text+"' and pass='"+textBox4.Text+"'";
            cmd = new SqlCommand(query, cnx);
            cnx.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
               
               Form1.mdiobj.connexionToolStripMenuItem.Enabled = false;
                Form1.mdiobj.deconnexionToolStripMenuItem.Enabled = true;
                Form1.mdiobj.gestionToolStripMenuItem.Enabled = true;
                this.Hide();
            }
            else { 
                MessageBox.Show("login ou mot de passe est incorrect");
            
            }
            
           

            cnx.Close();
            global.User = textBox3.Text;
            Form5 f5 = new Form5(global.User);
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            textBox4.Text = "";
            textBox3.Focus();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
           
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
