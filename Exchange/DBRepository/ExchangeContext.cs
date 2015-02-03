using Exchange.Models;
using System.Data.Entity;

namespace Exchange.DBRepository
{
    public class ExchangeContext : DbContext
    {
        public ExchangeContext()
            : base("name=ExchangeContext")
        { }

        public DbSet<BuyOrder> BuyOrders { get; set; }
        public DbSet<SellOrder> SellOrders { get; set; }
        public DbSet<CompletedOrder> CompletedOrders { get; set; }
    }
}
