using Microsoft.AspNetCore.Mvc;
using MobilePhoneBD.Data;
using MobilePhoneBD.Models.ViewMode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobilePhoneBD.Controllers
{
    public class BasketController:Controller
    {
        ApplicationDbContext db;

        public BasketController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index(int? catId, int? manId)
        {
            var ids = db.Baskets.Where(x => x.Quality != 0).Select(x => x.ProductId).ToList();
            var product = db.Products.Where(x => ids.Contains(x.Id));
            if (catId != null)
            {
                product = product.Where(x => x.CategoryId == catId);
            }
            if (manId != null)
            {
                product = product.Where(x => x.ManufacturerId == manId);
            }
            
            IndexViewModel model = new IndexViewModel
            {
                Products = product.ToList(),
                Category = db.Category.ToList(),
            };
            decimal buySum = 0;
            foreach (var t in model.Products)
            {
                var bas = db.Baskets.FirstOrDefault(x => x.ProductId == t.Id);
                t.Quality = bas.Quality;
                buySum += t.Quality * t.Price;
            }
            ViewBag.Sum = buySum;
            return View(model);
        }
    }
}
