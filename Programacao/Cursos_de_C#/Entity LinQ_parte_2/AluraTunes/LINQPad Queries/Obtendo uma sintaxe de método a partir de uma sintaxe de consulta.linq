<Query Kind="Program" />

void Main()
{
	var numeros = new int[] {1,2,3,4,5,6,7,8,9,10};
	
	var impares = from n in numeros.AsQueryable()
	              where n % 2 == 1
	              select n;
	impares.Dump();
}

// Define other methods and classes here
