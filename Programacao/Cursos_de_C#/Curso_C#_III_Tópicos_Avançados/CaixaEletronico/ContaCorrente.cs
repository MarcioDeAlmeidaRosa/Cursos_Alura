using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caelum.CaixaEletronico.Contas
{
    public class ContaCorrente : Conta
    {
        public override void Atualiza(double taxa)
        {
            base.Atualiza( 2 * taxa);
        }

        public override void Saca(double valorASerSacado)
        {
            if (valorASerSacado < 0)
                throw new ValorNegativoException();

            if (this.Saldo < valorASerSacado)
                throw new SaldoInsuficienteException();

            if (valorASerSacado > 200)
                if (!this.Titular.EhMaiorDeIdade)
                    throw new MenorIdadeException();

            this.Saldo -= valorASerSacado;
        }
    }
}
