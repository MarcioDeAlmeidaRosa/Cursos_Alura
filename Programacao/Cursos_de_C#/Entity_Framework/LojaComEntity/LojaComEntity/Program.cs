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

            decimal precoMinimo = 100;
            string nome = string.Empty;
            string categoria = string.Empty;
            categoria = "Roupas";

            var prodDAO = new ProdutoDao();
            var busca = prodDAO.BuscaPorPrecoNomeCategoria(precoMinimo , nome, categoria);

            foreach(var produto in busca)
            {
                Console.WriteLine(produto.Nome + " - R$" + produto.Preco );
            }
            Console.ReadLine();
        }
    }
}
