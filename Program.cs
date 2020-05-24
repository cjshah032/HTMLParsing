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
            Directory.SetCurrentDirectory("C:\\Users\\CHINMAY SHAH\\source\\repos\\OOPConcepts");
            string[] lines = File.ReadAllLines("HMTL.txt");
            bool tagNameflag = false, tagflag = false;
            Tree tags = new Tree();
            foreach (string line in lines)
            {
                for(int j=0; j<line.Length; j++)
                {
                    if(line[j] == '<')
                    {
                        Console.Write(line[j]);
                        tagNameflag = true;
                        tagflag = true;
                    }
                    else if(tagflag == true && line[j] != '/')
                    {
                        if (tagNameflag == true)
                        {
                            string tag = "";
                            while (line[j] != ' ')
                            {
                                Console.Write(line[j]);
                                if (line[j] == '>')
                                {
                                    tagflag = false;
                                    break;
                                }
                                tag += line[j];
                                j++;
                            }

                            tags.InsertNewBranch(tag);
                            tagNameflag = false;
                        }
                        else
                        {   
                            if(line[j] == '>')
                            {
                                tagflag = false;
                                Console.Write(line[j]);
                            }
                        }
                    }
                    else if(tagflag == true && line[j] == '/')
                    {
                        //string tag = "";
                        tagflag = false;
                        tagNameflag = false;
                        while(line[j] != '>')
                        {
                            Console.Write(line[j]);
                            //tag += line[j];
                            j++;
                        }
                        tags.GetParent();
                        j--;
                    }
                    else
                    {
                        Console.Write(line[j]);
                    }
                }
                Console.WriteLine();
            }

            tags.Traverse();
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
