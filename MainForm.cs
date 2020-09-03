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

namespace MaterialERP
{
    public partial class MainForm : Form
    {
        private AddMaterial addMaterial;
        private Moving moving;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            updateDataGrid();
            dataGridView2.Dock = DockStyle.Fill;
            dataGridView2.AutoGenerateColumns = true;
            dataGridView2.DataSource = bindingSource1;
            dataGridView2.Enabled = true;
            dataGridView2.ReadOnly = true;
            dataGridView2.ColumnHeadersVisible = true;
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[1].HeaderText = "Наименование";

            addMaterial = new AddMaterial();
            moving = new Moving();
        }

        private void updateDataGrid()
        {
            string query = "select * from pqca.materials order by material;";
            npgsqlCommand1 = new NpgsqlCommand(query, npgsqlConnection1)
            {
                CommandTimeout = 20
            };

            npgsqlDataAdapter1 = new NpgsqlDataAdapter(npgsqlCommand1);
            DataSet dataSet = new DataSet();
            npgsqlDataAdapter1.Fill(dataSet, "materials");
            bindingSource1.DataSource = dataSet.Tables[0];
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            // Выбор пункта меню Редактировать
            string id = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            string material = dataGridView2.CurrentRow.Cells[1].Value.ToString();

            addMaterial.SetMode(MaterialERP.AddMaterial.Modes.Edit);
            addMaterial.SetMaterialName(material);
            addMaterial.ShowDialog();
            
            if (addMaterial.Accepted)
            {
                // Нажали ОК
                string mat = addMaterial.GetMaterialName();
                
                if (mat != material)
                {
                    // Изменили название или номер партии
                    string query = string.Format(@"update pqca.materials set material='{0}' where id={1};", mat, id);
                    npgsqlCommand1 = new NpgsqlCommand(query, npgsqlConnection1);
                    
                    try
                    {
                        npgsqlConnection1.Open();
                        npgsqlCommand1.ExecuteNonQuery();
                        npgsqlConnection1.Close();
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message, "Ошибка базы данных", MessageBoxButtons.OK);
                    }

                    updateDataGrid();
                }
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            // Выбор пункта меню Удалить

        }


        private void AddMaterial(string material)
        {
            if (material != "")
            {
                string query = string.Format(@"insert into pqca.materials (material) values ('{0}')", material);
                npgsqlCommand1 = new NpgsqlCommand(query, npgsqlConnection1);

                try
                {
                    npgsqlConnection1.Open();
                    npgsqlCommand1.ExecuteNonQuery();
                    npgsqlConnection1.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка базы данных", MessageBoxButtons.OK);
                }


                updateDataGrid();
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            // Выбор пункта меню Добавить -> Материал
            addMaterial.SetMode(MaterialERP.AddMaterial.Modes.Add);
            addMaterial.SetMaterialName("");
            addMaterial.ShowDialog();

            string material = addMaterial.GetMaterialName();
            AddMaterial(material);

        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            // Выбор пункта меню Движение -> Приход
            moving.Type = Moving.Types.Entry;
            Dictionary<string, int> materials = new Dictionary<string, int>();


        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            // Выбор пункта меню Движение -> Расход
            moving.Type = Moving.Types.Sale;
            Dictionary<string, int> materials = new Dictionary<string, int>();


        }
    }
}