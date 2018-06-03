using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Consulta
{
    public class DespesaDAL : Repositorio<MLL.Despesa>
    {
        public DespesaDAL(Contexto c): base(c)
        {

        }
    }
}
