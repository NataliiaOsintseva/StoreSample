using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using Store.Infrastructure.Abstract;

namespace Store.Infrastructure.Concrete
{
    public class Authenticator : IAuthenticated
    {
        public bool Authenticate(string username, string password)
        {
            bool result = FormsAuthentication.Authenticate(username, password);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(username, false);
            }
            return result;
        }
    }
}