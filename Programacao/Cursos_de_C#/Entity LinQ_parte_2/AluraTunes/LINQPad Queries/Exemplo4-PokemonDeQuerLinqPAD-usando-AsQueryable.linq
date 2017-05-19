<Query Kind="Program" />

void Main()
{
	var pokemons = new[]
	                {
	                    new { Nome = "Pidgey", HP = 14 },
	                    new { Nome = "Ratata", HP = 21 },
	                    new { Nome = "Pidgeotto", HP = 52 },
	                    new { Nome = "Zubat", HP = 25 },
	                    new { Nome = "Pikachu", HP = 33 }
	                }.AsQueryable();
	pokemons.Dump();
	                var media = pokemons.Where(p => !p.Nome.StartsWith("P")).Select(p => p.HP).Max();
					media.Dump();
	                var resistenciaMedia = pokemons.Where(p => !p.Nome.StartsWith("P")).Max(p => p.HP);
					resistenciaMedia.Dump();
}

// Define other methods and classes here
