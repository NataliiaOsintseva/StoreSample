using Store.Domain.Abstract;
using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;

        public ProductController(IProductRepository prodRepo)
        {
            this.repository = prodRepo;
        }
        
        // GET: Product
        public ViewResult List()
        {
            return View(repository.Products);
        }
    }
}