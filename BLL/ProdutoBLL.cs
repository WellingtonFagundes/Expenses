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

namespace BLL
{
    public class ProdutoBLL:CriptografiaBLL
    {
        public Contexto db;

        public ProdutoBLL()
        {
            db = new Contexto();
        }

        public JObject ListaProdutosCadastradosPorUser(int codusu)
        {
            JObject obj = new JObject();
            JArray arr = new JArray();

            ProdutoDAL proDAL = new ProdutoDAL(db);

            IList<MLL.Produto> lista = proDAL.Tudo()
                                             .Where(p => p.Codigo_Usuario == codusu)
                                             .OrderBy(p => p.Descricao_Produto)
                                             .ToList();

            foreach(MLL.Produto pro in lista)
            {
                arr.Add(new JObject(
                            new JProperty("Codigo_Produto", pro.Codigo_Produto),
                            new JProperty("Descricao_Produto", pro.Descricao_Produto),
                            new JProperty("Valor_Produto", pro.Valor_Produto),
                            new JProperty("Url",pro.Url),
                            new JProperty("Path_Image",pro.Path_Image),
                            new JProperty("Arquivo_Image",pro.Arquivo_Image),
                            new JProperty("Cod_Usu",pro.Codigo_Usuario)
                            ));
            }

            obj.Add(new JProperty("lista", arr));

            return obj;
        }

        public JObject CadastrarProduto(string objprod)
        {
            JObject obj = new JObject();
            JArray arr = JArray.Parse(objprod);

            ProdutoDAL proDAL = new ProdutoDAL(db);

            string descricaoprod = arr[0].Value<string>("Descricao_Produto").ToString();
            string valorprod = arr[0].Value<decimal>("Valor_Produto").ToString();
            string url = arr[0].Value<string>("Url").ToString();
            string codusu = arr[0].Value<int>("Cod_Usu").ToString();

            MLL.Produto pro = new MLL.Produto
            {
                Descricao_Produto = descricaoprod,
                Valor_Produto = Convert.ToDecimal(valorprod),
                Url = url,
                Codigo_Usuario = Convert.ToInt32(codusu)
            };

            if (proDAL.Adicionar(pro))
            {
                obj.Add(new JProperty("ok", "ok"));
            }
            else
            {
                obj.Add(new JProperty("erro", proDAL.erro));
            }

            return obj;
        }

        public JObject EditarProduto(string objProd)
        {
            JObject obj = new JObject();
            JArray arr = JArray.Parse(objProd);

            ProdutoDAL proDAL = new ProdutoDAL(db);

            string codprod = arr[0].Value<int>("Codigo_Produto").ToString();
            string descricaoprod = arr[0].Value<string>("Descricao_Produto").ToString();
            string valorprod = arr[0].Value<decimal>("Valor_Produto").ToString();
            string url = arr[0].Value<string>("Url").ToString();
            string pathimage = arr[0].Value<string>("Path_Image").ToString();
            string arquivoimage = arr[0].Value<string>("Arquivo_Image").ToString();
            string codusu = arr[0].Value<int>("Cod_Usu").ToString();

            MLL.Produto pro = new MLL.Produto
            {
                Codigo_Produto = Convert.ToInt32(codprod),
                Descricao_Produto = descricaoprod,
                Valor_Produto = Convert.ToDecimal(valorprod),
                Url = url,
                Path_Image = pathimage,
                Arquivo_Image = arquivoimage,
                Codigo_Usuario = Convert.ToInt32(codusu)
            };

            if (proDAL.Editar(pro))
            {
                obj.Add(new JProperty("ok", "ok"));
            }
            else
            {
                obj.Add("erro", proDAL.erro);
            }

            return obj;
        }

        public string ExcluirProduto(int codprod)
        {
            string result = "";

            ProdutoDAL proDAL = new ProdutoDAL(db);

            MLL.Produto prod = proDAL.obtemPorId(codprod);

            if (proDAL.Remover(prod))
            {
                result = "ok";
            }
            else
            {
                result = "erro - " + proDAL.erro;
            }

            return result;

        }

        public JObject ObterPorId(int codigo)
        {
            JObject obj = new JObject();
            JArray arr = new JArray();

            ProdutoDAL proDAL = new ProdutoDAL(db);

            MLL.Produto prod = proDAL.obtemPorId(codigo);

            if (!object.Equals(prod, null))
            {
                arr.Add(new JObject(
                            new JProperty("Codigo_Produto",prod.Codigo_Produto),
                            new JProperty("Descricao_Produto",prod.Descricao_Produto),
                            new JProperty("Valor_Produto",prod.Valor_Produto),
                            new JProperty("Url",prod.Url),
                            new JProperty("Path_Image",prod.Path_Image),
                            new JProperty("Arquivo_Image",prod.Arquivo_Image),
                            new JProperty("Cod_Usu",prod.Codigo_Usuario)
                    ));
            }

            obj.Add(new JProperty("listaProduto", arr));

            return obj;
        }

        public JObject SalvarUploadCompraImagem(HttpPostedFileBase arquivo, int idProduto, string nomearquivoextensao, string caminhoFull, string caminhoRelativo)
        {
            JObject obj = new JObject();

            try
            {
                //Apontando para abrir o arquivo de acordo com o servidor web
                var pathSalvarNaBase = Path.Combine("http:\\EXPENSES" + caminhoRelativo + nomearquivoextensao);

                arquivo.SaveAs(caminhoFull);

                //Rotina para atualizar o id do produto selecionado com o caminho do arquivo de imagem
                ProdutoDAL proDAL = new ProdutoDAL(db);
                MLL.Produto produto = proDAL.obtemPorId(idProduto);

                produto.Path_Image = pathSalvarNaBase;
                produto.Arquivo_Image = nomearquivoextensao;

                if (proDAL.Editar(produto))
                {
                    obj.Add(new JProperty("ok", caminhoFull));
                }
                else
                {
                    obj.Add(new JProperty("erro", proDAL.erro));
                }

            }
            catch (Exception ex)
            {
                obj.Add(new JProperty("erro", ex.Message));
                return obj;
            }

            return obj;
        }


        public JObject BuscarCaminhoImagem(int codprod)
        {
            JObject obj = new JObject();
            JArray arr = new JArray();
            var caminho = "";

            ProdutoDAL proDAL = new ProdutoDAL(db);

            MLL.Produto pro = proDAL.obtemPorId(codprod);

            if (!object.Equals(pro, null))
            {
                caminho = pro.Path_Image;
                
                arr.Add(new JObject(
                        new JProperty("Caminho_Imagem",caminho)
                    ));
            }

            if (caminho != null)
            {
                obj.Add(new JProperty("listacaminho",arr));
            }
            else
            {
                obj.Add(new JProperty("vazio", "vazio"));
            }

            return obj;
        }

    }
}
