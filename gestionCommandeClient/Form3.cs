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
    public partial class Form3 : Form
    {
        private SqlConnection cnx = new SqlConnection("Data Source=DESKTOP-6D9ML6R;Initial Catalog=GestionCommandeClient;Integrated Security=True");
        private SqlCommand cmd;
        private SqlDataReader dr;
        public Form3()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string query = "select * from client where cin='" + cin.Text + "' ";
            cmd = new SqlCommand(query, cnx);
            cnx.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                nom.Text = dr.GetString(1);
                prenom.Text = dr.GetString(2);
                ville.Text = dr.GetString(3);
                tel.Text = dr.GetString(4);
            }
            else
                MessageBox.Show(cin.Text + " n'existe pas");
            dr.Close();
            cnx.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            cnx.Close();
            cnx.Open();
            string query = "select * from client  ";
            cmd = new SqlCommand(query, cnx);

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                this.dataGridView1.Rows.Clear();

                while (dr.Read())
                {
                    dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4]);
                } }
            dr.Close();
            cnx.Close();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            vider();
        }
        public  void vider()
        {
            cin.Text = "";
            nom.Text = "";
            prenom.Text = "";
            ville.Text = "";
            tel.Text = "";
            cin.Focus();
        }

        private void quitter_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ajouter_Click(object sender, EventArgs e)
        {

            string query = "insert into client values('" + cin.Text + "','" + nom.Text + "','" + prenom.Text + "','" + ville.Text + "','" + tel.Text + "')";
            cmd = new SqlCommand(query, cnx);
            cnx.Open();
            if (cin.Text == "" || nom.Text == "" || prenom.Text == "" || ville.Text == "" || tel.Text == "")
            {
                MessageBox.Show("veuillez remplir tous les champs");
            }
            else if (cmd.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("l'ajout fait avec sucees");
                Form3_Load(sender, e);
                vider();
            }
            else
            {
                MessageBox.Show("un probleme a ete survenu, veuillez contactez administrateur");


                cnx.Close();
                vider();

            }
        }
            private void modifier_Click(object sender, EventArgs e)
            {
            
                string query = "update client set nom='" + nom.Text + "',prenom='" + prenom.Text + "',ville='" + ville.Text + "',tel='" + tel.Text + "' where cin='" + cin.Text + "' ";
                cmd = new SqlCommand(query, cnx);
                cnx.Open();
                if (cmd.ExecuteNonQuery() == 1) {
                    MessageBox.Show("la modefication faite avec sucees");
                    Form3_Load(sender, e);
                    vider();
                }
                else
                {
                    MessageBox.Show("un probleme a ete survenu, veuillez contactez administrateur");
                }
            
                cnx.Close();


            }
        private void supprimer_Click(object sender, EventArgs e)
        {
            string query = "delete from client where cin='"+cin.Text+"' ";
            cmd = new SqlCommand(query, cnx);
            cnx.Open();
            if (cmd.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("la suppression faite avec sucees");
                Form3_Load(sender, e);
                vider();
            }
            else
            {
                MessageBox.Show("un probleme a ete survenu, veuillez contactez administrateur");
            }
            cnx.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dataGridView1.CurrentRow.Index;
            if (index >= 0)
            {
                cin.Text = dataGridView1.Rows[index].Cells[0].Value.ToString();
                nom.Text = dataGridView1.Rows[index].Cells[1].Value.ToString();
                prenom.Text = dataGridView1.Rows[index].Cells[2].Value.ToString();
                ville.Text = dataGridView1.Rows[index].Cells[3].Value.ToString();
                tel.Text = dataGridView1.Rows[index].Cells[4].Value.ToString();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            cnx.Close();
            Form7 F7 = new Form7();
            F7.Show();
            string s = "select * from client";
            cnx.Open();
            cmd = new SqlCommand(s, cnx);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "client");
            CrystalReport1 cr = new CrystalReport1();
            cr.SetDataSource(ds);
            F7.crystalReportViewer1.ReportSource = cr;
            cnx.Close();
            F7.crystalReportViewer1.Refresh();

        }
    }
}
