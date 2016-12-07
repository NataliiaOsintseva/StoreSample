using Store.Domain.Abstract;
using Store.Domain.Entities;
using Store.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Store.Models;

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

        public EditProductViewModel GetProduct(int productId)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            var productView = AutoMapper.Mapper.Map<Product, EditProductViewModel>(product);
            return productView;
        }

        public ViewResult Index()
        {
            return View(repository.Products);
        }

        public ViewResult Edit(int productID)
        {
            EditProductViewModel product = GetProduct(productID);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(EditProductViewModel product, HttpPostedFileBase upload = null)
        {
            

            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    product.ImageMimeType = upload.ContentType;
                    product.Image = upload;
                    
                    //upload.InputStream.Read(product.Image, 0, upload.ContentLength);
                }
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