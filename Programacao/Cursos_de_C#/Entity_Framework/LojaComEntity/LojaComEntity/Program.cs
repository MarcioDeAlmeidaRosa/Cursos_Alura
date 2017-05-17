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

            ////Incluindo venda
            //var vendaDao = new VendaDao();
            //var produtos = new List<Produto>();
            //produtos.Add(contexto.Produtos.FirstOrDefault(p => p.ID == 3));
            //produtos.Add(contexto.Produtos.FirstOrDefault(p => p.ID == 4));
            //vendaDao.IncluirVenda(new UsuarioDao().BuscaPorId(1), produtos);



            //Venda venda = vendaDao.RecuperaVenda(2);

            //foreach(var produtoVenda in venda.ProdutoVenda)
            //{
            //    Console.WriteLine(string.Format("Produto vendido: {0}", produtoVenda.Produto.Nome));
            //}

            contexto.PessoasFisica.Add(new PessoaFisica
            {
                Nome = "Guilherme",
                Cpf = "236.96",
                Senha = "123"
            });
            contexto.SaveChanges();
            contexto.PessoasJuridica.Add(new PessoaJuridica
            {
                Nome = "Aura",
                Cnpj = "6366",
                Senha = "563"
            });
            contexto.SaveChanges();

            Console.ReadLine();
        }
    }
}