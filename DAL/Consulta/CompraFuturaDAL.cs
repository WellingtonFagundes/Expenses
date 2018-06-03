using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Consulta
{
    public class CompraFuturaDAL : Repositorio<MLL.CompraFutura>
    {
        public CompraFuturaDAL(Contexto c) : base(c)
        {

        }
    }
}
