using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.Abstract
{
    public interface IAuthenticated
    {
        bool Authenticate(string username, string password);
    }
}
