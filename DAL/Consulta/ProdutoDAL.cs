using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Consulta
{
    public class ProdutoDAL:Repositorio<MLL.Produto>
    {
        public ProdutoDAL(Contexto c):base(c)
        {

        }
    }
}
