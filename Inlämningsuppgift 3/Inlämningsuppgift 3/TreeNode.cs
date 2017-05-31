using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Joakim Levin, TGSPA, 19900820 
namespace Inlämningsuppgift_3
{
    //A node in our tree that will contain a integer as a value
    //and it's left and right children.
    class TreeNode
    {
        public int value;
        public TreeNode left, right;

        public TreeNode(int value)
        {
            this.value = value;
            left = null;
            right = null;
        }
    }
}
