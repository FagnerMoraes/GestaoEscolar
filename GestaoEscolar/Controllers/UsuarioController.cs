using System.Web.Mvc;
using GestaoEscolar.Models;
using System.Web.Security;
using System;
using GestaoEscolar.DAO;
using GestaoEscolar.Infra;

namespace GestaoEscolar.Controllers
{
    public class UsuarioController : Controller
    {
        private UsuarioDAO dao;
        private UsuarioInfra infra;        

        public UsuarioController(UsuarioDAO dao, UsuarioInfra infra)
        {
            this.dao = dao;
            this.infra = infra;
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(Usuario usuario)
        {
            Contexto contexto = new Contexto();

            var users = new UsuarioDAO(contexto);

            if (!users.ValidaUsuario("adm", "adm"))
            {
                var usuer = new Usuario();
                usuer.Login = "adm";
                usuer.Senha = "adm";

                users.Salva(usuer);
            }


            if (ModelState.IsValid)
            {
                try
                {
                    if (infra.ValidarUsuario(usuario.Login, usuario.Senha))
                    {
                        FormsAuthentication.SetAuthCookie(usuario.Login, false);
                        return RedirectToAction("Index", "Home");
                    }
                }
                catch (Exception error)
                {
                    ViewBag.Mensagem = error;
                }
                ModelState.AddModelError("", "Dados do Login estão incorretos");
            }
            return View(usuario);
        }

        [Authorize]
        public ActionResult Adicionar()
        {
            return View();
        }

        public ActionResult Detalhes(int id)
        {
            ViewBag.Detalhe = true;

            var listaescola = dao.listarEscola();

            ViewBag.EscolaId = new SelectList(listaescola, "Id", "NomeEscola");

            var usuario = dao.UsuarioPorId(id);

            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adicionar(Usuario novousuario)
        {
            if (ModelState.IsValid)
            {
                dao.Salva(novousuario);
                return RedirectToAction("Index", "Home");
            }
            return View(novousuario);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}
