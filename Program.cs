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
            parser.tree.Traverse();
        }
    }
}
