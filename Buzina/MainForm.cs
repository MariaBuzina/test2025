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
            dataGridView1.ClearSelection();
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

                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                dataGridView1.Columns[2].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dataGridView1.Columns[3].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            ContextMenu contextMenu = new ContextMenu();
            contextMenu.MenuItems.Add(new MenuItem("Удалить", DeletePartner));
            contextMenu.MenuItems.Add(new MenuItem("История заказов", GetHistoryPartner));

            int row = dataGridView1.HitTest(e.X, e.Y).RowIndex;

            dataGridView1.ClearSelection();
            dataGridView1.Rows[row].Selected = true;

            contextMenu.Show(dataGridView1, new Point(e.X, e.Y));
        }

        /// <summary>
        /// Метод для получения истории заказов партнера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetHistoryPartner(object sender, EventArgs e)
        {
            int row = dataGridView1.SelectedRows[0].Index;
            int id = Convert.ToInt32(dataGridView1.Rows[row].Cells["partnerID"].Value);

            ViewHistoryForm viewHistoryForm = new ViewHistoryForm(id);
            this.Visible = false;
            viewHistoryForm.ShowDialog();
            this.Close();
        }

        /// <summary>
        /// Метод для удаления партнера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeletePartner(object sender, EventArgs e)
        {
            int row = dataGridView1.SelectedRows[0].Index;
            int id = Convert.ToInt32(dataGridView1.Rows[row].Cells["partnerID"].Value);

            DialogResult dialogResult = MessageBox.Show("Вы уверены, что хотите удалить запись?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    MySqlConnection connection = new MySqlConnection(Connection.con);
                    connection.Open();
                    MySqlCommand command = new MySqlCommand($@"UPDATE partner SET
                                                            partnerDeleted = '1'
                                                            WHERE partnerID = '{id}'", connection);
                    command.ExecuteNonQuery();
                    connection.Close();

                    MessageBox.Show("Запись успешно удалена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FillData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            if (row != -1)
            {
                int id = Convert.ToInt32(dataGridView1.Rows[row].Cells["partnerID"].Value);

                AddEditPartnerForm addEditPartnerForm = new AddEditPartnerForm(id);
                addEditPartnerForm.button1.Text = "Редактировать";
                this.Visible = false;
                addEditPartnerForm.ShowDialog();
                this.Close();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            FillData();
        }
    }
}
