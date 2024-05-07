using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static Ychebka.AddEmployee;

namespace Ychebka
{
    public partial class EmployeesPanel : Form
    {
        public EmployeesPanel()
        {
            InitializeComponent();
        }
        private string connection = "Server=localhost;port=5432;username=postgres;password=123;database=cleanFINAL";


        private void buttonBack_Click(object sender, EventArgs e)
        {
            // Открывать окно администратора
            AdminPanel AdminPanel = new AdminPanel();
            this.Hide();
            AdminPanel.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddEmployee addEmployee = new AddEmployee();
            this.Hide();
            addEmployee.Show();
        }
        private void LoadPositionsIntoComboBox()
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connection))
                {
                    conn.Open();
                    string query = "SELECT * FROM Должности";
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Установка источника данных для ComboBox и указание отображаемого идентификатора
                    comboBoxPositions.DataSource = dataTable;
                    comboBoxPositions.DisplayMember = "название";
                    comboBoxPositions.ValueMember = "должность_id";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки должностей: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void EmployeesPanel_Load(object sender, EventArgs e)
        {
            LoadPositionsIntoComboBox();



            using (NpgsqlConnection conn = new NpgsqlConnection(connection))
            {
                conn.Open();

                string query = "Select * from Сотрудники";
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, conn);
                DataTable data = new DataTable();
                adapter.Fill(data);

                dataGridViewEmployees.DataSource = data;
            }
        }
        private int selectedEmployeeId;

        private void UpdateEmployee(int employeeId, string lastName, string firstName, string middleName, string passport, string address, string contactInfo, string login, string password, int positionId)
        {
            if (!string.IsNullOrEmpty(textBoxLastName.Text) &&
                !string.IsNullOrEmpty(textBoxFirstName.Text) &&
                comboBoxPositions.SelectedItem != null &&
                !string.IsNullOrEmpty(textBoxPassport.Text) &&
                !string.IsNullOrEmpty(textBoxAddress.Text) &&
                !string.IsNullOrEmpty(textBoxContactInfo.Text) &&
                !string.IsNullOrEmpty(textBoxLogin.Text) &&
                !string.IsNullOrEmpty(textBoxPassword.Text))
            {
                try
                {
                    using (NpgsqlConnection conn = new NpgsqlConnection(connection))
                    {
                        conn.Open();

                        string query = @"UPDATE Сотрудники 
                             SET должность = @positionId, 
                                 фамилия = @lastName, 
                                 имя = @firstName, 
                                 отчество = @middleName, 
                                 серия_номер_паспорта = @passport, 
                                 адрес_жительства = @address, 
                                 контактная_информация = @contactInfo, 
                                 логин = @login, 
                                 пароль = @password
                             WHERE сотрудник_id = @employeeId";

                        using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@positionId", positionId);
                            cmd.Parameters.AddWithValue("@lastName", lastName);
                            cmd.Parameters.AddWithValue("@firstName", firstName);
                            cmd.Parameters.AddWithValue("@middleName", middleName);
                            cmd.Parameters.AddWithValue("@passport", passport);
                            cmd.Parameters.AddWithValue("@address", address);
                            cmd.Parameters.AddWithValue("@contactInfo", contactInfo);
                            cmd.Parameters.AddWithValue("@login", login);
                            cmd.Parameters.AddWithValue("@password", password);
                            cmd.Parameters.AddWithValue("@employeeId", employeeId);

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Данные сотрудника успешно обновлены", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Очищаем поля формы
                                ClearFormFields();
                                // Обновляем данные в DataGridView
                                RefreshDataGridView();
                            }
                            else
                            {
                                MessageBox.Show("Ошибка при обновлении данных сотрудника", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все поля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void RefreshDataGridView()
        {
            // Обновляем данные в DataGridView
            using (NpgsqlConnection conn = new NpgsqlConnection(connection))
            {
                conn.Open();

                string query = "SELECT * FROM Сотрудники";
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, conn);
                DataTable data = new DataTable();
                adapter.Fill(data);

                dataGridViewEmployees.DataSource = data;
            }
        }

        private void ClearFormFields()
        {
            // Очищаем поля формы
            textBoxLastName.Text = "";
            textBoxFirstName.Text = "";
            textBoxMiddleName.Text = "";
            textBoxPassport.Text = "";
            textBoxAddress.Text = "";
            textBoxContactInfo.Text = "";
            textBoxLogin.Text = "";
            textBoxPassword.Text = "";
            comboBoxPositions.SelectedIndex = -1;
        }
        private void dataGridViewEmployees_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewEmployees.Rows[e.RowIndex];

                // Получаем данные выбранной строки
                int employeeId = Convert.ToInt32(row.Cells["сотрудник_id"].Value);
                string lastName = row.Cells["фамилия"].Value.ToString();
                string firstName = row.Cells["имя"].Value.ToString();
                string middleName = row.Cells["отчество"].Value.ToString();
                string passport = row.Cells["серия_номер_паспорта"].Value.ToString();
                string address = row.Cells["адрес_жительства"].Value.ToString();
                string contactInfo = row.Cells["контактная_информация"].Value.ToString();
                string login = row.Cells["логин"].Value.ToString();
                string password = row.Cells["пароль"].Value.ToString();
                int positionId = Convert.ToInt32(row.Cells["должность"].Value);

                // Отображаем данные выбранной строки в форме для редактирования
                textBoxLastName.Text = lastName;
                textBoxFirstName.Text = firstName;
                textBoxMiddleName.Text = middleName;
                textBoxPassport.Text = passport;
                textBoxAddress.Text = address;
                textBoxContactInfo.Text = contactInfo;
                textBoxLogin.Text = login;
                textBoxPassword.Text = password;
                comboBoxPositions.SelectedValue = positionId;

                // Сохраняем сотрудника, чтобы знать, какую запись обновлять в базе данных
                selectedEmployeeId = employeeId;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Получаем новые данные из полей формы
            string lastName = textBoxLastName.Text;
            string firstName = textBoxFirstName.Text;
            string middleName = textBoxMiddleName.Text;
            string passport = textBoxPassport.Text;
            string address = textBoxAddress.Text;
            string contactInfo = textBoxContactInfo.Text;
            string login = textBoxLogin.Text;
            string password = textBoxPassword.Text;
            int positionId = Convert.ToInt32(comboBoxPositions.SelectedValue);

            // Обновляем данные в базе данных
            UpdateEmployee(selectedEmployeeId, lastName, firstName, middleName, passport, address, contactInfo, login, password, positionId);

        

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connection))
            {

                conn.Open();
                string query = $"Select * from Сотрудники WHERE фамилия LIKE  '%{textBoxSearch.Text}%'";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, conn);
                DataTable table = new DataTable();
                adapter.Fill(table);

                dataGridViewEmployees.DataSource = table;
            }
        }
    }
}
