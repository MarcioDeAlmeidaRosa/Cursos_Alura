using LojaComEntity.Entidades;
using Microsoft.Data.Entity;
using System.Configuration;

namespace LojaComEntity
{
    public class EntidadeContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<PessoaJuridica> PessoasJuridica { get; set; }
        public DbSet<PessoaFisica> PessoasFisica { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<ProdutoVenda> ProdutoVenda { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var stringConexao = ConfigurationManager.ConnectionStrings["lojaConnectionString"].ConnectionString;

            optionsBuilder.UseSqlServer(stringConexao);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProdutoVenda>().HasKey(pv => new { pv.VendaID, pv.ProdutoID });
            base.OnModelCreating(modelBuilder);
        }
    }
}