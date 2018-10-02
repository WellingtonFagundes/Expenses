using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLL
{
    public class UsuariosRegras
    {
        [Key]
        public int Codigo_Usuarios_Regras { get; set; }

        public int Codigo_Usuario { get; set; }
        public virtual Usuario Usuario { get; set; }

        public int Codigo_Regra { get; set; }
        public virtual Regras Regra { get; set; }
    }
}
