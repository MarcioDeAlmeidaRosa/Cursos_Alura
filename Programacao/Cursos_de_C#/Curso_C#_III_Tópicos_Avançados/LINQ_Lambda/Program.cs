using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_Lambda
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Conta> contas = new List<Conta>();
            contas.Add(ContaComSaldo(5000.0, "Genivaldo", 50));
            contas.Add(ContaComSaldo(5000.0, "Marques", 150));
            contas.Add(ContaComSaldo(6000.0, "Romildo", 100));
            contas.Add(ContaComSaldo(7000.0, "Marcio", 1000));
            contas.Add(ContaComSaldo(253, "Gabriel", 2000));
            contas.Add(ContaComSaldo(400, "Renato", 3000));
            contas.Add(ContaComSaldo(2500, "Bruno", 4000));
            contas.Add(ContaComSaldo(2300, "Sandro", 5000));
            contas.Add(ContaComSaldo(1000, "Glauter", 6000));
            contas.Add(ContaComSaldo(4000, "Marcos", 7000));
            //Filtro normal por uma determinada condição
            Console.WriteLine("---------------------Filtro normal---------------------");
            foreach (Conta conta in contas)
                if (conta.Saldo > 2000)
                    Console.WriteLine(conta.Saldo);
            //Filtro com LINQ por uma determinada condição
            Console.WriteLine("---------------------Filtro por LINQ---------------------");
            var lista = from c in contas where c.Saldo > 2000 select c;
            foreach (Conta conta in lista)
                Console.WriteLine(conta.Saldo); ;
            Console.WriteLine("---------------------Somando total por Lambida---------------------");
            Console.WriteLine("Total das contas : " + contas.Sum(c=> c.Saldo));
            Console.ReadKey();



            //Filtro simples com LINQ
            Console.WriteLine("---------------------Filtro simples com LINQ---------------------");
            var filtrados = from c in contas
                            where c.Titular.StartsWith("G")
                            select c;
            foreach (var f in filtrados)
                Console.WriteLine(f.Titular);
            Console.ReadKey();

            //Where com lambda no LINQ
            Console.WriteLine("---------------------Where com lambda no LINQ---------------------");
            var filtrados1 = lista.Where(c => c.Numero < 1000 && c.Saldo > 5000.0);
            foreach (var f in filtrados1)
                Console.WriteLine(f.Titular);
            Console.ReadKey();

            //Mínimo saldo com LINQ
            Console.WriteLine("---------------------Mínimo saldo com LINQ---------------------");
            var menorSaldo = lista.Min(c => c.Saldo);
            Console.WriteLine(menorSaldo);
            Console.ReadKey();


            //Count com o LINQ
            Console.WriteLine("---------------------Count com o LINQ---------------------");
            int quantidade = lista.Count(c => c.Saldo > 5000.0);
            Console.WriteLine(menorSaldo);
            Console.ReadKey();


            //Projeção no LINQ
            Console.WriteLine("---------------------Projeção no LINQ---------------------");
            var filtradas3 = from c in contas
                            where c.Numero < 1000
                            select new { Titular = c.Titular, Saldo = c.Saldo };
            foreach (var f in filtradas3)
                Console.WriteLine(f.Titular);
            Console.ReadKey();


            //LINQ com arrays
            Console.WriteLine("---------------------LINQ com arrays---------------------");
            int[] inteiros = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };// inicializa um array de inteiros
            int soma = inteiros.Where(x => x >= 10).Sum();
            Console.WriteLine(soma);
            Console.ReadKey();


            //Ordenação de coleções com o LINQ
            Console.WriteLine("---------------------Ordenação de coleções com o LINQ---------------------");
            var resultado = from c in contas
                            where c.Saldo > 1000
                            orderby c.Numero descending
                            select c;
            foreach (var f in resultado)
                Console.WriteLine(f.Titular);
            Console.ReadKey();


            //Ordenação de listas utilizando métodos
            Console.WriteLine("---------------------Ordenação de listas utilizando métodos---------------------");
            var resultado1 = contas.Where(c => c.Saldo < 1000)
                      .OrderByDescending(c => c.Numero);
            foreach (var f in resultado1)
                Console.WriteLine(f.Titular);
            Console.ReadKey();



            //Vários critérios de ordenação
            Console.WriteLine("---------------------Vários critérios de ordenação---------------------");
            List<string> palavras = new List<string>() { "Marta","Maria","Elaine", "Claudia", "Toia", "Carla", "Renata" };
            var ordenadas = from p in palavras
                            orderby p.Length, p
                            select p;
            foreach (var f in ordenadas)
                Console.WriteLine(f);
            Console.ReadKey();


            //Ordenação com vários critérios utilizando métodos
            Console.WriteLine("---------------------Ordenação com vários critérios utilizando métodos---------------------");
            List<string> palavras1 = new List<string>() { "Marta", "Maria", "Elaine", "Claudia", "Toia", "Carla", "Renata" };
            var ordenadas1 = palavras1.OrderBy(p => p.Length).ThenBy(p => p);
            foreach (var f in ordenadas1)
                Console.WriteLine(f);
            Console.ReadKey();

        }


        static Conta ContaComSaldo(double saldo, string nome, int numero)
        {
            return new Conta() { Saldo = saldo, Titular = nome, Numero = numero };
        }
        
    }

    

    class Conta
    {
        public double Saldo { get; set; }
        public string Titular { get; set; }
        public int Numero { get; set; }
    }
}
