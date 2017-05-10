using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaixaEletronico
{
    public class ContaPoupanca: Conta
    {
        public override bool Saca(double valorASerSacado)
        {
            if (this.Saldo >= valorASerSacado && valorASerSacado >= 0)
            {
                this.Saldo -= valorASerSacado + 0.10;
                return true;
            }
            return false;
        }

        public override void Atualiza(double taxa)
        {
            base.Atualiza(3 * taxa);
        }
    }
}
