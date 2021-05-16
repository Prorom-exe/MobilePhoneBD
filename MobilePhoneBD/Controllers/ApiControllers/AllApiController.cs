using Microsoft.AspNetCore.Mvc;
using MobilePhoneBD.Data;
using MobilePhoneBD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobilePhoneBD.Controllers.ApiControllers
{   [Route("api/[controller]")]
    [ApiController]
    public class AllApiController:ControllerBase
    {
        ApplicationDbContext db;

        public AllApiController(ApplicationDbContext db)
        {
            this.db = db;
        }
        [Route("GetMan")]
        [HttpGet]
        public async Task<IActionResult> GetMan(int catId)
        {
            var result = db.Мanufacturers.Where(x => x.CategoryId == catId).AsQueryable();
            return Ok(result);
        }

        [Route("Delete")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var basket = db.Baskets.FirstOrDefault(x => x.ProductId == id);
            if(basket != null)
            {
                db.Baskets.Remove(basket);
                db.SaveChanges();
            }
            var prod = db.Products.FirstOrDefault(x => x.Id == id);
            db.Products.Remove(prod);
            db.SaveChanges();
            return Ok();
        }


        [Route("BasketAdd")]
        [HttpGet]
        public async Task<IActionResult> BasketAdd(int id,int kol)
        {
            var basket = db.Baskets.FirstOrDefault(x => x.ProductId == id);
            var prod = db.Products.Find(id);
           if(basket != null)
            {
                basket.Quality += kol;
                prod.Quality = prod.Quality - kol;
                db.Products.Update(prod);
                db.Baskets.Update(basket);
                db.SaveChanges();
                return Ok();
            }

            Basket bas = new Basket
            {
                ProductId = id,
                Quality = kol
            };
            prod.Quality = prod.Quality - kol;
            db.Products.Update(prod);
            db.Baskets.Add(bas);
            db.SaveChanges();
            return Ok();
        }

        [Route("BasketDelete")]
        [HttpGet]
        public async Task<IActionResult> BasketDelete(int id)
        {
            var del = db.Baskets.FirstOrDefault(x => x.ProductId == x.Id);
            var prod = db.Products.Find(id);
            prod.Quality += del.Quality;
            del.Quality = 0;
            db.Products.Update(prod);
            db.Baskets.Update(del);
            db.SaveChanges();
            return Ok();
        }

        [Route("Buy")]
        [HttpGet]
        public async Task<IActionResult> Buy()
        {
            var buy = db.Baskets.AsQueryable();
            foreach(var x in buy)
            {
                x.Quality = 0;
            }
            db.Baskets.UpdateRange(buy);
            db.SaveChanges();
            return Ok();
        }
    }
}
