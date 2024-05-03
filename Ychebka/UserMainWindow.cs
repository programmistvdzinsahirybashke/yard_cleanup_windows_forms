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
    public partial class UserMainWindow : Form
    {

        private int UserID;
        public UserMainWindow()
        {
            InitializeComponent();
        }
        private string connection = "Server=localhost;port=5432;username=postgres;password=123;database=cleanFINAL";


        private void UserMainWindow_Load(object sender, EventArgs e)
        {

            // Получение ID пользователя
            UserID = GetUserID();

            //ВЫВОД АДРЕСОВ
            try
            {

                using (NpgsqlConnection conn = new NpgsqlConnection(connection))
                {
                    conn.Open();

                    // Создание SQL-запроса с параметром @UserID
                    string query = $"SELECT Города.название, Улицы.название, Улицы.номер\r\n         " +
                        "       FROM Сотрудники_и_дворы\r\n         " +
                        "       join Дворы on Дворы.двор_id  = Сотрудники_и_дворы.двор \r\n      " +
                        "          join Города on Дворы.город  = Города.город_id \r\n            " +
                        "    join Улицы on Дворы.улица  = Улицы.улица_id  \r\n          " +
                        "      WHERE Сотрудники_и_дворы.сотрудник=@UserID;";

                    // Создание команды с параметром
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", UserID);

                        // Создание адаптера данных и заполнение DataTable
                        using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                        {
                            DataTable data = new DataTable();
                            adapter.Fill(data);

                            // Установка DataTable как источника данных для DataGridView
                            dataGridViewAddress.DataSource = data;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            //ЗАДАЧИ
            try
            {

                using (NpgsqlConnection conn = new NpgsqlConnection(connection))
                {
                    conn.Open();

                    // Создание SQL-запроса с параметром @UserID
                    string query = $"select Города.название, Улицы.название, Улицы.номер, Типы_работ.название, Задачи_сотрудников.описание, Статусы_задач.название, TO_CHAR(дата_выдачи, 'YYYY-MM-DD HH24:MI:SS') as дата_выдачи, TO_CHAR(дата_завершения, 'YYYY-MM-DD HH24:MI:SS') as дата_завершения \r\n  " +
                        $"      from Задачи_сотрудников \r\n        join Сотрудники_и_дворы on Задачи_сотрудников.сотрудник_и_двор  = Сотрудники_и_дворы.двор_сотрудника_id \r\n        join Дворы on Дворы.двор_id = Сотрудники_и_дворы.двор \r\n        join Города on Дворы.город  = Города.город_id \r\n     " +
                        $"   join Улицы on Дворы.улица  = Улицы.улица_id  \r\n    " +
                        $"    join Сотрудники on Сотрудники.сотрудник_id = Сотрудники_и_дворы.сотрудник \r\n   " +
                        $"     join Должности_и_типы_работ on Задачи_сотрудников.должность_и_тип_работ = Должности_и_типы_работ.должность_и_тип_работы_id  \r\n   " +
                        $"     join Типы_работ on Должности_и_типы_работ.тип_работы = Типы_работ.тип_работы_id  \r\n     " +
                        $"   join Статусы_задач on Статусы_задач.статус_id = Задачи_сотрудников.статус \r\n    " +
                        $"    where Сотрудники.сотрудник_id = @UserID and Задачи_сотрудников.статус = 1;";

                    // Создание команды с параметром
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", UserID);

                        // Создание адаптера данных и заполнение DataTable
                        using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                        {
                            DataTable data = new DataTable();
                            adapter.Fill(data);

                            // Установка DataTable как источника данных для DataGridView
                            dataGridViewTasks.DataSource = data;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //ИНСТРУМЕНТЫ
            try
            {

                using (NpgsqlConnection conn = new NpgsqlConnection(connection))
                {
                    conn.Open();

                    // Создание SQL-запроса с параметром @UserID
                    string query = $"SELECT  Города.название, Улицы.название, Улицы.номер, Типы_инструментов.название, Сотрудники_и_инструменты.выданное_количество\r\n " +
                        $"       FROM Сотрудники_и_инструменты\r\n  " +
                        $"      join Инструменты_и_дворы on Сотрудники_и_инструменты.инструмент_и_двор_id = Инструменты_и_дворы.инструмент_и_двор_id \r\n   " +
                        $"     join Типы_инструментов on Типы_инструментов.тип_инструмента_id = Инструменты_и_дворы.инструмент_id\r\n   " +
                        $"     join Дворы on Инструменты_и_дворы.двор_id  = Дворы.двор_id  \r\n   " +
                        $"     join Города on Дворы.город  = Города.город_id \r\n     " +
                        $"   join Улицы on Дворы.улица  = Улицы.улица_id  \r\n      " +
                        $"  join Сотрудники on Сотрудники.сотрудник_id = Сотрудники_и_инструменты.сотрудник_id \r\n   " +
                        $"     WHERE Сотрудники_и_инструменты.сотрудник_id  = @UserID";

                    // Создание команды с параметром
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", UserID);

                        // Создание адаптера данных и заполнение DataTable
                        using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                        {
                            DataTable data = new DataTable();
                            adapter.Fill(data);

                            // Установка DataTable как источника данных для DataGridView
                            dataGridViewTools.DataSource = data;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //ВЫВОД ФИО
            using (NpgsqlConnection conn = new NpgsqlConnection(connection))
            {

                string query = $"Select * from Сотрудники where сотрудник_id=@UserID";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    NpgsqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();

                        string surname = (reader["фамилия"].ToString());
                        string name = (reader["имя"].ToString());
                        string otchestvo = (reader["отчество"].ToString());

                        labelFIO.Text = surname + " " + name + " " + otchestvo;           

                    }

                    
                }

            }

        }

        private void dataGridViewAddress_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
        private int GetUserID()
        {
            // Здесь должен быть ваш код для получения ID пользователя
            // Например, если ID хранится в поле класса или получается из другого источника данных, то можно его вернуть здесь
            return UserData.UserID; // Пример
        }
        private void buttonLogout_Click(object sender, EventArgs e)
        {
            Form1 Form1 = new Form1();
            this.Hide();
            Form1.Show();

        }
    }
}