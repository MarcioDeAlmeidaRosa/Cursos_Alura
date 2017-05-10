using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaixaEletronico
{
    public class ContaInvestimento : Conta, ITributavel
    {
        public override bool Saca(double valorASerSacado)
        {
            if (this.Saldo >= valorASerSacado && valorASerSacado >= 0)
            {
                this.Saldo -= valorASerSacado;
                return true;
            }
            return false;
        }

        public double CalculaTributo()
        {
            return this.Saldo * 0.03;
        }
    }
}