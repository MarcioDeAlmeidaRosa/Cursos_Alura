﻿using AluraTunes.Data;
using AluraTunes.Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;
using ZXing;

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

            if (EXECUTAR_TODOS_EXERCICIOS)
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

            if (EXECUTAR_TODOS_EXERCICIOS)
            {
                #region 07 - Acessando elementos de uma página
                Console.WriteLine("----------------------07 - Acessando elementos de uma página----------------------------");
                using (var contexto = new AluraTunesEntities())
                {
                    var query = from nf in contexto.NotasFiscais
                                orderby nf.NotaFiscalId
                                select new
                                {
                                    Numero = nf.NotaFiscalId,
                                    Data = nf.DataNotaFiscal,
                                    Cliente = nf.Cliente.PrimeiroNome + " " + nf.Cliente.Sobrenome,
                                    Total = nf.Total
                                };

                    //Qual o código necessário para obter os elementos da terceira página de um relatório sobre a consulta acima, considerando que cada página contém 20 elementos.
                    query = query.Skip(40).Take(20);

                    foreach (var nf in query)
                    {
                        Console.WriteLine("{0}\t{1}\t{2}\t{3}", nf.Numero, nf.Data, nf.Cliente, nf.Total);
                    }
                }
                Console.WriteLine("--------------------------------------------------");
                #endregion
            }

            if (EXECUTAR_TODOS_EXERCICIOS)
            {
                #region 08 - Obtendo uma sequência de elementos a partir de uma posição
                Console.WriteLine("----------------------08 - Obtendo uma sequência de elementos a partir de uma posição----------------------------");
                using (var contexto = new AluraTunesEntities())
                {

                    var query = from c in contexto.Clientes
                                orderby c.ClienteId
                                select new { c.ClienteId, Nome = c.PrimeiroNome + " " + c.Sobrenome };

                    var pos = 9;
                    query = query.Skip(pos - 1).Take(pos);


                    foreach (var cliente in query)
                    {
                        Console.WriteLine("{0}\t{1}", pos++, cliente.Nome);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("--------------------------------------------------");
                #endregion
            }

            if (EXECUTAR_TODOS_EXERCICIOS)
            {
                #region 09 - Desenvolvendo algoritmo de paginação
                Console.WriteLine("----------------------09 - Desenvolvendo algoritmo de paginação----------------------------");
                using (var contexto = new AluraTunesEntities())
                {
                    var totalRegistro = contexto.NotasFiscais.Count();
                    const int TOTAL_POR_PAGINA = 5;
                    var totalPagina = Math.Ceiling((decimal)totalRegistro / TOTAL_POR_PAGINA);

                    for (var pagina = 1; pagina <= totalPagina; pagina++)
                        ImprimirPagina(contexto, TOTAL_POR_PAGINA, pagina);
                }
                Console.WriteLine("--------------------------------------------------");
                #endregion
            }

            if (EXECUTAR_TODOS_EXERCICIOS)
            {
                #region 1 - Comprou tambem
                Console.WriteLine("----------------------1 - Comprou tambem----------------------------");
                using (var contexto = new AluraTunesEntities())
                {
                    Console.WriteLine("-----------------------ANÁLISE DE AFINIDADE---------------------------");
                    //ANÁLISE DE AFINIDADE --> verifica outros itens comrados por outras pessoas relacionado ao item que você esta comprado
                    //estimular venda casada

                    //Nome da música que esta sendo comprada
                    var nomeDaMusica = "Smells Like Teen Spirit";

                    //Lista um range de faixas de música com este nome
                    var faixasIDs = contexto.Faixas.Where(f => f.Nome == nomeDaMusica).Select(f => f.FaixaId);

                    //Filtra todos os itens de notas fiscais relacionados as faixas encontrada com base no nome do filtro
                    var queryItensNotas = from comprouItem
                                            in contexto.ItensNotasFiscais
                                              //usando selfjoin
                                          join comprouTambem in contexto.ItensNotasFiscais
                                            on comprouItem.NotaFiscalId equals comprouTambem.NotaFiscalId
                                          where faixasIDs.Contains(comprouItem.FaixaId)
                                             && comprouItem.FaixaId != comprouTambem.FaixaId
                                          select comprouTambem;

                    foreach (var item in queryItensNotas)
                    {
                        Console.WriteLine("{0}\t{1}", item.NotaFiscalId, item.Faixa.Nome);
                    }

                }
                Console.WriteLine("--------------------------------------------------");
                #endregion
            }

            if (EXECUTAR_TODOS_EXERCICIOS)
            {
                #region 4 - Consulta para análise de afinidade
                Console.WriteLine("----------------------4 - Consulta para análise de afinidade----------------------------");
                using (var contexto = new AluraTunesEntities())
                {
                    Console.WriteLine("-----------------------ANÁLISE DE AFINIDADE---------------------------");
                    //ANÁLISE DE AFINIDADE --> verifica outros itens comrados por outras pessoas relacionado ao item que você esta comprado
                    //estimular venda casada
                    /*
                     * Desenvolva uma consulta LINQ para trazer um relatório de análise de afinidade para um produto com FaixaId == 1.
                     */
                    //Variável que esta armazenando o id da faixa que esta sendo comprada pelo cliente
                    var faixaId = 1;

                    var queryAfinidade = from itensComprados
                                         in contexto.ItensNotasFiscais
                                         join comprouTambem in contexto.ItensNotasFiscais on itensComprados.NotaFiscalId equals comprouTambem.NotaFiscalId
                                         where itensComprados.FaixaId == faixaId
                                           && itensComprados.FaixaId != comprouTambem.FaixaId
                                         orderby comprouTambem.FaixaId
                                         select comprouTambem;

                    foreach (var item in queryAfinidade)
                    {
                        Console.WriteLine("{0}\t{1}\t{2}", item.NotaFiscalId, item.Faixa.Nome.PadRight(80), item.FaixaId);
                    }
                }
                Console.WriteLine("--------------------------------------------------");
                #endregion
            }

            if (EXECUTAR_TODOS_EXERCICIOS)
            {
                #region 1 - Execucao tardia - Execucao imediata
                Console.WriteLine("----------------------1 - Execucao tardia - Execucao imediata----------------------------");
                using (var contexto = new AluraTunesEntities())
                {
                    Console.WriteLine("-----------------------RELATÓRIO DE ANIVERSARIANTES DO MÊS---------------------------");
                    var mesAniversario = 1;

                    Console.WriteLine("------------------------EXEMPLO DE CONSULTA TARDIA--------------------------");
                    while (mesAniversario <= 12)
                    {
                        Console.WriteLine("--------------------------------------------------");
                        Console.WriteLine("Mês {0}", mesAniversario);
                        //Exemlo de consulta tardia, pois na linnha de baixo nó só criamos uma query
                        //e na primeira vez que executar esse código, o valor de mesAniversario = 1
                        //É armazenada a referência desta consulta na memória
                        var queryFuncionario = from f
                                                 in contexto.Funcionarios
                                               where f.DataNascimento.Value.Month == mesAniversario
                                               orderby f.DataNascimento.Value.Month, f.DataNascimento.Value.Day
                                               select f;
                        //Não sequencia nós incrementamos a variáve mesAniversario que irá asssumir 2
                        mesAniversario++;
                        //Nesse momento que a consulta é executada, sendo assim
                        //no momento eem que nós incrementamos o vallor da variável mesAniversario
                        //foi atualizado a reverência da memória, trocando o valor 1 que foi passsado
                        //inicialmente ara o valor 2
                        //com isso, o primeiro Console.WriteLine imprimiu o valor da variável como 1
                        //porém a pesquisa efetivou a busca com o valor 2
                        foreach (var funcionario in queryFuncionario)
                            Console.WriteLine("{0:dd/MM}\t{1} {2}", funcionario.DataNascimento, funcionario.PrimeiroNome, funcionario.Sobrenome);
                    }

                    mesAniversario = 1;

                    Console.WriteLine("-------------------------EXEMPLO DE EXECUÇÃO IMEDIATA-------------------------");
                    while (mesAniversario <= 12)
                    {
                        Console.WriteLine("--------------------------------------------------");
                        Console.WriteLine("Mês {0}", mesAniversario);

                        var listaFuncionario = (from f
                                                 in contexto.Funcionarios
                                                where f.DataNascimento.Value.Month == mesAniversario
                                                orderby f.DataNascimento.Value.Month, f.DataNascimento.Value.Day
                                                select f).ToList();

                        mesAniversario++;

                        foreach (var funcionario in listaFuncionario)
                            Console.WriteLine("{0:dd/MM}\t{1} {2}", funcionario.DataNascimento, funcionario.PrimeiroNome, funcionario.Sobrenome);
                    }
                }
                Console.WriteLine("--------------------------------------------------");
                #endregion
            }

            if (EXECUTAR_TODOS_EXERCICIOS)
            {
                #region 2 - Adicionando elementos após a definição de uma consulta
                Console.WriteLine("----------------------2 - Adicionando elementos após a definição de uma consulta----------------------------");
                using (var contexto = new AluraTunesEntities())
                {
                    var numeros = new List<int>();
                    numeros.Add(1);

                    var query
                        = numeros.Select(n => n * 10);

                    numeros.Add(2);
                    //CORRETO: a consulta query referencia a variável numeros. A definição de consulta 
                    ///query só é avaliada quando a consulta é enumerada pela instrução foreach na linha 
                    /////foreach (var numero in query), então nesse momento a lista numeros já contém 2 elementos.
                    foreach (var numero in query)
                        Console.WriteLine(numero);
                }
                Console.WriteLine("--------------------------------------------------");
                #endregion
            }

            if (EXECUTAR_TODOS_EXERCICIOS)
            {
                #region 3 - Modificando variáveis de uma consulta em tempo de execução
                Console.WriteLine("----------------------3 - Modificando variáveis de uma consulta em tempo de execução----------------------------");
                using (var contexto = new AluraTunesEntities())
                {
                    int[] numeros = { 1, 2 };

                    int fator = 10;
                    IEnumerable<int> query = numeros.Select(n => n * fator);

                    fator = 20;
                    //CORRETO: os dois elementos da origem de dados (1, 2) devem ser multiplicados pelo fator 20.
                    foreach (var numero in query)
                        Console.WriteLine(numero);

                    Console.WriteLine("A consulta trouxe {0} elementos.", query.Count());
                }
                Console.WriteLine("--------------------------------------------------");
                #endregion
            }

            if (EXECUTAR_TODOS_EXERCICIOS)
            {
                #region 4 - Remoção de elementos após a definição de uma consulta
                Console.WriteLine("----------------------4 - Remoção de elementos após a definição de uma consulta----------------------------");
                using (var contexto = new AluraTunesEntities())
                {
                    var numeros = new List<int>() { 1, 2 };

                    var query = numeros.Select(n => n * 10);

                    numeros.Clear();
                    //CORRETO: somente na linha foreach(var numero in query) a consulta query é avaliada, 
                    ///e nesse ponto todos os elementos da origem de dados numeros já tinham sido removidos 
                    ////pela linha numeros.Clear();
                    foreach (var numero in query)
                        Console.WriteLine(numero);
                }
                Console.WriteLine("--------------------------------------------------");
                #endregion
            }

            if (EXECUTAR_TODOS_EXERCICIOS)
            {
                #region 5 - Execução adiada
                Console.WriteLine("----------------------5 - Execução adiada----------------------------");
                using (var contexto = new AluraTunesEntities())
                {
                    var palavras = new List<string>() { "ALURA", "CURSOS" };

                    var maiusculas = palavras
                      .Select(p => p.ToLower());

                    palavras.Clear();

                    //Porque a consulta maiusculas havia sido declarada, mas ainda não havia 
                    //sido executada. Ela só é executada na linha foreach(var palavra in maiusculas), 
                    //e nesse momento a origem de dados palavras já tinha sido esvaziada pela linha palavras.Clear();.
                    foreach (var palavra in maiusculas)
                        Console.WriteLine(palavra);

                    Console.WriteLine("A consulta trouxe {0} elementos.", maiusculas.Count());
                }
                Console.WriteLine("--------------------------------------------------");
                #endregion
            }

            if (EXECUTAR_TODOS_EXERCICIOS)
            {
                #region 5 - Execução adiada
                Console.WriteLine("----------------------5 - Execução adiada----------------------------");
                using (var contexto = new AluraTunesEntities())
                {
                    var palavras = new List<string>() { "alura", "cursos" };

                    var maiusculas = palavras
                      .Select(p => p.ToUpper())
                      .ToList();

                    palavras.Clear();
                    //Porque a instrução Console.WriteLine() dentro do laço foreach está imprimindo 
                    //os elementos da lista maiusculas, que é uma lista em memória (e não uma variável de 
                    //consulta) que foi instanciada no momento em que a lista palavras ainda continha os dados 
                    //iniciais. Depois que a lista maiusculas é criada, ela não é mais afetada pelo esvaziamento da lista palavras. 
                    foreach (var palavra in maiusculas)
                        Console.WriteLine(palavra);
                }
                Console.WriteLine("--------------------------------------------------");
                #endregion
            }

            if (EXECUTAR_TODOS_EXERCICIOS)
            {
                #region 1- Linq to entities - Linq paralelo
                Console.WriteLine("----------------------1- Linq to entities - Linq paralelo----------------------------");
                using (var contexto = new AluraTunesEntities())
                {
                    Console.WriteLine("---------------------PROMOÇÃO - CÓDIGO QRCODE-----------------------------");
                    //INSTALAR BIBLIOTECA ZXING
                    // Install-Package ZXing.net
                    //Precisa também instalar o --> System.Drawing
                    //Site que lê qr-cod
                    //https://webqr.com/


                    BarcodeWriter barcodeWriter = new BarcodeWriter();
                    barcodeWriter.Format = BarcodeFormat.QR_CODE;
                    barcodeWriter.Options = new ZXing.Common.EncodingOptions
                    {
                        Width = 100,
                        Height = 100
                    };
                    var caminhoImagem = string.Format("{0}Imagens", caminhoPrograma);
                    if (!Directory.Exists(caminhoImagem))
                        Directory.CreateDirectory(caminhoImagem);

                    //Atribuindo valor para o código e Salvando imagem
                    barcodeWriter.Write("meu primeiro teste de qr-code").Save(string.Format(@"{0}\{1}", caminhoImagem, "meu-teste-qr-cod.jpg"), ImageFormat.Jpeg);

                    barcodeWriter.Write("https://marciodealmeidarosa.github.io/").Save(string.Format(@"{0}\{1}", caminhoImagem, "meu-site.jpg"), ImageFormat.Jpeg);
                    Console.WriteLine("----------------------QR-CODE GERADO----------------------------");


                    var queryFaixas = from f
                                        in contexto.Faixas
                                      select f;

                    var listaFaixasMemoria = queryFaixas.ToList();

                    #region [PROCESSAMENTO SEM PARALELISMO]
                    Console.WriteLine("----------------------PROCESSAMENTO SEM PARALELISMO----------------------------");
                    //Cria timer para contar o tempo
                    var stopwatch = Stopwatch.StartNew();
                    var queryCodigos = listaFaixasMemoria.Select(f => new
                    {
                        NomeArquivo = string.Format("{0}\\{1}.jpg", caminhoImagem, f.FaixaId),
                        Imagem = barcodeWriter.Write(string.Format("www.aluratunes.com/faixa/{0}", f.FaixaId))
                    });
                    //Cria variável para o contador
                    var contagem = queryCodigos.Count();
                    stopwatch.Stop();
                    Console.WriteLine("Carregamentoo de {0} imagems levou {1} ms", contagem, stopwatch.ElapsedMilliseconds);
                    Console.WriteLine("Carregamentoo de {0} imagems levou {1} s", contagem, stopwatch.ElapsedMilliseconds / 1000.0);
                    //Carregamentoo de 3503 imagems levou 2290 ms
                    //Carregamentoo de 3503 imagems levou 2,29 s
                    #endregion

                    #region [PROCESSAMENTO COM PARALELISMO]
                    Console.WriteLine("----------------------PROCESSAMENTO COM PARALELISMO----------------------------");
                    //Cria timer para contar o tempo
                    stopwatch = Stopwatch.StartNew();
                    queryCodigos = listaFaixasMemoria.AsParallel().Select(f => new
                    {
                        NomeArquivo = string.Format("{0}\\{1}.jpg", caminhoImagem, f.FaixaId),
                        Imagem = barcodeWriter.Write(string.Format("www.aluratunes.com/faixa/{0}", f.FaixaId))
                    });
                    //Cria variável para o contador
                    contagem = queryCodigos.Count();
                    stopwatch.Stop();
                    Console.WriteLine("Carregamentoo de {0} imagems levou {1} ms", contagem, stopwatch.ElapsedMilliseconds);
                    Console.WriteLine("Carregamentoo de {0} imagems levou {1} s", contagem, stopwatch.ElapsedMilliseconds / 1000.0);
                    //Carregamentoo de 3503 imagems levou 561 ms
                    //Carregamentoo de 3503 imagems levou 0,561 s
                    #endregion

                    foreach (var faixa in queryCodigos)
                    {
                        faixa.Imagem.Save(faixa.NomeArquivo, ImageFormat.Jpeg);
                    }
                }
                Console.WriteLine("--------------------------------------------------");
                #endregion
            }

            if (EXECUTAR_TODOS_EXERCICIOS)
            {
                #region 2- Linq to entities - Linq paralelo parte 2
                Console.WriteLine("----------------------2- Linq to entities - Linq paralelo parte 2----------------------------");
                using (var contexto = new AluraTunesEntities())
                {
                    Console.WriteLine("---------------------PROMOÇÃO - CÓDIGO QRCODE-----------------------------");
                    BarcodeWriter barcodeWriter = new BarcodeWriter();
                    barcodeWriter.Format = BarcodeFormat.QR_CODE;
                    barcodeWriter.Options = new ZXing.Common.EncodingOptions
                    {
                        Width = 100,
                        Height = 100
                    };
                    var caminhoImagem = string.Format("{0}Imagens", caminhoPrograma);
                    if (!Directory.Exists(caminhoImagem))
                        Directory.CreateDirectory(caminhoImagem);

                    var queryFaixas = from f
                                        in contexto.Faixas
                                      select f;

                    var listaFaixasMemoria = queryFaixas.ToList();


                    Console.WriteLine("----------------------PROCESSAMENTO COM PARALELISMO----------------------------");
                    //Cria timer para contar o tempo
                    var stopwatch = Stopwatch.StartNew();
                    var queryCodigos = listaFaixasMemoria.AsParallel().Select(f => new
                    {
                        NomeArquivo = string.Format("{0}\\{1}.jpg", caminhoImagem, f.FaixaId),
                        Imagem = barcodeWriter.Write(string.Format("www.aluratunes.com/faixa/{0}", f.FaixaId))
                    });
                    //Cria variável para o contador
                    var contagem = queryCodigos.Count();
                    stopwatch.Stop();
                    Console.WriteLine("Carregamentoo de {0} imagems levou {1} ms", contagem, stopwatch.ElapsedMilliseconds);
                    Console.WriteLine("Carregamentoo de {0} imagems levou {1} s", contagem, stopwatch.ElapsedMilliseconds / 1000.0);

                    #region [GERAÇÃO SEM PARALELISMO]
                    stopwatch = Stopwatch.StartNew();
                    foreach (var faixa in queryCodigos)
                    {
                        faixa.Imagem.Save(faixa.NomeArquivo, ImageFormat.Jpeg);
                    }
                    stopwatch.Stop();
                    Console.WriteLine("Geração da {0} imagems levou {1} ms", contagem, stopwatch.ElapsedMilliseconds);
                    Console.WriteLine("Geração da {0} imagems levou {1} s", contagem, stopwatch.ElapsedMilliseconds / 1000.0);
                    //Geraçao da 3503 imagems levou 13380 ms
                    //Geraçao da 3503 imagems levou 13,38 s
                    #endregion [GERAÇÃO SEM PARALELISMO]

                    #region [GERAÇÃO SEM PARALELISMO]
                    stopwatch = Stopwatch.StartNew();
                    queryCodigos.ForAll(arq => arq.Imagem.Save(arq.NomeArquivo, ImageFormat.Jpeg));
                    stopwatch.Stop();
                    Console.WriteLine("Geração da {0} imagems levou {1} ms", contagem, stopwatch.ElapsedMilliseconds);
                    Console.WriteLine("Geração da {0} imagems levou {1} s", contagem, stopwatch.ElapsedMilliseconds / 1000.0);
                    //Geraçao da 3503 imagems levou 3701 ms
                    //Geraçao da 3503 imagems levou 3,701 s
                    #endregion [GERAÇÃO SEM PARALELISMO]

                }
                Console.WriteLine("--------------------------------------------------");
                #endregion
            }

            if (EXECUTAR_TODOS_EXERCICIOS)
            {
                #region 1 - Linq to entities stores procedure
                Console.WriteLine("----------------------1 - Linq to entities stores procedure----------------------------");
                using (var contexto = new AluraTunesEntities())
                {
                    contexto.Database.Log = Console.WriteLine;
                    Console.WriteLine("----------------------RELATÓRIO VENDAS POR CLIENTE AGRUPADA POR ANO E POR MÊS----------------------------");

                    int clienteID = 15;

                    var vendasPorCliente = from c
                                         in contexto.ps_Itens_Por_Cliente(clienteID)
                                           group c by new { c.DataNotaFiscal.Year, c.DataNotaFiscal.Month }
                                         into agrupado
                                           orderby agrupado.Key.Year, agrupado.Key.Month
                                           select new
                                           {
                                               Ano = agrupado.Key.Year,
                                               Mes = agrupado.Key.Month,
                                               Total = agrupado.Sum(a => a.PrecoUnitario * a.Quantidade)
                                           };

                    foreach (var venda in vendasPorCliente)
                    {
                        Console.WriteLine("{0}\t{1}\t{2}", venda.Ano, venda.Mes, venda.Total);
                    }
                }
                Console.WriteLine("--------------------------------------------------");
                #endregion
            }

            if (EXECUTAR_EXERCICIO_ATUAL)
            {
                #region 5 - Agrupando consultas LINQ por mais de uma propriedade
                Console.WriteLine("----------------------5 - Agrupando consultas LINQ por mais de uma propriedade----------------------------");
                using (var contexto = new AluraTunesEntities())
                {
                    var alunos = new List<Aluno>
                    {
                        new Aluno{ AnoLetivo = "2016" , Curso= "C#" , Escola= "Caelum" , Nome = "Marcos"},
                        new Aluno{ AnoLetivo = "2016" , Curso= "F#" , Escola= "Caelum" , Nome = "Marcos"},
                        new Aluno{ AnoLetivo = "2016" , Curso= "Java" , Escola= "Caelum" , Nome = "Marcos"},

                        new Aluno{ AnoLetivo = "2017" , Curso= "F#" , Escola= "Alura" , Nome = "Marcos"},
                        new Aluno{ AnoLetivo = "2017" , Curso= "C#" , Escola= "Alura" , Nome = "Marcos"},
                        new Aluno{ AnoLetivo = "2017" , Curso= "Java" , Escola= "Alura" , Nome = "Marcos"},

                        new Aluno{ AnoLetivo = "2017" , Curso= "C#" , Escola= "Alura" , Nome = "Renato"},
                        new Aluno{ AnoLetivo = "2017" , Curso= "F#" , Escola= "Alura" , Nome = "Renato"},
                        new Aluno{ AnoLetivo = "2017" , Curso= "Java" , Escola= "Alura" , Nome = "Renato"},

                        new Aluno{ AnoLetivo = "2017" , Curso= "C#" , Escola= "Alura" , Nome = "Gustavo"},
                        new Aluno{ AnoLetivo = "2017" , Curso= "F#" , Escola= "Alura" , Nome = "Gustavo"},
                        new Aluno{ AnoLetivo = "2017" , Curso= "Java" , Escola= "Alura" , Nome = "Gustavo"},

                        new Aluno{ AnoLetivo = "2017" , Curso= "C#" , Escola= "Alura" , Nome = "Rodrigo"},
                        new Aluno{ AnoLetivo = "2017" , Curso= "F#" , Escola= "Alura" , Nome = "Rodrigo"},
                        new Aluno{ AnoLetivo = "2017" , Curso= "Java" , Escola= "Alura" , Nome = "Rodrigo"},

                        new Aluno{ AnoLetivo = "2017" , Curso= "C#" , Escola= "Alura" , Nome = "Robsom"},
                        new Aluno{ AnoLetivo = "2017" , Curso= "F#" , Escola= "Alura" , Nome = "Robsom"},
                        new Aluno{ AnoLetivo = "2017" , Curso= "Java" , Escola= "Alura" , Nome = "Robsom"},

                        new Aluno{ AnoLetivo = "2016" , Curso= "C#" , Escola= "Caelum" , Nome = "Anderson"},
                        new Aluno{ AnoLetivo = "2016" , Curso= "F#" , Escola= "Caelum" , Nome = "Anderson"},
                        new Aluno{ AnoLetivo = "2016" , Curso= "Java" , Escola= "Caelum" , Nome = "Anderson"},

                        //--

                        new Aluno{ AnoLetivo = "2016" , Curso= "C#" , Escola= "Ka Solution" , Nome = "Guilherme"},
                        new Aluno{ AnoLetivo = "2016" , Curso= "F#" , Escola= "Ka Solution" , Nome = "Guilherme"},
                        new Aluno{ AnoLetivo = "2016" , Curso= "Java" , Escola= "Ka Solution" , Nome = "Guilherme"},

                        new Aluno{ AnoLetivo = "2017" , Curso= "F#" , Escola= "Ka Solution" , Nome = "Ronaldo"},
                        new Aluno{ AnoLetivo = "2017" , Curso= "C#" , Escola= "Ka Solution" , Nome = "Ronaldo"},
                        new Aluno{ AnoLetivo = "2017" , Curso= "Java" , Escola= "Ka Solution" , Nome = "Ronaldo"},

                        new Aluno{ AnoLetivo = "2017" , Curso= "C#" , Escola= "Ka Solution" , Nome = "Marcio"},
                        new Aluno{ AnoLetivo = "2017" , Curso= "F#" , Escola= "Ka Solution" , Nome = "Marcio"},
                        new Aluno{ AnoLetivo = "2017" , Curso= "Java" , Escola= "Ka Solution" , Nome = "Marcio"},

                        new Aluno{ AnoLetivo = "2017" , Curso= "C#" , Escola= "Bras Figueiredo" , Nome = "Angela"},
                        new Aluno{ AnoLetivo = "2017" , Curso= "F#" , Escola= "Bras Figueiredo" , Nome = "Angela"},
                        new Aluno{ AnoLetivo = "2017" , Curso= "Java" , Escola= "Bras Figueiredo" , Nome = "Angela"},

                        new Aluno{ AnoLetivo = "2017" , Curso= "C#" , Escola= "Bras Figueiredo" , Nome = "Marcela"},
                        new Aluno{ AnoLetivo = "2017" , Curso= "F#" , Escola= "Bras Figueiredo" , Nome = "Marcela"},
                        new Aluno{ AnoLetivo = "2017" , Curso= "Java" , Escola= "Bras Figueiredo" , Nome = "Marcela"},

                        new Aluno{ AnoLetivo = "2017" , Curso= "C#" , Escola= "Bras Figueiredo" , Nome = "Rafaela"},
                        new Aluno{ AnoLetivo = "2017" , Curso= "F#" , Escola= "Bras Figueiredo" , Nome = "Rafaela"},
                        new Aluno{ AnoLetivo = "2017" , Curso= "Java" , Escola= "Bras Figueiredo" , Nome = "Rafaela"}
                    };


                    //Desenvolva uma nova consulta LINQ sobre a mesma lista de alunos, que traga um resultado agregado, contendo:
                    //O nome da escola,
                    //O nome do curso
                    //O ano letivo,

                    var alunosQuery =
                                    from a in alunos
                                    orderby a.AnoLetivo , a.Curso , a.Escola , a.Nome
                                    select new
                                    {
                                        Escola = a.Escola,
                                        Curso = a.Curso,
                                        AnoLetivo = a.AnoLetivo,
                                        Nome = a.Nome
                                    };
                    foreach(var aluno in alunosQuery)
                        Console.WriteLine("{0}\t{1}\t{2}\t{3}", aluno.AnoLetivo , aluno.Curso , aluno.Escola.PadRight(30) , aluno.Nome);

                    Console.WriteLine("--------------------------------------------------");
                    //Uma lista dos alunos que frequentam a mesma escola, no mesmo curso, no mesmo ano letivo
                    var lista = from l
                                  in alunosQuery
                                group l by new { l.AnoLetivo, l.Escola, l.Curso } into agrupado
                                orderby agrupado.Key.AnoLetivo, agrupado.Key.Escola, agrupado.Key.Curso
                                select new
                                {
                                    AnoLetivo = agrupado.Key.AnoLetivo,
                                    Escola = agrupado.Key.Escola,
                                    Curso = agrupado.Key.Curso,
                                    Alunos = agrupado.ToList()
                                };

                    foreach (var aluno in lista) {
                        Console.WriteLine("{0}\t{1}\t{2}", aluno.AnoLetivo, aluno.Curso, aluno.Escola.PadRight(30));
                        foreach(var cursando in aluno.Alunos)
                            Console.WriteLine("\t\t\t\t{0}", cursando.Nome);
                    }

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

    public class Aluno
    {
        public string Escola { get; set; }
        public string Curso { get; set; }
        public string AnoLetivo { get; set; }
        public string Nome { get; set; }
    }
}