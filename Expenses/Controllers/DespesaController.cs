using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Expenses.Controllers
{
    public class DespesaController : Controller
    {
        
        [HttpPost]
        public string DespesasCadastradas(int codVigencia,int codUser)
        {
            DespesaBLL desBLL = new DespesaBLL();

            ViewBag.StatusDespesa = desBLL.ObterStatusDespesa().ToString();
            ViewBag.listaFormaPag = desBLL.ObterFormaPag().ToString();

            return desBLL.DespesasCadastradas(codVigencia, codUser).ToString();
        }

        [HttpPost]
        public string CarregarDespesa(int codDespesa)
        {
            DespesaBLL desBLL = new DespesaBLL();

            ViewBag.StatusDespesa = desBLL.ObterStatusDespesa().ToString();
            ViewBag.listaFormaPag = desBLL.ObterFormaPag().ToString();

            return desBLL.CarregarDespesa(codDespesa).ToString();
        }


        [HttpPost]
        public string ExcluirDespesa(int codigo)
        {
            DespesaBLL desBLL = new DespesaBLL();
            MLL.Despesa despesa = desBLL.ObterPorId(codigo);

            return desBLL.ExcluirDespesa(despesa);
        }

        [HttpPost]
        public string CadastrarDespesa(string obj)
        {
            DespesaBLL desBLL = new DespesaBLL();

            return desBLL.CadastrarDespesa(obj).ToString();
        }

        [HttpPost]
        public string EditarDespesa(string obj)
        {
            DespesaBLL desBLL = new DespesaBLL();

            return desBLL.EditarDespesa(obj).ToString();
        }
    }
}