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
    public class FormasPagamentoBLL : CriptografiaBLL
    {
        public Contexto db;

        public FormasPagamentoBLL()
        {
            db = new Contexto();
        }

        public IList<MLL.FormaPagamento> ObterListaFormasPag()
        {
            FormaPagamentoDAL forma = new FormaPagamentoDAL(db);

            IList<MLL.FormaPagamento> lista = forma.Tudo().OrderBy(f => f.Descricao_Forma_Pagamento).ToList();

            foreach(MLL.FormaPagamento formas in lista)
            {
                formas.CodigoCript = CriptografaID(formas.Codigo_Forma_Pagamento);
            }

            return lista;

        }

        public JObject CadastrarFormaPag(MLL.FormaPagamento formaPag)
        {
            JObject obj = new JObject();
            FormaPagamentoDAL formaDAL = new FormaPagamentoDAL(db);

            if (formaDAL.Adicionar(formaPag))
            {
                obj.Add(new JProperty("ok", "ok"));
            }
            else
            {
                obj.Add(new JProperty("erro", formaDAL.erro));
            }

            return obj;
        }

        public string ExcluirFormaPag(MLL.FormaPagamento formaPag)
        {
            string result = "";
            FormaPagamentoDAL formaDAL = new FormaPagamentoDAL(db);

            if (formaDAL.Remover(formaPag))
            {
                result = "ok";
            }
            else
            {
                result = "erro - " + formaDAL.erro;
            }

            return result;
        }

        public MLL.FormaPagamento ObterPorId(int id)
        {
            FormaPagamentoDAL formaDAL = new FormaPagamentoDAL(db);

            MLL.FormaPagamento forma = formaDAL.obtemPorId(id);

            return forma;
        }

        public JObject EditarFormaPag(MLL.FormaPagamento forma)
        {
            JObject obj = new JObject();
            FormaPagamentoDAL formaDAL = new FormaPagamentoDAL(db);

            if (formaDAL.Editar(forma))
            {
                obj.Add(new JProperty("ok", "ok"));
            }
            else
            {
                obj.Add(new JProperty("erro", formaDAL.erro));
            }

            return obj;
        }
    }
}
