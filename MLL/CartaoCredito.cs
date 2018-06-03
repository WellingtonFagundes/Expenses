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
    public class CartaoCredito
    {
        public int Codigo_Cartao_Credito { get; set; }

        [Required()]
        [DisplayName("Descrição")]
        public string Compra { get; set; }

        [Required()]
        [DisplayName("Valor Mês")]
        public decimal Valor_Mes { get; set; }

        [Required()]
        [DisplayName("Parcelas")]
        public int Parcelas { get; set; }

        [DisplayName("Total")]
        public decimal Total { get; set; }

        public int Codigo_Vigencia { get; set; }
        public virtual Vigencia Vigencia { get; set; }

        public int Codigo_Usuario { get; set; }
        public Usuario Usuario { get; set; }

        [NotMapped()]
        public string CodigoCript { get; set; }
    }
}
