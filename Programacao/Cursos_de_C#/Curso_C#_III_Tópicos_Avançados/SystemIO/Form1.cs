using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystemIO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (Stream saida = File.Open(txtNomeArquivo.Text, FileMode.Create))
            using(StreamWriter escrita = new StreamWriter(saida))
            {
                
                escrita.Write(txtConteudoArquivo.Text);
                txtConteudoArquivo.Text = string.Empty;
                txtNomeArquivo.Text = string.Empty;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (File.Exists(txtNomeArquivo.Text))
            {
                txtConteudoArquivo.Text = string.Empty;
                using (Stream entrada = File.Open(txtNomeArquivo.Text, FileMode.Open))
                using(StreamReader leitor = new StreamReader(entrada))
                {
                    

                    string linhaArquivo = leitor.ReadToEnd(); ;
                    txtConteudoArquivo.Text = linhaArquivo;
                    //string linhaArquivo = leitor.ReadLine();
                    //while (linhaArquivo != null)
                    //{
                    //    txtConteudoArquivo.Text += linhaArquivo;
                    //    linhaArquivo = leitor.ReadLine();
                    //}
                }
            }
            else
            {
                MessageBox.Show("Arquivo informado não existe!");
            }
            
        }
    }
}
