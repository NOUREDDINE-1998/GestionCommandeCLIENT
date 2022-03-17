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
    public partial class Form5 : Form
    {
        private SqlConnection cnx = new SqlConnection("Data Source=DESKTOP-6D9ML6R;Initial Catalog=GestionCommandeClient;Integrated Security=True");
        private SqlCommand cmd;
        private SqlDataReader dr;
        public Form5(string login)
        {
            InitializeComponent();
            label8.Text = login;
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void Form5_Load(object sender, EventArgs e)
        {
            this.groupBox4.Enabled = false;
            cnx.Close();
            string query = "select distinct cin from commande";
            cnx.Open();
            cmd = new SqlCommand(query, cnx);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                comboBox1.Items.Clear();
                while (dr.Read())
                {
                    comboBox1.Items.Add(dr[0]);
                }
            }

                dr.Close();

            string query1 = "select ref_produit from produit";
            cmd = new SqlCommand(query1, cnx);
            SqlDataReader dr1 = cmd.ExecuteReader();
            while (dr1.Read())
            {
                comboBox2.Items.Add(dr1[0]);
            }
            dr1.Close();
            cnx.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Vider()
        {
            textBox1.Text = "";
            comboBox1.Text = "";
            textBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Vider();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cnx.Close();
            string query = "select num_commande,cin,date_commande from commande where num_commande='" + textBox1.Text + "'";
            cnx.Open();
            cmd = new SqlCommand(query, cnx);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                textBox1.Text = Convert.ToString(dr.GetInt32(0));
                comboBox1.Text = dr.GetString(1);
                dateTimePicker1.Value = dr.GetDateTime(2);
            }

            dr.Close();
            cnx.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cnx.Close();
            string query = "insert into commande values('" + textBox1.Text + "','" + comboBox1.Text + "','" + dateTimePicker1.Value + "','" + label8.Text + "')";
            cmd = new SqlCommand(query, cnx);
            cnx.Open();
            if (textBox1.Text == "" || comboBox1.Text == "" || label8.Text == "")
            {
                MessageBox.Show("veuillez remplir tous les champs");
            }
            else if (cmd.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("l'ajout fait avec sucees");
                Vider();
            }
            else
            {
                MessageBox.Show("un probleme a ete survenu, veuillez contactez administrateur");


                cnx.Close();
                Vider();
            };

        }

        private void button5_Click(object sender, EventArgs e)
        {
            cnx.Close();
            string query = "delete commande  where num_commande='" + textBox1.Text + "'";
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

        private void button4_Click(object sender, EventArgs e)
        {
            cnx.Close();
            string query = "update commande set cin='" + comboBox1.Text + "',date_commande= '" + dateTimePicker1.Value + "',login='" + label8.Text + "' where num_commande='" + textBox1.Text + "'";
            cmd = new SqlCommand(query, cnx);
            cnx.Open();
            if (textBox1.Text == "" || comboBox1.Text == "" || label8.Text == "")
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

        private void button6_Click(object sender, EventArgs e)
        {
            // desactiver la zone commande et activer la zone details en verifiant la textbox de numer_commande !=""
            if (textBox1.Text != "")
            {
                groupBox1.Enabled = false;
                groupBox4.Enabled = true;
                //pour chargee la grile de zone details
                cnx.Close();
                cnx.Open();
                string query = "select (detail_commandes.ref_produit),(produit.intitule),(produit.prix_vente),(detail_commandes.quantite) from produit inner join detail_commandes on (produit.ref_produit) = (detail_commandes.ref_produit) ";
                cmd = new SqlCommand(query, cnx);

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

                // la requete suivante est pour charger le total de tous les produits selon son prix * quantite 

                string query1 = "select SUM(produit.prix_vente * detail_commandes.quantite) as total from produit inner join detail_commandes on produit.ref_produit = detail_commandes.ref_produit";
                cmd = new SqlCommand(query1, cnx);
                SqlDataReader dr1 = cmd.ExecuteReader();
                while (dr1.Read())
                {
                    textBox3.Text = Convert.ToString(dr1.GetSqlMoney(0));
                }
                dr1.Close();

                cnx.Close();
            }
            else
                MessageBox.Show("veuillez saisir le numero de la commande");




        }

        private void button12_Click(object sender, EventArgs e)
        {
            comboBox2.Text = "";
            textBox2.Text = "";
            textBox1.Text = "";
            groupBox1.Enabled = true;
            groupBox4.Enabled = false;
            textBox3.Text = "";
            dataGridView1.Rows.Clear();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            cnx.Close();
            string query = "insert into detail_commandes values('" + textBox1.Text + "','" + comboBox2.Text + "','" + textBox2.Text + "')";
            cmd = new SqlCommand(query, cnx);
            cnx.Open();
            if (textBox1.Text == "" || textBox2.Text == "" || comboBox2.Text == "")
            {
                MessageBox.Show("veuillez remplir tous les champs");
            }
            else if (cmd.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("l'ajout fait avec sucees");
                button6_Click(sender, e);
                groupBox4.Enabled = true;


                Vider();
            }
            else
            {
                MessageBox.Show("un probleme a ete survenu, veuillez contactez administrateur");


                cnx.Close();
                Vider();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            cnx.Close();
            string query = "delete detail_commandes  where num_commande='" + textBox1.Text + "' and ref_produit='" + comboBox2.Text + "'";
            cmd = new SqlCommand(query, cnx);
            cnx.Open();
            if (cmd.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("la suppression  est faite avec sucees");
                button6_Click(sender, e);
                groupBox4.Enabled = true;
                Vider();
            }
            else
            {
                MessageBox.Show("un probleme a ete survenu, veuillez contactez l'administrateur");

                cnx.Close();

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            int index = dataGridView1.CurrentRow.Index;
            if (index >= 0)
            {
                comboBox2.Text = dataGridView1.Rows[index].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[index].Cells[3].Value.ToString();
                cnx.Close();
                string query = "select num_commande from detail_commandes where ref_produit='" + comboBox2.Text + "'";
                cnx.Open();
                cmd = new SqlCommand(query, cnx);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    textBox1.Text = dr.GetInt32(0).ToString();
                }
                dr.Close();
                cnx.Close();

            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            cnx.Close();
            string query = "update detail_commandes set quantite='" + textBox2.Text + "' where ref_produit='" + comboBox2.Text + "' and num_commande='" + textBox1.Text + "'";
            cmd = new SqlCommand(query, cnx);
            cnx.Open();
            if (textBox1.Text == "" || textBox2.Text == "" || comboBox2.Text == "")
            {
                MessageBox.Show("veuillez remplir tous les champs");
            }
            else if (cmd.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("la modefication est faite avec sucees");
                button6_Click(sender, e);
                groupBox4.Enabled = true;
                Vider();
            }
            else
            {
                MessageBox.Show("un probleme a ete survenu, veuillez contactez l'administrateur");


                cnx.Close();
                Vider();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            dataGridView1.Rows.Clear();
            groupBox1.Enabled = true;
            groupBox4.Enabled = false;
        }
    } 
}
