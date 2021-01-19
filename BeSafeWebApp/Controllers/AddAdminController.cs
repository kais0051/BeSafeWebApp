using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeSafeWebApp.BLL;
using BeSafeWebApp.Contracts.Entities;
using BeSafeWebApp.Contracts.Interfaces;

namespace BeSafeWebApp.Controllers
{
    public class AddAdminController : Controller
    {
        private IUserBusinessLogic userBusinessLogic;

        public AddAdminController(IUserBusinessLogic userBusiness)
        {
            this.userBusinessLogic = userBusiness;

        } // GET: AddAdmin

            public ActionResult Index()
        {
            var viewUser = new BeSafeWebApp.Contracts.Models.User();
            viewUser.Admins= userBusinessLogic.GetUsers().Result.ToList();

            return View(viewUser);
        }

      
    }
}
