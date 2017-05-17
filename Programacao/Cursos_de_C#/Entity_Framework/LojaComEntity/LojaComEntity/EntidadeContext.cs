using LojaComEntity.Entidades;
using Microsoft.Data.Entity;
using System.Configuration;

namespace LojaComEntity
{
    public class EntidadeContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var stringConexao = ConfigurationManager.ConnectionStrings["lojaConnectionString"].ConnectionString;

            optionsBuilder.UseSqlServer(stringConexao);
            base.OnConfiguring(optionsBuilder);
        }
    }
}