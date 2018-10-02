using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Consulta
{
    public class RegrasDAL : Repositorio<MLL.Regras>
    {
        public RegrasDAL(Contexto c) : base(c)
        {

        }
    }
}
