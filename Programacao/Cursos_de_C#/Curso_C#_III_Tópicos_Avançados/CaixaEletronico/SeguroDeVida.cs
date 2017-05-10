using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caelum.CaixaEletronico
{
    public class SeguroDeVida : ITributavel
    {
        public double ValorSeguro { get; private set; }

        public double CalculaTributo()
        {
            return 42;
        }
    }
}