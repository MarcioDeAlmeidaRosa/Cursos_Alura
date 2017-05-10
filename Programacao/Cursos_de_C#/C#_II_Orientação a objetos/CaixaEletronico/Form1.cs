using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaixaEletronico
{
    public partial class Form1 : Form
    {
        private Conta conta;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Conta umaConta = new Conta();
            umaConta.Numero = 1;
            umaConta.Titular = new Cliente("Joaquim José");
            //umaConta.Titular.Nome = "Joaquim José";
            umaConta.Titular.Idade = 12;
            umaConta.Titular.RgTitular = "45.639.789-9";
            umaConta.Titular.EnderecoTitular = "Avenida Paulista";
            //umaConta.Saldo = 2000.0;
            umaConta.Titular.Cpf = "263.963.854-56";
            umaConta.Agencia = 1;
            

            Conta outraConta = new Conta();
            outraConta.Numero = 2;
            outraConta.Titular = new Cliente("Silva Xavier");
            //outraConta.Titular.Nome = "Silva Xavier";
            outraConta.Titular.Idade =21;
            outraConta.Titular.RgTitular = "45.852.741-6";
            outraConta.Titular.EnderecoTitular = "Avenida Vergueiro";
            //outraConta.Saldo = 1500.0;
            outraConta.Titular.Cpf = "236.965.789-56";
            outraConta.Agencia = 2;

            var tranferiu = umaConta.Transferencia(300, outraConta);
            MessageBox.Show("Transferência realizada com sucesso? " + tranferiu);

            MessageBox.Show(umaConta.Titular.Nome + " - " + umaConta.Titular.Cpf + " - " + umaConta.Agencia + " - " + umaConta.Saldo);
            MessageBox.Show(outraConta.Titular.Nome + " - " + outraConta.Titular.Cpf + " - " + outraConta.Agencia + " - " + outraConta.Saldo);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            conta = new Conta();
            conta.Titular = new Cliente("Marcio de Almeida Rosa");
            conta.Deposita(250.0);
            conta.Numero = 2369;
            textoTitular.Text = conta.Titular.Nome;
            textoSaldo.Text = Convert.ToString(conta.Saldo);
            textoNumero.Text = Convert.ToString(conta.Numero);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conta.Deposita(Convert.ToDouble(textoValor.Text));
            MostraConta();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conta.Saca(Convert.ToDouble(textoValor.Text));
            MostraConta();
        }

        private void MostraConta()
        {
            textoTitular.Text = conta.Titular.Nome;
            textoSaldo.Text = Convert.ToString(conta.Saldo);
            textoNumero.Text = Convert.ToString(conta.Numero);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ContaPoupanca umaConta = new ContaPoupanca();
            umaConta.Numero = 1;
            umaConta.Titular = new Cliente("Joaquim José");
            //umaConta.Titular.Nome = "Joaquim José";
            umaConta.Titular.Idade = 12;
            umaConta.Titular.RgTitular = "45.639.789-9";
            umaConta.Titular.EnderecoTitular = "Avenida Paulista";
            //umaConta.Saldo = 2000.0;
            umaConta.Titular.Cpf = "263.963.854-56";
            umaConta.Agencia = 1;
            MessageBox.Show(umaConta.Titular.Nome + " - " + umaConta.Titular.Cpf + " - " + umaConta.Agencia + " - " + umaConta.Saldo);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Conta c1 = new Conta(); ;
            c1.Deposita(10);

            ContaPoupanca c2 = new ContaPoupanca();
            c2.Deposita(100);

            TotalizadorDeContas t = new TotalizadorDeContas();
            t.Adiciona(c1); ;
            t.Adiciona(c2);

            MessageBox.Show("Total de conta: " + t.Saldo);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Conta c = new Conta();
            ContaCorrente cc = new ContaCorrente();
            ContaPoupanca cp = new ContaPoupanca();

            c.Deposita(1000.0);
            cc.Deposita(1000.0);
            cp.Deposita(1000.0);

            AtualizadorDeContas atualizador = new AtualizadorDeContas(0.01);
            atualizador.Roda(c);
            atualizador.Roda(cc);
            atualizador.Roda(cp);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("conta  = " + c.Saldo);
            sb.AppendLine("conta corrente = " + cc.Saldo);
            sb.AppendLine("conta poupança = " + cp.Saldo);
            
            //conta  = 1010

            //conta corrente = 1030

            //conta poupança = 1020

            MessageBox.Show(sb.ToString());
        }
    }
}