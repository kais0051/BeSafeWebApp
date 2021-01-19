using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeSafeWebApp.Contracts.Models;

namespace BeSafeWebApp.Models
{
    public class TreeNode
    { 
        public  List<Contracts.Entities.MasterItemsSet> children { get; set; }
        public List<Contracts.Entities.Category> Categories { get; set; }
    }
}
