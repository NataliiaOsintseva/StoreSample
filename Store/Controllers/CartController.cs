using Store.Domain.Abstract;
using Store.Domain.Entities;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        private IOrder order;

        public CartController(IProductRepository repo, IOrder ord)
        {
            repository = repo;
            order = ord;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartListViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products.First(p => p.ProductID == productId);
            
            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                cart.RemoveFromList(product);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new Shipment());
        }

        [HttpPost] // Will be invoked after user presses 'submit' btn
        public ViewResult Checkout(Cart cart, Shipment shipment)
        {
            if (cart.Lists.Count() == 0)
            {
                ModelState.AddModelError("", "There is no items in the order!");
            }

            if (ModelState.IsValid)
            {
                order.ProcessOrder(cart, shipment);
                cart.ClearCart();
                return View("Completed");
            }
            else
            {
                return View(shipment);
            }
        }

    }
}