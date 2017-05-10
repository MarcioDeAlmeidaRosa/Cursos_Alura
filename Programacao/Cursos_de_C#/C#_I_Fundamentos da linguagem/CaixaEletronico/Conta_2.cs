using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CaixaEletronico
{
    public class Conta
    {
        public int numero;
        public double saldo;
        public int agencia;
        public Cliente titular;


        public bool Saca(double valorASerSacado)
        {
            if (this.saldo >= valorASerSacado && valorASerSacado >= 0)
            {
                if (valorASerSacado > 200)
                    if (!this.titular.EhMaiorDeIdade())
                        return false;

                this.saldo -= valorASerSacado;
                return true;
            }
            return false;
        }

        public void Deposita(double valorASerDepositado)
        {
            if (valorASerDepositado > 0)
            {
                this.saldo += valorASerDepositado;
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
            double saldoNaqueleMes = this.saldo;

            for (int i = 0; i < 12; i++)
            {
                saldoNaqueleMes = saldoNaqueleMes * 1.007;
            }

            double rendimento = saldoNaqueleMes - this.saldo;

            return rendimento;
        }
    }
}