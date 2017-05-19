<Query Kind="Program">
  <Connection>
    <ID>b54dcb6d-9f70-4f36-b4b9-f4e03093a543</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>D:\GIT\Cursos_Alura\Programacao\Cursos_de_C#\Entity LinQ_parte_2\AluraTunes\bin\Debug\AluraTunes.exe</CustomAssemblyPath>
    <CustomTypeName>AluraTunes.Data.AluraTunesEntities</CustomTypeName>
    <AppConfigPath>D:\GIT\Cursos_Alura\Programacao\Cursos_de_C#\Entity LinQ_parte_2\AluraTunes\bin\Debug\AluraTunes.exe.config</AppConfigPath>
  </Connection>
</Query>

void Main()
{
	using (var contexto = new AluraTunesEntities())
	            {
	                //definição de consulta
	                var queryGeneros = from listaGeneros in contexto.Generos select listaGeneros;
	                //imprimir consulta no console
					queryGeneros.Dump();
	
	                var queryFaixasGeneros = from faixas in contexto.Faixas
	                                         join listaGeneros in queryGeneros
	                                         on faixas.GeneroId equals listaGeneros.GeneroId
	                                         select new { faixas, listaGeneros };
	
	                //Aplicando paginação - limitando 10 linha de retorno
	                //Aplicando filtro limitando a 10, será aplicado direto na pesquisa no momento do uso "foreach"
	                queryFaixasGeneros = queryFaixasGeneros.Take(10);
	
	                //Aplicando condifuração para aplicar o log
	                contexto.Database.Log = Console.WriteLine;
	
	                queryFaixasGeneros.Dump();
	
	                
	            }
}

// Define other methods and classes here
