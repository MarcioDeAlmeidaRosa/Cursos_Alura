using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CaelumEstoque.Models
{
    public class CategoriaDoProduto
    {
        public int Id { get; set; }

        [Required, MaxLength(30, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres.")]
        public String Nome { get; set; }

        public String Descricao { get; set; }
    }
}