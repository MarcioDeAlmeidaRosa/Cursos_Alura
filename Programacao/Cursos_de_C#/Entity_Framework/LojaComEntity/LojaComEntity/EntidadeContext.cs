using LojaComEntity.Entidades;
using Microsoft.Data.Entity;
using System.Configuration;

namespace LojaComEntity
{
    public class EntidadeContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var stringConexao = ConfigurationManager.ConnectionStrings["lojaConnectionString"].ConnectionString;

            optionsBuilder.UseSqlServer(stringConexao);
            base.OnConfiguring(optionsBuilder);
        }
    }
}