using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CaixaEletronico
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

        public bool EhMaiorDeIdade
        {
            get
            {
                return this.Idade >= 18;
            }
        }
    }
}
