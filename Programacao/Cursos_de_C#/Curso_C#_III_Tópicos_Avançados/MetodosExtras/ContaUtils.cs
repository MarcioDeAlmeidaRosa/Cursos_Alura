using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MetodosExtras.Extra
{
    public static class ContaUtils
    {
        public static string AsXML(this Conta resource)
        {
            var stringWriter = new StringWriter();
            new XmlSerializer(resource.GetType()).Serialize(stringWriter, resource);
            return stringWriter.ToString();
        }

        //Esse código não compila, pois o Extension Method só pode acessar a interface pública da Conta
        //public static void MudaSaldo(this Conta conta, double novoSaldo)
        //{
        //    conta.Saldo = novoSaldo;
        //}

        //O código não compila, pois o this só pode ficar no primeiro argumento do extension method.
        //public static void MudaTitular(this Conta conta, this string titular)
        //{
        //    conta.Titular = titular;
        //}

        public static void MudaTitular(this Conta c, string titular)
        {
            c.Titular = titular;
        }
    }
}
