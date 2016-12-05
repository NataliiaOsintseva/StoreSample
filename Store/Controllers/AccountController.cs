using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Store.Infrastructure.Abstract;
using Store.Models;

namespace Store.Controllers
{
    public class AccountController : Controller
    {
        IAuthenticated authProvider;

        public AccountController(IAuthenticated auth)
        {
            authProvider = auth;
        }

        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (authProvider.Authenticate(loginModel.UserName, loginModel.Password))
                {
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password");
                }
            }
            return View();
        }
    }
}