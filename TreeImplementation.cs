using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 
    This file contains the implementaion of a general, non-binary tree.
    The node classes are defined non-specific to the Tags class.
    The class can be modified through change of variable names to work with different data.
*/

namespace OOPConcepts
{

    //<summary>
    //Stores the information about each branch/node in the tree. 
    //The names of each element are self explanatory.
    //</summary>
    class DNode                 
    {
        public string tag;
        public List<DNode> child;
        public DNode parent;
        public DNode(string data)
        {
            tag = data;
            child = new List<DNode>();
            parent = null;
        }
    }

    //<summary>
    //This class implements a non-binary tree structure (mostly to study heirarchy).
    //Various functions are defined (also self-explanatory)
    //to traverse through the tree and also to add new branches or delete existing branch.
    //The class can be modified to work on different types of nodes (only a change of variable names would be required)
    //</summary>
    class Tree
    {
        DNode root;
        DNode current;

        //Simple Constructor to initialize root and current node to null
        public Tree()
        {
            root = null;
            current = null;
        }

        //Sets the current node to its parent
        public void GetParent()
        {
            if (root == null)
            {
                return;
            }

            else if (current == root)
            {
                return;
            }

            else
            {
                current = current.parent;
                return;
            }
        }

        //Inserts a new branch/node in the tree. 
        public void InsertNewBranch(string data)
        {
            DNode newBranch = new DNode(data);
            if (root == null)
            {
                root = newBranch;
                current = root;
            }

            else
            {
                current.child.Add(newBranch);
                newBranch.parent = current;
                current = newBranch;
            }
        }

        //Deletes the current node. All the child elements are also lost with it.
        public void DeleteCurrBranch()
        {
            if (root == null)
            {
                Console.WriteLine("Tree is empty");
                return;
            }

            else if (current == root)
            {
                root = null;
                current = null;
            }

            else
            {
                DNode deleted = current;
                GetParent();
                current.child.Remove(deleted);
            }
        }

        //A combination of this and Traverse() is used to display all elements
        //on the console in a heirarchial model.
        void WriteElements(int tier, DNode curr)
        {
            string tabs = "";
            for (int i = 0; i < tier; i++)
                tabs += "\t";
            Console.WriteLine(tabs + curr.tag);
            tier++;

            if (curr.child != null)
            {
                foreach (DNode child in curr.child)
                    WriteElements(tier, child);
            }
            Console.WriteLine(tabs + curr.tag);
            return;
        }

        public void Traverse()
        {
            if (root == null)
            {
                Console.WriteLine("Tree is empty");
                return;
            }

            DNode curr = root;
            int tier = 0;
            WriteElements(tier, curr);
        }
    }
}
