using BeSafeWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BeSafeWebApp.Manager;
using EF6CodeFirstDemo;
using Microsoft.AspNetCore.Http;

namespace BeSafeWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
       
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        System.Data.SqlClient.SqlConnection con;
        public IActionResult Index()
        {
            return View(new User(){login = "admin" ,password = "admin"});
        
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login(string login,string password)
        {
           return RedirectToAction("Index", "Admin");
        }
    }
}
