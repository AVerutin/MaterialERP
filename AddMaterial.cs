using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MaterialERP
{
    public partial class AddMaterial : Form
    {
        public enum Modes {Add, Edit, Remove};
        private string _material;
        private Modes _mode;

        public bool Accepted { get; private set; }

        public AddMaterial()
        {
            Accepted = false;
            _material = "";
            _mode = Modes.Add;
            InitializeComponent();
        }

        public void SetMaterialName(string material)
        {
            _material = material;
            textBox1.Text = _material;
        }

        public void SetMode(Modes mode)
        {
            _mode = mode;
        }

        public string GetMaterialName()
        {
            return _material;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Нажата кнопка Добавить
            _material = textBox1.Text;
            Accepted = true;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Нажата кнопка Отмена
            _material = "";
            Accepted = false;
            Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Form2_Shown(object sender, EventArgs e)
        {
            if (_mode == Modes.Add)
            {
                textBox1.Text = _material;
                button1.Text = "Добавить";
            }
            else if (_mode == Modes.Edit)
            {
                // Редактирование записи
                textBox1.Text = _material;
                button1.Text = "Редактировать";
            }

            textBox1.Focus();
        }
    }
}
