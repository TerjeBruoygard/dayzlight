using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayzlightWebapp.Providers
{
    public class AuthProvider
    {
        public bool Login(string login, string pass)
        {
            return login.Equals("admin") && pass.Equals("admin");
        }
    }
}