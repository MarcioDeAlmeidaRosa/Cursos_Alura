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
            //Unchanged
            var usuario = dao.BuscaPorId(1);
            //Modified
            usuario.Nome = "Marcio de Almeida Rosa";
            dao.SaveChanges(usuario);
            Console.WriteLine("Usuário salvo com sucesso.");
            Console.ReadLine();
        }
    }
}
