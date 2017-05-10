using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaixaEletronico
{
    public class ContaCorrente : Conta
    {
        public override void Atualiza(double taxa)
        {
            base.Atualiza( 2 * taxa);
        }

        public override bool Saca(double valorASerSacado)
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
    }
}
