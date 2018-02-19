using System;
using System.Linq;

namespace DayzlightWebapp.Providers
{
    public class AuthProvider : IDisposable
    {
        private DbProvider db_ = new DbProvider();

        public bool Login(string login, string pass)
        {
            return db_.Admins.First(x => x.Login.Equals(login))?.Password.Equals(pass) == true;
        }

        public void Dispose()
        {
            db_.Dispose();
        }
    }
}