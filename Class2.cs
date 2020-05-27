using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPConcepts
{
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

    class Tree
    {
        DNode root;
        DNode current;

        public Tree()
        {
            root = null;
            current = null;
        }

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
