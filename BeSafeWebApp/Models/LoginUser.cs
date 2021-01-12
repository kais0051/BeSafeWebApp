namespace BeSafeWebApp.Models
{
    public class LoginUser
    {
        public LoginUser()
        {
        }

        public LoginUser(string v1, string v2)
        {
            login = v1;
            password = v2;
        }

        public string login { get; set; }
        public string password { get; set; }
    }
}