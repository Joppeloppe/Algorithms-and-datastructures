using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Joakim Levin, TGSPA, 19900820 
namespace Inlämningsuppgift_3
{
    class BinaryTree
    {
        public TreeNode root = null;
        private int size = 0;

        //Inserts a node into the binarytree.
        public TreeNode Insert(int value)
        {
            TreeNode node = new TreeNode(value);

            try
            {
                if (root == null)
                {
                    root = node;
                }
                else
                {
                    Add(node, ref root);
                    size++;
                    return node;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Value already exists.");
                return null;
            }

            return null;
        }

        //Adds the node to the correct leaf.
        private void Add(TreeNode node, ref TreeNode root)
        {
            if (node.value < root.value)
            {
                if (root.left != null)
                {
                    Add(node, ref root.left);
                }
                else
                {
                    root.left = node;
                }
            }
            else if (node.value > root.value)
            {
                if (root.right != null)
                {
                    Add(node, ref root.right);
                }
                else
                {
                    root.right = node;
                }
            }
        }

        //Searches for a node with the value as parameter.
        public TreeNode FindValue(int value)
        {
            if (value == root.value)
            {
                return root;
            }
            else if (value < root.value)
            {
                return FindChildValue(value, ref root.left);
            }
            else
            {
                return FindChildValue(value, ref root.right);
            }
        }

        //Used to search for a node in case the node is not the root.
        private TreeNode FindChildValue(int value, ref TreeNode root)
        {
            if (value == root.value)
            {
                return root;
            }
            else if (value < root.value)
            {
                return FindChildValue(value, ref root.left);
            }
            else
            {
                return FindChildValue(value, ref root.right);
            }
        }

        //Returns a string to describe the structure of the binarytree.
        public string DrawTree()
        {
            return DrawNode(root);
        }

        //Places the nodes on the correct place in the string.
        private string DrawNode(TreeNode node)
        {
            if (node == null)
            {
                return "Empty.";
            }

            if ((node.left == null) && (node.right == null))
            {
                return "" + node.value;
            }

            if ((node.left != null) && (node.right == null))
            {
                return "" + node.value + "(" + DrawNode(node.left) + ", _)";
            }

            if ((node.left == null) && (node.right != null))
            {
                return "" + node.value + "(_, " + DrawNode(node.right) + ")";
            }

            return node.value + "(" + DrawNode(node.left) + ", " + DrawNode(node.right) + ")";
        }

        //Finds the parent to a node with the value.
        private TreeNode FindParent(int value, ref TreeNode parent)
        {
            TreeNode nodeToReturn = null;

            if (value == root.value)
            {
                return null;
            }

            if (value < parent.value)
            {
                if (value == parent.left.value)
                {
                    nodeToReturn = parent;
                }
                else
                {
                    nodeToReturn = FindParent(value, ref parent.left);
                }
            }

            else if (value > parent.value)
            {
                if (value == parent.right.value)
                {
                    parent = root;
                    nodeToReturn = parent;
                }
                else
                {
                    nodeToReturn = FindParent(value, ref parent.right);
                }
            }

            return nodeToReturn;
        }

        //Finds and returns the leftmost node on the right sub-tree.
        public TreeNode LeftMostNodeOnRight(TreeNode nodeToDelete, ref TreeNode parent)
        {
            parent = nodeToDelete;
            nodeToDelete = nodeToDelete.right;

            while (nodeToDelete.left != null)
            {
                nodeToDelete = nodeToDelete.left;
            }

            return nodeToDelete;
        }

        //Finds and deletes the node with the value.
        public void Delete(int value)
        {
            TreeNode toDelete = FindValue(value);

            if (root == null)
            {
                Console.WriteLine("Tree is empty.");
            }

            else if (value == root.value)
            {
                DeleteNode(toDelete, ref root);
            }

            else if (value < root.value)
            {
                DeleteNode(toDelete, ref root.left);
            }

            else if (value > root.value)
            {
                DeleteNode(toDelete, ref root.right);
            }
        }

        //Finds the correct node to delete based on it's value.
        private TreeNode DeleteNode(TreeNode toDelete, ref TreeNode root)
        {
            if (toDelete.value == root.value)
            {
                if (root.left == null && root.right == null)
                {
                    root = null;
                    return root;
                }
                else if (root.left == null && root.right != null)
                {
                    TreeNode temp = root;
                    root = root.right;
                    temp = null;
                }
                else if (root.left != null && root.right == null)
                {
                    TreeNode temp = root;
                    root = root.left;
                    temp = null;
                }
                else
                {
                    TreeNode left = LeftMostNodeOnRight(toDelete , ref root);

                    Delete(left.value);
                    root.value = left.value;

                }
            }
            else if (toDelete.value < root.value)
            {
                DeleteNode(toDelete, ref root.left);
            }
            else if (toDelete.value > root.value)
            {
                DeleteNode(toDelete, ref root.right);
            }

            return root;
        }

        //Prints the inorder of the binary tree.
        public void InorderTraversal(TreeNode node)
        {
            if (node != null)
            {
                InorderTraversal(node.left);
                Console.Write(node.value + " ");
                InorderTraversal(node.right);
            }
        }
    }
}
