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
            umaConta.numero = 1;
            umaConta.titular = new Cliente();
            umaConta.titular.nome = "Joaquim José";
            umaConta.titular.idade = 12;
            umaConta.titular.rgTitular = "45.639.789-9";
            umaConta.titular.enderecoTitular = "Avenida Paulista";
            umaConta.saldo = 2000.0;
            umaConta.titular.cpf = "263.963.854-56";
            umaConta.agencia = 1;
            

            Conta outraConta = new Conta();
            outraConta.numero = 2;
            outraConta.titular = new Cliente();
            outraConta.titular.nome = "Silva Xavier";
            outraConta.titular.idade =21;
            outraConta.titular.rgTitular = "45.852.741-6";
            outraConta.titular.enderecoTitular = "Avenida Vergueiro";
            outraConta.saldo = 1500.0;
            outraConta.titular.cpf = "236.965.789-56";
            outraConta.agencia = 2;

            var tranferiu = umaConta.Transferencia(300, outraConta);
            MessageBox.Show("Transferência realizada com sucesso? " + tranferiu);

            MessageBox.Show(umaConta.titular.nome + " - " + umaConta.titular.cpf + " - " + umaConta.agencia + " - " + umaConta.saldo);
            MessageBox.Show(outraConta.titular.nome + " - " + outraConta.titular.cpf + " - " + outraConta.agencia + " - " + outraConta.saldo);

        }
        
    }
}