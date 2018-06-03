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
   public class FormaPagamento
   {
        public FormaPagamento()
        {
            Despesas = new HashSet<Despesa>();
        }

        public int Codigo_Forma_Pagamento { get; set; }

        [Required()]
        [DisplayName("Forma Pagamento")]
        public string Descricao_Forma_Pagamento { get; set; }

        public virtual ICollection<Despesa> Despesas { get; set; }

        [NotMapped()]
        public string CodigoCript { get; set; }

    }
}
