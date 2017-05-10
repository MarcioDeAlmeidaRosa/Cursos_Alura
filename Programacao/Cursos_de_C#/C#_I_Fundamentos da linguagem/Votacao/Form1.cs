using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Votacao
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int idade = 10;
            bool eHBrasileiro = false;
            if ((idade>=16) && (eHBrasileiro))
            {
                MessageBox.Show("Você pode votar");
            }
            else
            {
                MessageBox.Show("Você não pode votar");
            }
        }
    }
}
