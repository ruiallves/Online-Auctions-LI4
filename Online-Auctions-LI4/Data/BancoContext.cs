using Microsoft.EntityFrameworkCore;
using Online_Auctions_LI4.Models;

namespace Online_Auctions_LI4.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {

        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<LeilaoModel> Leilao { get; set;}
        public DbSet<ProdutoModel> Produto { get; set; }

    }
}
