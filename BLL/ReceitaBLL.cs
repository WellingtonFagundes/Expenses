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
    public class ReceitaBLL: CriptografiaBLL
    {
        public Contexto db;

        public ReceitaBLL()
        {
            db = new Contexto();
        }

        public JObject CarregarReceitas(int codVigencia,int codUsuario)
        {
            JObject obj = new JObject();
            JArray arr = new JArray();
            ReceitaDAL recDAL = new ReceitaDAL(db);

            List<MLL.Receita> lista = recDAL.Tudo()
                                            .Where(r => r.Codigo_Vigencia == codVigencia && r.Codigo_Usuario == codUsuario)
                                            .OrderBy(r => r.Codigo_Receita).ToList();


            foreach(MLL.Receita rec in lista)
            {
                arr.Add(new JObject(
                        new JProperty("Codigo_Receita", rec.Codigo_Receita),
                        new JProperty("Descricao_Receita", rec.Descricao_Receita),
                        new JProperty("Valor_Receita", rec.Valor_Receita),
                        new JProperty("Cod_Vigencia_Receita", rec.Codigo_Vigencia),
                        new JProperty("Cod_Usuario_Receita", rec.Codigo_Usuario)
                        ));
            }

            obj.Add(new JProperty("lista", arr));


            return obj;
        }

        public JObject CarregarReceita(int codigo)
        {
            JObject obj = new JObject();
            JArray arr = new JArray();
            ReceitaDAL recDAL = new ReceitaDAL(db);

            MLL.Receita receita = recDAL.obtemPorId(codigo);

            if (!object.Equals(receita, null))
            {
                arr.Add(new JObject(
                        new JProperty("Codigo_Receita", receita.Codigo_Receita),
                        new JProperty("Descricao_Receita", receita.Descricao_Receita),
                        new JProperty("Valor_Receita", receita.Valor_Receita),
                        new JProperty("Cod_Vigencia_Receita", receita.Codigo_Vigencia),
                        new JProperty("Cod_Usuario_Receita", receita.Codigo_Usuario)

                        ));

                obj.Add(new JProperty("lista", arr));

            }
            else
            {
                obj.Add("erro", recDAL.erro);
            }

            return obj;
        }

        public MLL.Receita ObterPorId(int codigo)
        {
            ReceitaDAL recDAL = new ReceitaDAL(db);

            MLL.Receita receita = recDAL.obtemPorId(codigo);

            return receita;
        }

        public string ExcluirReceita(MLL.Receita receita)
        {
            string result = "";
            ReceitaDAL recDAL = new ReceitaDAL(db);
            
            if (recDAL.Remover(receita))
            {
                result = "ok";
            }
            else
            {
                result = "erro - " + recDAL.erro;
            }

            return result;
        }

        public  JObject CadastrarReceita(string objreceita)
        {
            JObject obj = new JObject();
            JArray arr = JArray.Parse(objreceita);
            ReceitaDAL recDAL = new ReceitaDAL(db);

            string descricao = arr[0].Value<string>("Descricao_Receita").ToString();
            string valor = arr[0].Value<decimal>("Valor_Receita").ToString();
            string codigovigencia = arr[0].Value<int>("Cod_Vigencia_Receita").ToString();
            string codigousuario = arr[0].Value<int>("Cod_Usuario_Receita").ToString();

            MLL.Receita receita = new MLL.Receita
            {
                Descricao_Receita = descricao,
                Valor_Receita = Convert.ToDecimal(valor),
                Codigo_Vigencia = Convert.ToInt32(codigovigencia),
                Codigo_Usuario = Convert.ToInt32(codigousuario)
            };

            if (recDAL.Adicionar(receita))
            {
                obj.Add(new JProperty("ok", "ok"));
            }
            else
            {
                obj.Add(new JProperty("erro", recDAL.erro));
            }

            return obj;
        }


        public JObject EditarReceita(string objReceita)
        {
            JObject obj = new JObject();
            ReceitaDAL recDAL = new ReceitaDAL(db);
            JArray arr = JArray.Parse(objReceita);

            string codigoreceita = arr[0].Value<int>("Codigo_Receita").ToString();
            string descricao = arr[0].Value<string>("Descricao_Receita").ToString();
            string valor = arr[0].Value<decimal>("Valor_Receita").ToString();
            string codigovigencia = arr[0].Value<int>("Cod_Vigencia_Receita").ToString();
            string codigousuario = arr[0].Value<int>("Cod_Usuario_Receita").ToString();

            MLL.Receita receita = new MLL.Receita
            {
                Codigo_Receita = Convert.ToInt32(codigoreceita),
                Descricao_Receita = descricao,
                Valor_Receita = Convert.ToDecimal(valor),
                Codigo_Vigencia = Convert.ToInt32(codigovigencia),
                Codigo_Usuario = Convert.ToInt32(codigousuario)
            };

            if (recDAL.Editar(receita))
            {
                obj.Add(new JProperty("ok", "ok"));
            }
            else
            {
                obj.Add(new JProperty("erro", recDAL.erro));
            }

            return obj;
        }

    }
}
