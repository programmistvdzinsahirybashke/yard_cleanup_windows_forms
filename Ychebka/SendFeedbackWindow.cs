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
    public partial class SendFeedbackWindow : Form
    {
        public SendFeedbackWindow()
        {
            InitializeComponent();
        }

        private string connectionString = "Server=localhost;port=5432;username=postgres;password=123;database=cleanFINAL";

        // Метод для загрузки номеров из базы данных для выбранной улицы и заполнения ComboBox'а с номерами
        private void LoadNumbersForStreet(string selectedStreet, string cityName)
        {
            try
            {
                int cityID = GetCityID(cityName); // Получение ID города по названию

                string query = "SELECT Улицы.номер\r\n     " +
                    "   FROM Дворы\r\n   " +
                    "     inner join Улицы on Дворы.улица  = Улицы.улица_id\r\n   " +
                    "     inner join Города on Дворы.город  = Города.город_id\r\n    " +
                    "    WHERE Города.город_id = @cityID and Улицы.название = @Street;";

                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Street", selectedStreet);
                        cmd.Parameters.AddWithValue("@CityID", cityID);


                        using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                        {
                            DataTable data = new DataTable();
                            adapter.Fill(data);

                            comboBoxNumber.Items.Clear(); // Очистка ComboBox'а с номерами

                            // Добавление номеров в ComboBox
                            foreach (DataRow row in data.Rows)
                            {
                                string number = row["номер"].ToString();
                                comboBoxNumber.Items.Add(number);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Обработчик события выбора улицы
        private void comboBoxStreet_SelectedIndexChanged(object sender, EventArgs e)
        {
 
        }

        private int GetCityID(string cityName)
        {
            int cityID = -1; // Инициализация cityID с неверным значением

            try
            {
                string query = "SELECT город_id FROM Города WHERE название = @CityName;";

                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CityName", cityName);
                        object result = cmd.ExecuteScalar(); // Получение результата запроса

                        if (result != null)
                        {
                            // Если результат не равен null, преобразуем его в int
                            cityID = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return cityID;
        }


      
        // Обработчик события выбора города
        private void comboBoxCity_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // Метод для загрузки городов из базы данных и заполнения ComboBox'а с городами
        private void LoadStreetsForCity(string cityName)
        {
            try
            {
                int cityID = GetCityID(cityName); // Получение ID города по названию

                if (cityID != -1)
                {
                    string query = "SELECT Улицы.название\r\n    " +
                          "    FROM Улицы\r\n    " +
                          "    inner join Дворы on Дворы.улица = Улицы.улица_id\r\n   " +
                          "     WHERE Дворы.город = @CityID;";

                    using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                    {
                        conn.Open();

                        using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@CityID", cityID);

                            using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                            {
                                DataTable data = new DataTable();
                                adapter.Fill(data);

                                comboBoxStreet.Items.Clear(); // Очистка ComboBox'а с улицами

                                // Добавление улиц в ComboBox
                                foreach (DataRow row in data.Rows)
                                {
                                    string street = row["название"].ToString();
                                    comboBoxStreet.Items.Add(street);
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Город не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void LoadCities()
        {
            try
            {
                string query = "SELECT название FROM Города;";

                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                        {
                            DataTable data = new DataTable();
                            adapter.Fill(data);

                            // Добавление городов в ComboBox
                            foreach (DataRow row in data.Rows)
                            {
                                string city = row["название"].ToString();
                                comboBoxCity.Items.Add(city);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SendFeedbackWindow_Load(object sender, EventArgs e)
        {
            GenerateCaptcha();
            LoadCities();
        }
        private string captchaCode;

        private void GenerateCaptcha()
        {
            string chars = "ABCDUERTYUIOPasdzxcvbnfghjklqwe123456789";

            Random random = new Random();

            captchaCode = new string(Enumerable.Repeat(chars, 4).Select(s => s[random.Next(s.Length)]).ToArray());

            captchaPictureBox.Image = DrawCaptcha(captchaCode);
        }

        private Bitmap DrawCaptcha(string code)
        {
            Bitmap bitmap = new Bitmap(captchaPictureBox.Width, captchaPictureBox.Height);
            Graphics graphics = Graphics.FromImage(bitmap);

            Font font = new Font("Arial", 12);
            Brush brush = new SolidBrush(Color.Black);

            graphics.DrawString(code, font, brush, 10, 10);

            return bitmap;
        }
        private void comboBoxCity_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            comboBoxStreet.Text="";
            comboBoxNumber.Text = "";
            // Получение выбранного города
            string selectedCity = comboBoxCity.SelectedItem.ToString();


            // Загрузка улиц для выбранного города и заполнение ComboBox'а с улицами
            LoadStreetsForCity(selectedCity);
            ; // Очистка ComboBox'а с улицами
        }

        private void comboBoxStreet_SelectedIndexChanged_1(object sender, EventArgs e)
        {
        
            // Получение выбранной улицы
            string selectedStreet = comboBoxStreet.SelectedItem.ToString();
            string selectedCity = comboBoxCity.SelectedItem.ToString();

            // Загрузка номеров для выбранной улицы и заполнение ComboBox'а с номерами
            LoadNumbersForStreet(selectedStreet, selectedCity);
        }
        private int dvorId;
        private int zhitelId;
        private void buttonSendFeedback_Click(object sender, EventArgs e)
        {
            string enteredCode = textBox1.Text;
            if (enteredCode == captchaCode)
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    string name = textBoxName.Text;
                    string surname = textBoxSurname.Text;
                    string contacts = textBoxContact.Text;
                    string feedback_datetime_string = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                    DateTime feedback_datetime = Convert.ToDateTime(feedback_datetime_string);
                    string feedbackText = textBox3.Text;

                    string query_add_zhitel = "INSERT INTO Жители (фамилия, имя, контактная_информация) VALUES (@surname, @name, @contacts) " +
                        "ON CONFLICT (контактная_информация) DO NOTHING;";

                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query_add_zhitel, conn))
                    {
                        try
                        {
                            cmd.Parameters.AddWithValue("@name", name);
                            cmd.Parameters.AddWithValue("@surname", surname);
                            cmd.Parameters.AddWithValue("@contacts", contacts);

                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    //получени двора по адресу
                    string query_dvor_by_names = $"SELECT двор_id\r\n    " +
                      $"    FROM Дворы\r\n    " +
                      $"    inner join Улицы on Дворы.улица  = Улицы.улица_id\r\n    " +
                      $"    inner join Города on Дворы.город  = Города.город_id\r\n    " +
                      $"    WHERE Города.название = @selectedCity and Улицы.название = @selectedStreet and Улицы.номер = @selectedNumber;";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query_dvor_by_names, conn))
                    {
                        try
                        {
                            string selectedCity = comboBoxCity.SelectedItem.ToString();
                            string selectedStreet = comboBoxStreet.SelectedItem.ToString();
                            string selectedNumber = comboBoxNumber.SelectedItem.ToString();

                            cmd.Parameters.AddWithValue("@selectedCity", selectedCity);
                            cmd.Parameters.AddWithValue("@selectedStreet", selectedStreet);
                            cmd.Parameters.AddWithValue("@selectedNumber", Convert.ToInt32(selectedNumber));

                            // Выполнение запроса и получение результата
                            object result = cmd.ExecuteScalar();

                            // Проверка на null и преобразование результата
                            if (result != null)
                            {
                                dvorId = Convert.ToInt32(result);

                                // Используйте переменную dvorId по вашему усмотрению
                            }
                            else
                            {
                                // Если результат равен null, обработайте эту ситуацию
                                MessageBox.Show("Двор не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    //получение айди жителя
                    string query_zhitel_id = $"SELECT житель_id\r\n    " +
                        $"    FROM Жители\r\n   " +
                        $"     WHERE фамилия = @surname AND имя = @name AND контактная_информация = @contacts;";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query_zhitel_id, conn))
                    {
                        try
                        {
                            cmd.Parameters.AddWithValue("@name", name);
                            cmd.Parameters.AddWithValue("@surname", surname);
                            cmd.Parameters.AddWithValue("@contacts", contacts);

                            // Выполнение запроса и получение результата
                            object result = cmd.ExecuteScalar();

                            // Проверка на null и преобразование результата
                            if (result != null)
                            {
                                zhitelId = Convert.ToInt32(result);

                                // Используйте переменную dvorId по вашему усмотрению
                            }
                            else
                            {
                                // Если результат равен null, обработайте эту ситуацию
                                MessageBox.Show("Житель не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    //запись обращения в таблицу
                    string query_add_feedback = $"INSERT INTO Жалобы_и_предложения (двор_id, текст, дата_и_время_подачи, житель_id)" +
                        $" VALUES (@dvor, @feedbackText, @feedback_datetime, @zhitel)";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query_add_feedback, conn))
                    {
                        try
                        {
                            cmd.Parameters.AddWithValue("@dvor", dvorId);
                            cmd.Parameters.AddWithValue("@feedbackText", feedbackText);
                            cmd.Parameters.AddWithValue("@feedback_datetime", feedback_datetime);
                            cmd.Parameters.AddWithValue("@zhitel", zhitelId);

                            cmd.ExecuteNonQuery();

                            MessageBox.Show($"Содержимое вашего обращения:\n\n"+
                                                 $"Фамилия: {textBoxSurname.Text}\n\n"+
                                                 $"Имя: {textBoxName.Text}\n\n" +
                                                 $"Адрес: {comboBoxCity.Text}, {comboBoxStreet.Text}, {comboBoxNumber.Text}\n\n" +
                                                 $"Контактная информация: {textBoxContact.Text}\n\n"+
                                                 $"Текст обращения: {textBox3.Text}\n\n" , "Ваше обращение отправлено", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            textBoxSurname.Text = "";
                            textBoxName.Text = "";
                            textBoxContact.Text = "";
                            comboBoxCity.Text = "";
                            comboBoxStreet.Text = "";
                            comboBoxNumber.Text = "";
                            textBox3.Text = "";
                            textBox1.Text = "";
                            GenerateCaptcha();

                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show($"Произошла ошибка!\r\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //\r\nдвор= {dvorId},\r\nтекст = {feedbackText}\r\n,время={feedback_datetime}\r\n,житель={zhitelId}, 
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Капча не пройдена. Попробуйте еще раз");
                GenerateCaptcha();

            }
            }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            Form1 Form1 = new Form1();
            this.Hide();
            Form1.Show();
        }
    }
}
