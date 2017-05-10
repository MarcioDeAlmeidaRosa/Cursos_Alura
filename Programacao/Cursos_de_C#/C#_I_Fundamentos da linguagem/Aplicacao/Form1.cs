using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aplicacao
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double valorInvestido = 2000;
            int mes = 1;
            for (mes = 1; mes <= 12; mes++)
            {
                valorInvestido *= 1.01;
            }
            MessageBox.Show("Valor " + valorInvestido);

            valorInvestido = 2000;
            mes = 1;
            while (true)
            {
                valorInvestido *= 1.01;

                if (mes == 12)
                    break;
                mes++;
            }

            MessageBox.Show("Valor investido agora é " + valorInvestido);

            int total = 2;
            for (int i = 0; i < 5; i += 1)
            {
                total = total * 2;
            }
            MessageBox.Show("O total é: " + total);
        }
    }
}