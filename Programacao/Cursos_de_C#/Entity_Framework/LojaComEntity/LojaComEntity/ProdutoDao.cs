using Microsoft.Data.Entity;
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

        public void Remove(Produto produto)
        {
            contexto.Produtos.Remove(produto);
            contexto.SaveChanges();
            //contexto.Dispose();
        }
    }
}
