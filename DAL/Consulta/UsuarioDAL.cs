using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Consulta
{
    public class UsuarioDAL : Repositorio<MLL.Usuario>
    {
        public UsuarioDAL(Contexto c) : base(c)
        {

        }

        public IList<MLL.Usuario> ObterPorEmail(string email)
        {
            try
            {
                List<MLL.Usuario> lista = PegaContexto().Usuario.AsNoTracking()
                    .Where(x => x.Email == email).ToList();

                if (lista.Count == 0)
                {
                    return null;
                }
                else
                {
                    return lista;
                }
            }catch (Exception ex)
            {
                erro = ex.Message;
                return null;
            }
        }


        public MLL.Usuario ObterDadosUsuarioLogado(string email)
        {
            try
            {
                MLL.Usuario user = PegaContexto().Usuario.AsNoTracking()
                    .Where(x => x.Email == email).FirstOrDefault();

                if (user != null)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
                return null;
            }
        }
    }
}
