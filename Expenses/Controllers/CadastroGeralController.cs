using BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Expenses.Controllers
{
    public class CadastroGeralController : Controller
    {
        /*Actions de Forma de Pagamento
        =======================================================================================
        */
        // GET: CadastroGeral
        public ActionResult FormasPagamentoCadastrados()
        {
            FormasPagamentoBLL forma = new FormasPagamentoBLL();
            IList<MLL.FormaPagamento> lista = new List<MLL.FormaPagamento>();

            if (Session["usuarioLogadoID"] == null)
            {
                lista = null;
                Response.Redirect(Url.Action("Index", "Login"));
            }
            else
            {
                lista = forma.ObterListaFormasPag();
            }


            return View(lista);
        }

        public ActionResult CadastrarFormaPagamento()
        {
            return View();
        }

        [HttpPost]
        public string CadastrarFormaPagamento(MLL.FormaPagamento formaPag)
        {
            FormasPagamentoBLL formaBLL = new FormasPagamentoBLL();

            return formaBLL.CadastrarFormaPag(formaPag).ToString();
        }

        [HttpPost]
        public string ExcluirFormaPagamento(int codigo)
        {
            FormasPagamentoBLL formaBLL = new FormasPagamentoBLL();

            MLL.FormaPagamento forma = formaBLL.ObterPorId(codigo);

            return formaBLL.ExcluirFormaPag(forma).ToString();
        }

        public ActionResult EditarFormaPag()
        {
            FormasPagamentoBLL formaBLL = new FormasPagamentoBLL();

            MLL.FormaPagamento forma = formaBLL.ObterPorId(formaBLL.DescriptografaID(Request.QueryString["codigo"]));

            return View(forma);
        }

        [HttpPost]
        public string EditarFormaPag(MLL.FormaPagamento formaPag)
        {
            FormasPagamentoBLL formaBLL = new FormasPagamentoBLL();

            return formaBLL.EditarFormaPag(formaPag).ToString();
        }

        /*Actions de Compras Futuras
       =======================================================================================
       */
        public ActionResult ListaCompras()
        {
            ComprasFuturasBLL comBLL = new ComprasFuturasBLL();

            IList<MLL.CompraFutura> lista = new List<MLL.CompraFutura>();

            if (Session["usuarioLogadoID"] == null)
            {
               Response.Redirect(Url.Action("Index", "Login"));
            }
            else
            {
                ViewBag.listaStatus = comBLL.ObterListaStatus().ToString();
            }

            return View();
        }


        public string ComprasFuturasCadastradasPorUsuario()
        {
            ComprasFuturasBLL comfBLL = new ComprasFuturasBLL();

            return comfBLL.ObterComprasFuturasPorUsuario(Convert.ToInt32(Session["usuariologadoID"])).ToString();  
        }

       
        public ActionResult CadastrarCompraFutura()
        {
            ComprasFuturasBLL comBLL = new ComprasFuturasBLL();

            //Preenchea combo de status
            ViewBag.listaStatus = comBLL.ObterListaStatus().ToString();

            return View();
        }

        [HttpPost]
        public string CadastrarCompraFutura(string obj)
        {
            ComprasFuturasBLL compraBLL = new ComprasFuturasBLL();

            return compraBLL.CadastrarCompraFutura(obj).ToString();
        }

        public ActionResult EditarComprasFuturas()
        {
            ComprasFuturasBLL comBLL = new ComprasFuturasBLL();

            ViewBag.listaStatus = comBLL.ObterListaStatus();

            //MLL.CompraFutura compra = comBLL.ObterPorId();

            return View();
        }

        [HttpPost]
        public string EditarComprasFuturas(string obj)
        {
            ComprasFuturasBLL comBLL = new ComprasFuturasBLL();

            return comBLL.EditarCompraFutura(obj).ToString();
;       }

        [HttpPost]
        public string ExcluirCompraFutura(int codigo)
        {
            ComprasFuturasBLL comBLL = new ComprasFuturasBLL();

            return comBLL.ExcluirCompraFutura(codigo).ToString();
        }

        [HttpPost]
        public string ObterCompra(int codigo)
        {
            ComprasFuturasBLL comBLL = new ComprasFuturasBLL();

            return comBLL.ObterPorId(codigo).ToString();
        } 

        [HttpPost]
        public string ObterProdutosNaCompraDeUsuario()
        {
            ComprasFuturasBLL comBLL = new ComprasFuturasBLL();

            return comBLL.ObterProdutosNaCompraDeUsuario(Convert.ToInt32(Session["usuariologadoId"])).ToString();
        }

    }
}