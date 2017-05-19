<Query Kind="Program" />

public class Aluno
    {
        public string Escola { get; set; }
        public string Curso { get; set; }
        public string AnoLetivo { get; set; }
        public string Nome { get; set; }
    };
	
void Main()
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
				        alunosQuery.Dump();
						
	
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
									
									lista.Dump();
}

// Define other methods and classes here
