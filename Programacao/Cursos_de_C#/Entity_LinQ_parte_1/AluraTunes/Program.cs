using AluraTunes.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AluraTunes
{
    class Program
    {
        static void Main(string[] args)
        {
            //listar os gêneros rock
            #region
            var generos = new List<Genero>() {
                new Genero { ID = 1 , Nome = "Rock" },
                new Genero { ID = 2, Nome = "Raggae" },
                new Genero { ID = 3, Nome = "Rock Progressivo" },
                new Genero { ID = 4, Nome = "Punk Rock" },
                new Genero { ID = 5, Nome = "Clássica" }
            };
            //forma 1
            foreach (var genero in generos)
            {
                if (genero.Nome.Contains("Rock"))
                    Console.WriteLine("ID {0}\tgênero {1}", genero.ID, genero.Nome);
            }
            Console.WriteLine("--------------------------------------------------");
            //forma 2
            foreach (var genero in (from g in generos where g.Nome.Contains("Rock") select g))
            {
                Console.WriteLine("ID {0}\tgênero {1}", genero.ID, genero.Nome);
            }
            Console.WriteLine("--------------------------------------------------");
            #endregion
            //listar músicas do sistema
            #region
            var musicas = new List<Musica>
            {
                new Musica{ ID = 1 , Nome = "Sweet ChildO'Mine" , GeneroID= 1},
                new Musica{ ID = 2 , Nome = "I shot The Sheriff" , GeneroID= 2},
                new Musica{ ID = 3 , Nome = "Danúbio Azul" , GeneroID= 5}
            };
            //Forma 1
            foreach (var musica in (from m in musicas select m))
            {
                Console.WriteLine("ID {0}\tmúsica {1}\tgênero {2}", musica.ID, musica.Nome, musica.GeneroID);
            }
            Console.WriteLine("--------------------------------------------------");
            //Forma 2
            var musicaGenero = from m
                                 in musicas
                               join g in generos on m.GeneroID equals g.ID
                               select new { m, g };
            foreach (var musica in (from m in musicaGenero select m))
            {
                Console.WriteLine("ID {0}\tmúsica {1}\tgênero{2}", musica.m.ID, musica.m.Nome, musica.g.Nome);
            }
            Console.WriteLine("--------------------------------------------------");
            #endregion

            #region
            List<Genero> generos1 = new List<Genero>()
            {
                new Genero { ID = 1, Nome = "Rock" },
                new Genero { ID = 2, Nome = "Reggae" },
                new Genero { ID = 3, Nome = "Rock Progressivo" },
                new Genero { ID = 4, Nome = "Jazz" },
                new Genero { ID = 5, Nome = "Punk Rock" },
                new Genero { ID = 6, Nome = "Classica" }
            };

            List<Musica> musicas1 = new List<Musica>()
            {
                new Musica { ID = 1, Nome = "Sweet Child O'Mine", GeneroID = 1 },
                new Musica { ID = 2, Nome = "I Shot The Sheriff", GeneroID = 2 },
                new Musica { ID = 3, Nome = "Danúbio Azul", GeneroID = 6 }
            };

            //Crie uma consulta para listar os nomes das músicas cujo gênero tenha o nome "Reggae".

            //var query = ??? 
            var itens = from gg in generos1 join mm in musicas1 on gg.ID equals mm.GeneroID select new { gg.Nome };
            foreach (var item in itens)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("--------------------------------------------------");
            #endregion

            //Variável que vai representar a raiz do XML
            //no=> AluraTunes
            XElement root = XElement.Load(string.Format("{0}{1}", System.AppDomain.CurrentDomain.BaseDirectory.ToString().Replace(@"bin\Debug\",""), @"\Data\AluraTunes.xml"));
            //Definir consulta linq para acessar o dado xml
            //no=> Generos , subno => Genero
            var qyeryXMLGenero = from g in root.Element("Generos").Elements("Genero") select g;

            foreach(XElement genefo in qyeryXMLGenero)
            {
                Console.WriteLine("Genero: " + genefo.Element("GeneroId").Value + " Nome: " + genefo.Element("Nome").Value);
            }
            Console.WriteLine("--------------------------------------------------");
            var qyeryXMLMusicaEGenero = from g in root.Element("Generos").Elements("Genero")
                                        join m in root.Element("Musicas").Elements("Musica")
                                               on g.Element("GeneroId").Value equals m.Element("GeneroId").Value
                                        select new
                                        {
                                            Musica = m.Element("Nome").Value,
                                            Genero = g.Element("Nome").Value
                                        };

            foreach (var item in qyeryXMLMusicaEGenero)
            {
                Console.WriteLine("{0}\t{1}", item.Musica, item.Genero);
            }
            Console.WriteLine("--------------------------------------------------");

            Console.ReadLine();
        }
    }
}