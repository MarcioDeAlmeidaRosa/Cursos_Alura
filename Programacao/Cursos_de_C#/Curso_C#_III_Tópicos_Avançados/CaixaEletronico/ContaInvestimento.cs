using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caelum.CaixaEletronico.Contas
{
    public class ContaInvestimento : Conta, ITributavel
    {
        public override void Saca(double valorASerSacado)
        {
            if (valorASerSacado < 0)
                throw new ValorNegativoException();

            if (this.Saldo < valorASerSacado)
                throw new SaldoInsuficienteException();

            this.Saldo -= valorASerSacado;
        }

        public double CalculaTributo()
        {
            return this.Saldo * 0.03;
        }
    }
}