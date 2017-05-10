using Caelum.CaixaEletronico.Contas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caelum.CaixaEletronico
{
    public partial class CadastroConta : Form
    {
        private Form1 formularioPrincipal;
        public CadastroConta(Form1 formularioPrincipal)
        {
            this.formularioPrincipal = formularioPrincipal;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((string.IsNullOrWhiteSpace(txtNumero.Text)) || (string.IsNullOrWhiteSpace(txtNome.Text)) || (comboTipoConta.SelectedIndex < 0))
            {
                MessageBox.Show("Preencha os campos obrigatórios");
                return;
            }

            Conta novaConta = null;
            if (comboTipoConta.SelectedIndex == 0)
                novaConta = new ContaCorrente();
            else if (comboTipoConta.SelectedIndex == 1)
                novaConta = new ContaPoupanca();
            else
                novaConta = new ContaInvestimento();

            novaConta.Numero = Convert.ToInt32(txtNumero.Text);
            novaConta.Titular = new Usuarios.Cliente(txtNome.Text);

            formularioPrincipal.AdicionarConta(novaConta);

            this.Close();
        }
    }
}
