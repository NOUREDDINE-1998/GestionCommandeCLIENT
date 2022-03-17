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
    public partial class Form6 : Form
    {
        private SqlConnection cnx = new SqlConnection("Data Source=DESKTOP-6D9ML6R;Initial Catalog=GestionCommandeClient;Integrated Security=True");
        private SqlCommand cmd;
        private SqlDataReader dr;
        private SqlDataAdapter da;

        public Form6()
        {
            InitializeComponent();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                    cnx.Close();
            string query = "select num_commande, date_commande from commande where cin='" + textBox1.Text + "'";
            cnx.Open();
            cmd = new SqlCommand(query, cnx);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows) { 
                dataGridView1.Rows.Clear();
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr[0], dr[1]);
             
            }
            }
            dr.Close();
            cnx.Close();

            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1.Focus();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dataGridView1.CurrentRow.Index;
            int num_commande = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value.ToString());
           
            cnx.Close();
            string query = "select detail_commandes.ref_produit,produit.intitule,produit.prix_vente,(detail_commandes.quantite *produit.prix_vente) as [sous total]from produit inner join detail_commandes on produit.ref_produit = detail_commandes.ref_produit inner join commande on commande.num_commande = detail_commandes.num_commande where commande.num_commande=" + num_commande + "";
            cnx.Open();
            cmd = new SqlCommand(query, cnx);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dataGridView2.Rows.Clear();
                while (dr.Read())
                {
                    dataGridView2.Rows.Add(dr[0], dr[1], dr[2], dr[3]);

                }
            }
            dr.Close();
            // pour totaliser le prix d'un commande x d'un client y dans date precisee.
            string query1 = "select sum(detail_commandes.quantite *produit.prix_vente) as [total]from produit inner join detail_commandes on produit.ref_produit = detail_commandes.ref_produit inner join commande on commande.num_commande = detail_commandes.num_commande  where commande.num_commande=" + num_commande + "";
            cmd = new SqlCommand(query1, cnx);
            SqlDataReader dr1 = cmd.ExecuteReader();
            
                while (dr1.Read())
                {
                textBox3.Text = Convert.ToString(dr1.GetSqlMoney(0));

            }
            
            dr1.Close();
            cnx.Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentRow.Index;
            int num_commande = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value.ToString());
            cnx.Close();
            Form8 F8 = new Form8();
            F8.Show();
            string s = "select * from consultation where consultation.cin='"+textBox1.Text+ "' and consultation.num_commande=" + num_commande + "";
            cnx.Open();
            cmd = new SqlCommand(s, cnx);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds,"consultation");        
            CrystalReport2 cr = new CrystalReport2();
            cr.SetDataSource(ds);
            F8.crystalReportViewer1.ReportSource = cr;
            cnx.Close();
            F8.crystalReportViewer1.Refresh();
        }
    }
}
