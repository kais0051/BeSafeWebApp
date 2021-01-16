using System;
using System.Linq;
using System.Collections.Generic;
using BeSafeWebApp.DLL;
using BeSafeWebApp.Common;
using Entities = BeSafeWebApp.Contracts.Entities;
using Models = BeSafeWebApp.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using BeSafeWebApp.Contracts.Interfaces;

namespace BeSafeWebApp.DLL
{
    public class CategoryRepository : GenericRepository<Entities.Category>, ICategoryRepository
    {
        private BeSafeContext beSafeContext;
        public CategoryRepository(BeSafeContext context)
            : base(context)
        {
            beSafeContext = context;
        }

        public async Task<IList<Entities.Category>> GetAllCategories()
        {
            IEnumerable<Entities.Category> categories = beSafeContext.Categories.Include(x => x.MasterItems).ToList();
            categories = categories.Where(x => x.Parent == null);
           // categories = Traverse(categories);

            return categories.ToList();
            //return await beSafeContext.Categories.Include(x=>x.MasterItemsSets).ToListAsync();
        }

        public async Task<Entities.Category> GetCategoryById(long id)
        {
            return await this.GetByIdAsync(id);
        }

        private IEnumerable<Entities.Category> Traverse(IEnumerable<Entities.Category> categories)
        {
            foreach (var category in categories)
            {
                var subCategories = beSafeContext.Categories.Include(x => x.MasterItems).Where(x => x.ParentCategoryId == category.CategoryId).ToList();
                category.Children = subCategories;
                category.Children = Traverse(category.Children).ToList();
            }
            return categories;
        }

    }
}
