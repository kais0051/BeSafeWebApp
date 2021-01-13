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

namespace BeSafeWebApp.Controllers
{
    public class AdminController : Controller
    {
        private IUserBusinessLogic userBusinessLogic;
        private IAutoMapConverter<BeSafeEntities.User, BeSafeModels.User> mapUserEntityToModel;
        private IAutoMapConverter<BeSafeModels.User, BeSafeEntities.User> mapUserModelToEntity;

        private ICategoryBusinessLogic categoryBusinessLogic;
        private IAutoMapConverter<BeSafeEntities.Category, BeSafeModels.Category> mapCategoryEntityToModel;
        private IAutoMapConverter<BeSafeModels.Category, BeSafeEntities.Category> mapCategoryModelToEntity;

        private IOptions<AppConfig> config { get; set; }
        public AdminController(IUserBusinessLogic userBusiness,
                                ICategoryBusinessLogic categoryBusiness,
                                IAutoMapConverter<BeSafeEntities.User, BeSafeModels.User> convertUEntityToModel,
                                IAutoMapConverter<BeSafeModels.User, BeSafeEntities.User> convertUModelToEntity,
                                IAutoMapConverter<BeSafeEntities.Category, BeSafeModels.Category> mapCEntityToModel,
                                IAutoMapConverter<BeSafeModels.Category, BeSafeEntities.Category> mapCModelToEntity,
                                  IOptions<AppConfig> appConfig)
        {
            this.userBusinessLogic = userBusiness;
            mapUserEntityToModel = convertUEntityToModel;
            mapUserModelToEntity = convertUModelToEntity;
            this.mapCategoryEntityToModel = mapCEntityToModel;
            this.mapCategoryModelToEntity = mapCModelToEntity;
            this.categoryBusinessLogic = categoryBusiness;
            config = appConfig;
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
                // foreach (var item in categories)
                // {
                // homeModel.categories.Add(mapCategoryEntityToModel.ConvertObject(item));
                //homeModel.categories.Add(new BeSafeModels.Category()
                //{
                //    CategoryId=item.CategoryId,
                //    CategoryName=item.CategoryName,
                //    Parent=item.Parent,
                //    ParentCategoryId=item.ParentCategoryId,
                //    Children=item.Children,
                //    Remarks =item.Remarks
                //});
                //    }

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
            var users = userBusinessLogic.GetUsers().Result;
            return View(new BeSafeModels.User() { UserName = "admin", Password = "admin" });
        }


        [NoDirectAccess]
        public async Task<IActionResult> AddOrEditCategory(int categoryId = 0, string categoryAction = "")
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEditCategory(int CategoryId, [Bind("CategoryId,CategoryName,ParentCategoryId,Remarks,categoryAction")] BeSafeModels.Category category)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (CategoryId == 0)
                {
                    try
                    {
                        var CategoryEntity = mapCategoryModelToEntity.ConvertObject(category);
                        await categoryBusinessLogic.AddCategory(CategoryEntity);
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }

                }
                //Update
                else
                {
                    try
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
                    catch (DbUpdateConcurrencyException)
                    {
                        throw;
                    }
                }
                var categories = categoryBusinessLogic.GetAllCategories().Result;
                var categoryModel = mapCategoryEntityToModel.ConvertObjectCollection(categories);
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllCategory", categoryModel) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEditCategory", category) });
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int categoryId)
        {
            var CategoryEntity = await categoryBusinessLogic.GetCategoryById(categoryId);
            await categoryBusinessLogic.DeleteCategory(CategoryEntity);
            var categories = categoryBusinessLogic.GetAllCategories().Result;
            var categoryModel = mapCategoryEntityToModel.ConvertObjectCollection(categories);
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAllCategory", categoryModel) });
        }

        //// GET: AdminController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: AdminController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: AdminController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: AdminController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: AdminController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: AdminController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: AdminController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

    }
}
