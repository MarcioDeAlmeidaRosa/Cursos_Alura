using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caelum.CaixaEletronico.Contas
{
    public class ContaPoupanca : Conta, ITributavel
    {
        public override void Saca(double valorASerSacado)
        {
            if (valorASerSacado < 0)
                throw new ValorNegativoException();
            //"Valor para sacar é negativo"

            if (this.Saldo < valorASerSacado)
                throw new SaldoInsuficienteException();
            //"Valor insuficiente na conta"

            this.Saldo -= valorASerSacado + 0.10;
        }

        public override void Atualiza(double taxa)
        {
            base.Atualiza(3 * taxa);
        }

        public double CalculaTributo()
        {
            return this.Saldo * 0.02;
        }
    }
}
