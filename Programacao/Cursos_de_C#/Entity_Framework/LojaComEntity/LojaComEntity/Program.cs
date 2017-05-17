using Microsoft.Data.Entity;
using System;
using System.Linq;
using Microsoft.Data.Entity;

namespace LojaComEntity
{
    class Program
    {
        static void Main(string[] args)
        {
            //var dao = new UsuarioDao();
            //var renato = new Entidades.Usuario()
            //{
            //    Nome = "Marcelo",
            //    Senha = "523"
            //};
            //dao.Salva(renato);
            //Console.WriteLine("Salvou o usuário");
            ////Unchanged
            //var usuario = dao.BuscaPorId(1);
            ////Modified
            //usuario.Nome = "Marcio de Almeida Rosa";
            //dao.SaveChanges(usuario);
            //Console.WriteLine("Usuário salvo com sucesso.");
            EntidadeContext contexto = new EntidadeContext();

            //Categoria c = new Categoria() { Nome = "Informática" };
            //contexto.Categorias.Add(c);
            //contexto.SaveChanges();

            //Produto p = new Produto()
            //{
            //    Nome = "Mouse",
            //    Preco = 20,
            //    Categoria = c
            //};


            //contexto.Produtos.Add(p);
            //contexto.SaveChanges();

            //Produto p = new Produto()
            //{
            //    Nome = "Teclado",
            //    Preco = 20,
            //    CategoriaID = 1
            //};
            //contexto.Produtos.Add(p);
            //contexto.SaveChanges();


            //var p = contexto.Produtos.Include(produto => produto.Categoria).FirstOrDefault(pr => pr.ID == 1);

            //Console.WriteLine("Seu produto é " + p.Nome + " da categoria " + p.Categoria.Nome) ;

            var categorias = contexto.Categorias.Include(cat => cat.Produtos).FirstOrDefault(ca => ca.ID == 1);
            foreach(var p in categorias.Produtos)
            {
                Console.WriteLine("Seu produto é " + p.Nome);
            }


            Console.ReadLine();
        }
    }
}
