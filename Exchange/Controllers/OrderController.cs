using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Exchange.Models;
using Exchange.DBRepository;
using System.Threading.Tasks;

namespace Exchange.Controllers
{
    public class OrderController : ApiController
    {
        private ExchangeDBRepository repository = new ExchangeDBRepository();

        public IEnumerable<BuyOrder> GetBuyOrders()
        {
            return repository.BuyOrders;
        }
        public IEnumerable<SellOrder> GetSellOrders()
        {
            return repository.SellOrders;
        }
        public IEnumerable<CompletedOrder> GetCompletedOrders()
        {
            return repository.CompletedOrders;
        }

        public async Task<HttpResponseMessage> PostBuyOrder(BuyOrder buyorder)
        {
            if (ModelState.IsValid)
            {
                buyorder.CreatedAt = DateTime.Now;
                await repository.SaveBuyOrderAsync(buyorder);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, buyorder);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = buyorder.BuyOrderID }));
                await CompletedOrderAdded();
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
        public async Task<HttpResponseMessage> PostSellOrder(SellOrder sellorder)
        {
            if (ModelState.IsValid)
            {
                sellorder.CreatedAt = DateTime.Now;
                await repository.SaveSellOrderAsync(sellorder);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, sellorder);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = sellorder.SellOrderID }));
                await CompletedOrderAdded();
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        public async Task CompletedOrderAdded()
        {
            //toList because enumerator will not allow to save entity to db
            foreach (var sellorder in repository.SellOrders.Where(q=>q.Quantity > 0).ToList())
            {
                foreach (var buyorder in repository.BuyOrders.ToList().Where(q => q.Quantity > 0))
                {
                    if (sellorder.Price <= buyorder.Price)
                    {
                        CompletedOrder completedOrder = new CompletedOrder()
                        {
                            BuyOrderDate = buyorder.CreatedAt,
                            SellOrderDate = sellorder.CreatedAt,
                            Price = sellorder.Price,
                            CompletedAt = DateTime.Now,
                            BuyComment = buyorder.Commentary,
                            SellComment = sellorder.Commentary,
                        };

                        //quantity accounting(because we can't sell negative number of items)
                        if (buyorder.Quantity >= sellorder.Quantity)
                        {
                            completedOrder.Quantity = sellorder.Quantity;
                            buyorder.Quantity -= sellorder.Quantity;
                            sellorder.Quantity = 0;
                        }
                        else
                        {
                            sellorder.Quantity -= buyorder.Quantity;
                            completedOrder.Quantity = buyorder.Quantity;
                            buyorder.Quantity = 0;
                        }

                        await repository.SaveBuyOrderAsync(buyorder);
                        await repository.SaveSellOrderAsync(sellorder);
                        await repository.SaveCompletedOrderAsync(completedOrder);
                    }
                }
            }

        }
    }
}