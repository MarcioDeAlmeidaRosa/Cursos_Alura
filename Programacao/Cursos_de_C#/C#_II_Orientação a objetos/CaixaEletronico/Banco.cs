using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaixaEletronico
{
    public class Banco
    {
        public Conta[] Contas { get; private set; }


        public void Adicionar(Conta conta)
        {
            var totalLinha = Contas != null ? Contas.Length : 0;

            Conta[] auxiliar = new Conta[totalLinha + 1];

            if (Contas != null)
                for (int i = 0; i < Contas.Length; i++)
                    auxiliar[i] = Contas[i];
            else
                Contas = auxiliar;

            auxiliar[auxiliar.Length - 1] = conta;

            Contas = auxiliar;
        }
    }
}
