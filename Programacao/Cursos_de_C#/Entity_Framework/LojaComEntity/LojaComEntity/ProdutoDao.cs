using Microsoft.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace LojaComEntity
{
    public class ProdutoDao
    {
        private EntidadeContext contexto;

        public ProdutoDao()
        {
            contexto = new EntidadeContext();
        }

        public void Salva(Produto produto)
        {
            contexto.Produtos.Add(produto);
            contexto.SaveChanges();
            //contexto.Dispose();
        }

        public void SaveChanges(Produto produto)
        {
            contexto.SaveChanges();
            //contexto.Dispose();
        }

        public Produto BuscaPorId(int id)
        {
            var produto = contexto.Produtos.Include(pr=> pr.Categoria).FirstOrDefault(u => u.ID == id);
            //contexto.Dispose();
            return produto;
        }

        public IList<Produto> BuscaPorPrecoNomeCategoria(decimal precoMinimo,string nome, string categoria)
        {
            var busca = contexto.Produtos.AsQueryable();

            if (precoMinimo > 0)
            {
                //busca = from produto
                //          in busca
                //        where produto.Preco >= precoMinimo
                //        select produto;
                busca = busca.Where(produto => produto.Preco >= precoMinimo);
            }

            if (!string.IsNullOrWhiteSpace(nome))
            {
                //busca = from produto 
                //          in busca
                //       where produto.Nome == nome
                //      select produto;
                busca = busca.Where(produto => produto.Nome == nome);
            }

            if (!string.IsNullOrWhiteSpace(categoria))
            {
                //busca = from produto
                //          in busca
                //        where produto.Categoria.Nome == categoria
                //        select produto;
                busca = busca.Where(produto => produto.Categoria.Nome == categoria);
            }

            //contexto.Dispose();
            //return produto;
            return busca.ToList();
        }

        public void Remove(Produto produto)
        {
            contexto.Produtos.Remove(produto);
            contexto.SaveChanges();
            //contexto.Dispose();
        }
    }
}
