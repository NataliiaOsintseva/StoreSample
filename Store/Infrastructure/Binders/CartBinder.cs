using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Store.Domain.Entities;
using System.Web.Mvc;

namespace Store.Infrastructure.Binders
{
    // This class is made to avoid mocking Session parameter in unit tests
    // Also, now Cart object is not created in controller

    public class CartBinder : IModelBinder
    {
        private string sessionKey = "Cart";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Cart cart = null;

            // If this session already has the Cart, get it
            if (controllerContext.HttpContext.Session != null)
            {
                cart = (Cart)controllerContext.HttpContext.Session[sessionKey];
            }

            // If there were no Carts in the session, create it and assign to session property
            if (cart == null)
            {
                cart = new Cart();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = cart;
                }
            }

            return cart;
        }
    }
}