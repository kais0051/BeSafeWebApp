using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeSafeWebApp.Contracts.Entities
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public long? ParentCategoryId { get; set; }
        public string Remarks { get; set; }
        public Category Parent { get; set; }

        public ICollection<Category> Children { get; set; }
        public ICollection<MasterItemsSet> MasterItems { get; set; }
    }

}
