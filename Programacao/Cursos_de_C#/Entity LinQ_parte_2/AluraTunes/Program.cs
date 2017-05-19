using AluraTunes.Data;
using AluraTunes.Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace AluraTunes
{
    class Program
    {
        private static string caminhoPrograma = string.Empty;
        private const bool EXECUTAR_TODOS_EXERCICIOS = false;
        private const bool EXECUTAR_EXERCICIO_ATUAL = true;

        static Program()
        {
            caminhoPrograma = System.AppDomain.CurrentDomain.BaseDirectory.ToString().Replace(@"bin\Debug\", "");
        }

        static void Main(string[] args)
        {
            if (EXECUTAR_TODOS_EXERCICIOS)
            {
                #region 2 - Linq to entities paginado
                Console.WriteLine("----------------------2 - Linq to entities paginado----------------------------");
                using (var contexto = new AluraTunesEntities())
                {
                    Console.WriteLine("-----------------------LISTAGEM DE NOTAS FISCAIS---------------------------");
                    //Configurando geração de log
                    //contexto.Database.Log = Console.WriteLine;

                    //limita a quantidade de registro na página
                    const int NUMEROTOTALREGISTROPORPAGINA = 10;
                    int numeroTotalRegistro = contexto.NotasFiscais.Count();

                    //Metodo matemático que arredonda a conta pra cima para não quebrar o calculo de número de páginas
                    var totalDePaginas = Math.Ceiling((decimal)numeroTotalRegistro / NUMEROTOTALREGISTROPORPAGINA);

                    for (var pagina = 1; pagina <= totalDePaginas; pagina++)
                        ImprimirPagina(contexto, NUMEROTOTALREGISTROPORPAGINA, pagina);
                }
                Console.WriteLine("--------------------------------------------------");
                #endregion
            }

            if (EXECUTAR_TODOS_EXERCICIOS)
            {
                #region 3 - Subconsulta
                Console.WriteLine("----------------------2 - Linq to entities paginado----------------------------");
                using (var contexto = new AluraTunesEntities())
                {
                    Console.WriteLine("-----------------------NOTAS ACIMA DA MÉDIA---------------------------");
                    //Configurando geração de log
                    contexto.Database.Log = Console.WriteLine;

                    decimal queryMedia = contexto.NotasFiscais.Average(n => n.Total);
                    var queryC = from nf
                                 in contexto.NotasFiscais
                                 where nf.Total > queryMedia
                                 orderby nf.Total descending
                                select new
                                 {
                                     Numero = nf.NotaFiscalId,
                                     Data = nf.DataNotaFiscal,
                                     Cliente = nf.Cliente.PrimeiroNome + " " + nf.Cliente.Sobrenome,
                                     Valor = nf.Total
                                 };

                    foreach (var nf in queryC)
                        Console.WriteLine(string.Format("{0}\t{1}\t{2}\t{3}", nf.Numero, nf.Data, nf.Cliente.PadRight(40), nf.Valor));

                    Console.WriteLine("-----------------------A média é {0}---------------------------", queryMedia);
                }
                Console.WriteLine("--------------------------------------------------");
                #endregion
            }

            if (EXECUTAR_EXERCICIO_ATUAL)
            {
                #region 4 - Clientes compraram produto mais vendido
                Console.WriteLine("----------------------4 - Clientes compraram produto mais vendido----------------------------");
                using (var contexto = new AluraTunesEntities())
                {
                    Console.WriteLine("-----------------------CLIENTES QUE COMPRARAM OS PRODUTOS MAIS VENDIDOS---------------------------");
                    //Configurando geração de log
                    //contexto.Database.Log = Console.WriteLine;

                    var queryMaisVendidas = from f
                                              in contexto.Faixas
                                            where f.ItemNotaFiscals.Count() > 0//Elimina faixas que não tem venda vinculada
                                            let totalDeVendas = f.ItemNotaFiscals.Sum(nf => nf.PrecoUnitario * nf.Quantidade)
                                            orderby totalDeVendas descending
                                            select new
                                            {
                                                FaixaID = f.FaixaId,
                                                NomeFaixa = f.Nome,
                                                TotalVendido = totalDeVendas
                                            };

                    foreach (var faixa in queryMaisVendidas)
                        Console.WriteLine(string.Format("{0}\t{1}\t{2}", faixa.FaixaID, faixa.NomeFaixa.PadRight(40), faixa.TotalVendido));

                    //Depois de organizar as faixas com seus totais de venda, recupero a mais vendida
                    var produtoMaisVendido = queryMaisVendidas.First();
                    Console.WriteLine("---------------------Produto mais vendido-----------------------------");
                    Console.WriteLine(string.Format("{0}\t{1}\t{2}", produtoMaisVendido.FaixaID, produtoMaisVendido.NomeFaixa.PadRight(40), produtoMaisVendido.TotalVendido));

                    var queryCliente = from inf
                                       in contexto.ItensNotasFiscais
                                       where inf.FaixaId == produtoMaisVendido.FaixaID
                                       select new
                                       {
                                           NomeCliente = inf.NotaFiscal.Cliente.PrimeiroNome + " " + inf.NotaFiscal.Cliente.Sobrenome
                                       };

                    Console.WriteLine("---------------------Compradores dos produtos mais vendidos-----------------------------");
                    foreach (var cliente in queryCliente)
                        Console.WriteLine(string.Format("{0}", cliente.NomeCliente));
                }
                Console.WriteLine("--------------------------------------------------");
                #endregion
            }

            Console.ReadLine();
        }

        private static void ImprimirPagina(AluraTunesEntities contexto, int NUMEROTOTALREGISTROPORPAGINA, int numeorDaPagina)
        {
            var queryNFs = from nf
                                             in contexto.NotasFiscais
                           orderby nf.NotaFiscalId
                           select new
                           {
                               Numero = nf.NotaFiscalId,
                               Data = nf.DataNotaFiscal,
                               Cliente = nf.Cliente.PrimeiroNome + " " + nf.Cliente.Sobrenome,
                               Total = nf.Total
                           };

            //Define o 
            var numeroDePulos = (numeorDaPagina - 1) * NUMEROTOTALREGISTROPORPAGINA;

            //OBS: tive que colocar o Skip antes do Take por que ao contrário não estava trazendo registro 

            //Move para a segunda página
            //Skip --> precisa ter ordenação da query para funcionar
            queryNFs = queryNFs.Skip(numeroDePulos);


            // Make Uppercase, ou pressione CTRL+SHIFT+u.
            //Transformar em minúsculas, ou pressione CTRL+u.

            queryNFs = queryNFs.Take(NUMEROTOTALREGISTROPORPAGINA);


            Console.WriteLine("----------------------Página " + numeorDaPagina + "----------------------------");
            foreach (var nf in queryNFs)
                Console.WriteLine(string.Format("{0}\t{1}\t{2}\t{3}", nf.Numero, nf.Data, nf.Cliente.PadRight(40), nf.Total));
        }

        private static void ComandosAntigos()
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

            foreach (XElement genefo in qyeryXMLGenero)
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
                                 select new { Fabricante = fabricantes.Element("Nome").Value, Modelo = modelos.Element("Nome").Value };

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
                              select new { MusicaID = musicasLista.Element("MusicaId").Value, MusicaNome = musicasLista.Element("Nome").Value, GeneroNome = generosLista.Element("Nome").Value };
            foreach (var musica in queryMusica)
                Console.WriteLine("ID música {0} - Nome música {1} - Gênero {2}", musica.MusicaID, musica.MusicaNome, musica.GeneroNome);
            #endregion

            #region LINQ TO ENTITIES
            Console.WriteLine("#####################LINQ TO ENTITIES###############################");
            //Criamos o banco de dados AluraTunes.mdf
            Console.WriteLine("--------------------------------------------------");
            using (var contexto = new AluraTunesEntities())
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
            using (var contexto = new AluraTunesEntities())
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
                    .Where(cftm => cftm.f.Nome.Contains("Balls "))
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

            using (var contexto = new AluraTunesEntities())
            {
                Console.WriteLine("---------------------EXEMPLO SINTAXE DE CONSULTA-----------------------------");
                var querySintaxeConsulta1 = from a in contexto.Artistas
                                            where a.Nome.Contains("Led")
                                            select a;
                foreach (var artista in querySintaxeConsulta1)
                    Console.WriteLine("ArtistaId {0} - Nome {1}", artista.ArtistaId, artista.Nome);
                Console.WriteLine("--------------------------------------------------");

                Console.WriteLine("---------------------EXEMPLO SINTAXE DE MÉTODO-----------------------------");
                var querySintaxeMetodo2 = contexto.Artistas.Where(a => a.Nome.Contains("Led"));
                foreach (var artista in querySintaxeMetodo2)
                    Console.WriteLine("ArtistaId {0} - Nome {1}", artista.ArtistaId, artista.Nome);
                Console.WriteLine("--------------------------------------------------");
            }

            Console.WriteLine("--------------------------------------------------");
            using (var contexto = new AluraTunesEntities())
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

            Console.WriteLine("----------------------EXERCÍCIO DECRESCENTE----------------------------");
            #region EXERCÍCIO DECRESCENTE
            using (var contexto = new AluraTunesEntities())
            {
                var query = from nf in contexto.NotasFiscais
                            orderby nf.Total descending, nf.Cliente.PrimeiroNome + " " + nf.Cliente.Sobrenome
                            select new
                            {
                                Data = nf.DataNotaFiscal,
                                Cliente = nf.Cliente.PrimeiroNome + " " + nf.Cliente.Sobrenome,
                                Total = nf.Total
                            };
                foreach (var nf in query)
                {
                    Console.WriteLine("{0}\t{1}\t{2}", nf.Data.ToString("dd/MM/yyyy"), nf.Cliente.PadRight(40), nf.Total);
                }
            }
            #endregion
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine("-------------------------Ordenando Consultas Linq - Sintaxe de Método-------------------------");
            using (var contexto = new AluraTunesEntities())
            {
                var query1 = from alb in contexto.Albums select alb;

                var query2 = query1.OrderBy(o => o.Artista.Nome).ThenBy(o => o.Titulo);

                foreach (var item in query2)
                    Console.WriteLine(item.Titulo);
            }
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine("----------------------Ordenação Decrescente - Sintaxe de Método----------------------------");
            using (var contexto = new AluraTunesEntities())
            {
                var querySentaxeQuery = from nf in contexto.NotasFiscais
                                        orderby nf.Total descending, nf.Cliente.PrimeiroNome + " " + nf.Cliente.Sobrenome
                                        select nf;

                foreach (var item in querySentaxeQuery)
                    Console.WriteLine("{0}\t{1}", item.Cliente.PrimeiroNome.PadRight(40), item.Total);

                var querySitaxeMetodo = contexto.NotasFiscais.OrderByDescending(o => o.Total).ThenBy(o => o.Cliente.PrimeiroNome + " " + o.Cliente.Sobrenome);
                foreach (var item in querySitaxeMetodo)
                    Console.WriteLine("{0}\t{1}", item.Cliente.PrimeiroNome.PadRight(40), item.Total);
            }
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine("----------------------Linq to entities count----------------------------");
            using (var contexto = new AluraTunesEntities())
            {
                var queryTotalFaixa = from f
                                      in contexto.Faixas
                                      where f.Album.Artista.Nome == "Led Zeppelin"
                                      select f;


                foreach (var faixa in queryTotalFaixa)
                {
                    Console.WriteLine("{0}", faixa.Nome);
                }

                Console.WriteLine("Led Zeppelin tem {0} no banco de dados", queryTotalFaixa.Count());
                Console.WriteLine("Total de faixas no banco {0}", contexto.Faixas.Count());
                Console.WriteLine("Led Zeppelin tem {0} no banco de dados", contexto.Faixas.Count(c => c.Album.Artista.Nome == "Led Zeppelin"));
            }
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine("----------------------2 - Linq to entities Sum----------------------------");
            using (var contexto = new AluraTunesEntities())
            {
                var queryItemNf = from i
                                  in contexto.ItensNotasFiscais
                                  where i.Faixa.Album.Artista.Nome == "Led Zeppelin"
                                  select i;
                foreach (var item in queryItemNf)
                    Console.WriteLine("Faixa {0} - quantidade {1} - preço unitário {2}", item.Faixa.Nome, item.Quantidade, item.PrecoUnitario);

                var queryItemNfTotal = from inf in queryItemNf
                                       select new { totalDosItens = inf.Quantidade * inf.PrecoUnitario };

                foreach (var item in queryItemNfTotal)
                    Console.WriteLine("Total do item R${0}", item.totalDosItens);

                Console.WriteLine("Total do artista R${0}", queryItemNfTotal.Sum(i => i.totalDosItens));
            }
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine("----------------------3 - Linq to entities groupby----------------------------");
            using (var contexto = new AluraTunesEntities())
            {
                var queryItemNF = from i
                                  in contexto.ItensNotasFiscais
                                  where i.Faixa.Album.Artista.Nome == "Led Zeppelin"
                                  group i by i.Faixa.Album into agrupado
                                  let totalPorAlbum = agrupado.Sum(agru => agru.PrecoUnitario * agru.Quantidade)
                                  orderby totalPorAlbum descending
                                  select new
                                  {
                                      TituloDoAlbum = agrupado.Key.Titulo,
                                      TotalPorAlbum = totalPorAlbum
                                  };
                foreach (var agrupado in queryItemNF)
                    Console.WriteLine("{0}\t{1}", agrupado.TituloDoAlbum.PadRight(40), agrupado.TotalPorAlbum);
            }
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine("----------------------6 - Calculando Quantidades Com Projeção de Dados----------------------------");
            using (var contexto = new AluraTunesEntities())
            {
                var quantidadeSintaxeMetodo = contexto.Faixas.Where(f => f.Album.Artista.Nome == "Led Zeppelin").Count();
                Console.WriteLine("Quantidade de faixas (SintaxeMetodo) {0}", quantidadeSintaxeMetodo);
                var quantidadeSintaxeMetodo2 = contexto.Faixas.Count(f => f.Album.Artista.Nome == "Led Zeppelin");
                Console.WriteLine("Quantidade de faixas (SintaxeMetodo) {0}", quantidadeSintaxeMetodo2);
            }
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine("----------------------7 - Agrupando Valores de Consultas Linq----------------------------");
            using (var contexto = new AluraTunesEntities())
            {
                var artistaEalbumQuery = from alb
                                           in contexto.Albums
                                         select new
                                         {
                                             Artista = alb.Artista.Nome,
                                             Titulo = alb.Titulo
                                         };
                foreach (var item in artistaEalbumQuery)
                    Console.WriteLine("{0}\t{1}", item.Artista, item.Titulo);

                var agrupamento = from g in artistaEalbumQuery
                                  group g by g.Artista into total
                                  let totalTitulo = total.Count()
                                  let nomeArtista = total.Key
                                  orderby nomeArtista
                                  select new { Artista = nomeArtista, TotalTitulo = totalTitulo };
                foreach (var item in agrupamento)
                    Console.WriteLine("{0} tem {1} título{2}", item.Artista.PadRight(60), item.TotalTitulo, item.TotalTitulo > 1 ? "s" : "");
            }
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine("----------------------8 - Evitando Repetição de Expressões----------------------------");
            using (var contexto = new AluraTunesEntities())
            {
                var query =
                             from inf in contexto.ItensNotasFiscais
                             where inf.Faixa.Album.Artista.Nome == "Led Zeppelin"
                             group inf by inf.Faixa.Album into agrupado
                             let totalSomatoria = agrupado.Sum(a => a.PrecoUnitario * a.Quantidade)
                             orderby totalSomatoria descending
                             select new
                             {
                                 Album = agrupado.Key.Titulo,
                                 Valor = totalSomatoria,
                                 NumeroVendas = agrupado.Count()
                             };
            }
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine("----------------------1 - Linq to entities min max avg----------------------------");
            using (var contexto = new AluraTunesEntities())
            {
                contexto.Database.Log = Console.WriteLine;
                var queryMaiorVemnda = contexto.NotasFiscais.Max(nf => nf.Total);
                Console.WriteLine("Maior venda é de R${0}", queryMaiorVemnda);
                var queryMenorVemnda = contexto.NotasFiscais.Min(nf => nf.Total);
                Console.WriteLine("Menor venda é de R${0}", queryMenorVemnda);
                var queryMmediaVemnda = contexto.NotasFiscais.Average(nf => nf.Total);
                Console.WriteLine("Média de venda é de R${0}", queryMmediaVemnda);
                Console.WriteLine("--------------------------------------------------");
                var calculoTotal = (from nf
                                     in contexto.NotasFiscais
                                    group nf by 1 into agrupadorNf
                                    select new
                                    {
                                        ValorMaximo = agrupadorNf.Max(nf => nf.Total),
                                        ValorMinimo = agrupadorNf.Min(nf => nf.Total),
                                        ValorMedio = agrupadorNf.Average(nf => nf.Total)
                                    }).Single();
                Console.WriteLine("R${0} valor máximo \nR${1} valor mínimo\nR${2} valor médio", calculoTotal.ValorMaximo, calculoTotal.ValorMinimo, calculoTotal.ValorMedio);
            }
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine("----------------------2 - Linq métodos extensão - Calculo de mediana----------------------------");
            using (var contexto = new AluraTunesEntities())
            {
                contexto.Database.Log = Console.WriteLine;

                var vendaMedia = contexto.NotasFiscais.Average(nf => nf.Total);
                Console.WriteLine("Venda média: {0}", vendaMedia);

                var queryNF = from nf in contexto.NotasFiscais select nf.Total;

                //Calculando a mediana
                var totalNf = queryNF.Count();
                //Ordena a lista por conta do Skip
                var queryNFOrdenada = queryNF.OrderBy(total => total);
                //Skip precisa que a lista precisa estar ordenada
                var elementoCentral = queryNFOrdenada.Skip(totalNf / 2).First();

                var mediana = elementoCentral;


                Console.WriteLine("Mediana {0}", mediana);
            }
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine("----------------------3 - Linq métodos extensão - Calculo de mediana----------------------------");
            using (var contexto = new AluraTunesEntities())
            {
                contexto.Database.Log = Console.WriteLine;

                var vendaMedia = contexto.NotasFiscais.Average(nf => nf.Total);
                Console.WriteLine("Venda média: {0}", vendaMedia);

                var queryNF = from nf in contexto.NotasFiscais select nf.Total;
                decimal mediana = Mediana(queryNF);

                Console.WriteLine("Mediana por função {0}", mediana);

                decimal vendaMediana = contexto.NotasFiscais.Mediana(m => m.Total);

                Console.WriteLine("Mediana por método de extensão {0}", mediana);
            }
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine("----------------------5 - Calculando Menor Valor----------------------------");
            using (var contexto = new AluraTunesEntities())
            {
                contexto.Database.Log = Console.WriteLine;

                //Marque o trecho de código que seleciona o(s) pokemon(s) menos resistente(s):
                var pokemons = new[]
                {
                    new { Nome = "Pidgey", HP = 14 },
                    new { Nome = "Ratata", HP = 21 },
                    new { Nome = "Pidgeotto", HP = 52 },
                    new { Nome = "Zubat", HP = 25 },
                    new { Nome = "Pikachu", HP = 33 }
                };

                var menorHP = pokemons.Select(p => p.HP).Min();
                var pokemon = pokemons.Where(p => p.HP == menorHP);
                /*
                 * CORRETO: primeiro o valor do menor HP (menor resistência) é calculado, e em seguida obtem-se o(s) pokemon(s) que possuem o menor HP.
                 */
                foreach (var pok in pokemon)
                    Console.WriteLine("Pokemon com menor HP é {0} {1}", pok.Nome, pok.HP);



            }
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine("----------------------6 - Calculando a Média----------------------------");
            using (var contexto = new AluraTunesEntities())
            {
                contexto.Database.Log = Console.WriteLine;

                //Marque o trecho de código que seleciona a resistência média dos pokemons:
                var pokemons = new[]
                {
                    new { Nome = "Pidgey", HP = 14 },
                    new { Nome = "Ratata", HP = 21 },
                    new { Nome = "Pidgeotto", HP = 52 },
                    new { Nome = "Zubat", HP = 25 },
                    new { Nome = "Pikachu", HP = 33 }
                };

                var media = pokemons.Select(p => p.HP).Average();
                var resistenciaMedia = pokemons.Average(p => p.HP);
                /*
                 * CORRETO: o método Average está sendo usado para obter a média da propriedade HP da lista de pokemons.
                 */
                Console.WriteLine("Média de HP é {0}", media);
                Console.WriteLine("Média de HP é {0}", resistenciaMedia);



            }
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine("----------------------7 - Calculando Maior Valor----------------------------");
            using (var contexto = new AluraTunesEntities())
            {
                contexto.Database.Log = Console.WriteLine;

                //Qual alternativa calcula a maior resistência entre os pokemons cujo nome NÃO começa com a letra "P"?
                var pokemons = new[]
                {
                    new { Nome = "Pidgey", HP = 14 },
                    new { Nome = "Ratata", HP = 21 },
                    new { Nome = "Pidgeotto", HP = 52 },
                    new { Nome = "Zubat", HP = 25 },
                    new { Nome = "Pikachu", HP = 33 }
                };

                var media = pokemons.Where(p => !p.Nome.StartsWith("P")).Select(p => p.HP).Max();
                var resistenciaMedia = pokemons.Where(p => !p.Nome.StartsWith("P")).Max(p => p.HP);
                /*
                 * CORRETO: a cláusula Where está filtrando a consulta para trazer somente os pokemons cujo nome não 
                 * comece com a letra "P", e o método Max especifica a propriedade cujo valor máximo deve ser encontrado.
                 */
                Console.WriteLine("Maior de HP é {0}", media);
                Console.WriteLine("Maior de HP é {0}", resistenciaMedia);
            }
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine("----------------------8 - Métodos de Extensão - Teoria----------------------------");
            using (var contexto = new AluraTunesEntities())
            {
                contexto.Database.Log = Console.WriteLine;

                //Qual alternativa calcula a maior resistência entre os pokemons cujo nome NÃO começa com a letra "P"?
                var pokemons = new[]
                {
                    new { Nome = "Pidgey", HP = 14 },
                    new { Nome = "Ratata", HP = 21 },
                    new { Nome = "Pidgeotto", HP = 52 },
                    new { Nome = "Zubat", HP = 25 },
                    new { Nome = "Pikachu", HP = 33 }
                };

                var media = pokemons.Where(p => !p.Nome.StartsWith("P")).Select(p => p.HP).Max();
                var resistenciaMedia = pokemons.Where(p => !p.Nome.StartsWith("P")).Max(p => p.HP);
                /*
                 * CORRETO: a cláusula Where está filtrando a consulta para trazer somente os pokemons cujo nome não 
                 * comece com a letra "P", e o método Max especifica a propriedade cujo valor máximo deve ser encontrado.
                 */
                Console.WriteLine("Maior de HP é {0}", media);
                Console.WriteLine("Maior de HP é {0}", resistenciaMedia);
            }
            Console.WriteLine("--------------------------------------------------");


            Console.WriteLine("----------------------9 - Métodos de Extensão - Prática----------------------------");
            var tiposSanguineos = new List<TipoSanguineo>
            {
                new TipoSanguineo { Codigo = "A" },
                new TipoSanguineo { Codigo = "B" },
                new TipoSanguineo { Codigo = "AB" },
                new TipoSanguineo { Codigo = "O" },
            };

            Console.WriteLine("Primeiro da lista {0}", tiposSanguineos.First().Codigo);
            Console.WriteLine("Segundo da lista {0}", tiposSanguineos.Second1().Codigo);
            Console.WriteLine("Segundo da lista {0}", tiposSanguineos.Second2().Codigo);
            Console.WriteLine("Segundo da lista {0}", tiposSanguineos.Second3().Codigo);
            Console.WriteLine("--------------------------------------------------");
        }

        private static decimal Mediana(IQueryable<decimal> queryNF)
        {
            //Calculando a mediana
            var totalNf = queryNF.Count();
            //Ordena a lista por conta do Skip
            var queryNFOrdenada = queryNF.OrderBy(total => total);
            //Skip precisa que a lista precisa estar ordenada
            var elementoCentral1 = queryNFOrdenada.Skip(totalNf / 2).First();
            var elementoCentral2 = queryNFOrdenada.Skip((totalNf - 1) / 2).First();

            var mediana = (elementoCentral1 + elementoCentral2) / 2;
            return mediana;
        }

        private static void GetFaixas(AluraTunesEntities contexto, string nomeArtista, string album)
        {
            var queryFiltroDinamico = from f in contexto.Faixas select f;

            if (!string.IsNullOrEmpty(nomeArtista))
                queryFiltroDinamico = queryFiltroDinamico.Where(f => f.Album.Artista.Nome.Contains(nomeArtista));

            if (!string.IsNullOrWhiteSpace(album))
                queryFiltroDinamico = queryFiltroDinamico.Where(f => f.Album.Titulo.Contains(album));

            queryFiltroDinamico = queryFiltroDinamico.OrderBy(o => o.Album.Titulo).ThenBy(o => o.Nome);

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

    static class LinqExtension
    {
        public static decimal Mediana<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal>> selector)
        {
            //Calculando a mediana
            var totalNf = source.Count();

            var funcSeletor = selector.Compile();

            //Ordena a lista por conta do Skip
            var queryNFOrdenada = source.Select(funcSeletor).OrderBy(total => total);
            //Skip precisa que a lista precisa estar ordenada
            var elementoCentral1 = queryNFOrdenada.Skip(totalNf / 2).First();
            var elementoCentral2 = queryNFOrdenada.Skip((totalNf - 1) / 2).First();

            if ((totalNf % 2) == 0)//caso par, considera os 2 medianos
                return (elementoCentral1 + elementoCentral2) / 2;
            //Caso impar, consideram somente o valor da primeira
            return elementoCentral1  / 2;
        }

        public static TSource Second1<TSource>(this IEnumerable<TSource> source)
        {
            return source.Skip(1).First();
        }

        public static TSource Second2<TSource>(this IEnumerable<TSource> source)
        {
            return source.ElementAt(1);
        }

        public static TSource Second3<TSource>(this IEnumerable<TSource> source)
        {
            return source.ToArray()[1];
        }
    }

    class TipoSanguineo
    {
        public string Codigo { get; set; }
    }
}