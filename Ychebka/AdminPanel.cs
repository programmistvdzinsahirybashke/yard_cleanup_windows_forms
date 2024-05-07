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
    public partial class AdminPanel : Form
    {
        public AdminPanel()
        {
            InitializeComponent();
        }
        private string connection = "Server=localhost;port=5432;username=postgres;password=123;database=cleanFINAL";
        private int UserID;


        private void buttonLogout_Click(object sender, EventArgs e)
        {
            Form1 Form1 = new Form1();
            this.Hide();
            Form1.Show();
        }

        private void buttonOpenEmployeesPanel_Click(object sender, EventArgs e)
        {
            EmployeesPanel employeesPanel = new EmployeesPanel();
            this.Hide();
            employeesPanel.Show();
        }

        private void buttonOpenTasksPanel_Click(object sender, EventArgs e)
        {
            TasksPanel tasksPanel = new TasksPanel();
            this.Hide();
            tasksPanel.Show();
        }

        private void buttonOpenToolsPanel_Click(object sender, EventArgs e)
        {
            ToolsPanel toolsPanel = new ToolsPanel();
            this.Hide();
            toolsPanel.Show();
        }

        private void buttonOpenAddressesPanel_Click(object sender, EventArgs e)
        {
            AddressesPanel addressesPanel = new AddressesPanel();
            this.Hide();
            addressesPanel.Show();
        }

        private void buttonOpenFeedbackPanel_Click(object sender, EventArgs e)
        {
            FeedbackPanel feedbackPanel = new FeedbackPanel();
            this.Hide();
            feedbackPanel.Show();
        }
        private int GetUserID()
        {
            // Здесь должен быть ваш код для получения ID пользователя
            // Например, если ID хранится в поле класса или получается из другого источника данных, то можно его вернуть здесь
            return UserData.UserID; // Пример
        }
        private void AdminPanel_Load(object sender, EventArgs e)
        {
            UserID = GetUserID();

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
    }
}
