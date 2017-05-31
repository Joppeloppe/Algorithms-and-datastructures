using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Joakim Levin, TGSPA, 19900820 
namespace Inlämningsuppgift_3
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryTree binaryTree = new BinaryTree();

            binaryTree.Insert(1);
            binaryTree.Insert(4);
            binaryTree.Insert(6);
            binaryTree.Insert(3);
            binaryTree.Insert(2);
            binaryTree.Insert(5);

            Console.WriteLine(binaryTree.DrawTree());

            //Console.WriteLine("Remove 5.");
            //binaryTree.Delete(5);

            //Console.WriteLine("Remove 3.");
            //binaryTree.Delete(3);

            Console.WriteLine("Remove 1.");
            binaryTree.Delete(1);

            Console.WriteLine(binaryTree.DrawTree());

            Console.WriteLine("Inorder travesal: ");
            binaryTree.InorderTraversal(binaryTree.root);

            Console.ReadLine();
        }
    }
}
