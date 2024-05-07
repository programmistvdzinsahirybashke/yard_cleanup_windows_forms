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
    public partial class FeedbackPanel : Form
    {
        public FeedbackPanel()
        {
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            // Открывать окно администратора
            AdminPanel AdminPanel = new AdminPanel();
            this.Hide();
            AdminPanel.Show();
        }
        private string connection = "Server=localhost;port=5432;username=postgres;password=123;database=cleanFINAL";


        private void FeedbackPanel_Load(object sender, EventArgs e)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connection))
            {
                conn.Open();

                string query = "Select * from Жалобы_и_предложения";
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, conn);
                DataTable data = new DataTable();
                adapter.Fill(data);

                dataGridViewFeedback.DataSource = data;
            }
        }
    }
}
