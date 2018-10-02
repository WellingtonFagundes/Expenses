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
            //Cria perfil de Administrador na primeira carga do sistema
            PerfilBLL perBLL = new PerfilBLL();
            if (perBLL.ChecarPerfilAdmin() == true)
            {
                //Cria usuário coligado ao administrador na primeira carga do sistema
                UsuarioBLL usuBLL = new UsuarioBLL();
                usuBLL.ChecarUserAdm();
            }

            
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
                    var uploadPath = @"~\images\Arquivos_Expenses\Usuarios\";
                    var pathRelativo = @"\images\Arquivos_Expenses\Usuarios\";
                    string caminhoarquivofull = Path.Combine(Server.MapPath(@uploadPath + Path.GetFileName(arquivo.FileName)));

                    var path = "http:\\EXPENSES" + pathRelativo + arquivo.FileName;

                    arquivo.SaveAs(caminhoarquivofull);

                    usu.Path_Image = path;
                }
            }

            
            UsuarioBLL usuBLL = new UsuarioBLL();

            return usuBLL.InsereUsuario(usu).ToString();
        }
    }
}