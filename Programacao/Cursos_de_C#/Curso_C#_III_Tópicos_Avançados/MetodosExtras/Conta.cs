using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetodosExtras
{
    public class Conta
    {
        public string Titular { get; set; }
        public int Numero { get; set; }
        public double Saldo { get; private set; }


        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
