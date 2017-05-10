using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaixaEletronico
{
    public class MenorIdadeException: Exception
    {
        public MenorIdadeException()
            : base("Titular não pode ser menor de idade")
        {

        }
    }
}
