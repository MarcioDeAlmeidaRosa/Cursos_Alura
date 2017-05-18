using AluraTunes.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AluraTunes
{
    class Program
    {
        static void Main(string[] args)
        {
            //listar os gêneros rock
            var generos = new List<Genero>() {
                new Genero { ID = 1 , Nome = "Rock" },
                new Genero { ID = 2, Nome = "Raggae" },
                new Genero { ID = 3, Nome = "Rock Progressivo" },
                new Genero { ID = 4, Nome = "Punk Rock" },
                new Genero { ID = 5, Nome = "Clássica" }
            };
            //forma 1
            foreach(var genero in generos)
            {
                if (genero.Nome.Contains("Rock"))
                    Console.WriteLine("ID {0}\tgênero {1}", genero.ID, genero.Nome);
            }
            Console.WriteLine("--------------------------------------------------");
            //forma 2
            foreach (var genero in (from g in generos where g.Nome.Contains("Rock") select g))
            {
                if (genero.Nome.Contains("Rock"))
                    Console.WriteLine("ID {0}\tgênero {1}", genero.ID, genero.Nome);
            }


            Console.ReadLine();
        }
    }
}
