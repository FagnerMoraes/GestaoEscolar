using GestaoEscolar.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestaoEscolar.Infra
{
    public class UsuarioInfra
    {
        private UsuarioDAO dao;

        public UsuarioInfra(UsuarioDAO dao)
        {
            this.dao = dao;
        }

        public bool ValidarUsuario(string login, string senha)
        {
            bool isValid = false;

            var usuario = dao.BuscarUsuario(login, senha);

            if (usuario != null)
                isValid = true;            

            return isValid;
        }
    }
}