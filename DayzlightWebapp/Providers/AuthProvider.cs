using MySql.Data.MySqlClient;

namespace DayzlightWebapp.Providers
{
    public class AuthProvider
    {
        MySqlConnection dbConn_ = new DbProvider().CreateConnection();

        public bool Login(string login, string pass)
        {
            return login.Equals("admin") && pass.Equals("admin");
        }
    }
}