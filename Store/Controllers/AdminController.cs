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
using AutoMapper;

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
            Product product = repository.GetProductById(productId);

            var productView = Mapper.Map<Product, EditProductViewModel>(product);
            return productView;
        }

        public ViewResult Index()
        {
            return View(repository.Products);
        }

        public ViewResult Edit(int productId)
        {
            EditProductViewModel product = GetProduct(productId);
            product.ProductColour = new Dictionary<string, bool>()
            {
                { Colour.Yellow.ToString(), false },
                { Colour.Black.ToString(), true }
            };
            Session["productId"] = productId;
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(EditProductViewModel vm, HttpPostedFileBase file = null)
        {
            int productID = (Session["productId"]) != null ? (int)Session["productId"] : 0;
            var product = repository.GetProductById(productID);
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    vm.ImageData = convertToByteArray(file);
                    product.ImageMimeType = file.ContentType;
                    product.ImageData = vm.ImageData;
                }

                product.Name = vm.Name;
                product.Description = vm.Description;
                product.Category = vm.Category.ToString();
                product.ProductColour = vm.ProductColour;
                product.Price = vm.Price;
                repository.SaveChanges();
                TempData["message"] = string.Format($"{product.Name} has been saved");
                return RedirectToAction("Index");
            }
            else
            {
                return View(vm);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new EditProductViewModel());
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

        private byte[] convertToByteArray(HttpPostedFileBase file)
        {
            byte[] image = null;
            using (var reader = new System.IO.BinaryReader(file.InputStream))
            {
                image = reader.ReadBytes(file.ContentLength);

            }

            return image;
        }

        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/images/profile"), pic);
                // file is uploaded
                file.SaveAs(path);

                // save the image path path to the database or you can send image
                // directly to database
                // in-case if you want to store byte[] ie. for DB
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }

            }
            // after successfully uploading redirect the user
            return RedirectToAction("Index", "Admin");
        }

        public FileContentResult GetImage()
        {
            int prodId = (int)Session["productId"];
            Product product = repository.GetProductById(prodId);
            if (product.ImageData != null)
            {
                return File(product.ImageData, product.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        public IList<SelectListItem> GetColors()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text="Blue", Value="Blue" },
                new SelectListItem { Text="Pink", Value="Pink" }
            };
        }
    }
}