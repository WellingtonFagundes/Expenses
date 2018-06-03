using BLL;
using MLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Expenses.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
         
            UsuarioBLL usuBLL = new UsuarioBLL();

           
            if (usuBLL.Login(usuario) == true)
            {
                Usuario user = usuBLL.ObtemDadosUsuarioLogado(usuario.Email);

                Session["usuarioLogadoID"] = user.Codigo_Usuario.ToString();
                Session["nomeUsuarioLogado"] = user.Nome_Usuario.ToString();
                Session["emailUsuarioLogado"] = user.Email.ToString();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Falha ao efetuar o login";
            }

            return RedirectToAction("Index","Login");
        }

        public ActionResult Cadastro()
        {
            PerfilBLL perBLL = new PerfilBLL();

            ViewBag.listaperfil = perBLL.ObterListaPerfil();

            return View();
        }

        [HttpPost]
        public string Salvar(Usuario usu,FormCollection form)
        {
            HttpPostedFileBase arquivo = null;

            if (Request.Files.Count > 0)
            {
                arquivo = Request.Files[0];
            }

            var img = Path.GetFileName(arquivo.ToString());

            if (ModelState.IsValid)
            {
                if (arquivo != null && arquivo.ContentLength > 0)
                {
                    var path = Path.Combine("C:/Compras_Imagens/", Path.GetFileName(arquivo.FileName));

                    arquivo.SaveAs(path);

                    usu.Path_Image = path;
                }
            }

            usu.Administrador = false;

            UsuarioBLL usuBLL = new UsuarioBLL();

            return usuBLL.InsereUsuario(usu).ToString();
        }
    }
}