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

namespace BeSafeWebApp.Controllers
{
    [Authorize]
    public class MasterItemSetController : Controller
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
        public MasterItemSetController(IUserBusinessLogic userBusiness,
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
        public ActionResult Index()
        {
            
            return View();
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
                    masterItem.CreatedDate = DateTime.Today;
                    return View(masterItem);
                }
                else
                {
                    var masterItemEntity = await masterItemBusinessLogic.GetMasterItemById(itemId);
                    var masterItemModel = mapMasterItemEntityToModel.ConvertObject(masterItemEntity);
                    masterItemEntity.CreatedDate = DateTime.Today;
                    
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
        public async Task<IActionResult> AddOrEditCategoryItem(long ItemId, [Bind("ItemId,CreatedDate,CategoryId,ItemType,Name,Description,ItemLink,UploadFile")] BeSafeModels.MasterItemsSet masterItemsSet)
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
                        //masterItem.CreatedDate = DateTime.Now;
                        masterItem.CreatedDate = masterItemsSet.CreatedDate; //he dont accept this
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
                catch (Exception ex)
                {
                    throw;
                }
                var masterItems = masterItemBusinessLogic.GetMasterItemsByCategoryId(masterItemsSet.CategoryId).Result;
                var masterItemsSets = mapMasterItemEntityToModel.ConvertObjectCollection(masterItems);
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllMasterItem", masterItemsSets) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEditCategoryItem", masterItemsSet) });
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategoryItem(string ItemId, string CategoryId)
        {
            int intItemId = Convert.ToInt32(ItemId);
            int intCategoryId = Convert.ToInt32(CategoryId);
            var CategoryItemEntity = await masterItemBusinessLogic.GetMasterItemById(intItemId);
            await masterItemBusinessLogic.DeleteMasterItem(CategoryItemEntity);
            var categoryItems = masterItemBusinessLogic.GetMasterItemsByCategoryId(intCategoryId).Result;
            var categoryItemModel = mapMasterItemEntityToModel.ConvertObjectCollection(categoryItems);
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAllMasterItem", categoryItemModel) });
        }

        public async Task<IActionResult> Download(string id)
        {
            int intItemId = Convert.ToInt32(id);
            var CategoryItemEntity = await masterItemBusinessLogic.GetMasterItemById(intItemId);

            var uploads = Path.Combine(hostingEnvironment.WebRootPath, "UploadedMasterItem");
            var filePath = Path.Combine(uploads, CategoryItemEntity.ItemLink);
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/x-msdownload", CategoryItemEntity.ItemLink);
        }

    }
}
