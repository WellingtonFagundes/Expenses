using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapeamento
{
    public class PerfilMap : EntityTypeConfiguration<MLL.Perfil>
    {
        public PerfilMap()
        {
            ToTable("TB_PERFIL");

            HasKey(x => x.Codigo_Perfil);

            Property(x => x.Codigo_Perfil).HasColumnName("PER_CODIGO");
            Property(x => x.Nome_Perfil).HasColumnName("PER_NOME");

        }
    }
}
