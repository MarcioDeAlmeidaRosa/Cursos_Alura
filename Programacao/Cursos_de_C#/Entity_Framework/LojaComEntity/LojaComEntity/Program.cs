using LojaComEntity.DAO;
using LojaComEntity.Entidades;
using Microsoft.Data.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LojaComEntity
{
    class Program
    {
        static void Main(string[] args)
        {
            EntidadeContext contexto = new EntidadeContext();

            //var usuarioDAO = new UsuarioDao();
            //var usuario = usuarioDAO.BuscaPorId(1);


            //Venda venda = new Venda()
            //{
            //    Cliente = usuario
            //};

            //venda.ProdutoVenda = new List<ProdutoVenda>();
            //venda.ProdutoVenda.Add(new ProdutoVenda
            //{
            //    Produto = contexto.Produtos.FirstOrDefault(p => p.ID == 1),
            //    Venda = venda
            //});
            //venda.ProdutoVenda.Add(new ProdutoVenda
            //{
            //    Produto = contexto.Produtos.FirstOrDefault(p => p.ID == 2),
            //    Venda = venda
            //});

            //contexto.Vendas.Add(venda);
            //contexto.SaveChanges();

            Venda venda = contexto
                .Vendas
                .Include(v => v.ProdutoVenda)
                .FirstOrDefault(v => v.ID == 1);

            foreach(var produtoVenda in venda.ProdutoVenda)
            {
                Console.WriteLine(string.Format("Produto vendido: {0}", produtoVenda.Produto.Nome));
            }

            Console.ReadLine();
        }
    }
}