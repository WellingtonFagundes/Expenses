using DAL.Mapeamento;
using MLL;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Contexto : DbContext
    {
        public Contexto() : base(Caminho_Conexao())
        {

        }

        private static string Caminho_Conexao()
        {
            var settingsSection = ConfigurationManager.GetSection("appSettings") as NameValueCollection;
            var configValue = settingsSection["FlagBase"];
            string conexao;

            if (Int32.Parse(configValue) == 0)
            {
                conexao = "ModeloDados";
            }
            else
            {
                conexao = "ModeloDados2";
            }

            return conexao;
        }

        public DbSet<CartaoCredito> CartaoCredito { get; set; }
        public DbSet<CompraFutura> CompraFutura { get; set; }
        public DbSet<Despesa> Despesa { get; set; }
        public DbSet<FormaPagamento> FormaPagamento { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<Receita> Receita { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Vigencia> Vigencia { get; set; }
        public DbSet<Regras> Regras { get; set; }
        public DbSet<UsuariosRegras> UsuariosRegras { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CartaoCreditoMap());
            modelBuilder.Configurations.Add(new CompraFuturaMap());
            modelBuilder.Configurations.Add(new DespesaMap());
            modelBuilder.Configurations.Add(new FormaPagamentoMap());
            modelBuilder.Configurations.Add(new PerfilMap());
            modelBuilder.Configurations.Add(new ReceitaMap());
            modelBuilder.Configurations.Add(new UsuarioMap());
            modelBuilder.Configurations.Add(new VigenciaMap());
            modelBuilder.Configurations.Add(new RegrasMap());
            modelBuilder.Configurations.Add(new UsuariosRegrasMap());
            modelBuilder.Configurations.Add(new ProdutoMap());

            //1 x N (FORMA PAGAMENTO X DESPESA)
            modelBuilder.Entity<MLL.FormaPagamento>()
                .HasMany(x => x.Despesas)
                .WithRequired(y => y.FormaPagamento)
                .HasForeignKey(y => y.Codigo_Pagamento);

            //1 x N (USUARIOS X COMPRAS FUTURAS)
            modelBuilder.Entity<MLL.Usuario>()
                .HasMany(x => x.ComprasFuturas)
                .WithRequired(y => y.Usuario)
                .HasForeignKey(y => y.Codigo_Usuario);


            //1 x N (USUARIOS x DESPESA)
            modelBuilder.Entity<MLL.Usuario>()
                .HasMany(x => x.Despesas)
                .WithRequired(y => y.Usuario)
                .HasForeignKey(y => y.Codigo_Usuario);


            //1 x N (USUARIOS x RECEITA)
            modelBuilder.Entity<MLL.Usuario>()
                .HasMany(x => x.Receitas)
                .WithRequired(y => y.Usuario)
                .HasForeignKey(y => y.Codigo_Usuario);


            //1 X N (USUARIOS x CARTAO CREDITO)
            modelBuilder.Entity<MLL.Usuario>()
                .HasMany(x => x.CartoesCredito)
                .WithRequired(y => y.Usuario)
                .HasForeignKey(y => y.Codigo_Usuario);


            //1 X N (USUARIOS x VIGENCIA)
            modelBuilder.Entity<MLL.Usuario>()
                .HasMany(x => x.Vigencias)
                .WithRequired(y => y.Usuario)
                .HasForeignKey(y => y.Codigo_Usuario);


            //1 X N (USUARIOS x USUARIOSREGRAS)
            modelBuilder.Entity<MLL.Usuario>()
                .HasMany(x => x.UsuariosRegras)
                .WithRequired(y => y.Usuario)
                .HasForeignKey(y => y.Codigo_Usuario);

            //1 X N (USUARIOS x PRODUTOS)
            modelBuilder.Entity<MLL.Usuario>()
                .HasMany(x => x.Produtos)
                .WithRequired(y => y.Usuario)
                .HasForeignKey(y => y.Codigo_Usuario);

            //1 X N (REGRAS x USUARIOSREGRAS)
            modelBuilder.Entity<MLL.Regras>()
                .HasMany(x => x.UsuariosRegras)
                .WithRequired(y => y.Regra)
                .HasForeignKey(y => y.Codigo_Regra);
                

            //1 x N (VIGENCIA X CARTAO CREDITO)
            modelBuilder.Entity<MLL.Vigencia>()
                .HasMany(x => x.CartoesCredito)
                .WithRequired(y => y.Vigencia)
                .HasForeignKey(y => y.Codigo_Vigencia);

            //1 x N (VIGENCIA x RECEITA)
            modelBuilder.Entity<MLL.Vigencia>()
                .HasMany(x => x.Receitas)
                .WithRequired(y => y.Vigencia)
                .HasForeignKey(y => y.Codigo_Vigencia);

            //1 x N (VIGENCIA x DESPESA)
            modelBuilder.Entity<MLL.Vigencia>()
                .HasMany(x => x.Despesas)
                .WithRequired(y => y.Vigencia)
                .HasForeignKey(y => y.Codigo_Vigencia);


            //1 x N (PERFIL x USUARIOS)
            modelBuilder.Entity<MLL.Perfil>()
                .HasMany(x => x.Usuarios)
                .WithRequired(y => y.Perfil)
                .HasForeignKey(y => y.Codigo_Perfil);

            //1 x N (PRODUTO x COMPRASFUTURAS)
            modelBuilder.Entity<MLL.Produto>()
                .HasMany(x => x.ComprasFuturas)
                .WithRequired(y => y.Produto)
                .HasForeignKey(y => y.Codigo_Prod);

            //1 x N (PRODUTO x DESPESAS)
            modelBuilder.Entity<MLL.Produto>()
                .HasMany(x => x.Despesas)
                .WithRequired(y => y.Produto)
                .HasForeignKey(y => y.Codigo_Produto);
        }


        public override int SaveChanges()
        {
            //Detectando as alterações
            ChangeTracker.DetectChanges();

            //Salvando os dados
            try
            {
                return base.SaveChanges();
            }catch (DbEntityValidationException ex)
            {
                //Recuperar as mensagens de erro como uma lista de strings
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .SelectMany(x => x.ErrorMessage);

                //Join
                var fullErrorMessage = string.Join(";", errorMessages);


                //Combinar mensagem de exceção
                var exceptionMessage = string.Concat(ex.Message, "Os erros de validação são : ", fullErrorMessage);


                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
            
        }


    }
}
