using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapeamento
{
    public class DespesaMap : EntityTypeConfiguration<MLL.Despesa>
    {
        public DespesaMap()
        {
            ToTable("TB_DESPESA");

            HasKey(x => x.Codigo_Despesa);

            Property(x => x.Codigo_Despesa).HasColumnName("DES_CODIGO");
            Property(x => x.Descricao_Despesa).HasColumnName("DES_DESCRICAO");
            Property(x => x.Valor_Despesa).HasColumnName("DES_VALOR");
            Property(x => x.Status).HasColumnName("DES_STATUS");
            Property(x => x.Codigo_Pagamento).HasColumnName("DES_FOPCODIGO");
            Property(x => x.Codigo_Usuario).HasColumnName("DES_USUCODIGO");
            Property(x => x.Codigo_Vigencia).HasColumnName("DES_VIGCODIGO");

            HasRequired(x => x.FormaPagamento)
                .WithMany()
                .HasForeignKey(y => y.Codigo_Pagamento);

            HasRequired(x => x.Usuario)
                .WithMany()
                .HasForeignKey(y => y.Codigo_Usuario);

            HasRequired(x => x.Vigencia)
                .WithMany()
                .HasForeignKey(y => y.Codigo_Vigencia);
        }
    }
}
