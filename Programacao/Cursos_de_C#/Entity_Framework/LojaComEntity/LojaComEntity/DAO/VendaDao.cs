using LojaComEntity.Entidades;
using Microsoft.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace LojaComEntity.DAO
{
    public class VendaDao
    {
        private EntidadeContext contexto;

        public VendaDao()
        {
            contexto = new EntidadeContext();
        }

        public void IncluirVenda(Usuario usuario, IList<Produto> produtos )
        {
            Venda venda = new Venda()
            {
                Cliente = usuario
            };
            venda.ProdutoVenda = new List<ProdutoVenda>();
            foreach (var produto in produtos)
                venda.ProdutoVenda.Add(new ProdutoVenda
                {
                    Produto = produto,
                    Venda = venda
                }); ;

            contexto.Vendas.Add(venda);
            contexto.SaveChanges();
            //contexto.Dispose();
        }

        public Venda RecuperaVenda(int idVenda)
        {
            var venda = contexto
                .Vendas
                .Include(v => v.ProdutoVenda)
                .ThenInclude(produtoVenda => produtoVenda.Produto)
                .FirstOrDefault(v => v.ID == 1);
            //contexto.Dispose();
            return venda;
        }

    }
}
