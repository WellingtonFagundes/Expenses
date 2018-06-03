using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Consulta
{
    public class CartaoCreditoDAL : Repositorio<MLL.CartaoCredito>
    {
        public CartaoCreditoDAL(Contexto c) : base(c)
        {

        }
    }
}
