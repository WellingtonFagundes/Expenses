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
    public class CompraFutura
    {
        public int Codigo_Compra { get; set; }

        [Required()]
        [DisplayName("Descrição")]
        public string Descricao_Compra { get; set; }

        [Required()]
        [DisplayName("Valor")]
        public decimal Valor_Compra { get; set; }

        [Required()]
        [DisplayName("Status")]
        public Status Status { get; set; }
        
        [Required()]
        [DisplayName("URL")]
        public string URL { get; set; }

        public string Path_Image { get; set; }
        public string Arquivo_Image { get; set; }

        public int Codigo_Usuario { get; set; }
        public virtual Usuario Usuario { get; set; }

        [NotMapped()]
        public string CodigoCript { get; set; }
    }
}
