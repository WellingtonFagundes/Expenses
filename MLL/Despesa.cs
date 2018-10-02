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
    public class Despesa
    {
        public int Codigo_Despesa { get; set; }

        [Required()]
        [DisplayName("Descrição")]
        public string Descricao_Despesa { get; set; }

        [Required()]
        [DisplayName("Valor")]
        public decimal Valor_Despesa { get; set; }

        [Required()]
        [DisplayName("Status")]
        public Status Status { get; set; }

        public int Codigo_Pagamento { get; set; }
        public virtual FormaPagamento FormaPagamento { get; set; }
        
        public int Codigo_Usuario { get; set; }
        public virtual Usuario Usuario { get; set; }

        public int Codigo_Vigencia { get; set; }
        public virtual Vigencia Vigencia { get; set; }

        public int Codigo_Produto { get; set; }
        public virtual Produto Produto { get; set; }

        [NotMapped()]
        public string CodigoCript { get; set; }
    }

    public enum Status
    {
        [Description("OK")]
        OK = 1,

        [Description("Aguardando")]
        Aguardando = 2
    }
}
