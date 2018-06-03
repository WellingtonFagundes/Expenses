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
                                                   .OrderBy(c => c.Descricao_Compra)
                                                   .Where(c => c.Codigo_Usuario == codusu)
                                                   .ToList();

            foreach(MLL.CompraFutura com in lista)
            {
                arr.Add(new JObject(
                        new JProperty("Codigo_Compra", com.Codigo_Compra),
                        new JProperty("Descricao_Compra", com.Descricao_Compra),
                        new JProperty("Valor_Compra", com.Valor_Compra),
                        new JProperty("Status", com.Status.Description()),
                        new JProperty("URL", com.URL),
                        new JProperty("Codigo_Usuario", com.Codigo_Usuario),
                        new JProperty("Path_Image", com.Path_Image)
                        
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

            string descricaocompra = arrcompra[0].Value<string>("Descricao_Compra").ToString();
            string valorcompra = arrcompra[0].Value<decimal>("Valor_Compra").ToString();
            string status = arrcompra[0].Value<int>("Status").ToString();
            string url = arrcompra[0].Value<string>("URL").ToString();
            string codigousuario = arrcompra[0].Value<int>("Codigo_Usuario").ToString();

            MLL.CompraFutura compra = new MLL.CompraFutura
            {
                Descricao_Compra = descricaocompra,
                Valor_Compra = Convert.ToDecimal(valorcompra),
                Status = (MLL.Status)Convert.ToInt32(status),
                URL = url,
                Codigo_Usuario = Convert.ToInt32(codigousuario)
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
                        new JProperty("Descricao_Compra",compra.Descricao_Compra),
                        new JProperty("Valor_Compra",compra.Valor_Compra),
                        new JProperty("Status",compra.Status),
                        new JProperty("URL",compra.URL),
                        new JProperty("Codigo_Usuario",compra.Codigo_Usuario)
                        ));
            }

            obj.Add(new JProperty("listacompras", arr));

            return obj;
        }

        public JObject EditarCompraFutura(string objcompra)
        {
            JObject obj = new JObject();
            JArray arrCompra = JArray.Parse(objcompra);

            string codigocompra = arrCompra[0].Value<int>("Codigo_Compra").ToString();
            string descricaocompra = arrCompra[0].Value<string>("Descricao_Compra").ToString();
            string valorcompra = arrCompra[0].Value<decimal>("Valor_Compra").ToString();
            string status = arrCompra[0].Value<int>("Status").ToString();
            string url = arrCompra[0].Value<string>("URL").ToString();
            string codigousuario = arrCompra[0].Value<int>("Codigo_Usuario").ToString();


            MLL.CompraFutura compra = new MLL.CompraFutura
            {
                Codigo_Compra = Convert.ToInt32(codigocompra),
                Descricao_Compra = descricaocompra,
                Valor_Compra = Convert.ToDecimal(valorcompra),
                Status = (MLL.Status)Convert.ToInt32(status),
                URL = url,
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

        public JObject SalvarUploadCompraImagem(HttpPostedFileBase arquivo, int idCompra, string nomearquivoextensao,string caminhoFull,string caminhoRelativo)
        {
            JObject obj = new JObject();

            try
            {
                //Apontando para abrir o arquivo de acordo com o servidor web
                var pathSalvarNaBase = Path.Combine("http:\\EXPENSES" + caminhoRelativo + nomearquivoextensao);

                arquivo.SaveAs(caminhoFull);

                //Rotina para atualizar o id da compra selecionado com o caminho do arquivo de imagem
                CompraFuturaDAL comDAL = new CompraFuturaDAL(db);
                MLL.CompraFutura compra = comDAL.obtemPorId(idCompra);

                compra.Path_Image = pathSalvarNaBase;
                compra.Arquivo_Image = nomearquivoextensao;

                if (comDAL.Editar(compra))
                {
                    obj.Add(new JProperty("ok", caminhoFull));
                }
                else
                {
                    obj.Add(new JProperty("erro", comDAL.erro));
                }

            }
            catch (Exception ex)
            {
                obj.Add(new JProperty("erro", ex.Message));
                return obj;
            }

            return obj;
        }


        public JObject BuscarCaminhoImagem(int id)
        {
            var caminho = "";
            JObject obj = new JObject();
            JArray arr = new JArray();

            CompraFuturaDAL comDAL = new CompraFuturaDAL(db);

            MLL.CompraFutura compra = comDAL.obtemPorId(id);

            if (!object.Equals(compra, null)) { 
                caminho = compra.Path_Image;

                arr.Add(new JObject(
                        new JProperty("Caminho_Imagem", caminho))
                        );
            }

            if (caminho != null)
            {
                obj.Add(new JProperty("listaCaminho", arr));
            }
            else
            {
                obj.Add(new JProperty("vazio", "vazio"));
            }

            return obj;
        }
    }
}
