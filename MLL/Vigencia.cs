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
    public class Vigencia
    {
        public Vigencia()
        {
            CartoesCredito = new HashSet<CartaoCredito>();
            Despesas = new HashSet<Despesa>();
            Receitas = new HashSet<Receita>();
        }

        public int Codigo_Vigencia { get; set; }

        [Required()]
        [DisplayName("Data Início")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Data_Inicio { get; set; }

        [Required()]
        [DisplayName("Data Final")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",ApplyFormatInEditMode = true)]
        public DateTime Data_Final { get; set; }

        public int Codigo_Usuario { get; set; }
        public virtual Usuario Usuario { get; set; }

        public virtual ICollection<CartaoCredito> CartoesCredito { get; set; }
        public virtual ICollection<Despesa> Despesas { get; set; }
        public virtual ICollection<Receita> Receitas { get; set; }

        [NotMapped()]
        public string CodigoCript { get; set; }
    }
}
