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
        private Banco banco;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Conta umaConta = new ContaCorrente();
            umaConta.Numero = 1;
            umaConta.Titular = new Cliente("Joaquim José");
            //umaConta.Titular.Nome = "Joaquim José";
            umaConta.Titular.Idade = 12;
            umaConta.Titular.RgTitular = "45.639.789-9";
            umaConta.Titular.EnderecoTitular = "Avenida Paulista";
            //umaConta.Saldo = 2000.0;
            umaConta.Titular.Cpf = "263.963.854-56";
            umaConta.Agencia = 1;


            Conta outraConta = new ContaPoupanca();
            outraConta.Numero = 2;
            outraConta.Titular = new Cliente("Silva Xavier");
            //outraConta.Titular.Nome = "Silva Xavier";
            outraConta.Titular.Idade = 21;
            outraConta.Titular.RgTitular = "45.852.741-6";
            outraConta.Titular.EnderecoTitular = "Avenida Vergueiro";
            //outraConta.Saldo = 1500.0;
            outraConta.Titular.Cpf = "236.965.789-56";
            outraConta.Agencia = 2;

            try
            {
                umaConta.Transferencia(300, outraConta);
                MessageBox.Show("Transferência realizada com sucesso.");
            }
            catch (ValorNegativoException ex)
            {
                MessageBox.Show("Valor para sacar é negativo");
            }
            catch (SaldoInsuficienteException ex)
            {
                MessageBox.Show("Valor insuficiente na conta");
            }
            catch (MenorIdadeException ex)
            {
                MessageBox.Show("Titular não pode ser menor de idade");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            MessageBox.Show(umaConta.Titular.Nome + " - " + umaConta.Titular.Cpf + " - " + umaConta.Agencia + " - " + umaConta.Saldo);
            MessageBox.Show(outraConta.Titular.Nome + " - " + outraConta.Titular.Cpf + " - " + outraConta.Agencia + " - " + outraConta.Saldo);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            conta = new ContaCorrente();
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
            MostraConta(conta);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                conta.Saca(Convert.ToDouble(textoValor.Text));
            }
            catch (ValorNegativoException ex)
            {
                MessageBox.Show("Valor para sacar é negativo");
            }
            catch (SaldoInsuficienteException ex)
            {
                MessageBox.Show("Valor insuficiente na conta");
            }
            catch (MenorIdadeException ex)
            {
                MessageBox.Show("Titular não pode ser menor de idade");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MostraConta(conta);
        }

        private void MostraConta(Conta contaAtual)
        {
            textoTitular.Text = contaAtual.Titular.Nome;
            textoSaldo.Text = Convert.ToString(contaAtual.Saldo);
            textoNumero.Text = Convert.ToString(contaAtual.Numero);
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
            Conta c1 = new ContaCorrente(); ;
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
            Conta c = new ContaPoupanca();
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

        private void button7_Click(object sender, EventArgs e)
        {
            Conta[] contas = new Conta[2];
            contas[0] = new ContaCorrente();
            contas[0].Deposita(100);
            contas[1] = new ContaPoupanca();
            contas[1].Deposita(1000);

            foreach (Conta conta in contas)
            {
                MessageBox.Show("O saldo da conta e: " + conta.Saldo);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            banco = new Banco();
            banco.Adicionar(
                new ContaCorrente
                {
                    Agencia = 1,
                    Numero = 123,
                    TipoConta = 0,
                    Titular = new Cliente("Marcio")
                    {
                        Cpf = "236.852.963-45",
                        EnderecoTitular = "Avenida Paulista",
                        Idade = 25,
                        RgTitular = "56.963.789-2"
                    }

                }
                );

            banco.Adicionar(
                new ContaPoupanca
                {
                    Agencia = 2,
                    Numero = 6598,
                    TipoConta = 1,
                    Titular = new Cliente("Ronaldo")
                    {
                        Cpf = "256.845.769-69",
                        EnderecoTitular = "Avenida Vergueiro",
                        Idade = 63,
                        RgTitular = "74.321.796-8"
                    }
                }
                );

            banco.Adicionar(
               new ContaInvestimento
               {
                   Agencia = 3,
                   Numero = 56322,
                   TipoConta = 1,
                   Titular = new Cliente("Marcelo")
                   {
                       Cpf = "562.741.695-52",
                       EnderecoTitular = "Avenida Brigadeiro",
                       Idade = 55,
                       RgTitular = "42.852.369-9"
                   }
               }
               );

            for (int i = 0; i < banco.Contas.Length; i++)
            {
                banco.Contas[i].Deposita((i + 1) * 100);
            }
            MessageBox.Show("Total de contas incluídas: " + banco.Contas.Length);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            comboCotas.Items.Clear();
            button8_Click(sender, e);
            foreach (Conta conta in banco.Contas)
            {
                comboCotas.Items.Add(conta.Titular.Nome);
                destinoDaTransferencia.Items.Add(conta.Titular.Nome);
            }
        }

        private void comboCotas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboCotas.SelectedIndex >= 0)
            {
                conta = banco.Contas[comboCotas.SelectedIndex];
                MostraConta(conta);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if ((comboCotas.SelectedIndex >= 0) && (destinoDaTransferencia.SelectedIndex >= 0) && (!string.IsNullOrWhiteSpace(textoValorTransderencia.Text)))
            {
                try
                {
                    banco.Contas[comboCotas.SelectedIndex].Transferencia(Convert.ToDouble(textoValorTransderencia.Text), banco.Contas[destinoDaTransferencia.SelectedIndex]);
                }
                catch (ValorNegativoException ex)
                {
                    MessageBox.Show("Valor para sacar é negativo");
                }
                catch (SaldoInsuficienteException ex)
                {
                    MessageBox.Show("Valor insuficiente na conta");
                }
                catch (MenorIdadeException ex)
                {
                    MessageBox.Show("Titular não pode ser menor de idade");
                }
            }
            else
            {
                MessageBox.Show("Selecione conta origem e conta destino para a transferência e valor da transferência");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            button8_Click(sender, e);

            GerenciadorDeImposto tt = new GerenciadorDeImposto();
            foreach (Conta conta in banco.Contas)
            {
                if (conta is ITributavel)
                    tt.Acumula((ITributavel)conta);
            }
            tt.Acumula(new SeguroDeVida());
            MessageBox.Show("Total de tributos calculado: " + tt.Total);
        }
    }
}