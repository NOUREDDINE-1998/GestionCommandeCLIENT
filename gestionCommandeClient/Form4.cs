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
    public partial class Form4 : Form
    {
        private SqlConnection cnx = new SqlConnection("Data Source=DESKTOP-6D9ML6R;Initial Catalog=GestionCommandeClient;Integrated Security=True");
        private SqlCommand cmd;
        private SqlDataReader dr;
        public Form4()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dataGridView1.CurrentRow.Index;
            if (index >= 0)
            {
                reference.Text = dataGridView1.Rows[index].Cells[0].Value.ToString();
                intitule.Text = dataGridView1.Rows[index].Cells[1].Value.ToString();
                categorie.Text = dataGridView1.Rows[index].Cells[2].Value.ToString();
                prix.Text = dataGridView1.Rows[index].Cells[3].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Vider();

        }
        public void Vider()
        {
            reference.Text = "";
            intitule.Text = "";
            categorie.Text = "";
            prix.Text = "";
            reference.Focus();
        }

        private void rechercher_Click(object sender, EventArgs e)
        {
            cnx.Close();
            cnx.Open();
            string query = "select * from produit where ref_produit='" + reference.Text + "'";
            cmd = new SqlCommand(query, cnx);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                intitule.Text = dr.GetString(1);
                categorie.Text = dr.GetString(2);
                prix.Text = dr.GetSqlMoney(3).ToString();
            }
            else
                MessageBox.Show(reference.Text + " n'existe pas");
            dr.Close();
            cnx.Close();
        }

        private void quitter_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ajouter_Click(object sender, EventArgs e)
        {
            cnx.Close();
            string query = "insert into produit values('" + reference.Text + "','" + intitule.Text + "','" + categorie.Text + "','" + prix.Text + "')";
            cmd = new SqlCommand(query, cnx);
            cnx.Open();
            if (reference.Text == "" || intitule.Text == "" || categorie.Text == "" || prix.Text == "")
            {
                MessageBox.Show("veuillez remplir tous les champs");
            }
            else if (cmd.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("l'ajout fait avec sucees");
                Form4_Load( sender,  e);


                Vider();
            }
            else
            {
                MessageBox.Show("un probleme a ete survenu, veuillez contactez administrateur");


                cnx.Close();
                Vider();
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            cnx.Close();
            string query = "select distinct categorie from produit ";
            cmd = new SqlCommand(query, cnx);
            cnx.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                categorie.Items.Clear();

                while (dr.Read())
                {
                    categorie.Items.Add(dr[0]);
                }
            }
            dr.Close();

            string query1 = "select * from produit  ";
            cmd = new SqlCommand(query1, cnx);

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                this.dataGridView1.Rows.Clear();

                while (dr.Read())
                {
                    dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3]);
                }
            }
            dr.Close();
            cnx.Close();



        }

        private void modifier_Click(object sender, EventArgs e)
        {
            cnx.Close();
            string query = "update produit set intitule='" + intitule.Text + "',categorie='" + categorie.Text + "',prix_vente='" + prix.Text + "' where ref_produit='" + reference.Text + "'";
            cmd = new SqlCommand(query, cnx);
            cnx.Open();
            if (reference.Text == "" || intitule.Text == "" || categorie.Text == "" || prix.Text == "")
            {
                MessageBox.Show("veuillez remplir tous les champs");
            }
            else if (cmd.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("la modefication est faite avec sucees");

                Vider();
            }
            else
            {
                MessageBox.Show("un probleme a ete survenu, veuillez contactez l'administrateur");


                cnx.Close();
                Vider();
            }
        }

        private void supprimer_Click(object sender, EventArgs e)
        {
            cnx.Close();
            string query = "delete produit  where ref_produit='" + reference.Text + "'";
            cmd = new SqlCommand(query, cnx);
            cnx.Open();
            if (cmd.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("la suppression  est faite avec sucees");

                Vider();
            }
            else
            {
                MessageBox.Show("un probleme a ete survenu, veuillez contactez l'administrateur");


                cnx.Close();
                Vider();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
