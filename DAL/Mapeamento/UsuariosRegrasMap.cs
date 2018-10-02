using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapeamento
{
    public class UsuariosRegrasMap:EntityTypeConfiguration<MLL.UsuariosRegras>
    {
        public UsuariosRegrasMap()
        {
            ToTable("TB_USUARIOS_REGRAS");

            HasKey(x => x.Codigo_Usuarios_Regras);

            Property(x => x.Codigo_Usuarios_Regras).HasColumnName("USR_CODIGO");
            Property(x => x.Codigo_Usuario).HasColumnName("USR_USUCODIGO");
            Property(x => x.Codigo_Regra).HasColumnName("USR_REGCODIGO");

            HasRequired(x => x.Usuario)
                .WithMany()
                .HasForeignKey(y => y.Codigo_Usuario);

            HasRequired(x => x.Regra)
                .WithMany()
                .HasForeignKey(y => y.Codigo_Regra);
        }
    }
}
