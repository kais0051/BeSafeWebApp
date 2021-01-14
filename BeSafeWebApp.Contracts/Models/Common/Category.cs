
namespace BeSafeWebApp.Contracts.Models
{
    using System;
    using System.Collections.Generic;

    public class Category
    {
        public Category()
        {
           // Parent = new Category();
           // Children = new List<Category>();
        }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public long? ParentCategoryId { get; set; }
        public string Remarks { get; set; }
        public Category Parent { get; set; }

        public ICollection<Category> Children { get; set; }

        public string categoryAction { get; set; }
    }
}
