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
    public class Usuario
    {
        public Usuario()
        {
            CartoesCredito = new HashSet<CartaoCredito>();
            Receitas = new HashSet<Receita>();
            Despesas = new HashSet<Despesa>();
            ComprasFuturas = new HashSet<CompraFutura>();
            Vigencias = new HashSet<Vigencia>();
            Produtos = new HashSet<Produto>();
            UsuariosRegras = new HashSet<UsuariosRegras>();
        }

        public int Codigo_Usuario { get; set; }

        [Required()]
        [DisplayName("Nome Usuário")]
        public string Nome_Usuario { get; set; }

        [Required()]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required()]
        [DisplayName("Senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [NotMapped()]
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage = "As senhas não coincidem")]
        public string ConfirmaSenha
        {
            get { return this.Senha; }
            set { }
        }

        [Required()]
        [DisplayName("Cargo")]
        public string Cargo { get; set; }

        public DateTime? Excluido { get; set; }

        public string Path_Image { get; set; }

        public int Codigo_Perfil { get; set; }
        public virtual Perfil Perfil { get; set; }

        public virtual ICollection<CartaoCredito> CartoesCredito { get; set; }
        public virtual ICollection<Receita> Receitas { get; set; }
        public virtual ICollection<Despesa> Despesas { get; set; }
        public virtual ICollection<CompraFutura> ComprasFuturas { get; set; }
        public virtual ICollection<Vigencia> Vigencias { get; set; }
        public virtual ICollection<Produto> Produtos { get; set; }
        public virtual ICollection<UsuariosRegras> UsuariosRegras { get; set; }

        [NotMapped()]
        public string idcriptografado { get; set; }

    }
}
