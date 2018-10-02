using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLL
{
    public class Regras
    {

        public Regras()
        {
            UsuariosRegras = new HashSet<UsuariosRegras>();
        }
        
        public int Codigo_Regra { get; set; }

        [Required]
        public string Descricao_Regra { get; set; }

        public virtual ICollection<UsuariosRegras> UsuariosRegras { get; set; }

        [NotMapped()]
        public int idcriptografado { get; set; }
    }
}
