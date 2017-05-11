using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garbage_Collector
{
    class Program
    {
        static void Main(string[] args)
        {
            //O que é a CLR?
            //É o nome da máquina virtual. Ela interpreta o código intermediário e transforma em chamadas do sistema operacional.
            //A CLR é a máquina virtual do mundo .NET. É ela que interpreta o código intermediário (IL) e transforma em chamadas que o sistema operacional entende.



            //Para que serve o Garbage Collector?
            //É o responsável por liberar os espaços de memória que não estão sendo mais utilizados.
            /*
             * Sempre que damos um new, a CLR aloca um espaço de memória. O Garbage Collector (GC) é o mecanismo responsável por liberar os espaços de memória que não estão sendo mais utilizados de volta para o sistema operacional.
             * Ele funciona de maneira transparente à nossa aplicação. Ou seja, ele está lá rodando sem que sua aplicação perceba.
             */


            //Como é possível que uma classe escrita em C# fale com uma classe escrita em VB.NET, por exemplo?
            //Porque para a CLR o que importa é a IL. A IL é sempre a mesma e independente da linguagem que a originou.
            //A CLR entende apenas código IL. Ela não faz ideia que esse código foi originado de um código-fonte C#. Classes escritas em VB.NET e C#, quando compiladas, geram a mesma IL. E, no fim das contas, por ser tudo IL, eles conseguem se falar.
            //Isso quer dizer que se no futuro alguém criar uma nova linguagem e o compilador gerar IL, então uma classe em C# vai conseguir consumir uma classe escrita nessa nova linguagem!

        }
    }
}
