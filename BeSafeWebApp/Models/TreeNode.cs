using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeSafeWebApp.Models
{
    public class TreeNode
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public TreeNode ParentCategory { get; set; }
        public List<TreeNode> Children { get; set; }
    }
}
