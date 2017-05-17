using Microsoft.Data.Entity;
using System;
using System.Collections;
using System.Linq;

namespace LojaComEntity
{
    class Program
    {
        static void Main(string[] args)
        {
            EntidadeContext contexto = new EntidadeContext();

            decimal precoMinimo = 60;

            var busca = from p 
                           in contexto.Produtos
                            //where p.Preco > precoMinimo
                            //where p.Categoria.Nome == "Roupas"
                        where p.Categoria.Nome == "Roupas" && 
                              p.Preco > precoMinimo
                        orderby p.Preco
                       select p;

            var resultado = busca.ToList();

            foreach(var produto in resultado)
            {
                Console.WriteLine(produto.Nome + " - R$" + produto.Preco );
            }
            Console.ReadLine();
        }
    }
}
