using Store.Domain.Abstract;
using Store.Domain.Entities;
using Store.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository repository;

        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }
                
        public ViewResult Index()
        {
            return View(repository.Products);
        }

        public ViewResult Edit(int productID)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productID);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.Save(product);
                TempData["message"] = string.Format($"{product.Name} has been saved");
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Product());
        }
         
        [HttpPost]
        public ActionResult Delete(int productId)
        {
            Product prodToDelete = repository.Delete(productId);
            if (prodToDelete != null)
            {
                TempData["message"] = string.Format($"{prodToDelete.Name} has been successfully deleted");
            }
            return RedirectToAction("Index");
        }
    }
}