using System.Linq;
using System.Web.Mvc;
using GestaoEscolar.Models;
using System.Web.Security;

namespace GestaoEscolar.Controllers
{
    public class UsuarioController : Controller
    {
        readonly Contexto _banco = new Contexto();
        
        public ActionResult LogIn() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(Usuario usuario) 
        {
           
            if (ModelState.IsValid)
            {
                if (ValidarUsuario(usuario.Login, usuario.Senha))
                {

                    FormsAuthentication.SetAuthCookie(usuario.Login, false);
                    return RedirectToAction("Index", "Home");
                }
                
                ModelState.AddModelError("", "Dados do Login estão incorretos");
            }

            return View(usuario);
        }

        [Authorize]
        public ActionResult Registrar() 
        {
            ViewBag.Funcionarios = new SelectList(_banco.Funcionarios, "Id", "NomeFuncionario");

            return View();
        }

        [HttpPost]
        public ActionResult Registrar(Usuario novousuario)
        {
            if (ModelState.IsValid) 
            {
                _banco.Usuarios.Add(novousuario);
                _banco.SaveChanges();

                return RedirectToAction("Index", "Home");

            }
            
            return View(novousuario);
        }

        public ActionResult LogOut() 
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


        private bool ValidarUsuario(string login, string senha)
        {
            bool isValid = false;

            var usuario = _banco.Usuarios.FirstOrDefault(u => u.Login == login && u.Senha == senha);

            if (usuario != null)
            {
                isValid = true;
            }

            return isValid;
        }

    }
}
