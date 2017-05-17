using LojaComEntity.Entidades;
using Microsoft.Data.Entity;
using System.Linq;

namespace LojaComEntity.DAO
{
    public class CategoriaDao
    {
        private EntidadeContext contexto;

        public CategoriaDao()
        {
            contexto = new EntidadeContext();
        }

        public void Salva(Categoria categoria)
        {
            contexto.Categorias.Add(categoria);
            contexto.SaveChanges();
            //contexto.Dispose();
        }

        public void SaveChanges(Categoria categoria)
        {
            contexto.SaveChanges();
            //contexto.Dispose();
        }

        public Categoria BuscaPorId(int id)
        {
            var categoria = contexto.Categorias.Include(pr => pr.Produtos).FirstOrDefault(u => u.ID == id);
            //contexto.Dispose();
            return categoria;
        }

        public void Remove(Categoria categoria)
        {
            contexto.Categorias.Remove(categoria);
            contexto.SaveChanges();
            //contexto.Dispose();
        }
    }
}
