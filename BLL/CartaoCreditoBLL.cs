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
    public class CartaoCreditoBLL : CriptografiaBLL
    {
        public Contexto db;

        public CartaoCreditoBLL()
        {
            db = new Contexto();
        }

        public JObject CartoesCreditoCadastrados(int codVigencia,int codUser)
        {
            JObject obj = new JObject();
            JArray arr = new JArray();
            CartaoCreditoDAL carDAL = new CartaoCreditoDAL(db);

            List<MLL.CartaoCredito> lista = carDAL.Tudo()
                                                  .Where(c => c.Codigo_Vigencia == codVigencia && c.Codigo_Usuario == codUser)
                                                  .OrderBy(c => c.Codigo_Cartao_Credito).ToList();


            foreach(MLL.CartaoCredito car in lista)
            {
                arr.Add(new JObject(
                        new JProperty("Codigo_Cartao_Credito", car.Codigo_Cartao_Credito),
                        new JProperty("Compra", car.Compra),
                        new JProperty("Valor_Mes", car.Valor_Mes),
                        new JProperty("Parcelas", car.Parcelas),
                        new JProperty("Total", car.Total),
                        new JProperty("Codigo_Vigencia_Cartao", car.Codigo_Vigencia),
                        new JProperty("Codigo_Usuario_Cartao", car.Codigo_Usuario)
                        ));
            }

            obj.Add(new JProperty("lista", arr));

            return obj;
        }

        public JObject CarregarCartaoCredito(int codigo)
        {
            JObject obj = new JObject();
            JArray arr = new JArray();
            CartaoCreditoDAL carDAL = new CartaoCreditoDAL(db);

            MLL.CartaoCredito car = carDAL.obtemPorId(codigo);

            if (!object.Equals(car,null))
            {
                arr.Add(new JObject(
                        new JProperty("Codigo_Cartao_Credito", car.Codigo_Cartao_Credito),
                        new JProperty("Compra", car.Compra),
                        new JProperty("Valor_Mes", car.Valor_Mes),
                        new JProperty("Parcelas", car.Parcelas),
                        new JProperty("Total", car.Total),
                        new JProperty("Codigo_Vigencia_Cartao", car.Codigo_Vigencia),
                        new JProperty("Codigo_Usuario_Cartao", car.Codigo_Usuario)
                        ));

                obj.Add(new JProperty("listaCartao", arr));
            }
            else
            {
                obj.Add(new JProperty("erro", carDAL.erro));
            }

           
            return obj;
        }

        public MLL.CartaoCredito ObterPorId(int codigo)
        {
            CartaoCreditoDAL carDAL = new CartaoCreditoDAL(db);

            return carDAL.obtemPorId(codigo);
        }

        public string ExcluirCartao(MLL.CartaoCredito cartao)
        {
            string result = "";

            CartaoCreditoDAL carDAL = new CartaoCreditoDAL(db);

            if (carDAL.Remover(cartao))
            {
                result = "ok";
            }
            else
            {
                result = "erro - " + carDAL.erro;
            }

            return result;
        }

        public JObject SalvarCartao(string objCartao)
        {
            JObject obj = new JObject();
            JArray arr = JArray.Parse(objCartao);
            CartaoCreditoDAL carDAL = new CartaoCreditoDAL(db);

            string compra = arr[0].Value<string>("Compra").ToString();
            string valormes = arr[0].Value<decimal>("Valor_Mes").ToString();
            string parcelas = arr[0].Value<int>("Parcelas").ToString();
            string total = arr[0].Value<decimal>("Total").ToString();
            string codigovigencia = arr[0].Value<int>("Codigo_Vigencia_Cartao").ToString();
            string codigousuario = arr[0].Value<int>("Codigo_Usuario_Cartao").ToString();

            MLL.CartaoCredito cartao = new MLL.CartaoCredito
            {
                Compra = compra,
                Valor_Mes = Convert.ToDecimal(valormes),
                Parcelas = Convert.ToInt32(parcelas),
                Total = Convert.ToDecimal(total),
                Codigo_Vigencia = Convert.ToInt32(codigovigencia),
                Codigo_Usuario = Convert.ToInt32(codigousuario)
            };

            if (carDAL.Adicionar(cartao))
            {
                obj.Add(new JProperty("ok", "ok"));
            }
            else
            {
                obj.Add(new JProperty("erro", carDAL.erro));
            }

            return obj;
        }

        public JObject EditarCartao(string objCartao)
        {
            JObject obj = new JObject();
            JArray arr = JArray.Parse(objCartao);
            CartaoCreditoDAL carDAL = new CartaoCreditoDAL(db);

            string codigocartao = arr[0].Value<int>("Codigo_Cartao_Credito").ToString();
            string compra = arr[0].Value<string>("Compra").ToString();
            string valormes = arr[0].Value<decimal>("Valor_Mes").ToString();
            string parcelas = arr[0].Value<int>("Parcelas").ToString();
            string total = arr[0].Value<decimal>("Total").ToString();
            string codigovigencia = arr[0].Value<int>("Codigo_Vigencia_Cartao").ToString();
            string codigousuario = arr[0].Value<int>("Codigo_Usuario_Cartao").ToString();

            MLL.CartaoCredito cartao = new MLL.CartaoCredito
            {
                Codigo_Cartao_Credito = Convert.ToInt32(codigocartao),
                Compra = compra,
                Valor_Mes = Convert.ToDecimal(valormes),
                Parcelas = Convert.ToInt32(parcelas),
                Total = Convert.ToDecimal(total),
                Codigo_Vigencia = Convert.ToInt32(codigovigencia),
                Codigo_Usuario = Convert.ToInt32(codigousuario)
            };

            if (carDAL.Editar(cartao))
            {
                obj.Add(new JProperty("ok", "ok"));
            }
            else
            {
                obj.Add(new JProperty("erro", carDAL.erro));
            }

            return obj;
        }
    }
}
