using System;

namespace LojaComEntity
{
    class Program
    {
        static void Main(string[] args)
        {
            var dao = new UsuarioDao();
            var renato = new Entidades.Usuario()
            {
                Nome = "Marcelo",
                Senha = "523"
            };
            dao.Salva(renato);
            Console.WriteLine("Salvou o usuário");
            var usuario = dao.BuscaPorId(2);
            Console.WriteLine("Usuário recuperado " + usuario.Nome);

            dao.Remove(usuario);
            Console.WriteLine("Usuário removido com sucesso.");




            Console.ReadLine();
        }
    }
}
