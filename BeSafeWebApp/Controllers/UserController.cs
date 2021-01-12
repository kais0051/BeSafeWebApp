using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeSafeWebApp.Contracts.Interfaces;
using BeSafeWebApp.Contracts.Configurations;
using Microsoft.Extensions.Options;
using BeSafeWebApp.Common;
using BeSafeEntities = BeSafeWebApp.Contracts.Entities;
using BeSafeModels = BeSafeWebApp.Contracts.Models;

namespace BeSafeWebApp.Controllers
{
    public class UserController : Controller
    {
        private IUserBusinessLogic UserBusinessLogic;
        private IAutoMapConverter<BeSafeEntities.User, BeSafeModels.User> mapEntityToModel;
        private IAutoMapConverter<BeSafeModels.User, BeSafeEntities.User> mapModelToEntity;

        private IOptions<AppConfig> config { get; set; }
        public UserController(IUserBusinessLogic userBusinessLogic,
                                  IOptions<AppConfig> appConfig)
        {
            UserBusinessLogic = userBusinessLogic;
            config = appConfig;
        }



        // GET: AdminController
        [HttpGet]
        public ActionResult Login()
        {

            return View(new BeSafeModels.User() { UserName = "admin", Password = "admin" });
        }
        [HttpPost]
        public ActionResult Login(BeSafeModels.User user)
        {
            var LogedinUser = UserBusinessLogic.UserValidation(user.UserName, user.Password).Result;
            if (LogedinUser.ID > 0)
            {

                return RedirectToAction("Index","Admin");
            }
            else
            {
                return View(new BeSafeModels.User() { UserName = "admin", Password = "admin" });
            }

        }
        // GET: UserController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
