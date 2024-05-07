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

                string query = $"SELECT * FROM Сотрудники WHERE логин=@login";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@login", login);

                    NpgsqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();

                        int UserID = Convert.ToInt32(reader["сотрудник_id"].ToString());
                        string StoredPassword = reader["пароль"].ToString();
                        int PositionID = Convert.ToInt32(reader["должность"].ToString());

                        // Сравнение пароля
                        if (password == StoredPassword)
                        {
                            // Например, при аутентификации пользователя
                            UserData.UserID = UserID;

                            if (PositionID == 21)
                            {
                                // Открывать окно администратора
                                AdminPanel AdminPanel = new AdminPanel();
                                this.Hide();
                                AdminPanel.Show();
                            }
                            else
                            {
                                // Открывать окно пользователя
                                UserMainWindow userWindow = new UserMainWindow();
                                this.Hide();
                                userWindow.Show();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Неверный логин или пароль!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль!");
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

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
          
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void buttonLogin_KeyDown(object sender, KeyEventArgs e)
        {
          
        }
    }
}
