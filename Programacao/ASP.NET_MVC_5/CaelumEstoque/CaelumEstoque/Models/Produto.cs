using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CaelumEstoque.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Required, StringLength(20, ErrorMessage = "A propriedade {0}, só poderé receber até {1} caracteres.")]
        public String Nome { get; set; }

        [Range(1d, 10000d, ErrorMessage = "O valor deve {0} estar entre R${1} até {2}.")]
        public float Preco { get; set; }

        public CategoriaDoProduto Categoria { get; set; }

        [Required]
        public int? CategoriaId { get; set; }

        public String Descricao { get; set; }

        [Required]
        public int Quantidade { get; set; }
    }
}