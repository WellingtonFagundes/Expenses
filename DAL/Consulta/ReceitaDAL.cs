using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Consulta
{
    public class ReceitaDAL : Repositorio<MLL.Receita>
    {
        public ReceitaDAL(Contexto c) : base(c)
        {

        }
    }
}
