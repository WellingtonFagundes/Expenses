using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapeamento
{
    public class ReceitaMap :EntityTypeConfiguration<MLL.Receita>
    {
        public ReceitaMap()
        {
            ToTable("TB_RECEITA");

            HasKey(x => x.Codigo_Receita);

            Property(x => x.Codigo_Receita).HasColumnName("REC_CODIGO");
            Property(x => x.Descricao_Receita).HasColumnName("REC_DESCRICAO");
            Property(x => x.Valor_Receita).HasColumnName("REC_VALOR");
            Property(x => x.Codigo_Usuario).HasColumnName("REC_USUCODIGO");
            Property(x => x.Codigo_Vigencia).HasColumnName("REC_VIGCODIGO");

            HasRequired(x => x.Usuario)
                .WithMany()
                .HasForeignKey(y => y.Codigo_Usuario);

            HasRequired(x => x.Vigencia)
                .WithMany()
                .HasForeignKey(y => y.Codigo_Vigencia);
        }
    }
}
