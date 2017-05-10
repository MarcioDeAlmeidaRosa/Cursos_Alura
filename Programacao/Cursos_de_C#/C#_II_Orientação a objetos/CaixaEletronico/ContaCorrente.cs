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
    }
}
