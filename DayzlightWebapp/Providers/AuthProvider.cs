namespace DayzlightWebapp.Providers
{
    public class AuthProvider : DbProvider
    {
        public bool Login(string login, string pass)
        {
            return login.Equals("admin") && pass.Equals("admin");
        }
    }
}