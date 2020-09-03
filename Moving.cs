using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MaterialERP
{
    public partial class Moving : Form
    {
        public enum Types {Entry, Sale};

        public Types Type { get; set; }
        public bool Accepted;

        private List<Material> _materials;
        private int _partno;
        private double _weight;
        private double _volume;
        private int _material;

        public Moving()
        {
            Type = Types.Entry;
            Accepted = false;
            _materials = null;
            _partno = 0;
            _weight = 0;
            _volume = 0;
            _material = 0;
            InitializeComponent();
        }

        private void Moving_Shown(object sender, EventArgs e)
        {
            if (Type == Types.Entry)
            {
                // Поступление материала
                groupBox1.Text = "Поступление материала:";

            }
            else
            {
                // Расход материала
                groupBox1.Text = "Расход материала";

            }

            // Заполняем список материалов
            if (_materials != null)
            {
                for (int i = 0; i < _materials.Count; i++)
                {
                    comboBox1.Items.Add(_materials[i].ToString());
                }
            }
        }

        public void AddMaterials(Material material)
        {
            if (material != null)
            {
                _materials.Add(material);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Нажата кнопка Добавить
            _partno = Int32.Parse(textBox1.Text);
            _weight = Double.Parse(textBox2.Text);
            _volume = Double.Parse(textBox3.Text);
            for (int i =0; i<_materials.Count; i++)
            {

            }

            Accepted = true;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Нажата кнопка Отмена
            _materials = null;
            _partno = 0;
            _material = 0;
            _volume = 0;
            _weight = 0;

            Accepted = false;
            Close();
        }
    }
}
