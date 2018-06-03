using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Expenses.Controllers
{
    public class VigenciaController : Controller
    {
        public ActionResult ListaVigencia()
        {
            if (Session["usuarioLogadoID"] == null)
            {
                Response.Redirect(Url.Action("Index", "Login"));
            }

            return View();
        }


        // GET: Vigencia
        public ActionResult VigenciasCadastradas()
        {
            //Exemplo utilizando PartialView e objeto ajax
            //if (Request.IsAjaxRequest())
            //{
            //    return PartialView("_ListaVigencia", vigBLL.VigenciasCadastradas(datainicio, datafim));
            //}
            return View();
        }



        [HttpPost]
        public string VigenciasCadastradas(string datainicio = "", string datafim = "")
        {
            VigenciaBLL vigBLL = new VigenciaBLL();

            int codUser = Convert.ToInt32(Session["usuarioLogadoId"]);
            
            return vigBLL.VigenciasCadastradas(datainicio, datafim,codUser).ToString();
        }

        /* Exemplo de retorno JSON
        public ActionResult loaddata(string datainicio = "", string datafim = "")
        {
            VigenciaBLL vigBLL = new VigenciaBLL();

            var data = vigBLL.VigenciasCadastradas(datainicio, datafim).ToList();

            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }
        */

        public ActionResult CadastrarVigencia()
        {
            return View();
        }

        [HttpPost]
        public string CadastrarVigencia(MLL.Vigencia vigencia)
        {
            VigenciaBLL vigBLL = new VigenciaBLL();

            vigencia.Codigo_Usuario = Convert.ToInt32(Session["usuarioLogadoId"]);

            return vigBLL.CadastrarVigencia(vigencia).ToString();
        }


        [HttpGet]
        public ActionResult EditarVigencia()
        {
            VigenciaBLL vigBLL = new VigenciaBLL();

            MLL.Vigencia vigencia = vigBLL.ObterPorId(Convert.ToInt32(Request.QueryString["codigo"]));

            DespesaBLL desBLL = new DespesaBLL();
            ViewBag.StatusDespesa = desBLL.ObterStatusDespesa().ToString();
            ViewBag.listaFormaPag = desBLL.ObterFormaPag().ToString();

            return View(vigencia);
        }

        [HttpPost]
        public string EditarVigencia(MLL.Vigencia vigencia)
        {
            VigenciaBLL vigBLL = new VigenciaBLL();

            vigencia.Codigo_Usuario = Convert.ToInt32(Session["usuarioLogadoId"]);

            return vigBLL.EditarVigencia(vigencia).ToString();
        }

        public ActionResult ExcluirVigencia()
        {
            return View();
        }


        [HttpPost]
        public string ExcluirVigencia(int codigo)
        {
            VigenciaBLL vigBLL = new VigenciaBLL();

            MLL.Vigencia vigencia = vigBLL.ObterPorId(codigo);

            return vigBLL.ExcluirVigencia(vigencia);
        }

    }
}