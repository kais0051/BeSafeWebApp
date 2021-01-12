using System;
using System.Collections.Generic;
using System.Linq;
using BeSafeWebApp.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using BeSafeWebApp.DLL;
using BeSafeWebApp.Common;

namespace BeSafeWebApp.DLL
{
    public static class BeSafeInitializer
    {
        public static void Initialize(BeSafeContext context)
        {
            //if (StaticConfigs.GetConfig("UseInMemoryDatabase") != "true")
            //{
            //    context.Database.EnsureCreated();
            //}
           
        }
    }    
}