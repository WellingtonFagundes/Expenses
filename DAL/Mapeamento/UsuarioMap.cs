using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapeamento
{
    public class UsuarioMap : EntityTypeConfiguration<MLL.Usuario>
    {
        public UsuarioMap()
        {
            ToTable("TB_USUARIOS");

            HasKey(x => x.Codigo_Usuario);

            Property(x => x.Codigo_Usuario).HasColumnName("USU_CODIGO");
            Property(x => x.Nome_Usuario).HasColumnName("USU_NOME");
            Property(x => x.Email).HasColumnName("USU_EMAIL");
            Property(x => x.Senha).HasColumnName("USU_SENHA");
            Property(x => x.Cargo).HasColumnName("USU_CARGO");
            Property(x => x.Excluido).HasColumnName("USU_EXCLUIDO");
            Property(x => x.Administrador).HasColumnName("USU_ADMINISTRADOR");
            Property(x => x.Codigo_Perfil).HasColumnName("USU_PERCODIGO");
            Property(x => x.Path_Image).HasColumnName("USU_PATHIMAGE");

            HasRequired(x => x.Perfil)
                .WithMany()
                .HasForeignKey(y => y.Codigo_Perfil);
        }
    }
}
