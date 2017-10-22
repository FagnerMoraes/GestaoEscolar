using GestaoEscolar.Models;
using System.Collections.Generic;
using System.Linq;

namespace GestaoEscolar.DAO
{
    public class UsuarioDAO
    {
        private Contexto contexto;

        public UsuarioDAO(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public IList<Usuario> Listar()
        {
            return contexto.Usuarios.ToList();
        }

        public IList<Escola> listarEscola()
        {
            return contexto.Escolas.ToList();
        }

        public void Salva(Usuario usuario)
        {
            contexto.Usuarios.Add(usuario);
            contexto.SaveChanges();
        }

        public Usuario BuscarUsuario(string login, string senha)
        {
            return contexto.Usuarios.FirstOrDefault(u => u.Login == login && u.Senha == senha);
        }

        public Usuario UsuarioPorId(int id)
        {
            return contexto.Usuarios.FirstOrDefault(arg => arg.Id == id);
        }

        public bool ValidaUsuario(string login, string senha)
        {
            return contexto.Usuarios.Any(arg => arg.Login == login && arg.Senha == senha);
            
        }
    }
}