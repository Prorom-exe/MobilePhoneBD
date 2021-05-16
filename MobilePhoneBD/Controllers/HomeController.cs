using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MobilePhoneBD.Data;
using MobilePhoneBD.Models;
using MobilePhoneBD.Models.ViewMode;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MobilePhoneBD.Controllers
{
    public class HomeController : Controller
    {
       
        private ApplicationDbContext db;
        IWebHostEnvironment _appEnvironment;

        public HomeController(ApplicationDbContext db, IWebHostEnvironment appEnvironment)
        {
            this.db = db;
            _appEnvironment = appEnvironment;
        }

        public  IActionResult Index(int? catId, int? manId)
        {
            var product = db.Products.AsQueryable();
            if(catId != null)
            {
                product = product.Where(x => x.CategoryId == catId);
            }
            if(manId != null)
            {
                product = product.Where(x => x.ManufacturerId == manId);
            }
            IndexViewModel model = new IndexViewModel
            {
                Products = product.ToList(),
                Category = db.Category.ToList(),
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateViewModel model = new CreateViewModel
            {
                Category = db.Category.ToList()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            bool hard = false;
            if (!String.IsNullOrWhiteSpace(model.NewCat))
            {
                Category category = new Category
                {
                    Name = model.NewCat
                };
                db.Category.Add(category);
                db.SaveChanges();
                model.CatId = category.Id;
                if (String.IsNullOrWhiteSpace(model.NewMan))
                {
                    int id = db.Category.FirstOrDefault(x => x.Name == model.NewMan).Id;
                    Manufacturers man = new Manufacturers
                    {
                        CategoryId = id,
                        Name = model.NewMan
                    };
                    db.Мanufacturers.Add(man);
                    db.SaveChanges();
                    hard = true;
                    model.ManId = man.Id;
                }
            }
            if (!String.IsNullOrWhiteSpace(model.NewMan))
            {
                Manufacturers man1 = new Manufacturers
                {
                    CategoryId = model.CatId,
                    Name = model.NewMan
                };
                db.Мanufacturers.Add(man1);
                db.SaveChanges();
                model.ManId = man1.Id;
            }
            string path = " ";
            if (model.UploadedFile != null)
            {
                // путь к папке Files
                 path = "/Files/" + model.UploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await model.UploadedFile.CopyToAsync(fileStream);
                }
                
            }
            Products product = new Products
            {
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CatId,
                ManufacturerId = model.ManId,
                Price = model.Price,
                Quality = model.Quality,
                Link = path
            };
            db.Products.Add(product);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
       
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var prod = db.Products.Find(id);
            EditViewModel model = new EditViewModel 
            { 
                Id = prod.Id,
                Name = prod.Name,
                Description = prod.Description,
                Price = prod.Price,
                Quality = prod.Quality
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            var prod = db.Products.Find(model.Id);
            prod.Name = model.Name;
            prod.Description = model.Description;
            prod.Price = model.Price;
            prod.Quality = model.Quality;
            string path;
            if (model.File != null)
            {
                // путь к папке Files
                path = "/Files/" + model.File.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await model.File.CopyToAsync(fileStream);
                }
                prod.Link = path;
            }
            db.Products.Update(prod);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
