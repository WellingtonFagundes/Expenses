using BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Expenses.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        public ActionResult UsuariosCadastrados()
        {
            UsuarioBLL usuBLL = new UsuarioBLL();
            IList<MLL.Usuario> lista = new List<MLL.Usuario>();

            if (Session["usuariologadoId"] == null)
            {
                lista = null;
                Response.Redirect(Url.Action("Index","Login"));
            }
            else
            {
                lista = usuBLL.UsuariosCadastrados();
            }

            return View(lista);
        }

        public ActionResult CadastrarUsuario()
        {
            PerfilBLL perBLL = new PerfilBLL();

            ViewBag.listaPerfis = perBLL.ObterListaPerfil();


            return View();
        }

        [HttpPost]
        public string CadastrarUsuario(MLL.Usuario usu,FormCollection form)
        {
            string uploadPath = "";
            string pathRelative = "";
            string caminhoarquivo = "";

            UsuarioBLL usuBLL = new UsuarioBLL();

            //Checagem de upload do arquivo (FOTO)
            HttpPostedFileBase arquivo = null;
            if (Request.Files.Count > 1)
            {
                arquivo = Request.Files[0];

                //Nome e extensão do arquivo
                var img = Path.GetFileName(arquivo.FileName);

                if (ModelState.IsValid)
                {
                    if (arquivo != null && arquivo.ContentLength > 0)
                    {
                        uploadPath = @"~\images\Arquivos_Expenses\Usuarios\";
                        pathRelative = @"\images\Arquivos_Expenses\Usuarios\" + img;
                        caminhoarquivo = Path.Combine(Server.MapPath(@uploadPath + img));
                    }
                }

            }
            return usuBLL.CadastrarUsuario(arquivo,usu,form,caminhoarquivo,pathRelative).ToString();
        }

        public ActionResult EditarUsuario()
        {
            UsuarioBLL usuBLL = new UsuarioBLL();
            MLL.Usuario usuario = usuBLL.ObterPorID(usuBLL.DescriptografaID(Request.QueryString["codigo"]));
            
            PerfilBLL perBLL = new PerfilBLL();

            ViewBag.listaPerfis = perBLL.ObterListaPerfil();

            return View(usuario);
        }

        [HttpPost]
        public string EditarUsuario(MLL.Usuario usu,FormCollection form)
        {
            string uploadPath = "";
            string pathRelative = "";
            string caminhoarquivo = "";

            UsuarioBLL usuBLL = new UsuarioBLL();

           
            //Checagem de upload do arquivo (FOTO)
            HttpPostedFileBase arquivo = null;
            if (Request.Files.Count > 1)
            {
                //No caso de se carregar uma nova imagem faz-se os procedimentos para carregar o arquivo
                //e abastece a propriedade path_Image com um novo carminho relativo com o nome do web server principal
                //no caso NOTE e a aplicação EXPENSES
                arquivo = Request.Files[0];

                //Nome e extensão do arquivo
                var img = Path.GetFileName(arquivo.FileName);

                if (ModelState.IsValid)
                {
                    if (arquivo != null && arquivo.ContentLength > 0)
                    {
                        uploadPath = @"~\images\Arquivos_Expenses\Usuarios\";
                        pathRelative = @"images\Arquivos_Expenses\Usuarios\" + img;
                        caminhoarquivo = Path.Combine(Server.MapPath(@uploadPath + img));
                    }

                    //Caminho com o endereço do Web Server para exibir a foto imagem
                    var pathSalvarNaBase = Path.Combine("http:\\NOTE\\EXPENSES\\" + pathRelative);
                    //Caminho mapeado no servidor fisicamente
                    arquivo.SaveAs(caminhoarquivo);

                    usu.Path_Image = pathSalvarNaBase;
                }

            }
            else
            {
                //O usuário não carregando nenhuma foto nova grava-se o caminho da imagem já cadastrada
                usu.Path_Image = form["txtFileFoto"];
            }

            return usuBLL.EditarUsuario(usu).ToString();
        }

        [HttpPost]
        public string ExcluirUsuario(int codigo)
        {
            UsuarioBLL usuBLL = new UsuarioBLL();

            MLL.Usuario usu = usuBLL.ObterPorID(codigo);

            return usuBLL.ExcluirUsuario(usu);
        }

    }
}