using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CaelumEstoque.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required, StringLength(30, ErrorMessage = "A propriedade {0}, só poderé receber até {1} caracteres.")]
        public string Nome { get; set; }

        [Required, MinLength(5,ErrorMessage ="Tamanho mínimo da {0} deve ser de {1} caracteres."), MaxLength(10, ErrorMessage = "Tamanho máximo da {0} deve ser de {1} caracteres.")]
        public string Senha { get; set; }
    }
}