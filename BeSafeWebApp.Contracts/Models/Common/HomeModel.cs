using System;
using System.Collections.Generic;
using System.Text;

namespace BeSafeWebApp.Contracts.Models
{
    public class HomeModel
    {
        public HomeModel()
        {
            categories = new List<Category>();
            User = new User();
        }

        public User User { get; set; }
        public IList<Category> categories { get; set; }
    }
}
