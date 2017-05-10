using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotaFiscal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double valorDaNotaFiscal = 10;
            double percentualImposto = 0;

            if (valorDaNotaFiscal < 100)
                percentualImposto = 0.02;
            else if (valorDaNotaFiscal < 3000)
                percentualImposto = 0.025;
            else if (valorDaNotaFiscal < 7000)
                percentualImposto = 0.028;
            else
                percentualImposto = 0.03;

            MessageBox.Show("Percentual do imposto é " + percentualImposto);
        }
    }
}
