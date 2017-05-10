using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caelum.CaixaEletronico
{
    public class SaldoInsuficienteException : Exception
    {
        public SaldoInsuficienteException()
            : base("Valor insuficiente na conta")
        {

        }
    }
}