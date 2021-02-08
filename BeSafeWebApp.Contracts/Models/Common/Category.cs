
using System.ComponentModel.DataAnnotations;

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
        // we delete this -->  [StringLength(13)]
        public string CategoryName { get; set; }
        public long? ParentCategoryId { get; set; }

        // we delete this -->  [StringLength(30)]
        public string Remarks { get; set; }
        public Category Parent { get; set; }

        public ICollection<Category> Children { get; set; }
        public ICollection<MasterItemsSet> MasterItems { get; set; }
        public string categoryAction { get; set; }
    }
}
