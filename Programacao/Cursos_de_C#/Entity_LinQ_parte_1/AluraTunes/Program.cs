using AluraTunes.Data;
using AluraTunes.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AluraTunes
{
    class Program
    {
        private static string caminhoPrograma = string.Empty;

        static Program()
        {
            caminhoPrograma = System.AppDomain.CurrentDomain.BaseDirectory.ToString().Replace(@"bin\Debug\", "");
        }

        static void Main(string[] args)
        {
            //listar os gêneros rock
            #region LINQ TO OBJECT
            Console.WriteLine("#####################LINQ TO OBJECT###############################");
            var generos = new List<Entidades.Genero>() {
                new Entidades.Genero { ID = 1 , Nome = "Rock" },
                new Entidades.Genero { ID = 2, Nome = "Raggae" },
                new Entidades.Genero { ID = 3, Nome = "Rock Progressivo" },
                new Entidades.Genero { ID = 4, Nome = "Punk Rock" },
                new Entidades.Genero { ID = 5, Nome = "Clássica" }
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
            //listar músicas do sistema
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
            
            List<Entidades.Genero> generos1 = new List<Entidades.Genero>()
            {
                new Entidades.Genero { ID = 1, Nome = "Rock" },
                new Entidades.Genero { ID = 2, Nome = "Reggae" },
                new Entidades.Genero { ID = 3, Nome = "Rock Progressivo" },
                new Entidades.Genero { ID = 4, Nome = "Jazz" },
                new Entidades.Genero { ID = 5, Nome = "Punk Rock" },
                new Entidades.Genero { ID = 6, Nome = "Classica" }
            };

            List<Entidades.Musica> musicas1 = new List<Entidades.Musica>()
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
            #endregion LINQ TO OBJECT

            #region LINQ TO XML
            Console.WriteLine("#####################LINQ TO XML###############################");
            //Variável que vai representar a raiz do XML
            //no=> AluraTunes
            XElement root = XElement.Load(string.Format("{0}{1}", caminhoPrograma, @"\Data\AluraTunes.xml"));
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

            XElement xmlRootAutomovel = XElement.Load(string.Format("{0}{1}", caminhoPrograma, @"\Data\Automoveis.xml"));

            var queryAutomovel = from fabricantes in xmlRootAutomovel.Element("Fabricantes").Elements("Fabricante")
                                 join modelos in xmlRootAutomovel.Element("Modelos").Elements("Modelo")
                                 on fabricantes.Element("FabricanteId").Value equals modelos.Element("FabricanteId").Value
                                 select new { Fabricante = fabricantes.Element("Nome").Value , Modelo = modelos.Element("Nome").Value };

            foreach (var item in queryAutomovel)
            {
                Console.WriteLine("{0}\t{1}", item.Fabricante, item.Modelo);
            }

            Console.WriteLine("--------------------------------------------------");
            XElement rootMusica = XElement.Load(caminhoPrograma + @"data/AluraTunesCompleto.xml");
            var queryMusica = from musicasLista 
                                in rootMusica.Element("Musicas").Elements("Musica")
                              join generosLista in rootMusica.Element("Generos").Elements("Genero")
                              on musicasLista.Element("GeneroId").Value equals generosLista.Element("GeneroId").Value
                              select new { MusicaID = musicasLista.Element("MusicaId").Value , MusicaNome = musicasLista.Element("Nome").Value, GeneroNome = generosLista.Element("Nome").Value  };
            foreach(var musica in queryMusica)
                Console.WriteLine("ID música {0} - Nome música {1} - Gênero {2}", musica.MusicaID, musica.MusicaNome, musica.GeneroNome);
            #endregion

            #region LINQ TO ENTITIES
            Console.WriteLine("#####################LINQ TO ENTITIES###############################");
            //Criamos o banco de dados AluraTunes.mdf
            Console.WriteLine("--------------------------------------------------");
            using(var contexto = new AluraTunesEntities())
            {
                //definição de consulta
                var queryGeneros = from listaGeneros in contexto.Generos select listaGeneros;
                //imprimir consulta no console
                foreach (var genero in queryGeneros)
                    Console.WriteLine("Gênero {0} - {1}", genero.GeneroId, genero.Nome);
                Console.WriteLine("--------------------------------------------------");

                var queryFaixasGeneros = from faixas in contexto.Faixas
                                         join listaGeneros in queryGeneros
                                         on faixas.GeneroId equals listaGeneros.GeneroId
                                         select new { faixas, listaGeneros };

                //Aplicando paginação - limitando 10 linha de retorno
                //Aplicando filtro limitando a 10, será aplicado direto na pesquisa no momento do uso "foreach"
                queryFaixasGeneros = queryFaixasGeneros.Take(10);

                //Aplicando condifuração para aplicar o log
                contexto.Database.Log = Console.WriteLine;

                foreach (var faixaGenero in queryFaixasGeneros)
                    Console.WriteLine("Faixa {0} - Gênero {1}", faixaGenero.faixas.Nome, faixaGenero.listaGeneros.Nome);

                Console.WriteLine("--------------------------------------------------");
            }
            Console.WriteLine("--------------------------------------------------");


            //Filtro forma 1 - Filtros LINQ (SINTAXE DE CONSULTA)
            using(var contexto = new AluraTunesEntities())
            {
                var textoBusca = "Led";

                var queryArtista = from artistas 
                              in contexto.Artistas
                            where artistas.Nome.Contains(textoBusca)
                            select artistas;
                foreach (var artista in queryArtista)
                    Console.WriteLine("Artista {0} - {1}", artista.ArtistaId, artista.Nome);
                Console.WriteLine("--------------------------------------------------");
            }


            //Filtro forma 2 - Função (SINTAXE DE MÉTODO) => (LANBIDA)
            using (var contexto = new AluraTunesEntities())
            {
                var textoBusca = "Led";

                var queryArtista = contexto.Artistas.Where(a => a.Nome.Contains(textoBusca));
                foreach (var artista in queryArtista)
                    Console.WriteLine("Artista {0} - {1}", artista.ArtistaId, artista.Nome);
                Console.WriteLine("--------------------------------------------------");
            }

            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine("---------------------EXEMPLO SINTAXE DE MÉTODO  X  EXEMPLO SINTAXE DE CONSULTA-----------------------------");
            using (var context = new AluraTunesEntities())
            {
                //SINTAXE DE MÉTODO
                //=================
                Console.WriteLine("---------------------EXEMPLO SINTAXE DE MÉTODO-----------------------------");
                var querySitaxeMetodo = context.Faixas
                    .Join(context.Generos, f => f.GeneroId, g => g.GeneroId, (f, c) => new { f, c })
                    .Join(context.TipoMidias, fm => fm.f.TipoMidiaId, tm => tm.TipoMidiaId, (fm, tm) => new { fm.c, fm.f, tm })
                    .Where(cftm=> cftm.f.Nome.Contains("Balls "))
                    .Select(cftm => new
                    {
                        FaixaID = cftm.f.GeneroId,
                        FaixaNome = cftm.f.Nome,
                        GeneroID = cftm.c.GeneroId,
                        GeneroNome = cftm.c.Nome,
                        TipoMediaID = cftm.tm.TipoMidiaId,
                        TipoMediaNome = cftm.tm.Nome
                    });
                foreach (var artista in querySitaxeMetodo)
                    Console.WriteLine("FaixaID {0} - FaixaNome {1} - GeneroID {2} - GeneroNome {3} - TipoMediaID {4} - TipoMediaNome {5}"
                        , artista.FaixaID
                        , artista.FaixaNome
                        , artista.GeneroID
                        , artista.GeneroNome
                        , artista.TipoMediaID
                        , artista.TipoMediaNome
                        );
                Console.WriteLine("--------------------------------------------------");

                //SINTAXE DE CONSULTA
                //===================
                Console.WriteLine("---------------------EXEMPLO SINTAXE DE CONSULTA-----------------------------");
                var querySitaxeConsulta = from listaFaixas in context.Faixas
                                          join listaGeneros in context.Generos on listaFaixas.GeneroId equals listaGeneros.GeneroId
                                          join listaTipoMedias in context.TipoMidias on listaFaixas.TipoMidiaId equals listaTipoMedias.TipoMidiaId
                                          where listaFaixas.Nome.Contains("Balls ")
                                          select new
                                          {
                                              FaixaID = listaFaixas.GeneroId,
                                              FaixaNome = listaFaixas.Nome,
                                              GeneroID = listaGeneros.GeneroId,
                                              GeneroNome = listaGeneros.Nome,
                                              TipoMediaID = listaTipoMedias.TipoMidiaId,
                                              TipoMediaNome = listaTipoMedias.Nome
                                          };
                foreach (var artista in querySitaxeMetodo)
                    Console.WriteLine("FaixaID {0} - FaixaNome {1} - GeneroID {2} - GeneroNome {3} - TipoMediaID {4} - TipoMediaNome {5}"
                        , artista.FaixaID
                        , artista.FaixaNome
                        , artista.GeneroID
                        , artista.GeneroNome
                        , artista.TipoMediaID
                        , artista.TipoMediaNome
                        );
                Console.WriteLine("--------------------------------------------------");
            }

            using(var contexto = new AluraTunesEntities())
            {
                Console.WriteLine("---------------------EXEMPLO SINTAXE DE CONSULTA-----------------------------");
                var querySintaxeConsulta1 = from a in contexto.Artistas
                            where a.Nome.Contains("Led")
                            select a;
                foreach (var artista in querySintaxeConsulta1)
                    Console.WriteLine("ArtistaId {0} - Nome {1}", artista.ArtistaId, artista.Nome);
                Console.WriteLine("--------------------------------------------------");

                Console.WriteLine("---------------------EXEMPLO SINTAXE DE MÉTODO-----------------------------");
                var querySintaxeMetodo2 = contexto.Artistas.Where(a=> a.Nome.Contains("Led"));
                foreach (var artista in querySintaxeMetodo2)
                    Console.WriteLine("ArtistaId {0} - Nome {1}", artista.ArtistaId, artista.Nome);
                Console.WriteLine("--------------------------------------------------");
            }

            Console.WriteLine("--------------------------------------------------");
            using(var contexto = new AluraTunesEntities())
            {
                Console.WriteLine("---------------------EXEMPLO SINTAXE DE MÉTODO-----------------------------");
                var querySintaxeMetodo2 = contexto.Generos.Where(g => g.Nome == "Rock");
                foreach (var artista in querySintaxeMetodo2)
                    Console.WriteLine("GeneroId {0} - Nome {1}", artista.GeneroId, artista.Nome);
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("---------------------EXEMPLO SINTAXE DE CONSULTA-----------------------------");
                var querySintaxeMetodo3 = from g in contexto.Generos where g.Nome == "Rock" select g;
                foreach (var artista in querySintaxeMetodo3)
                    Console.WriteLine("GeneroId {0} - Nome {1}", artista.GeneroId, artista.Nome);
                Console.WriteLine("--------------------------------------------------");
            }

            Console.WriteLine("--------------------------------------------------");
            using (var contexto = new AluraTunesEntities())
            {
                var textoBusca = "Led";
                Console.WriteLine("---------------------EXEMPLO SINTAXE DE CONSULTA-----------------------------");
                var queryArtistaAlbum = from artista in contexto.Artistas
                                        join alb in contexto.Albums on artista.ArtistaId equals alb.ArtistaId
                                        where artista.Nome.Contains(textoBusca)
                                        select new
                                        {
                                            NomeArtista = artista.Nome,
                                            NomeAlbum = alb.Titulo
                                        };
                foreach (var item in queryArtistaAlbum)
                    Console.WriteLine("Artista: {0} - Album: {1}", item.NomeArtista, item.NomeAlbum);
                Console.WriteLine("---------------------EXEMPLO SINTAXE DE CONSULTA SEM JOIN-----------------------------");
                var queryArtistaAlbum2 = from albums in contexto.Albums
                                         where albums.Artista.Nome.Contains(textoBusca)
                                         select new
                                         {
                                             NomeArtista = albums.Artista.Nome,
                                             NomeAlbum = albums.Titulo
                                         };
                foreach (var item in queryArtistaAlbum2)
                    Console.WriteLine("Artista: {0} - Album: {1}", item.NomeArtista, item.NomeAlbum);
                Console.WriteLine("--------------------------------------------------");
            }
            Console.WriteLine("--------------------------------------------------");

            #endregion LINQ TO ENTITIES

            #region LINQ COM FILTROS DINÂMICOS
            Console.WriteLine("#####################LINQ COM FILTROS DINÂMICOS###############################");
            using (var contexto = new AluraTunesEntities())
            {
                //Atribuição de log
                contexto.Database.Log = Console.WriteLine;

                string nomeArtista = string.Empty;
                string album = string.Empty;

                nomeArtista = "Led Zeppelin";
                GetFaixas(contexto, nomeArtista, album);
                Console.WriteLine("--------------------------------------------------");
                album = "Graffiti";
                GetFaixas(contexto, nomeArtista, album);
                Console.WriteLine("--------------------------------------------------");
                album = string.Empty;
                GetFaixasSintaxeQuery(contexto, nomeArtista, album);
                Console.WriteLine("--------------------------------------------------");
                album = "Graffiti";
                GetFaixas(contexto, nomeArtista, album);
                Console.WriteLine("--------------------------------------------------");
            }
            #endregion

            Console.WriteLine("--------------------------------------------------");

            Console.ReadLine();
        }

        private static void GetFaixas(AluraTunesEntities contexto, string nomeArtista, string album)
        {
            var queryFiltroDinamico = from f in contexto.Faixas select f;

            if (!string.IsNullOrEmpty(nomeArtista))
                queryFiltroDinamico = queryFiltroDinamico.Where(f => f.Album.Artista.Nome.Contains(nomeArtista));

            if (!string.IsNullOrWhiteSpace(album))
                queryFiltroDinamico = queryFiltroDinamico.Where(f => f.Album.Titulo.Contains(album));

            queryFiltroDinamico = queryFiltroDinamico.OrderBy(o => o.Album.Titulo).ThenBy(o=> o.Nome);

            foreach (var faixa in queryFiltroDinamico)
                Console.WriteLine("{0}\t{1}", faixa.Album.Titulo.PadRight(40), faixa.Nome);
        }

        private static void GetFaixasSintaxeQuery(AluraTunesEntities contexto, string nomeArtista, string album)
        {
            var queryFiltroDinamico = from f
                                        in contexto.Faixas
                                      where (!string.IsNullOrEmpty(nomeArtista) ? f.Album.Artista.Nome.Contains(nomeArtista) : true)
                                      && (!string.IsNullOrEmpty(album) ? f.Album.Titulo.Contains(album) : true)
                                      orderby f.Nome, f.Album.Titulo
                                      select f;

            foreach (var faixa in queryFiltroDinamico)
                Console.WriteLine("{0}\t{1}", faixa.Album.Titulo.PadRight(40), faixa.Nome);
        }
    }
}