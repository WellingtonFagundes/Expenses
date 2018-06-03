using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Expenses.Controllers
{
    public class ReceitaController : Controller
    {
        // GET: Receita
        [HttpPost]
        public string CarregarReceitas(int codVigencia,int codUsuario)
        {
            ReceitaBLL recBLL = new ReceitaBLL();

            return recBLL.CarregarReceitas(codVigencia,codUsuario).ToString();
        }

        [HttpPost]
        public string CarregarReceita(int codigo)
        {
            ReceitaBLL recBLL = new ReceitaBLL();

            return recBLL.CarregarReceita(codigo).ToString();
        }


        [HttpPost]
        public string ExcluirReceita(int codigo)
        {
            ReceitaBLL recBLL = new ReceitaBLL();
            MLL.Receita receita = recBLL.ObterPorId(codigo);

            return recBLL.ExcluirReceita(receita);
        }


        [HttpPost]
        public string CadastrarReceita(string obj)
        {
            ReceitaBLL recBLL = new ReceitaBLL();

            return recBLL.CadastrarReceita(obj).ToString();
        }


        [HttpPost]
        public string EditarReceita(string obj)
        {
            ReceitaBLL recBLL = new ReceitaBLL();

            return recBLL.EditarReceita(obj).ToString();
        }
    }
}