namespace LojaComEntity.Entidades
{
    public class ProdutoVenda
    {
        public int VendaID { get; set; }
        public virtual Venda Venda { get; set; }
        public int ProdutoID { get; set; }
        public virtual Produto Produto { get; set; }
    }
}
