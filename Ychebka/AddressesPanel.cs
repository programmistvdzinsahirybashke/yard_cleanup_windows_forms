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
    public partial class AddressesPanel : Form
    {
        public AddressesPanel()
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

        private void AddressesPanel_Load(object sender, EventArgs e)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connection))
            {
                conn.Open();

                string query = $"SELECT Города.название, Улицы.название, Улицы.номер\r\n                FROM Сотрудники_и_дворы\r\n                join Дворы on Дворы.двор_id  = Сотрудники_и_дворы.двор \r\n                join Города on Дворы.город  = Города.город_id \r\n                join Улицы on Дворы.улица  = Улицы.улица_id  ";
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, conn);
                DataTable data = new DataTable();
                adapter.Fill(data);

                dataGridViewAddresses.DataSource = data;
            }
        }
    }
}
