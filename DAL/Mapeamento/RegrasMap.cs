using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapeamento
{
    public class RegrasMap:EntityTypeConfiguration<MLL.Regras>
    {
        public RegrasMap()
        {
            ToTable("TB_REGRAS");

            HasKey(x => x.Codigo_Regra);

            Property(x => x.Codigo_Regra).HasColumnName("REG_CODIGO");
            Property(x => x.Descricao_Regra).HasColumnName("REG_DESCRICAO");


        }
    }
}
