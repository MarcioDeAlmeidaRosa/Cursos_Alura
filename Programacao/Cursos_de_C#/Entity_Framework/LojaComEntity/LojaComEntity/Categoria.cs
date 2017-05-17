using System.Collections.Generic;

namespace LojaComEntity
{
    public class Categoria
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public virtual IList<Produto> Produtos { get; set; }
    }
}
