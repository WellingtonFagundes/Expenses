using DAL;
using DAL.Consulta;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BLL
{
    public class PerfilBLL : CriptografiaBLL
    {
        public Contexto db;

        public PerfilBLL()
        {
            db = new Contexto();
        }

        public bool ExibirMenuPerfilAdm(string email)
        {
            UsuarioDAL usuDAL = new UsuarioDAL(db);

            MLL.Usuario usuPerfilAdm = usuDAL.Tudo()
                                             .Where(u => u.Email == email)
                                             .OrderBy(u => u.Nome_Usuario).FirstOrDefault();
  
            if ((usuPerfilAdm != null) && (usuPerfilAdm.Perfil.Nome_Perfil == "Administrador"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool ChecarPerfilAdmin()
        {
            PerfilDAL perDAL = new PerfilDAL(db);

            MLL.Perfil perAdmin = perDAL.Tudo()
                                        .Where(p => p.Nome_Perfil == "Administrador")
                                        .OrderBy(p => p.Nome_Perfil).FirstOrDefault();

            if (perAdmin == null)
            {
                MLL.Perfil perfil = new MLL.Perfil();

                perfil.Nome_Perfil = "Administrador";
                
                if (perDAL.Adicionar(perfil) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }


        }

       public List<SelectListItem> ObterListaPerfil()
        {
            PerfilDAL perDAL = new PerfilDAL(db);

            IList<MLL.Perfil> listaPer = perDAL.Tudo()
                                               .Where(p => p.Nome_Perfil != "Administrador")
                                               .OrderBy(p => p.Nome_Perfil).ToList();

            List<SelectListItem> lista = new List<SelectListItem>();
            
            foreach(MLL.Perfil per in listaPer)
            {
                lista.Add(new SelectListItem { Text = per.Nome_Perfil, Value = per.Codigo_Perfil.ToString() });
            }

            return lista;
        }

        public IList<MLL.Perfil> ListaPerfisCadastrados()
        {
            PerfilDAL perDAL = new PerfilDAL(db);

            IList<MLL.Perfil> listaPer = perDAL.Tudo().OrderBy(p => p.Nome_Perfil).ToList();

            foreach (MLL.Perfil per in listaPer)
            {
                per.CodigoCript = CriptografaID(per.Codigo_Perfil);
            }

            return listaPer;
        }

        public JObject CadastrarPerfil(MLL.Perfil perfil,FormCollection form)
        {
            JObject obj = new JObject();

            PerfilDAL perDAL = new PerfilDAL(db);

            if (perDAL.Adicionar(perfil))
            {
                obj.Add(new JProperty("ok", "ok"));
            }
            else
            {
                obj.Add(new JProperty("erro", perDAL.erro));
            }

            return obj;
        }

        public MLL.Perfil ObterPorId(int codigo)
        {
            PerfilDAL perDAL = new PerfilDAL(db);

            return perDAL.obtemPorId(codigo);
        }

        public string ExcluirPerfil(MLL.Perfil perfil)
        {
            string result = "";
            PerfilDAL perDAL = new PerfilDAL(db);

            if (perDAL.Remover(perfil) == true)
            {
                result = "ok";
            }
            else
            {
                result = "error - " + perDAL.erro;
            }

            return result;
        }

        public JObject EditarPerfil(MLL.Perfil perfil,FormCollection form)
        {
            JObject obj = new JObject();
            PerfilDAL perDAL = new PerfilDAL(db);
          

            if (perDAL.Editar(perfil) == true)
            {
                obj.Add(new JProperty("ok", "ok"));
            }
            else
            {
                obj.Add(new JProperty("erro", perDAL.erro));
            }

            return obj;
        }
    }
}
