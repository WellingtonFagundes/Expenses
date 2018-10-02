using DAL;
using DAL.Consulta;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BLL
{
    public class ComprasFuturasBLL: CriptografiaBLL
    {
        public Contexto db;

        public ComprasFuturasBLL()
        {
            db = new Contexto();
        }

        public JObject ObterComprasFuturasPorUsuario(int codusu)
        {
            JObject obj = new JObject();
            JArray arr = new JArray();

            CompraFuturaDAL comfDAL = new CompraFuturaDAL(db);

            IList<MLL.CompraFutura> lista = comfDAL.Tudo()
                                                   .OrderBy(c => c.Codigo_Compra)
                                                   .Where(c => c.Codigo_Usuario == codusu)
                                                   .ToList();

            foreach(MLL.CompraFutura com in lista)
            {
                arr.Add(new JObject(
                        new JProperty("Codigo_Compra", com.Codigo_Compra),
                        new JProperty("Desc_Prod", com.Produto.Descricao_Produto),
                        new JProperty("Val_Prod", com.Produto.Valor_Produto),
                        new JProperty("Status", com.Status.Description()),
                        new JProperty("URL", com.Produto.Url),
                        new JProperty("Codigo_Usuario", com.Codigo_Usuario),
                        new JProperty("Cod_Prod", com.Codigo_Prod),
                        new JProperty("Path_Image", com.Produto.Path_Image)
                        
                        ));
            }

            obj.Add(new JProperty("lista", arr));

            return obj;
        }

        public JObject CadastrarCompraFutura(string objcompra)
        {
            JObject obj = new JObject();
            JArray arrcompra = JArray.Parse(objcompra);

            CompraFuturaDAL compraDAL = new CompraFuturaDAL(db);

            string descricaocompra = arrcompra[0].Value<string>("Desc_Prod").ToString();
            string valorcompra = arrcompra[0].Value<decimal>("Val_Prod").ToString();
            string status = arrcompra[0].Value<int>("Status").ToString();
            string url = arrcompra[0].Value<string>("URL").ToString();
            string codigousuario = arrcompra[0].Value<int>("Codigo_Usuario").ToString();
            string codigoproduto = arrcompra[0].Value<int?>("Cod_Prod").ToString();

            MLL.CompraFutura compra = new MLL.CompraFutura
            {
                Status = (MLL.Status)Convert.ToInt32(status),
                Codigo_Usuario = Convert.ToInt32(codigousuario),
                Codigo_Prod = Convert.ToInt32(codigoproduto)
            };

            if (compraDAL.Adicionar(compra))
            {
                obj.Add(new JProperty("ok", "ok"));
            }
            else
            {
                obj.Add(new JProperty("erro", compraDAL.erro));
            }

            return obj;
        }

        //Método para preenchimento da combo Status no cadastro de Compras Futuras
        public JObject ObterListaStatus()
        {
            JObject obj = new JObject();
            JArray arr = new JArray();

            arr.Add(new JObject(
                    new JProperty("texto", MLL.Status.OK.Description()),
                    new JProperty("codigo", (int)MLL.Status.OK)
                    ));

            arr.Add(new JObject(
                    new JProperty("texto", MLL.Status.Aguardando.Description()),
                    new JProperty("codigo", (int)MLL.Status.Aguardando)
                    ));

            obj.Add(new JProperty("lista", arr));

            return obj;

        }

        public JObject ObterPorId(int id)
        {
            JObject obj = new JObject();
            JArray arr = new JArray();
            CompraFuturaDAL comDAL = new CompraFuturaDAL(db);

            MLL.CompraFutura compra = comDAL.obtemPorId(id);

            if (!object.Equals(compra, null))
            {
                arr.Add(new JObject(
                        new JProperty("Codigo_Compra", compra.Codigo_Compra),
                        new JProperty("Desc_Prod",compra.Produto.Descricao_Produto),
                        new JProperty("Val_Prod",compra.Produto.Valor_Produto),
                        new JProperty("Status",compra.Status),
                        new JProperty("URL",compra.Produto.Url),
                        new JProperty("Codigo_Usuario",compra.Codigo_Usuario),
                        new JProperty("Cod_Prod",compra.Codigo_Prod)
                        ));
            }

            obj.Add(new JProperty("listacompras", arr));

            return obj;
        }

        public JObject ObterProdutosNaCompraDeUsuario(int codUser)
        {
            JObject obj = new JObject();
            JArray arr = new JArray();
            ProdutoDAL proDAL = new ProdutoDAL(db);

            IList<MLL.Produto> produtos = proDAL.Tudo()
                                                .Where(p => p.Codigo_Usuario == codUser)
                                                .OrderBy(p => p.Codigo_Produto)
                                                .ToList();

            foreach(var pro in produtos)
            {
                arr.Add(new JObject(
                            new JProperty("Codigo_Produto", pro.Codigo_Produto),
                            new JProperty("Descricao_Produto",pro.Descricao_Produto),
                            new JProperty("Valor_Produto",pro.Valor_Produto),
                            new JProperty("Url",pro.Url)
                            
                            ));
            }

            obj.Add(new JProperty("listaProdutos",arr));

            return obj;
        }


        public JObject EditarCompraFutura(string objcompra)
        {
            JObject obj = new JObject();
            JArray arrCompra = JArray.Parse(objcompra);

            string codigocompra = arrCompra[0].Value<int>("Codigo_Compra").ToString();
            string codigoproduto = arrCompra[0].Value<int?>("Cod_Prod").ToString();
            string status = arrCompra[0].Value<int>("Status").ToString();
            string codigousuario = arrCompra[0].Value<int>("Codigo_Usuario").ToString();


            MLL.CompraFutura compra = new MLL.CompraFutura
            {
                Codigo_Compra = Convert.ToInt32(codigocompra),
                Codigo_Prod = Convert.ToInt32(codigoproduto),
                Status = (MLL.Status)Convert.ToInt32(status),
                Codigo_Usuario = Convert.ToInt32(codigousuario)
            };

            CompraFuturaDAL comDAL = new CompraFuturaDAL(db);

            if (comDAL.Editar(compra))
            {
                obj.Add(new JProperty("ok", "ok"));
            }
            else
            {
                obj.Add(new JProperty("erro", comDAL.erro));
            }

            return obj;
        }

        public string ExcluirCompraFutura(int codigo)
        {
            string result = "";
            CompraFuturaDAL comDAL = new CompraFuturaDAL(db);

            MLL.CompraFutura compra = comDAL.obtemPorId(codigo);

            if (comDAL.Remover(compra))
            {
                result = "ok";
            }
            else
            {
                result = "erro - " + comDAL.erro;
            }

            return result;
        }

       


      
    }
}
