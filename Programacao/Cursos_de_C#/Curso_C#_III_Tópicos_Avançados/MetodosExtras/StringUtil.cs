using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetodosExtras.Extra
{
    public static class StringUtil
    {
        public static string Pluralize(this string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return texto;
            return texto.EndsWith("s") ? texto : texto + "s";
        }
    }
}
