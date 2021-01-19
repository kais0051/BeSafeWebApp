using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace BeSafeWebApp.Contracts.Models
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class User
    {
        [JsonProperty(PropertyName = "ID")]        
        public int ID { get; set; }

        [JsonProperty(PropertyName = "UserName")]
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "Password")]
        [Display(Name = "Password")]
        // [Range(1, 2147483647, ErrorMessage = "Please select a category<br/>")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "Name")]
        [Display(Name = "Name")]
        //[RegularExpression(@"^\d+.\d{0,2}$", ErrorMessage = "Price can't have more than 2 decimal places")]
        //[Range(0.01, 1000, ErrorMessage = "Price can't be larger than $1000<br/>")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "Email")]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "ContactNumber")]
        [Display(Name = "ContactNumber")]
        public string ContactNumber { get; set; }

        [JsonProperty(PropertyName = "Phone")]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "IsActive")]
        [Display(Name = "IsActive")]
        public bool IsActive { get; set; }

        [JsonProperty(PropertyName = "CreatedDate")]
        [Display(Name = "CreatedDate")]
        public DateTime? CreatedDate { get; set; }

        public List<Entities.User> Admins { get; set; }
    }

    public class UserLogin
    {
       
        [JsonProperty(PropertyName = "UserName")]
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }
    }
    }
