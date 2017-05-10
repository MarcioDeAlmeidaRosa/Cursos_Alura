using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImpressaoDeSomatoria
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int somatoria=0 ;
            for (int i = 1; i <= 1000; i++)
            {
                somatoria += i;
            }

            MessageBox.Show("A soma dos números de 1 até 1000 é: " + somatoria);
        }
    }
}
