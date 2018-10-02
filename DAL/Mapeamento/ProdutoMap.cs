using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapeamento
{
    public class ProdutoMap: EntityTypeConfiguration<MLL.Produto>
    {
        public ProdutoMap()
        {
            ToTable("TB_PRODUTO");

            HasKey(x => x.Codigo_Produto);

            Property(x => x.Codigo_Produto).HasColumnName("PRO_CODIGO");
            Property(x => x.Descricao_Produto).HasColumnName("PRO_DESCRICAO");
            Property(x => x.Valor_Produto).HasColumnName("PRO_VALOR");
            Property(x => x.Url).HasColumnName("PRO_URL");
            Property(x => x.Path_Image).HasColumnName("PRO_PATHIMAGE");
            Property(x => x.Arquivo_Image).HasColumnName("PRO_ARQUIVOIMAGE");
            Property(x => x.Codigo_Usuario).HasColumnName("PRO_USUCODIGO");


            HasRequired(x => x.Usuario)
                .WithMany()
                .HasForeignKey(y => y.Codigo_Usuario);
        }
    }
}
