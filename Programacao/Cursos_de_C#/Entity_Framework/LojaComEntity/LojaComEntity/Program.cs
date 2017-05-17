using Microsoft.Data.Entity;
using System;
using System.Linq;

namespace LojaComEntity
{
    class Program
    {
        static void Main(string[] args)
        {
            Categoria c = new Categoria() { Nome = "Informática" };
            CategoriaDao catDao = new CategoriaDao();
            catDao.Salva(c);

            Produto p = new Produto()
            {
                Nome = "Mouse",
                Preco = 20,
                Categoria = c
            };
            ProdutoDao prDao = new ProdutoDao();
            prDao.Salva(p);



            p = new Produto()
            {
                Nome = "Teclado",
                Preco = 20,
                CategoriaID = 1
            };
            prDao.Salva(p);


            p = prDao.BuscaPorId(1);

            Console.WriteLine("Seu produto é " + p.Nome + " da categoria " + p.Categoria.Nome) ;

            var categorias = catDao.BuscaPorId(1);
            foreach(var pro in categorias.Produtos)
            {
                Console.WriteLine("Seu produto é " + pro.Nome);
            }


            Console.ReadLine();
        }
    }
}
