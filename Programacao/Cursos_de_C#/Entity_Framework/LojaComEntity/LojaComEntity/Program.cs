using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace LojaComEntity
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Rodou");
            //Console.ReadLine();
            //IDbConnection con = new SqlConnection("");
            //IDbCommand command = con.CreateCommand();
            //command.CommandText = "select * from usuario";
            //command.CommandType = CommandType.Text;
            //IDataReader leitor = command.ExecuteReader();

            //while (leitor.Read())
            //{
            //    int id = Convert.ToInt32( leitor["id"]);
            //    string nome = Convert.ToString(leitor["id"]);
            //}

            EntidadeContext contexto = new EntidadeContext();

            var usuario = new Entidades.Usuario()
            {
                Nome = "Marcio",
                Senha = "123"
            };

            contexto.Usuarios.Add(usuario);
            contexto.SaveChanges();
            contexto.Dispose();

            Console.WriteLine("Salvou o usuário");
            Console.ReadLine();

        }
    }
}
