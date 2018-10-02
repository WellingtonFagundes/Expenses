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
            Property(x => x.Status).HasColumnName("COF_STATUS");
            Property(x => x.Codigo_Usuario).HasColumnName("COF_USUCODIGO");
            Property(X => X.Codigo_Prod).HasColumnName("COF_PROCODIGO");

            HasRequired(x => x.Usuario)
                .WithMany()
                .HasForeignKey(y => y.Codigo_Usuario);

            HasRequired(x => x.Produto)
               .WithMany()
               .HasForeignKey(y => y.Codigo_Prod);
        }
    }
}
