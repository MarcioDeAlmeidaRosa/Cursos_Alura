using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConjuntoXLisitas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Conta> contas = new List<Conta>();
            var conta1 = new Conta();
            contas.Add(conta1);
            contas.Add(conta1);
            MessageBox.Show("Total de registro na Lista: " + contas.Count);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HashSet<Conta> contas = new HashSet<Conta>();
            var conta1 = new Conta();
            contas.Add(conta1);
            contas.Add(conta1);
            MessageBox.Show("Total de registro no Conjunto: " + contas.Count);

            //O conjunto, ao encontrar a "categoria", pergunta para cada objeto que está ali se ele é igual ao que estamos inserindo. Para saber se é igual, ele faz uso do método Equals().

            //Se o método Equals() estiver mal implementado, confundiremos o conjunto! Neste caso, ele dirá que o objeto não é igual a ele mesmo, permitindo incluir novamente o mesmo objeto: o programa imprimirá 2.

            //Portanto, para que o Set faça seu trabalho corretamente, os métodos Equals() e GetHashCode() devem estar bem implementados!

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SortedSet<Conta> contas = new SortedSet<Conta>();
            var conta1 = new Conta();
            contas.Add(conta1);
            contas.Add(conta1);
            MessageBox.Show("Total de registro no Conjunto: " + contas.Count);

            //O conjunto, ao encontrar a "categoria", pergunta para cada objeto que está ali se ele é igual ao que estamos inserindo. Para saber se é igual, ele faz uso do método Equals().

            //Se o método Equals() estiver mal implementado, confundiremos o conjunto! Neste caso, ele dirá que o objeto não é igual a ele mesmo, permitindo incluir novamente o mesmo objeto: o programa imprimirá 2.

            //Portanto, para que o Set faça seu trabalho corretamente, os métodos Equals() e GetHashCode() devem estar bem implementados!
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Dictionary<string, Conta> contas = new Dictionary<string, Conta>();
            Conta c = new ContaPoupanca() { Numero = 10 };
            contas.Add("victor", c);

            //Para recuperar o valor é pesquisado pela chave
            Conta contaVictor = contas["victor"];
            MessageBox.Show("Número da conta do Victor: " + contaVictor.Numero);


            Dictionary<string, string> nomesEPalavras = new Dictionary<string, string>();
            nomesEPalavras.Add("Magu", "Marcio");
            nomesEPalavras.Add("Ed", "Edemilson");
            foreach (var i in nomesEPalavras)
            {
                MessageBox.Show(i.Key + "->" + i.Value);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SortedDictionary<string, string> nomes = new SortedDictionary<string, string>();
            nomes.Add("Adriano", "Almeida");
            nomes.Add("Mario", "Amaral");
            nomes.Add("Eric", "Torti");
            nomes.Add("Guilherme", "Silveira");
            foreach (var i in nomes)
            {
                MessageBox.Show(i.Key + " " + i.Value);
            }
        }
    }
}
