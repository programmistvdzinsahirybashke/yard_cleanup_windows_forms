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

namespace Ychebka
{
    public partial class ToolsPanel : Form
    {
        public ToolsPanel()
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

        private void ToolsPanel_Load(object sender, EventArgs e)
        {

            using (NpgsqlConnection conn = new NpgsqlConnection(connection))
            {
                conn.Open();

                string query = @"SELECT 
                ii.двор_id AS Двор_ID,
                g.название AS Город,
                u.название AS Улица,
                u.номер AS Номер_улицы,
                ti.название AS Тип_инструмента, 
                ti.описание AS Описание_типа_инструмента, 
                ii.общее_количество AS Общее_количество, 
                ii.доступное_количество AS Доступное_количество, 
                ii.недоступное_количество AS Недоступное_количество,
                s.фамилия AS Фамилия_сотрудника,
                s.имя AS Имя_сотрудника,
                si.сотрудник_id AS Сотрудник_ID,
                si.выданное_количество AS Выданное_количество
                FROM 
                    Инструменты_и_дворы ii
                JOIN 
                    Типы_инструментов ti ON ii.инструмент_id = ti.тип_инструмента_id
                LEFT JOIN 
                    Сотрудники_и_инструменты si ON ii.инструмент_и_двор_id = si.инструмент_и_двор_id
                LEFT JOIN 
                    Сотрудники s ON si.сотрудник_id = s.сотрудник_id
                LEFT JOIN 
                    Дворы d ON ii.двор_id = d.двор_id
                LEFT JOIN 
                    Улицы u ON d.улица = u.улица_id
                LEFT JOIN 
                    Города g ON d.город = g.город_id;
                ";

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, conn);
                DataTable data = new DataTable();
                adapter.Fill(data);

                dataGridViewTools.DataSource = data;
            }

        }
    }
}
