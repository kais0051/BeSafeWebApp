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
using static BeSafeWebApp.Common.Helper;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BeSafeWebApp.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private IUserBusinessLogic userBusinessLogic;
        private IAutoMapConverter<BeSafeEntities.User, BeSafeModels.User> mapUserEntityToModel;
        private IAutoMapConverter<BeSafeModels.User, BeSafeEntities.User> mapUserModelToEntity;

        private ICategoryBusinessLogic categoryBusinessLogic;
        private IAutoMapConverter<BeSafeEntities.Category, BeSafeModels.Category> mapCategoryEntityToModel;
        private IAutoMapConverter<BeSafeModels.Category, BeSafeEntities.Category> mapCategoryModelToEntity;

        private IMasterItemBusinessLogic masterItemBusinessLogic;
        private IAutoMapConverter<BeSafeEntities.MasterItemsSet, BeSafeModels.MasterItemsSet> mapMasterItemEntityToModel;
        private IAutoMapConverter<BeSafeModels.MasterItemsSet, BeSafeEntities.MasterItemsSet> mapMasterItemModelToEntity;

        private IOptions<AppConfig> config { get; set; }
        public AdminController(IUserBusinessLogic userBusiness,
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
            this.userBusinessLogic = userBusiness;
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
        public ActionResult Index1()
        {
            try
            {
                var user = userBusinessLogic.GetUsers().Result.FirstOrDefault();
                var categories = categoryBusinessLogic.GetAllCategories().Result;

                BeSafeModels.HomeModel homeModel = new BeSafeModels.HomeModel();
                //var irem = mapUserEntityToModel.ConvertObject(user);
                homeModel.User = mapUserEntityToModel.ConvertObject(user);
                homeModel.categories = mapCategoryEntityToModel.ConvertObjectCollection(categories);
                //foreach (var item in categories)
                //{
                //    homeModel.categories.Add(
                //        new BeSafeModels.Category()
                //        {
                //            CategoryId=item.CategoryId,
                //            CategoryName=item.CategoryName,
                //            ParentCategoryId=item.ParentCategoryId,
                //            Parent= mapCategoryEntityToModel.ConvertObject(item.Parent),
                //            Children= mapCategoryEntityToModel.ConvertObjectCollection(item.Children),
                //            MasterItems=mapMasterItemEntityToModel.ConvertObjectCollection(item.MasterItems),
                //            Remarks=item.Remarks
                //        }
                //        );
                //}

                return View(homeModel);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;

            // var users = userBusinessLogic.GetUsers().Result;
            return View(new BeSafeModels.User() { UserName = "admin", Password = "admin" });
        }

        [NoDirectAccess]
        public async Task<IActionResult> AddOrEditCategory(long categoryId = 0, string categoryAction = "")
        {
            try
            {
                if (categoryId == 0)
                    return View(new BeSafeModels.Category());
                else
                {
                    var CategoryEntity = await categoryBusinessLogic.GetCategoryById(categoryId);
                    var CategoryModel = mapCategoryEntityToModel.ConvertObject(CategoryEntity);
                    if (CategoryModel == null)
                    {
                        return NotFound();
                    }
                    CategoryModel.categoryAction = categoryAction;
                    return View(CategoryModel);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEditCategory(long CategoryId, [Bind("CategoryId,CategoryName,ParentCategoryId,Remarks,categoryAction")] BeSafeModels.Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (CategoryId == 0)
                    {
                        var CategoryEntity = mapCategoryModelToEntity.ConvertObject(category);
                        await categoryBusinessLogic.AddCategory(CategoryEntity);
                    }
                    else
                    {
                        switch (category.categoryAction.SetEmptyIfNull().ToUpper())
                        {
                            case "ADD":
                                BeSafeEntities.Category NewCategory = new BeSafeEntities.Category();
                                NewCategory.CategoryName = category.CategoryName;
                                NewCategory.ParentCategoryId = CategoryId;
                                NewCategory.Remarks = category.Remarks;
                                await categoryBusinessLogic.AddCategory(NewCategory);
                                break;
                            case "EDIT":
                            default:
                                var CategoryEntity = await categoryBusinessLogic.GetCategoryById(CategoryId);
                                CategoryEntity.CategoryName = category.CategoryName;
                                CategoryEntity.Remarks = category.Remarks;
                                //var CategoryEntity = mapCategoryModelToEntity.ConvertObject(category);
                                await categoryBusinessLogic.UpdateCategory(CategoryEntity);
                                break;
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }

                var categories = categoryBusinessLogic.GetAllCategories().Result;
                var categoryModel = mapCategoryEntityToModel.ConvertObjectCollection(categories);
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllCategory", categoryModel) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEditCategory", category) });
        }

        // POST: Transaction/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategory(string CategoryId)
        {
            int intCategoryId = Convert.ToInt32(CategoryId);
            var CategoryEntity = await categoryBusinessLogic.GetCategoryById(intCategoryId);
            await categoryBusinessLogic.DeleteCategory(CategoryEntity);
            var categories = categoryBusinessLogic.GetAllCategories().Result;
            var categoryModel = mapCategoryEntityToModel.ConvertObjectCollection(categories);
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAllCategory", categoryModel) });
        }

        [NoDirectAccess]
        public async Task<IActionResult> AddOrEditCategoryItem(long categoryId = 0, long itemId = 0, string itemAction = "")
        {
            try
            {
                if (itemId == 0)
                {
                    BeSafeModels.MasterItemsSet masterItem = new BeSafeModels.MasterItemsSet();
                    masterItem.CategoryId = categoryId;
                    return View(masterItem);
                }
                else
                {
                    var masterItemEntity = await masterItemBusinessLogic.GetMasterItemById(itemId);
                    var masterItemModel = mapMasterItemEntityToModel.ConvertObject(masterItemEntity);
                    
                    if (masterItemModel == null)
                    {
                        return NotFound();
                    }

                    masterItemModel.itemAction = itemAction;
                    return View(masterItemModel);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEditCategoryItem(long ItemId, [Bind("ItemId,CategoryId,ItemType,Name,Description,ItemLink,UploadFile")] BeSafeModels.MasterItemsSet masterItemsSet)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (ItemId == 0)
                    {
                        var masterItem = mapMasterItemModelToEntity.ConvertObject(masterItemsSet);
                        if (masterItemsSet.UploadFile != null)
                        {
                            var uniqueFileName = Util.GetUniqueFileName(masterItemsSet.UploadFile.FileName);
                            var uploads = Path.Combine(hostingEnvironment.WebRootPath, "UploadedMasterItem");
                            var filePath = Path.Combine(uploads, uniqueFileName);
                            masterItemsSet.UploadFile.CopyTo(new FileStream(filePath, FileMode.Create));
                            masterItem.ItemLink = uniqueFileName;
                        }
                        masterItem.CreatedDate = DateTime.Now;
                        await masterItemBusinessLogic.AddMasterItem(masterItem);
                    }
                    else
                    {
                        //var masterItem = await masterItemBusinessLogic.GetMasterItemById(ItemId);
                        //masterItem. = category.CategoryName;
                        //masterItem.Remarks = category.Remarks;
                        var masterItem = mapMasterItemModelToEntity.ConvertObject(masterItemsSet);
                        //var CategoryEntity = mapCategoryModelToEntity.ConvertObject(category);
                        await masterItemBusinessLogic.UpdateMasterItem(masterItem);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                var masterItems = masterItemBusinessLogic.GetMasterItemsByCategoryId(masterItemsSet.CategoryId).Result;
                var masterItemsSets = mapMasterItemEntityToModel.ConvertObjectCollection(masterItems);
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllMasterItem", masterItemsSets) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEditCategoryItem", masterItemsSet) });
        }

        
    }
}
