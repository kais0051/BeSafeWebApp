using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.Reflection;
using System.Collections;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BeSafeWebApp.Common
{
    public class JsonQueryModelBinder : IModelBinder
    {   
        //Implement base member
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {

            var kvps = bindingContext.ActionContext.HttpContext.Request.Query.ToList().Take(1);
            var json = kvps.ElementAt(0).Value;

            //var serializer = new JavaScriptSerializer();
            //serializer.MaxJsonLength = 12097152; //default: 2097152
            try
            {
                //bindingContext.Model = serializer.Deserialize(json, bindingContext.ModelType);  
                bindingContext.Model = JsonConvert.DeserializeObject(json, bindingContext.ModelType);
            }
            catch (Exception ex)
            {
                bindingContext.ModelState.AddModelError(
                    bindingContext.ModelName, ex.Message);               
            }
            return Task.CompletedTask;
        }    
    }   
}
