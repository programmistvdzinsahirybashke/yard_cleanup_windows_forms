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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string connection = "Server=localhost;port=5432;username=postgres;password=123;database=cleanFINAL";

        private void button1_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connection))
            {
                string login = textBoxLogin.Text;
                string password = textBoxPassword.Text;


                string query = $"Select * from Сотрудники where логин=@login";



                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {

                    conn.Open();
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@password", password);

                    NpgsqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();

                        int UserID = Convert.ToInt32(reader["сотрудник_id"].ToString());
                        string StoredPassword = reader["пароль"].ToString();

                        if (password == StoredPassword)
                        {
                            // Например, при аутентификации пользователя
                            UserData.UserID = UserID;

                            MessageBox.Show("Успешная авторизация");
                            UserMainWindow EmployeesPanel = new UserMainWindow();
                            this.Hide();
                            EmployeesPanel.Show();
                        }
                        else
                        {
                            MessageBox.Show("Неверный пароль!");

                        }
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин!");
                    }

                }
            }

        }

            private void button2_Click(object sender, EventArgs e)
        {
            SendFeedbackWindow SendFeedbackWindow = new SendFeedbackWindow();
            this.Hide();
            SendFeedbackWindow.Show();
        }

        private void textBoxLogin_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
