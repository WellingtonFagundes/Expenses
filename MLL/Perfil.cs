using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLL
{
    public class Perfil
    {
        public Perfil()
        {
            Usuarios = new HashSet<Usuario>();
        }


        public int Codigo_Perfil { get; set; }

        [Required()]
        [DisplayName("Nome Perfil")]
        public string Nome_Perfil { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }

        [NotMapped()]
        public string CodigoCript { get; set; }
    }
}
