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
    public class Receita
    {
        public int Codigo_Receita { get; set; }
        
        [Required()]
        [DisplayName("Descrição")]
        public string Descricao_Receita { get; set; }

        [Required()]
        [DisplayName("Valor")]
        public decimal Valor_Receita { get; set; }


        public int Codigo_Vigencia { get; set; }
        public virtual Vigencia Vigencia { get; set; }

        public int Codigo_Usuario { get; set; }
        public virtual Usuario Usuario { get; set; }

        [NotMapped()]
        public string CodigoCript { get; set; }
    }
}
