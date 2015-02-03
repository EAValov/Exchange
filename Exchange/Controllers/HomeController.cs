using Exchange.DBRepository;
using Exchange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Exchange.Controllers
{
    public class HomeController : Controller
    {
        private ExchangeDBRepository repository = new ExchangeDBRepository();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ActionName("CreateBuyOrder")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateBuyOrder(BuyOrder buyorder)
        {
            if (ModelState.IsValid)
            {
                await repository.SaveBuyOrderAsync(buyorder);
            }
            await CompletedOrderAdded();
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("CreateSellOrder")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateSellOrder(SellOrder sellorder)
        {
            if (ModelState.IsValid)
            {
                await repository.SaveSellOrderAsync(sellorder);
            }
            await CompletedOrderAdded();
            return RedirectToAction("Index");

        }
        public async Task CompletedOrderAdded()
        {
            foreach (var sellorder in repository.SellOrders.Where(h=>h.Quantity > 0))
            {
                foreach (var buyorder in repository.BuyOrders.Where(m => m.Quantity > 0))
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
                        while (sellorder.Quantity >= 0 || buyorder.Quantity >= 0)
                        {
                            sellorder.Quantity--;
                            buyorder.Quantity--;
                            completedOrder.Quantity++;
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
