using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CaixaEletronico
{
    public class Conta
    {
        public int Numero { get; set; }
        public double Saldo{get; protected set;}
        public int Agencia { get; set; }
        public Cliente Titular { get; set; }
        public int TipoConta { get; set; }


        public virtual bool Saca(double valorASerSacado)
        {
            if (this.Saldo >= valorASerSacado && valorASerSacado >= 0)
            {
                if (valorASerSacado > 200)
                    if (!this.Titular.EhMaiorDeIdade)
                        return false;
                this.Saldo -= valorASerSacado;
                return true;
            }
            return false;
        }

        public void Deposita(double valorASerDepositado)
        {
            if (valorASerDepositado > 0)
            {
                this.Saldo += valorASerDepositado;
            }

        }

        public bool Transferencia(double valor, Conta destino)
        {
            if (this.Saca(valor))
            {
                destino.Deposita(valor);
                return true;
            }
            return false;
        }

        public double CalculaRendimentoAnual()
        {
            double saldoNaqueleMes = this.Saldo;

            for (int i = 0; i < 12; i++)
            {
                saldoNaqueleMes = saldoNaqueleMes * 1.007;
            }

            double rendimento = saldoNaqueleMes - this.Saldo;

            return rendimento;
        }

        public virtual void Atualiza(double taxa)
        {
            this.Saldo += this.Saldo * taxa;
        }
    }
}