using System.Collections.Generic;

namespace LojaComEntity.Entidades
{
    public class Produto
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        //Navigation Property
        public virtual Categoria Categoria { get; set; }
        public int CategoriaID { get; set; }
        public virtual IList<ProdutoVenda> ProdutoVenda { get; set; }
    }
}
