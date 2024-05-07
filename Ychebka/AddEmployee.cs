using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ychebka
{
    public partial class AddEmployee : Form
    {
        public AddEmployee()
        {
            InitializeComponent();
            // Загрузка данных о должностях из базы данных
            LoadPositionsIntoComboBox();
        }
        private string connectionString = "Server=localhost;port=5432;username=postgres;password=123;database=cleanFINAL";

        // Метод для загрузки списка должностей из базы данных
        private void LoadPositionsIntoComboBox()
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Должности";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int positionId = Convert.ToInt32(reader["должность_id"]);
                                string positionName = reader["название"].ToString();
                                comboBoxPosition.Items.Add(new PositionItem(positionId, positionName));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки должностей: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textBoxLastName.Text) &&
                !string.IsNullOrEmpty(textBoxFirstName.Text) &&
                comboBoxPosition.SelectedItem != null &&
                !string.IsNullOrEmpty(textBoxPassport.Text) &&
                !string.IsNullOrEmpty(textBoxAddress.Text) &&
                !string.IsNullOrEmpty(textBoxContactInfo.Text) &&
                !string.IsNullOrEmpty(textBoxLogin.Text) &&
                !string.IsNullOrEmpty(textBoxPassword.Text))
                { // Получение выбранной должности из комбобокса
                    PositionItem selectedPosition = (PositionItem)comboBoxPosition.SelectedItem;
                int positionId = selectedPosition.Id;

                // Получение значений полей из формы
                string firstName = textBoxFirstName.Text;
                string lastName = textBoxLastName.Text;
                string middleName = textBoxMiddleName.Text; // Отчество
                string passport = textBoxPassport.Text; // Серия и номер паспорта
                string address = textBoxAddress.Text; // Адрес жительства
                string contactInfo = textBoxContactInfo.Text; // Контактная информация
                string login = textBoxLogin.Text;
                string password = textBoxPassword.Text;

                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    // Создание SQL-запроса для добавления сотрудника
                    string query = "INSERT INTO Сотрудники (должность, фамилия, имя, отчество, серия_номер_паспорта, адрес_жительства, контактная_информация, логин, пароль) " +
                                   "VALUES (@positionId, @lastName, @firstName, @middleName, @passport, @address, @contactInfo, @login, @password)";

                    // Создание команды с параметрами
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

                        // Выполнение команды
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Проверка успешности операции
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Сотрудник успешно добавлен", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Очистка полей формы
                            ClearFormFields();
                        }
                        else
                        {
                            MessageBox.Show("Не удалось добавить сотрудника", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                } 
                }
                else
                {
                    MessageBox.Show("Пожалуйста, заполните все поля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFormFields()
        {
            comboBoxPosition.SelectedIndex = -1;
            textBoxFirstName.Clear();
            textBoxLastName.Clear();
            textBoxMiddleName.Clear();
            textBoxPassport.Clear();
            textBoxAddress.Clear();
            textBoxContactInfo.Clear();
            textBoxLogin.Clear();
            textBoxPassword.Clear();
        }
        private void AddEmployeeToDatabase(int positionId)
        {
            string firstName = textBoxFirstName.Text;
            string lastName = textBoxLastName.Text;
            string middleName = textBoxMiddleName.Text; // Отчество
            string passport = textBoxPassport.Text; // Серия и номер паспорта
            string address = textBoxAddress.Text; // Адрес жительства
            string contactInfo = textBoxContactInfo.Text; // Контактная информация
            string login = textBoxLogin.Text;
            string password = textBoxPassword.Text;

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                string query = "INSERT INTO Сотрудники (должность, фамилия, имя, отчество, серия_номер_паспорта, адрес_жительства, контактная_информация, логин, пароль) " +
                               "VALUES (@positionId, @lastName, @firstName, @middleName, @passport, @address, @contactInfo, @login, @password)";

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

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Сотрудник успешно добавлен", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFormFields();
                    }
                    else
                    {
                        MessageBox.Show("Не удалось добавить сотрудника", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        // Класс для представления должности
        public class PositionItem
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public PositionItem(int id, string name)
            {
                Id = id;
                Name = name;
            }

            public override string ToString()
            {
                return Name;
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            EmployeesPanel employeesPanel = new EmployeesPanel();
            this.Hide();
            employeesPanel.Show();
        }
    }
}
