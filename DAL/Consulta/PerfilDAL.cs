using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Consulta
{
    public class PerfilDAL : Repositorio<MLL.Perfil>
    {
        public PerfilDAL(Contexto c): base(c)
        {

        }
    }
}
