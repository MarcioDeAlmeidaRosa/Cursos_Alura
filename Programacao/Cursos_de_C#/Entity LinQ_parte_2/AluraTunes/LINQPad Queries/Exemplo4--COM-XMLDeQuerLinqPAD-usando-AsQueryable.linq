<Query Kind="Program" />

void Main()
{
                string caminhoPrograma = System.AppDomain.CurrentDomain.BaseDirectory.ToString().Replace(@"bin\Debug\", "");
				caminhoPrograma = @"D:\GIT\Cursos_Alura\Programacao\Cursos_de_C#\Entity LinQ_parte_2\AluraTunes\LINQPad Queries";
	            //Variável que vai representar a raiz do XML
	            //no=> AluraTunes
	            XElement root = XElement.Load(string.Format("{0}{1}", caminhoPrograma, @"\AluraTunes.xml"));
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
	
	            qyeryXMLMusicaEGenero.Dump();
	            Console.WriteLine("--------------------------------------------------");
	
	            XElement xmlRootAutomovel = XElement.Load(string.Format("{0}{1}", caminhoPrograma, @"\Automoveis.xml"));
	
	            var queryAutomovel = from fabricantes in xmlRootAutomovel.Element("Fabricantes").Elements("Fabricante")
	                                 join modelos in xmlRootAutomovel.Element("Modelos").Elements("Modelo")
	                                 on fabricantes.Element("FabricanteId").Value equals modelos.Element("FabricanteId").Value
	                                 select new { Fabricante = fabricantes.Element("Nome").Value, Modelo = modelos.Element("Nome").Value };
	
	            queryAutomovel.Dump();
	
	            Console.WriteLine("--------------------------------------------------");
	            XElement rootMusica = XElement.Load(caminhoPrograma + @"/AluraTunesCompleto.xml");
	            var queryMusica = from musicasLista
	                                in rootMusica.Element("Musicas").Elements("Musica")
	                              join generosLista in rootMusica.Element("Generos").Elements("Genero")
	                              on musicasLista.Element("GeneroId").Value equals generosLista.Element("GeneroId").Value
	                              select new { MusicaID = musicasLista.Element("MusicaId").Value, MusicaNome = musicasLista.Element("Nome").Value, GeneroNome = generosLista.Element("Nome").Value };
	            queryMusica.Dump();
}

// Define other methods and classes here
