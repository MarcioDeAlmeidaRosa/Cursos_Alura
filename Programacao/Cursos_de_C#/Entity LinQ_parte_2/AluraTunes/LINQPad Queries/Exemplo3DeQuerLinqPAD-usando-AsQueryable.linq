<Query Kind="Program" />

void Main()
{
	int[] numeros = { 1, 2 };
	
	                    int fator = 10;
	                    IEnumerable<int> query = numeros.AsQueryable().Select(n => n * fator);
	
	                    fator = 20;
	                    //CORRETO: os dois elementos da origem de dados (1, 2) devem ser multiplicados pelo fator 20.
	                    query.Dump();
}

// Define other methods and classes here
