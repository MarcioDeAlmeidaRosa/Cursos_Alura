using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caelum.Leilao
{
    public class Avaliador
    {
        private double maiorDeTodos = double.MinValue;
        private double menorValor = double.MaxValue;

        public void Avalia(Leilao leilao)
        {
            foreach(var lance in leilao.Lances)
            {
                if (lance.Valor > maiorDeTodos)
                    maiorDeTodos = lance.Valor;
                if (lance.Valor < menorValor)
                    menorValor = lance.Valor;
            }
        }

        public double MaiorDeTodos
        {
            get { return maiorDeTodos; }
        }

        public double MenorDeTodos
        {
            get { return menorValor; }
        }
    }
}
