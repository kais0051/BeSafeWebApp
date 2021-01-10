namespace BeSafeWebApp.Models
{
    public class User
    {
        public User()
        {
        }

        public User(string v1, string v2)
        {
            login = v1;
            password = v2;
        }

        public string login { get; set; }
        public string password { get; set; }
    }
}