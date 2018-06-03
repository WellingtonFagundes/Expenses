using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapeamento
{
    public class CartaoCreditoMap : EntityTypeConfiguration<MLL.CartaoCredito>
    {
        public CartaoCreditoMap()
        {
            ToTable("TB_CARTAO_CREDITO");

            //Key
            HasKey(x => x.Codigo_Cartao_Credito);

            //Propriedades
            Property(x => x.Codigo_Cartao_Credito).HasColumnName("CAC_CODIGO");
            Property(x => x.Compra).HasColumnName("CAC_COMPRA");
            Property(x => x.Valor_Mes).HasColumnName("CAC_VALORMES");
            Property(x => x.Parcelas).HasColumnName("CAC_PARCELAS");
            Property(x => x.Total).HasColumnName("CAC_TOTAL");
            Property(x => x.Codigo_Vigencia).HasColumnName("CAC_VIGCODIGO");
            Property(x => x.Codigo_Usuario).HasColumnName("CAC_USUCODIGO");

            HasRequired(x => x.Vigencia)
                .WithMany()
                .HasForeignKey(y => y.Codigo_Vigencia);

            HasRequired(x => x.Usuario)
                .WithMany()
                .HasForeignKey(y => y.Codigo_Usuario);
        }
    }
}
