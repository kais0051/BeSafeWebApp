using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeSafeWebApp.Contracts.Models
{
    public class MasterItemsSet
    {
        public long ItemId { get; set; }
        public long CategoryId { get; set; }
        public string ItemType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ItemLink { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
