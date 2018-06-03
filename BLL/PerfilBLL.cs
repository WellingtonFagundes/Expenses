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

       public List<SelectListItem> ObterListaPerfil()
        {
            PerfilDAL perDAL = new PerfilDAL(db);

            IList<MLL.Perfil> listaPer = perDAL.Tudo().OrderBy(p => p.Nome_Perfil).ToList();

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

            if (form["chkDiferenciado"] == "on")
            {
                perfil.Diferenciado = true;
            }else if (form["chkDiferenciado"] == null)
            {
                perfil.Diferenciado = false;
            }

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
            
            if (form["chkDiferenciado"] == "on")
            {
                perfil.Diferenciado = true;
            }else if (form["chkDiferenciado"] == null)
            {
                perfil.Diferenciado = false;
            }

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
