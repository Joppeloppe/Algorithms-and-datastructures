using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Joakim Levin, TGSPA, 19900820
namespace Inlämningsuppgift1
{
    class ListNode
    {
        public object Data { get; private set; }
        public ListNode NextNode { get; set; }

        public ListNode(object dataValue, ListNode nextNode)
        {
            Data = dataValue;
            NextNode = nextNode;
        }
    }
}
