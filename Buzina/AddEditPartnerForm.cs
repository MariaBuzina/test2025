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
    public partial class AddEditPartnerForm : Form
    {
        public AddEditPartnerForm()
        {
            InitializeComponent();
            FillPartnerType();
        }

        int idPartner;

        /// <summary>
        /// Метод для подстановки данных о партнере в поля для редактирования
        /// </summary>
        /// <param name="id">Идентификатор партнера</param>
        public AddEditPartnerForm(int id)
        {
            InitializeComponent();
            FillPartnerType();

            idPartner = id;
            MySqlConnection connection = new MySqlConnection(Connection.con);
            connection.Open();
            MySqlCommand command = new MySqlCommand($@"SELECT * FROM partner WHERE partnerID = '{id}'", connection);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                textBox1.Text = reader[2].ToString();
                textBox2.Text = reader[8].ToString();
                textBox3.Text = reader[6].ToString();
                textBox4.Text = reader[3].ToString();
                textBox6.Text = reader[4].ToString();
                maskedTextBox1.Text = reader[5].ToString();
                comboBox1.SelectedValue = reader[1].ToString();
            }
            connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            this.Visible = false;
            mainForm.ShowDialog();
            this.Close();
        }

        /// <summary>
        /// Заполнение comboBox типом партнера
        /// </summary>
        private void FillPartnerType()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(Connection.con);
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT partnertypeID, partnertypeTitle FROM partnertype", connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                comboBox1.DataSource = table;
                comboBox1.DisplayMember = "partnertypeTitle";
                comboBox1.ValueMember = "partnertypeID";

                connection.Close();

                comboBox1.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar) || Char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar) || Char.IsControl(e.KeyChar) || (e.KeyChar >= 'А' && e.KeyChar <= 'я') || ".,".Contains(e.KeyChar)))
                e.Handled = true;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsWhiteSpace(e.KeyChar) || Char.IsControl(e.KeyChar) || (e.KeyChar >= 'А' && e.KeyChar <= 'я') || "-".Contains(e.KeyChar)))
                e.Handled = true;
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar) || Char.IsControl(e.KeyChar) || (e.KeyChar >= 'a' && e.KeyChar <= 'z') || "@.".Contains(e.KeyChar)))
                e.Handled = true;
        }

        /// <summary>
        /// Метод для добавления и редактирования партнера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox6.Text == "" || comboBox1.SelectedIndex == -1 || maskedTextBox1.MaskFull == false)
            {
                MessageBox.Show("Пожалуйста, заполните все поля!", "Внмание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (button1.Text == "Добавить")
                {
                    try
                    {
                        MySqlConnection connection = new MySqlConnection(Connection.con);
                        connection.Open();
                        MySqlCommand command = new MySqlCommand($@"INSERT INTO partner
                                                                (partnerType,
                                                                partnerTitle,
                                                                partnerDirector,
                                                                partnerEmail, 
                                                                partnerPhone,
                                                                partnerAddress, 
                                                                partnerRating)
                                                                VALUES (
                                                                '{comboBox1.SelectedValue}', 
                                                                '{textBox1.Text}', 
                                                                '{textBox4.Text}', 
                                                                '{textBox6.Text}', 
                                                                '{maskedTextBox1.Text}', 
                                                                '{textBox3.Text}', 
                                                                '{textBox2.Text}')", connection);
                        command.ExecuteNonQuery();
                        connection.Close();

                        MessageBox.Show("Запись успешно добавлена", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                        textBox4.Clear();
                        textBox6.Clear();
                        maskedTextBox1.Clear();
                        comboBox1.SelectedIndex = -1;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    try
                    {
                        MySqlConnection connection = new MySqlConnection(Connection.con);
                        connection.Open();
                        MySqlCommand command = new MySqlCommand($@"UPDATE partner SET
                                                                partnerType = '{comboBox1.SelectedValue}',
                                                                partnerTitle = '{textBox1.Text}',
                                                                partnerDirector = '{textBox4.Text}',
                                                                partnerEmail = '{textBox6.Text}',
                                                                partnerPhone = '{maskedTextBox1.Text}',
                                                                partnerAddress = '{textBox3.Text}',
                                                                partnerRating = '{textBox2.Text}'
                                                                WHERE partnerID = '{idPartner}'", connection);
                        command.ExecuteNonQuery();
                        connection.Close();

                        MessageBox.Show("Запись успешно отредактирована", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        MainForm mainForm = new MainForm();
                        this.Visible = false;
                        mainForm.ShowDialog();
                        this.Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
