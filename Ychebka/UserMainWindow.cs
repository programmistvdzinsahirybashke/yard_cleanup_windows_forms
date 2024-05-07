using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;

using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

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
                    string query = $"select задача_id, Города.название, Улицы.название, Улицы.номер, Типы_работ.название, Задачи_сотрудников.описание, Статусы_задач.название, TO_CHAR(дата_выдачи, 'YYYY-MM-DD HH24:MI:SS') as дата_выдачи, TO_CHAR(дата_завершения, 'YYYY-MM-DD HH24:MI:SS') as дата_завершения, фото \r\n  " +
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

        private void dataGridViewTasks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Получаем индекс текущей строки в DataGridView
            int rowIndex = e.RowIndex;

            // Проверяем, что индекс строки корректен и не является индексом заголовка столбца
            if (rowIndex >= 0 && rowIndex < dataGridViewTasks.Rows.Count - 1)
            {
                // Получаем значение ID задачи из выбранной строки
                int taskId = Convert.ToInt32(dataGridViewTasks.Rows[rowIndex].Cells["задача_id"].Value);

                using (NpgsqlConnection conn = new NpgsqlConnection(connection))
                {
                    try
                    {
                        conn.Open();
                        string query = "SELECT статус_id, название FROM Статусы_задач WHERE название = 'На проверке' OR название = 'Выдана'";
                        NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, conn);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        comboBoxStatus.DisplayMember = "название";
                        comboBoxStatus.ValueMember = "статус_id";
                        comboBoxStatus.DataSource = dt;

                        // Запрос для получения фотографии по заданному ID задачи
                        query = "SELECT фото FROM Задачи_сотрудников WHERE задача_id = @taskId";
                        using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@taskId", taskId);
                            object imageDataObj = cmd.ExecuteScalar();

                            // Проверка, что значение фото не равно null
                            if (imageDataObj != DBNull.Value)
                            {
                                byte[] imageData = (byte[])imageDataObj;

                                // Преобразование массива байтов в объект Image
                                using (MemoryStream ms = new MemoryStream(imageData))
                                {
                                    pictureBox1.Image = Image.FromStream(ms);
                                }
                            }
                            else
                            {
                                // Очистка изображения PictureBox, если фото отсутствует
                                pictureBox1.Image = null;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при загрузке статусов задач: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void comboBoxStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonSaveStatus_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxStatus.SelectedItem != null)
                {
                    DataRowView selectedRow = (DataRowView)comboBoxStatus.SelectedItem;
                    if (selectedRow != null && selectedRow["название"] != null)
                    {
                        string selectedStatus = selectedRow["название"].ToString();
                        if (!string.IsNullOrEmpty(selectedStatus))
                        {
                            int selectedStatusId = GetStatusId(selectedStatus);
                            if (selectedStatusId > 0)
                            {

                                UpdateTaskStatusInDatabase(selectedStatusId, selectedImagePath);
                                using (NpgsqlConnection conn = new NpgsqlConnection(connection))
                                {
                                    conn.Open();

                                    string query = $"select задача_id, Города.название, Улицы.название, Улицы.номер, Типы_работ.название, Задачи_сотрудников.описание, Статусы_задач.название, TO_CHAR(дата_выдачи, 'YYYY-MM-DD HH24:MI:SS') as дата_выдачи, TO_CHAR(дата_завершения, 'YYYY-MM-DD HH24:MI:SS') as дата_завершения, фото \r\n  " +
                                        $"      from Задачи_сотрудников \r\n        join Сотрудники_и_дворы on Задачи_сотрудников.сотрудник_и_двор  = Сотрудники_и_дворы.двор_сотрудника_id \r\n        join Дворы on Дворы.двор_id = Сотрудники_и_дворы.двор \r\n        join Города on Дворы.город  = Города.город_id \r\n     " +
                                        $"   join Улицы on Дворы.улица  = Улицы.улица_id  \r\n    " +
                                        $"    join Сотрудники on Сотрудники.сотрудник_id = Сотрудники_и_дворы.сотрудник \r\n   " +
                                        $"     join Должности_и_типы_работ on Задачи_сотрудников.должность_и_тип_работ = Должности_и_типы_работ.должность_и_тип_работы_id  \r\n   " +
                                        $"     join Типы_работ on Должности_и_типы_работ.тип_работы = Типы_работ.тип_работы_id  \r\n     " +
                                        $"   join Статусы_задач on Статусы_задач.статус_id = Задачи_сотрудников.статус \r\n    " +
                                        $"    where Сотрудники.сотрудник_id = @UserID and Задачи_сотрудников.статус = 1;";

                                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                                    {
                                        cmd.Parameters.AddWithValue("@UserID", UserID);

                                        using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                                        {
                                            DataTable data = new DataTable();
                                            adapter.Fill(data);
                                            dataGridViewTasks.DataSource = data;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show($"Не удалось получить айди статуса задачи {selectedStatus}, {selectedStatusId}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Выберите статус задачи", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private int GetStatusId(string statusName)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connection))
                try
                {
                    conn.Open();
                    string query = "SELECT статус_id FROM Статусы_задач WHERE название = @statusName";
                    NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@statusName", statusName);
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при получении айди статуса задачи: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }
                finally
                {
                    conn.Close();
                }
        }
        private Image currentPhoto;

        private void UpdateTaskStatusInDatabase(int selectedStatusId, string imagePath)
        {
            try
            {
                if (dataGridViewTasks.CurrentRow != null && dataGridViewTasks.CurrentRow.Cells["задача_id"] != null && dataGridViewTasks.CurrentRow.Cells["задача_id"].Value != null)
                {
                    int taskId = Convert.ToInt32(dataGridViewTasks.CurrentRow.Cells["задача_id"].Value);
                    DataRowView selectedRowforotchet = comboBoxStatus.SelectedItem as DataRowView;
                    if (selectedRowforotchet != null && selectedRowforotchet["название"] != null)
                    {
                        string selectedStatusforotchet = selectedRowforotchet["название"].ToString();
                        string selectedTextZadachi = dataGridViewTasks.CurrentRow.Cells["описание"].Value.ToString();

                        DialogResult result = MessageBox.Show($"Вы уверены, что хотите сохранить изменения?" +
                            $"\nНомер задачи = {taskId}" +
                            $"\nТекст задачи = {selectedTextZadachi}" +
                            $"\nНовый статус задачи = {selectedStatusforotchet}", "Подтверждение", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            using (NpgsqlConnection conn = new NpgsqlConnection(connection))
                            {
                                try
                                {
                                    conn.Open();
                                    string query = "UPDATE Задачи_сотрудников SET статус = @selectedStatusId";

                                    if (!string.IsNullOrEmpty(imagePath))
                                    {
                                        query += ", фото = @photo";
                                    }

                                    if (selectedStatusId == 2 || selectedStatusId == 4)
                                    {
                                        query += ", дата_завершения = @feedback_datetime";
                                    }

                                    query += " WHERE задача_id = @taskId";

                                    NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                                    cmd.Parameters.AddWithValue("@selectedStatusId", selectedStatusId);

                                    if (!string.IsNullOrEmpty(imagePath))
                                    {
                                        byte[] imageData = File.ReadAllBytes(imagePath);
                                        cmd.Parameters.AddWithValue("@photo", imageData);
                                    }

                                    cmd.Parameters.AddWithValue("@taskId", taskId);

                                    if (selectedStatusId == 2 || selectedStatusId == 4)
                                    {
                                        DateTime feedback_datetime = DateTime.Now;
                                        cmd.Parameters.AddWithValue("@feedback_datetime", feedback_datetime);
                                    }

                                    cmd.ExecuteNonQuery();

                                    MessageBox.Show("Статус задачи успешно обновлен", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    comboBoxStatus.Text = "";
                                    pictureBox1.Image = null;
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"Ошибка при обновлении статуса задачи: статус = {selectedStatusId} {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        else if (result == DialogResult.No)
                        {
                            // Действие при ответе "Нет"
                        }
                    }
                    else
                    {
                        MessageBox.Show("Выберите статус задачи", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Выберите задачу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string selectedImagePath;

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png|All Files (*.*)|*.*";
                openFileDialog.Title = "Выберите изображение";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedImagePath = openFileDialog.FileName;
                    Image image = Image.FromFile(selectedImagePath);

                    // Отображаем изображение в PictureBox
                    pictureBox1.Image = image;
                    // Используйте путь к выбранному изображению для предварительного просмотра или загрузки.
                }
            }
        }
    }
}