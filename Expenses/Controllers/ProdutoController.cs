using BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Expenses.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Produto
        public ActionResult ListaProdutos()
        {
            ProdutoBLL proBLL = new ProdutoBLL();

            IList<MLL.Produto> lista = new List<MLL.Produto>();

            if (Session["usuarioLogadoID"] == null)
            {
                Response.Redirect(Url.Action("Index","Login"));
            }
            
            return View();
        }

        public string ProdutosCadastradosPorUsuario()
        {
            ProdutoBLL proBLL = new ProdutoBLL();

            return proBLL.ListaProdutosCadastradosPorUser(Convert.ToInt32(Session["usuarioLogadoID"])).ToString();
        }

        public ActionResult CadastrarProduto()
        {
            return View();
        }

        [HttpPost]
        public string CadastrarProduto(string obj)
        {
            ProdutoBLL proBLL = new ProdutoBLL();

            return proBLL.CadastrarProduto(obj).ToString();
        }

        public ActionResult EditarProduto()
        {
            return View();
        }

        [HttpPost]
        public string EditarProduto(string obj)
        {
            ProdutoBLL proBLL = new ProdutoBLL();

            return proBLL.EditarProduto(obj).ToString();
        }

        [HttpPost]
        public string ExcluirProduto(int codprod)
        {
            ProdutoBLL proBLL = new ProdutoBLL();

            return proBLL.ExcluirProduto(codprod);
        }

        [HttpPost]
        public string ObterProduto(int codigo)
        {
            ProdutoBLL proBLL = new ProdutoBLL();

            return proBLL.ObterPorId(codigo).ToString();
        }

        [HttpPost]
        public string FileUploadImagem()
        {
            HttpPostedFileBase arquivo = Request.Files["file"];

            int codigoproduto = Convert.ToInt32(Request.Form["idproduto"]);

            ProdutoBLL proBLL = new ProdutoBLL();

            var uploadPath = @"~\images\Arquivos_Expenses\";
            var pathRelative = @"\images\Arquivos_Expenses\";
            string caminhoarquivo = Path.Combine(Server.MapPath(@uploadPath + Path.GetFileName(arquivo.FileName)));

            return proBLL.SalvarUploadCompraImagem(arquivo, codigoproduto, arquivo.FileName, caminhoarquivo, pathRelative).ToString();
        }

        [HttpPost]
        public string BuscarCaminhoImagem(int codprod)
        {
            ProdutoBLL proBLL = new ProdutoBLL();

            return proBLL.BuscarCaminhoImagem(codprod).ToString();
        }


    }
}