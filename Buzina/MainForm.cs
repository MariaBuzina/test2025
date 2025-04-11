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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            FillData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddEditPartnerForm addEditPartnerForm = new AddEditPartnerForm();
            this.Visible = false;
            addEditPartnerForm.ShowDialog();
            this.Close();
        }

        /// <summary>
        /// Метод для вывода списка партнеров
        /// </summary>
        private void FillData()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(Connection.con);
                connection.Open();
                MySqlCommand command = new MySqlCommand(@"SELECT
                                                        partnerID,
                                                        partnerType,
                                                        concat_ws(' ', partnertypeTitle, ' | ', partnerTitle) AS 'Тип | Наименование партнера',
                                                        partnerDirector AS 'Директор',
                                                        partnerPhone AS 'Телефон',
                                                        partnerRating  AS 'Рейтинг',
                                                        ifnull(sum(partnerproductCount), 0) AS 'Скидка'
                                                        FROM partner
                                                        INNER JOIN partnertype ON partnertype.partnertypeID = partner.partnerType
                                                        LEFT JOIN partnerproduct ON partnerproduct.partnerproductPartnerID = partner.partnerID
                                                        WHERE partnerDeleted = '0'
                                                        GROUP BY partnerID", connection);

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                foreach (DataRow row in table.Rows)
                {
                    int count = Convert.ToInt32(row["Скидка"]);
                    Discount discount = new Discount();
                    int disc = discount.GetDiscount(count);
                    row["Скидка"] = disc;
                }

                dataGridView1.DataSource = table;

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = false;
                connection.Close();

                dataGridView1.ClearSelection();
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
