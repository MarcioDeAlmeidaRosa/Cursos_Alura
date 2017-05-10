using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caelum.CaixaEletronico.Usuarios
{
    public class Cliente
    {
        public string Nome { get; set; }
        private string cpf;
        public string Cpf
        {
            get { return cpf; }
            set { cpf = value; }
        }
        public string RgTitular { get; set; }
        public int Idade { get; set; }
        public string EnderecoTitular { get; set; }
        public string Documentos { get; set; }

        public bool EhMaiorDeIdade
        {
            get
            {
                return this.Idade >= 18;
            }
        }

        public Cliente(string nome)
        {
            this.Nome = nome;
        }

        public bool PodeAbrirContaSozinho
        {
            get
            {
                return (this.Idade >= 18 ||
                this.Documentos.Contains("emancipacao")) &&
                !string.IsNullOrEmpty(this.cpf);
            }
        }
    }
}
