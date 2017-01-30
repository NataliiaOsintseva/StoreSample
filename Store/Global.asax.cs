using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Store.Infrastructure.Binders;
using Store.Domain.Entities;
using Store.App_Start;
using System.Threading;
using System.Globalization;

namespace Store
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ModelBinders.Binders.Add(typeof(Cart), new CartBinder());
            MappingConfig.RegisterMaps();
        }
    }
}
