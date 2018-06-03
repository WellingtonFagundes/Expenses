using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;

namespace Expenses.Controllers
{
    public class CartaoCreditoController : Controller
    {
        [HttpPost]
        // GET: CartaoCredito
        public string CartoesCreditoCadastrados(int codVigencia,int codUser)
        {
            CartaoCreditoBLL cartaoBLL = new CartaoCreditoBLL();

            return cartaoBLL.CartoesCreditoCadastrados(codVigencia,codUser).ToString();
        }


        [HttpPost]
        public string CarregarCartaoCredito(int codigo)
        {
            CartaoCreditoBLL carBLL = new CartaoCreditoBLL();

            return carBLL.CarregarCartaoCredito(codigo).ToString();
        }

        [HttpPost]
        public string ExcluirCartaoCredito(int codigo)
        {
            CartaoCreditoBLL carBLL = new CartaoCreditoBLL();
            MLL.CartaoCredito cartao = carBLL.ObterPorId(codigo);

            return carBLL.ExcluirCartao(cartao).ToString();
        }

        [HttpPost]
        public string SalvarCartaoCredito(string obj)
        {
            CartaoCreditoBLL carBLL = new CartaoCreditoBLL();

            return carBLL.SalvarCartao(obj).ToString();
        }

        [HttpPost]
        public string EditarCartaoCredito(string obj)
        {
            CartaoCreditoBLL carBLL = new CartaoCreditoBLL();

            return carBLL.EditarCartao(obj).ToString();
        }
    }
}