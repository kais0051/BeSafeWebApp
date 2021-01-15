using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeSafeWebApp.Contracts.Models
{
    public class MasterItemsSet
    {
        public MasterItemsSet()
        {
            ItemTypeList = new List<ItemType>()
            {
                new ItemType{ Name = "Document",  Type = "Document" },
                new ItemType{ Name = "Image",  Type = "Image" },
                new ItemType{ Name = "Lien",  Type = "Lien" },
                new ItemType{ Name = "AUTRE",  Type = "AUTRE" },
            };
        }
        
        public long ItemId { get; set; }
        public long CategoryId { get; set; }
        public string ItemType { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string ItemLink { get; set; }
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Upload File")]
        public IFormFile UploadFile { set; get; }
        //[BindProperty]
        //public BufferedSingleFileUploadDb UploaderFile { get; set; }

        public List<ItemType> ItemTypeList { get; set; }
        public string itemAction { get; set; }
        public Category Category { get; set; }
    }
    public class BufferedSingleFileUploadDb
    {
        [Required]
        [Display(Name = "Uploader File")]
        public IFormFile FormFile { set; get; }
    }

    public class ItemType
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
