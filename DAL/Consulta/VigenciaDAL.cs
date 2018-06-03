using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Consulta
{
    public class VigenciaDAL : Repositorio<MLL.Vigencia>
    {
        public VigenciaDAL(Contexto c) : base(c)
        {

        }
    }
}
