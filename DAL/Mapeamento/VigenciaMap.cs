using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapeamento
{
    public class VigenciaMap : EntityTypeConfiguration<MLL.Vigencia>
    {
        public VigenciaMap()
        {
            ToTable("TB_VIGENCIA");

            HasKey(x => x.Codigo_Vigencia);

            Property(x => x.Codigo_Vigencia).HasColumnName("VIG_CODIGO");
            Property(X => X.Data_Inicio).HasColumnName("VIG_DATAINICIO");
            Property(x => x.Data_Final).HasColumnName("VIG_DATAFINAL");
            Property(x => x.Codigo_Usuario).HasColumnName("VIG_USUCODIGO");
        }
    }
}
