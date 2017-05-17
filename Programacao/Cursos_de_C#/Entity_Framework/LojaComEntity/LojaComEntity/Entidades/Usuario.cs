using System.Collections.Generic;

namespace LojaComEntity.Entidades
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }

        public virtual IList<Produto> Produto { get; set; }
    }
}
