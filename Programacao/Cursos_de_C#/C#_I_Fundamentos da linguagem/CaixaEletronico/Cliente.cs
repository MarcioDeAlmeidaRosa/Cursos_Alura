using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CaixaEletronico
{
    public class Cliente
    {
        public string nome;
        public string cpf;
        public string rgTitular;
        public int idade;
        public string enderecoTitular;

        public bool EhMaiorDeIdade()
        {
            return this.idade >= 18;
        }
    }
}
