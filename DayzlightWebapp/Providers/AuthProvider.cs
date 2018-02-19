using DayzlightWebapp.Models;
using System;
using System.Linq;

namespace DayzlightWebapp.Providers
{
    public class AuthProvider : IDisposable
    {
        private DbProvider db_ = new DbProvider();

        public bool Login(SigninModel singin)
        {
            return db_.Admins.FirstOrDefault(x => x.Login.Equals(singin.Login))?.Password.Equals(singin.Password) == true;
        }

        public void Dispose()
        {
            db_.Dispose();
        }
    }
}