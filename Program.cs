using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OOPConcepts
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the path (with filename and extension)");
            string path = Console.ReadLine();
            string input = File.ReadAllText(path);
            Console.WriteLine("Enter the new file name (NOTE: It will be saved in the same directory as the old file)");
            string newfilename = Console.ReadLine();
            TagParser parser = new TagParser(input);
            parser.WriteOutput(Path.GetDirectoryName(path), newfilename);
            //"C:\Users\CHINMAY SHAH\source\repos\OOPConcepts\HMTL.txt"
        }
    }

    class DNode
    {
        public string Name;
        public List<DNode> next;
        public DNode prev;
        public DNode(string data)
        {
            Name = data;
            next = new List<DNode>();
            prev = null;
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
                current = current.prev;
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
                current.next.Add(newBranch);
                newBranch.prev = current;
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
                current.next.Remove(deleted);
            }
        }

        void WriteElements(int tier, DNode curr)
        {
            string tabs = "";
            for (int i = 0; i < tier; i++)
                tabs += "\t";
            Console.WriteLine(tabs + curr.Name);
            tier++;

            if (curr.next != null)
            {
                foreach (DNode child in curr.next)
                    WriteElements(tier, child);
            }
            Console.WriteLine(tabs + curr.Name);
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
