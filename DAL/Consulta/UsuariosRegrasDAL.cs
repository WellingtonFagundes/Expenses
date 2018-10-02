using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Consulta
{
    public class UsuariosRegrasDAL:Repositorio<MLL.UsuariosRegras>
    {
        public UsuariosRegrasDAL(Contexto c):base(c)
        {

        }
    }
}
