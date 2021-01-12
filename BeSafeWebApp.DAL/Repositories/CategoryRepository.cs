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
            IEnumerable<Entities.Category> categories = beSafeContext.Categories.Where(x => x.Parent == null).ToList();
            categories = Traverse(categories);

            return categories.ToList();
        }

        public async Task<Entities.Category> GetCategoryById(int id)
        {
            return await this.GetByIdAsync(id);
        }

        private IEnumerable<Entities.Category> Traverse(IEnumerable<Entities.Category> categories)
        {
            foreach (var category in categories)
            {
                var subCategories = beSafeContext.Categories.Where(x => x.ParentCategoryId == category.CategoryId).ToList();
                category.Children = subCategories;
                category.Children = Traverse(category.Children).ToList();
            }
            return categories;
        }

    }
}
