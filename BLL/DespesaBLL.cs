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
    public class DespesaBLL: CriptografiaBLL
    {
        public Contexto db;

        public DespesaBLL()
        {
            db = new Contexto();
        }

        public JObject DespesasCadastradas(int codVigencia,int codUser)
        {
            JObject obj = new JObject();
            JArray arr = new JArray();
            DespesaDAL desDAL = new DespesaDAL(db);

            List<MLL.Despesa> lista = desDAL.Tudo()
                                            .Where(d => d.Codigo_Vigencia == codVigencia && d.Codigo_Usuario == codUser)
                                            .OrderBy(d => d.Codigo_Despesa)
                                            .ToList();

            foreach(MLL.Despesa des in lista)
            {
                arr.Add(new JObject(
                        new JProperty("Codigo_Despesa", des.Codigo_Despesa),
                        new JProperty("Descricao_Despesa", des.Descricao_Despesa),
                        new JProperty("Valor_Despesa", des.Valor_Despesa),
                        new JProperty("Status_Despesa", des.Status.Description()),
                        new JProperty("Forma_Pagamento_Despesa", des.Codigo_Pagamento),
                        new JProperty("Codigo_Vigencia_Despesa", des.Codigo_Vigencia),
                        new JProperty("Codigo_Usuario_Despesa", des.Codigo_Usuario)
                        ));
            }

            obj.Add(new JProperty("listaDespesas", arr));

            return obj;
        }

        public JObject CarregarDespesa(int codDespesa)
        {
            JObject obj = new JObject();
            JArray arr = new JArray();
            DespesaDAL desDAL = new DespesaDAL(db);

            MLL.Despesa despesa = desDAL.obtemPorId(codDespesa);

            if (!object.Equals(despesa, null))
            {
                arr.Add(new JObject(
                        new JProperty("Codigo_Despesa", despesa.Codigo_Despesa),
                        new JProperty("Descricao_Despesa", despesa.Descricao_Despesa),
                        new JProperty("Valor_Despesa", despesa.Valor_Despesa),
                        new JProperty("Status_Despesa", despesa.Status.Description()),
                        new JProperty("Forma_Pagamento_Despesa", despesa.Codigo_Pagamento),
                        new JProperty("Codigo_Vigencia_Despesa", despesa.Codigo_Vigencia),
                        new JProperty("Codigo_Usuario_Despesa", despesa.Codigo_Usuario)
                    ));

                obj.Add(new JProperty("listaDespesa", arr));
            }
            else
            {
                obj.Add(new JProperty("erro", desDAL.erro));
            }

            return obj;
        }

        public MLL.Despesa ObterPorId(int codigo)
        {
            DespesaDAL desDAL = new DespesaDAL(db);

            return desDAL.obtemPorId(codigo);
        }

        public string ExcluirDespesa(MLL.Despesa despesa)
        {
            DespesaDAL desDAL = new DespesaDAL(db);
            string result = "";

            if (desDAL.Remover(despesa))
            {
                result = "ok";
            }
            else
            {
                result = "erro - " + desDAL.erro;
            }

            return result;
        }


        public JObject CadastrarDespesa(string objDespesa)
        {
            JObject obj = new JObject();
            JArray arr = JArray.Parse(objDespesa);

            DespesaDAL desDAL = new DespesaDAL(db);

            string descricao = arr[0].Value<string>("Descricao_Despesa").ToString();
            string valor = arr[0].Value<decimal>("Valor_Despesa").ToString();
            string status = arr[0].Value<int>("Status_Despesa").ToString();
            string formapag = arr[0].Value<int>("Forma_Pagamento_Despesa").ToString();
            string codvigencia = arr[0].Value<int>("Codigo_Vigencia_Despesa").ToString();
            string codusuario = arr[0].Value<int>("Codigo_Usuario_Despesa").ToString();

            MLL.Despesa des = new MLL.Despesa
            {
                Descricao_Despesa = descricao,
                Valor_Despesa = Convert.ToDecimal(valor),
                Status = (MLL.Status)Convert.ToInt32(status),
                Codigo_Pagamento = Convert.ToInt32(formapag),
                Codigo_Vigencia = Convert.ToInt32(codvigencia),
                Codigo_Usuario = Convert.ToInt32(codusuario)
            };

            if (desDAL.Adicionar(des))
            {
                obj.Add(new JProperty("ok", "ok"));
            }
            else
            {
                obj.Add(new JProperty("erro", desDAL.erro));
            }

            return obj;

        }


        public JObject EditarDespesa(string objDespesa)
        {
            JObject obj = new JObject();
            JArray arr = JArray.Parse(objDespesa);

            DespesaDAL desDAL = new DespesaDAL(db);

            string codigodespesa = arr[0].Value<int>("Codigo_Despesa").ToString();
            string descricao = arr[0].Value<string>("Descricao_Despesa").ToString();
            string valor = arr[0].Value<decimal>("Valor_Despesa").ToString();
            string status = arr[0].Value<int>("Status_Despesa").ToString();
            string formapag = arr[0].Value<int>("Forma_Pagamento_Despesa").ToString();
            string codvigencia = arr[0].Value<int>("Codigo_Vigencia_Despesa").ToString();
            string codusuario = arr[0].Value<int>("Codigo_Usuario_Despesa").ToString();

            MLL.Despesa des = new MLL.Despesa
            {
                Codigo_Despesa = Convert.ToInt32(codigodespesa),
                Descricao_Despesa = descricao,
                Valor_Despesa = Convert.ToDecimal(valor),
                Status = (MLL.Status)Convert.ToInt32(status),
                Codigo_Pagamento = Convert.ToInt32(formapag),
                Codigo_Vigencia = Convert.ToInt32(codvigencia),
                Codigo_Usuario = Convert.ToInt32(codusuario)
            };

            if (desDAL.Editar(des))
            {
                obj.Add(new JProperty("ok", "ok"));
            }
            else
            {
                obj.Add(new JProperty("erro", desDAL.erro));
            }

            return obj;

        }

        public JObject ObterStatusDespesa()
        {
            JObject obj = new JObject();
            JArray arr = new JArray();

            arr.Add(new JObject(
                   new JProperty("codigo",(int) MLL.Status.OK),
                   new JProperty("texto", MLL.Status.OK.Description())
                ));

            arr.Add(new JObject(
                new JProperty("codigo", (int)MLL.Status.Aguardando),
                new JProperty("texto", MLL.Status.Aguardando.Description())
                ));

            obj.Add(new JProperty("lista", arr));

            return obj;
        }

        public JObject ObterFormaPag()
        {
            JObject obj = new JObject();
            JArray arr = new JArray();
            FormaPagamentoDAL formPag = new FormaPagamentoDAL(db);

            List<MLL.FormaPagamento> lista = formPag.Tudo()
                                                    .OrderBy(f => f.Descricao_Forma_Pagamento)
                                                    .ToList();

            foreach(MLL.FormaPagamento forma in lista)
            {
                arr.Add(new JObject(
                    new JProperty("codigo", forma.Codigo_Forma_Pagamento),
                    new JProperty("texto", forma.Descricao_Forma_Pagamento)
                    ));
            }

            obj.Add(new JProperty("lista", arr));

            return obj;
        }
    }
}
