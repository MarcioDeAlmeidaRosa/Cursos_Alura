using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaAplicacaoConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Mensagem que vai para o terminal");

            //TextReader leitor = Console.In;
            //string linha = string.Empty;

            //do
            //{
            //    // usa o texto da linha atual
            //    linha = leitor.ReadLine();
            //    Console.WriteLine("Você digitou " + linha);
            //} while (linha != null);

            //string linha = string.Empty;
            //do
            //{
            //    linha = Console.ReadLine();
            //    Console.WriteLine("Você digitou " + linha);

            //} while (linha != null);

            using (TextReader leitor = Console.In)
            {
                string linha = string.Empty;

                do
                {
                    // usa o texto da linha atual
                    linha = leitor.ReadLine();
                    Console.WriteLine("Você digitou " + linha);
                } while (linha != null);

            }
        }
    }
}