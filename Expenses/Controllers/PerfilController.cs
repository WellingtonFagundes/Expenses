using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Expenses.Controllers
{
    public class PerfilController : Controller
    {
        // GET: Perfil
        public ActionResult PerfisCadastrados()
        {
            PerfilBLL perBLL = new PerfilBLL();
            IList<MLL.Perfil> lista = new List<MLL.Perfil>();

            if (Session["usuarioLogadoId"] == null)
            {
                lista = null;
                Response.Redirect(@Url.Action("Index","Login"));
            }
            else
            {
                lista = perBLL.ListaPerfisCadastrados();
            }

            return View(lista);
        }

        public ActionResult CadastrarPerfil()
        {
            return View();
        }

        [HttpPost]
        public string CadastrarPerfil(MLL.Perfil perfil,FormCollection form)
        {
            PerfilBLL perBLL = new PerfilBLL();

            return perBLL.CadastrarPerfil(perfil,form).ToString();
        } 

        [HttpPost]
        public string ExcluirPerfil(int codigo)
        {
            PerfilBLL perBLL = new PerfilBLL();

            MLL.Perfil perfil = perBLL.ObterPorId(codigo);

            return perBLL.ExcluirPerfil(perfil);
        }

        public ActionResult EditarPerfil()
        {
            PerfilBLL perBLL = new PerfilBLL();

            MLL.Perfil perfil = perBLL.ObterPorId(perBLL.DescriptografaID(Request.QueryString["codigo"]));


            return View(perfil);
        }


        [HttpPost]
        public string EditarPerfil(MLL.Perfil perfil,FormCollection form)
        {
            PerfilBLL perBLL = new PerfilBLL();

            return perBLL.EditarPerfil(perfil,form).ToString();
        }
    }
}