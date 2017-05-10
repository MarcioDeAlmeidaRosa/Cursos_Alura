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
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //int numeroDaConta;
            //numeroDaConta = 1;

            //double saldoDaConta = 100.0;
            //double valor = 110.0;
            //bool podeSacar = (valor <= saldoDaConta) && (valor >= 0);
            //if (podeSacar)
            //{
            //    double saldoApoSaque = saldoDaConta - valor;
            //    MessageBox.Show("O saldo atual é " + saldoApoSaque);
            //}
            //else
            //{
            //    MessageBox.Show("O saldo insuficiente");
            //}



            //int idadeJoao = 10;
            //int idadeMaria = 25;
            //int idadeJose = 30;


            //MessageBox.Show("A média das idades é: " + (idadeJoao + idadeMaria + idadeJose) / 3);

            Conta umaConta = new Conta();
            umaConta.Numero = 1;
            umaConta.Titular = new Cliente();
            umaConta.Titular.Nome = "Joaquim José";
            umaConta.Titular.Idade = 12;
            umaConta.Titular.RgTitular = "45.639.789-9";
            umaConta.Titular.EnderecoTitular = "Avenida Paulista";
            //umaConta.Saldo = 2000.0;
            umaConta.Titular.Cpf = "263.963.854-56";
            umaConta.Agencia = 1;
            

            Conta outraConta = new Conta();
            outraConta.Numero = 2;
            outraConta.Titular = new Cliente();
            outraConta.Titular.Nome = "Silva Xavier";
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
        
    }
}