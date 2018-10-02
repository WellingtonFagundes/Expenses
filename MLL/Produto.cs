using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLL
{
    public class Produto
    {
        public Produto()
        {
            ComprasFuturas = new HashSet<CompraFutura>();
            Despesas = new HashSet<Despesa>();
        }


        public int Codigo_Produto { get; set; }

        [Required]
        [DisplayName("Descrição")]
        public string Descricao_Produto { get; set; }

        [Required]
        [DisplayName("Valor")]
        public decimal Valor_Produto { get; set; }

        public string Url { get; set; }

        public string Path_Image { get; set; }

        public string Arquivo_Image { get; set; }

        public int Codigo_Usuario { get; set; }
        public virtual Usuario Usuario { get; set; }

        public virtual ICollection<CompraFutura> ComprasFuturas { get; set; }

        public virtual ICollection<Despesa> Despesas { get; set; }




    }
}
