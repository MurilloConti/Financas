using apiFnancas.Model;
using Financas_api.Model;
using Microsoft.EntityFrameworkCore;

namespace Financas_api
{
    public class FinancaContext : DbContext
    {
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Carteira> Carteiras { get; set; }
        public DbSet<FundoDeInvestimento> Fundos { get; set; }
        public DbSet<Rendimentos> Rendimentos { get; set; }
        public DbSet<Indice> Indices { get; set; }
        public DbQuery<EvolucaoRendiemnto> EvolucaoRendiemntos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
             optionsBuilder.UseMySQL("server=localhost;database=financas;user=root;password=123456;SslMode=none;Allow User Variables=True");
            //optionsBuilder.UseMySQL("server=localhost;database=financas;user=root;password=1234;SslMode=none");            
        }


        /// <summary>
        /// Define como será criada tabelas de JOIN N:N
        /// </summary>
        /// <param name="modelBuilder"></param>
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //Definir chaves primarias para serem usadas como chave
        //    modelBuilder
        //        .Entity<ServicoShop>()
        //        .HasKey(pp => new { pp.ServicoId, pp.ShopId });
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
