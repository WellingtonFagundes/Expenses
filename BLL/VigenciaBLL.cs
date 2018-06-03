using DAL;
using DAL.Consulta;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class VigenciaBLL:CriptografiaBLL
    {
        public Contexto db;

        public VigenciaBLL()
        {
            db = new Contexto();
        }

        public JObject VigenciasCadastradas(string datainicio,string datafim,int coduser)
        {
            JObject obj = new JObject();
            JArray arr = new JArray();
            VigenciaDAL vigDAL = new VigenciaDAL(db);
            List<MLL.Vigencia> lista = null;

            if ((!string.IsNullOrEmpty(datainicio)) && (!string.IsNullOrEmpty(datafim)))
            {
                DateTime dtinicio = Convert.ToDateTime(datainicio);
                DateTime dtfim = Convert.ToDateTime(datafim);

                //Fazendo o between de datas caso não sejam nulas e filtrando pelo usuário logado
                lista = vigDAL.Tudo()
                              .Where(v => v.Data_Inicio >= dtinicio && v.Data_Final <= dtfim
                                    && v.Codigo_Usuario == coduser)
                              .OrderBy(v => v.Data_Inicio).ToList();

            }
            else
            {
                //Caso não seja passado os parâmetros para pesquisa lista todos ordenado por código selecionando pelo usuário logado
                lista = vigDAL.Tudo()
                               .Where(v => v.Codigo_Usuario == coduser)
                               .OrderBy(v => v.Codigo_Vigencia).ToList();
            }

            //Buscando os dados do banco
            foreach (MLL.Vigencia vig in lista)
            {
                arr.Add(new JObject(
                       new JProperty("Codigo_Vigencia",vig.Codigo_Vigencia),
                       new JProperty("Data_Inicio",vig.Data_Inicio.ToShortDateString()),
                       new JProperty("Data_Fim",vig.Data_Final.ToShortDateString())
                    ));
            }

            obj.Add(new JProperty("lista", arr));

            return obj;
        }

        public JObject CadastrarVigencia(MLL.Vigencia vigencia)
        {
            JObject obj = new JObject();

            VigenciaDAL vigDAL = new VigenciaDAL(db);

            if (vigDAL.Adicionar(vigencia))
            {
                obj.Add(new JProperty("ok", "ok"));
            }
            else
            {
                obj.Add(new JProperty("erro", vigDAL.erro));
            }

            return obj;
        }


        public MLL.Vigencia ObterPorId(int codigo)
        {
            VigenciaDAL vigDAL = new VigenciaDAL(db);

            return vigDAL.obtemPorId(codigo);
        }

        public JObject EditarVigencia(MLL.Vigencia vigencia)
        {
            JObject obj = new JObject();

            VigenciaDAL vigDAL = new VigenciaDAL(db);

            if (vigDAL.Editar(vigencia))
            {
                obj.Add(new JProperty("ok", "ok"));
            }
            else
            {
                obj.Add(new JProperty("erro", vigDAL.erro));
            }

            return obj;
        }

        public string ExcluirVigencia(MLL.Vigencia vigencia)
        {
            string result = "";

            VigenciaDAL vigDAL = new VigenciaDAL(db);

            if (vigDAL.Remover(vigencia))
            {
                result = "ok";
            }
            else
            {
                result = "Erro - " + vigDAL.erro;
            }

            return result;
        }
    }
}
