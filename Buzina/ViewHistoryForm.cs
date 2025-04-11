using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Buzina
{
    public partial class ViewHistoryForm : Form
    {
        public ViewHistoryForm()
        {
            InitializeComponent();
        }

        public ViewHistoryForm(int id)
        {
            InitializeComponent();
            MySqlConnection connection = new MySqlConnection(Connection.con);
            connection.Open();
            MySqlCommand command = new MySqlCommand($@"SELECT
                                                    productTitle AS 'Наименование',
                                                    partnerproductCount AS 'Количество',
                                                    partnerproductDate AS 'Дата продажи'
                                                    FROM partnerproduct
                                                    INNER JOIN product ON product.productArticle = partnerproduct.partnerproductProductArticle
                                                    WHERE partnerproductPartnerID = '{id}'", connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            connection.Close();

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            this.Visible = false;
            mainForm.ShowDialog();
            this.Close();
        }
    }
}
