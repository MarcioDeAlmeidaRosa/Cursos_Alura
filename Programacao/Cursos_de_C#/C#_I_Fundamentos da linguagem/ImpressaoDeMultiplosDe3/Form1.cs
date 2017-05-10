using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImpressaoDeMultiplosDe3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int multiplos3 = 0;
            for (int i = 1; i <= 100; i++)
            {
                if ((i % 3) != 0)
                    multiplos3 += i;
            }
            MessageBox.Show("Total é = " + multiplos3);
        }
    }
}
