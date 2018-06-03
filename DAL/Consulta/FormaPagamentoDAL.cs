using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Consulta
{
    public class FormaPagamentoDAL : Repositorio<MLL.FormaPagamento>
    {
        public FormaPagamentoDAL(Contexto c): base(c)
        {

        }
    }
}
