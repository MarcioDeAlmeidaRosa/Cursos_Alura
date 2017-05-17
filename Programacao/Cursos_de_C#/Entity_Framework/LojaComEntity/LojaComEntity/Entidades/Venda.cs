using System.Collections.Generic;

namespace LojaComEntity.Entidades
{
    public class Venda
    {
        public int ID { get; set; }
        public virtual Usuario Cliente { get; set; }
        public int ClienteID { get; set; }

        public IList<ProdutoVenda> ProdutoVenda { get; set; }
    }
}
