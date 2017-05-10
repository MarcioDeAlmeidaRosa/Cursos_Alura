using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TabelaMutiplicadores
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string linhaImpressao = "";
            int repeticaoInterna = 0;
            for (int i = 1; i <= 10; i++)
            {
                linhaImpressao += i + " ";

                for (int y = 2; y <= repeticaoInterna+1; y++)
                {
                    linhaImpressao += (i * y) + " ";
                }
                repeticaoInterna += 1;

                linhaImpressao += "\n";
            }
            MessageBox.Show(linhaImpressao);
        }
    }
}