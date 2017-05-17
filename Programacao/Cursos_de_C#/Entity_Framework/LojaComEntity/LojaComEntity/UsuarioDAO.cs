using LojaComEntity.Entidades;
using System.Linq;

namespace LojaComEntity
{
    public class UsuarioDao
    {
        private EntidadeContext contexto;

        public UsuarioDao()
        {
            contexto = new EntidadeContext();
        }

        public void Salva(Usuario usuario)
        {
            contexto.Usuarios.Add(usuario);
            contexto.SaveChanges();
            //contexto.Dispose();
        }

        public void SaveChanges(Usuario usuario)
        {
            contexto.SaveChanges();
            //contexto.Dispose();
        }

        public Usuario BuscaPorId(int id)
        {
            var usuario = contexto.Usuarios.FirstOrDefault(u => u.ID == id);
            contexto.SaveChanges();
            //contexto.Dispose();
            return usuario;
        }

        public void Remove(Usuario usuario)
        {
            contexto.Usuarios.Remove(usuario);
            contexto.SaveChanges();
            //contexto.Dispose();
        }
    }
}
