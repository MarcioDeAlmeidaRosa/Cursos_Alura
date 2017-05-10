using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaixaEletronico
{
    public class GerenciadorDeImposto
    {
        public double Total { get; private set; }

        public void Acumula(ITributavel cp)
        {
            this.Total += cp.CalculaTributo();
        }
    }
}
