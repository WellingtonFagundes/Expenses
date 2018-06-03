using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapeamento
{
    public class CompraFuturaMap :EntityTypeConfiguration<MLL.CompraFutura>
    {
        public CompraFuturaMap()
        {
            ToTable("TB_COMPRAS_FUTURAS");

            HasKey(x => x.Codigo_Compra);

            Property(x => x.Codigo_Compra).HasColumnName("COF_CODIGO");
            Property(x => x.Descricao_Compra).HasColumnName("COF_DESCRICAO");
            Property(x => x.Valor_Compra).HasColumnName("COF_VALOR");
            Property(x => x.Status).HasColumnName("COF_STATUS");
            Property(x => x.URL).HasColumnName("COF_URL");
            Property(x => x.Path_Image).HasColumnName("COF_PATHIMAGE");
            Property(x => x.Arquivo_Image).HasColumnName("COF_ARQUIVOIMAGE");
            Property(x => x.Codigo_Usuario).HasColumnName("COF_USUCODIGO");

            HasRequired(x => x.Usuario)
                .WithMany()
                .HasForeignKey(y => y.Codigo_Usuario);
        }
    }
}
