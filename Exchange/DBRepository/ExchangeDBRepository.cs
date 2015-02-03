using Exchange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Exchange.DBRepository
{
    public class ExchangeDBRepository
    {
        private ExchangeContext context = new ExchangeContext();

        public IEnumerable<BuyOrder> BuyOrders { get { return context.BuyOrders; } }
        public IEnumerable<SellOrder> SellOrders { get { return context.SellOrders; } }
        public IEnumerable<CompletedOrder> CompletedOrders { get { return context.CompletedOrders; } }

        public async Task<int> SaveBuyOrderAsync(BuyOrder buyOrder)
        {
            if (buyOrder.BuyOrderID == 0)
            {
                context.BuyOrders.Add(buyOrder);
            }
            else
            {
                BuyOrder dbEntry = context.BuyOrders.Find(buyOrder.BuyOrderID);
                if (dbEntry == buyOrder)
                {
                    context.Entry(buyOrder).State = System.Data.Entity.EntityState.Modified;
                }
                else if (dbEntry != null)
                {
                    context.BuyOrders.Remove(dbEntry);
                    context.BuyOrders.Add(buyOrder);
                }
            }
            return await context.SaveChangesAsync();
        }

        public async Task<int> SaveSellOrderAsync(SellOrder sellOrder)
        {
            if (sellOrder.SellOrderID == 0)
            {
                context.SellOrders.Add(sellOrder);
            }
            return await context.SaveChangesAsync();
        }

        public async Task<int> SaveCompletedOrderAsync(CompletedOrder CompletedOrder)
        {
            if (CompletedOrder.CompletedOrderID == 0)
            {
                context.CompletedOrders.Add(CompletedOrder);
            }
            return await context.SaveChangesAsync();
        }
    }
}