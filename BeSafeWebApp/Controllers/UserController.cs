﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BeSafeWebApp.Contracts.Interfaces;
using BeSafeWebApp.Contracts.Configurations;
using Microsoft.Extensions.Options;
using BeSafeWebApp.Common;
using BeSafeEntities = BeSafeWebApp.Contracts.Entities;
using BeSafeModels = BeSafeWebApp.Contracts.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;

namespace BeSafeWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private IUserBusinessLogic UserBusinessLogic;
        private IAutoMapConverter<BeSafeEntities.User, BeSafeModels.User> mapUserEntityToModel;
        private IAutoMapConverter<BeSafeModels.User, BeSafeEntities.User> mapUserModelToEntity;

        private ICategoryBusinessLogic categoryBusinessLogic;
        private IAutoMapConverter<BeSafeEntities.Category, BeSafeModels.Category> mapCategoryEntityToModel;
        private IAutoMapConverter<BeSafeModels.Category, BeSafeEntities.Category> mapCategoryModelToEntity;

        private IMasterItemBusinessLogic masterItemBusinessLogic;
        private IAutoMapConverter<BeSafeEntities.MasterItemsSet, BeSafeModels.MasterItemsSet> mapMasterItemEntityToModel;
        private IAutoMapConverter<BeSafeModels.MasterItemsSet, BeSafeEntities.MasterItemsSet> mapMasterItemModelToEntity;

        private IOptions<AppConfig> config { get; set; }
        public UserController(IUserBusinessLogic userBusiness,
                                ICategoryBusinessLogic categoryBusiness,
                                IMasterItemBusinessLogic masterItemBusiness,
                                IAutoMapConverter<BeSafeEntities.User, BeSafeModels.User> convertUEntityToModel,
                                IAutoMapConverter<BeSafeModels.User, BeSafeEntities.User> convertUModelToEntity,
                                IAutoMapConverter<BeSafeEntities.Category, BeSafeModels.Category> mapCEntityToModel,
                                IAutoMapConverter<BeSafeModels.Category, BeSafeEntities.Category> mapCModelToEntity,
                                 IAutoMapConverter<BeSafeEntities.MasterItemsSet, BeSafeModels.MasterItemsSet> mapItemEntityToModel,
                                IAutoMapConverter<BeSafeModels.MasterItemsSet, BeSafeEntities.MasterItemsSet> mapItemModelToEntity,
                                  IOptions<AppConfig> appConfig,
                                  IHostingEnvironment environment)
        {
            this.UserBusinessLogic = userBusiness;
            mapUserEntityToModel = convertUEntityToModel;
            mapUserModelToEntity = convertUModelToEntity;
            this.mapCategoryEntityToModel = mapCEntityToModel;
            this.mapCategoryModelToEntity = mapCModelToEntity;
            this.categoryBusinessLogic = categoryBusiness;
            this.masterItemBusinessLogic = masterItemBusiness;
            this.mapMasterItemEntityToModel = mapItemEntityToModel;
            this.mapMasterItemModelToEntity = mapItemModelToEntity;
            config = appConfig;
            hostingEnvironment = environment;
        }

        [HttpGet]
        public ActionResult Register()
        {

            return View(new BeSafeModels.User());
        }
        [HttpPost]
        public async Task<ActionResult> Register(BeSafeModels.User user)
        {
            if (ModelState.IsValid)
            {
                BeSafeEntities.User LogedinUser = UserBusinessLogic.UserValidation(user.UserName, user.Password).Result;
                if (LogedinUser == null)
                {
                    var userEntity = mapUserModelToEntity.ConvertObject(user);
                    userEntity.CreatedDate = DateTime.Now;
                    LogedinUser = await UserBusinessLogic.AddUser(userEntity);

                }
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, LogedinUser.UserName),
                    new Claim(ClaimTypes.Sid, LogedinUser.ID.ToString()),
                    new Claim(ClaimTypes.Email, LogedinUser.Email)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Login");

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction("Index", "Admin");
            }
            return View(user);
        }


        // GET: AdminController
        [HttpGet]
        public ActionResult Login()
        {

            return View(new BeSafeModels.UserLogin() { UserName = "admin", Password = "admin" });
        }
        [HttpPost]
        public async Task<ActionResult> Login(BeSafeModels.UserLogin user, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var LogedinUser = UserBusinessLogic.UserValidation(user.UserName, user.Password).Result;
                if (LogedinUser != null && LogedinUser.ID > 0)
                {
                    var claims = new List<Claim>
                                        {
                                            new Claim(ClaimTypes.Name, LogedinUser.UserName),
                                            new Claim(ClaimTypes.Sid, LogedinUser.ID.ToString()),
                                            new Claim(ClaimTypes.Email, LogedinUser.Email)
                                        };
                    var claimsIdentity = new ClaimsIdentity(claims, "Login");

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return Redirect(ReturnUrl == null ? "/Admin/Index" : ReturnUrl);

                    //  return RedirectToAction("Index", "Admin");
                }else
                {
                    user.ErrorMessage = "Invalid UserName/Password.";
                }
            }
            return View(user);
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "User");
        }

        // GET: UserController
        public ActionResult Index()
        {
            var ViewItems = new BeSafeWebApp.Models.TreeNode();
            ViewItems.children = new List<BeSafeEntities.MasterItemsSet>();
            ViewItems.Categories = new List<BeSafeEntities.Category>();
            ViewItems.Categories = categoryBusinessLogic.GetAllCategories().Result.Where(c => c.ParentCategoryId == null)
                .ToList();

            return View(ViewItems);
        }


        public ActionResult Navigate(int idcategory)
        {
            var ViewItems = new BeSafeWebApp.Models.TreeNode();
            ViewItems.children = new List<BeSafeEntities.MasterItemsSet>();
            ViewItems.Categories = new List<BeSafeEntities.Category>();
            ViewItems.Categories = categoryBusinessLogic.GetCategories(idcategory).Result.ToList();
            var listitems= masterItemBusinessLogic.GetAllMasterItems().Result.Where(c => c.CategoryId == idcategory)
                .ToList();
            foreach (var masterItemsSet in listitems)
            {
                if(masterItemsSet.ItemType.ToLower() != "lien")
                    masterItemsSet.ItemLink = Path.Combine("\\UploadedMasterItem", masterItemsSet.ItemLink);
            }
           ViewItems.children.AddRange(listitems);
            return View("index",ViewItems);
        }
    }
}
