using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetodosExtras.Extra;

namespace MetodosExtras
{
    class Program
    {
        static void Main(string[] args)
        {
            //Estendendo comportamentos através de métodos extras 
            Console.WriteLine("--------------------Estendendo comportamentos através de métodos extras--------------------");
            Console.WriteLine("Teste".Pluralize());
            Console.ReadKey();

            //Definindo métodos para extensão
            Console.WriteLine("--------------------Definindo métodos para extensão--------------------");
            Conta conta = new Conta();
            conta.Numero = 10;
            Console.WriteLine(conta.AsXML());
            Console.ReadKey();


            //O que podemos usar em Extension Methods
            Console.WriteLine("--------------------O que podemos usar em Extension Methods--------------------");
            conta = new Conta();
            conta.Numero = 100;
            //Esse código não compila, pois o Extension Method só pode acessar a interface pública da Conta
            //conta.MudaSaldo(100);
            Console.WriteLine(conta.AsXML());
            Console.ReadKey();



            //Extension Method e método estático
            Console.WriteLine("--------------------Extension Method e método estático--------------------");
            Conta c = new Conta();
            string titular = "victor";
            //Extension Method é um método estático comum e, portanto, o código do exercício funciona
            ContaUtils.MudaTitular(c, titular);
        }
    }
}
