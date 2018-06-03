using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapeamento
{
    public class FormaPagamentoMap :EntityTypeConfiguration<MLL.FormaPagamento>
    {
        public FormaPagamentoMap()
        {
            ToTable("TB_FORMA_PAGAMENTO");

            HasKey(x => x.Codigo_Forma_Pagamento);

            Property(x => x.Codigo_Forma_Pagamento).HasColumnName("FOP_CODIGO");
            Property(x => x.Descricao_Forma_Pagamento).HasColumnName("FOP_DESCRICAO");
        }
    }
}
