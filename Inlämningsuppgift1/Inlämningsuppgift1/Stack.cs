using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Joakim Levin, TGSPA, 19900820
namespace Inlämningsuppgift1
{
    class Stack
    {
        ListNode topNode = null;

        int count;

        public Stack()
        {
            count = 0;
        }

        //Adds a node to the top of the stack with the data as a parameter.
        public void Push(object data) 
        {
            ListNode newNode = new ListNode(data, topNode);
            topNode = newNode;
            count++;
        }

        //Removes the top node, and returns it's data.
        public object Pop()
        {
            if (count == 0)
            {
                return null;
            }
            else
            {
                object data = topNode.Data;

                topNode = topNode.NextNode;
                count--;
                return data;
            }
        }

        //Checks the topnode's data, and returns the data.
        public object Peek()
        {

            if (count == 0)
            {
                return null;
            }
            else
            {
                return topNode.Data;
            }
        }

        //Returns the number of elements in the stack.
        public int Count()
        {
            return count;
        }

    }
}
