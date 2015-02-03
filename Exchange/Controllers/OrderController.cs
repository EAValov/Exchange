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
        public IEnumerable<CompletedOrder> GetCompletedOrderOrders()
        {
            return repository.CompletedOrders;
        }

        //public async Task<HttpResponseMessage> PostBuyOrder(BuyOrder buyorder)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await repository.SaveBuyOrderAsync(buyorder);

        //        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, buyorder);
        //        response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = buyorder.BuyOrderID }));
        //        await CompletedOrderAdded();
        //        return response;
        //    }
        //    else
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        //    }
        //}
        //public async Task<HttpResponseMessage> PostSellOrder(SellOrder sellorder)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await repository.SaveSellOrderAsync(sellorder);

        //        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, sellorder);
        //        response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = sellorder.SellOrderID }));
        //        await CompletedOrderAdded();
        //        return response;
        //    }
        //    else
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        //    }
        //}

        //public async Task CompletedOrderAdded()
        //{
        //    foreach (var sellorder in repository.SellOrders)
        //    {
        //        foreach (var buyorder in repository.BuyOrders)
        //        {
        //            if (sellorder.Price <= buyorder.Price)
        //            {
        //                CompletedOrder completedOrder = new CompletedOrder()
        //                {
        //                    BuyOrderDate = buyorder.CreatedAt,
        //                    SellOrderDate = sellorder.CreatedAt,
        //                    Price = sellorder.Price,
        //                    CompletedAt = DateTime.Now,
        //                    BuyComment = buyorder.Commentary,
        //                    SellComment = sellorder.Commentary,
        //                };
        //                while (sellorder.Quantity >= 0 || buyorder.Quantity >= 0)
        //                {
        //                    sellorder.Quantity--;
        //                    buyorder.Quantity--;
        //                    completedOrder.Quantity++;
        //                }

        //                await repository.SaveBuyOrderAsync(buyorder);
        //                await repository.SaveSellOrderAsync(sellorder);
        //                await repository.SaveCompletedOrderAsync(completedOrder);
        //            }
        //        }
        //    }

        //}
    }
}