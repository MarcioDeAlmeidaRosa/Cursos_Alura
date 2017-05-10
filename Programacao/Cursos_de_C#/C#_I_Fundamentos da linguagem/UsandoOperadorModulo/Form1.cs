using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UsandoOperadorModulo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((15 % 3) == 0)
                MessageBox.Show("15 é divisível por 3");
            else
                MessageBox.Show("15 é não divisível por 3");

            if ((15 % 4) == 0)
                MessageBox.Show("15 é divisível por 4");
            else
                MessageBox.Show("15 é não divisível por 4");

            string divisiveisPor3 = "";
            string divisiveisPor4 = "";
            for (int i = 1; i <= 30; i++)
            {
                if ((i % 3) == 0)
                    divisiveisPor3 += i + " ";
                else if ((i % 4) == 0)
                    divisiveisPor4 += i + " ";

            }
            MessageBox.Show("Números divisíveis por 3 = " + divisiveisPor3);
            MessageBox.Show("Números divisíveis por 4 = " + divisiveisPor4);
        }
    }
}