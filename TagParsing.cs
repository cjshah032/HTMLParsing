using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OOPConcepts
{
    // <summary>
    // This class contains information about the tags encountered in an HTML document. 
    // <summary>
    class Tag
    {
        string Value;                               //to store the tag in its entirity, attributes and styles included
        public string TagName { get; set; }         //to store only the name of the tag

        public enum TagType                         //enlists the different types of tags 
        {
            StartTag,
            EndTag,
            SelfContained
        }

        bool hasStyles;                             //boolean for checking if there are styles or attributes associated with a tag

        public TagType type { get; set; }           //to store the type of tag

        //The different indexes will be useful for deciding the 
        //tagtype and also extracting the name of the tag
        int StartIndex { get; set; }
        int EndIndex { get; set; }
        int SpaceIndex { get; set; }
        int SlashIndex { get; set; }


        //Constructor function (no other overloads)
        //<summary>
        //It takes in the value and index parameters, then decides the type of the tag, and finally sets the name of the tag
        //</summary>
        public Tag(string value, int start, int end, int space, int slash)
        {
            Value = value;
            StartIndex = start;
            EndIndex = end;
            SpaceIndex = space;
            SlashIndex = slash;

            if (SlashIndex == StartIndex + 1)
            {
                type = TagType.EndTag;
                hasStyles = false;
            }

            else if (SlashIndex == EndIndex - 1)
            {
                type = TagType.SelfContained;
                if (SpaceIndex != -1)
                    hasStyles = true;
                else hasStyles = false;
            }

            else
            {
                type = TagType.StartTag;
                if (SpaceIndex != -1)
                    hasStyles = true;
                else hasStyles = false;
            }


            SetTagName();
        }


        //Extracts the name of the tag 
        void SetTagName()
        {
            if (hasStyles == true)
            {
                switch (type)
                {
                    case TagType.SelfContained:

                    case TagType.StartTag:
                        TagName = Value.Substring(1, SpaceIndex - StartIndex - 1);
                        break;
                }
            }

            else
            {
                switch (type)
                {
                    case TagType.EndTag:
                        TagName = Value.Substring(2, EndIndex - StartIndex - 2);
                        break;

                    case TagType.SelfContained:
                        TagName = Value.Substring(1, SlashIndex - StartIndex - 1);
                        break;

                    case TagType.StartTag:
                        TagName = Value.Substring(1, EndIndex - StartIndex - 1);
                        break;
                }
            }
        }

        //Overriding ToString function to display the tag as intended whenever it is called
        public override string ToString()
        {
            switch (type)
            {
                case TagType.StartTag:
                    return string.Format("<{0}>", TagName);

                case TagType.EndTag:
                    return string.Format("</{0}>", TagName);

                case TagType.SelfContained:
                    return string.Format("<{0}/>", TagName);

                default:
                    return string.Format("<{0}>", TagName);
            }
        }
    }

    //<summary>
    //This class reads in the HTML file and finds the tags in it. A list of the tags is maintained for reference.
    //Also implements a tree structure to display the heirarchy of tags as found in the document
    //After the implementation the attributes and styles associated with all tags are stripped off and the new skeletal 
    //document is stored in another file
    //</summary>
    class TagParser
    {
        List<Tag> tags = new List<Tag>();                       //stores all the tags encountered in the HTML document
        List<string> tagNames = new List<string>();             //stores the names of the tags encountered
        public Tree tree = null;                                //to implement the tree

        string Input = default;                                 //This stores the entire document contents for doing operations
        StringBuilder Output = null;                            //To store the output till it is written into a file

        //Constructor (no other overloads)
        //<summary>
        //takes the contents of a document as input.
        //Finds the tags, maintains a list, implements the tree and prepares the output.
        //</summary>
        public TagParser (string input)
        {
            tags.Clear();
            tagNames.Clear();
            Input = input;
            Output = new StringBuilder(Input);
            tree = new Tree();

            int start = 0, end = 0, slash = 0, space = 0;

            while (end != (Input[Input.Length - 1] != '>' ? Input.Length - 2 : Input.Length - 1))
            {
                start = Input.IndexOf('<', end);
                end = Input.IndexOf('>', start);
                slash = Input.IndexOf('/', start, end - start);
                space = Input.IndexOf(' ', start, end - start);

                string value = default;
                value = Input.Substring(start, end - start + 1);
                Tag t = new Tag(value, start, end, space, slash);
                tags.Add(t);
                tagNames.Add(t.TagName);
                Output = Output.Replace(value, t.ToString());
                if(t.type == Tag.TagType.StartTag)
                    tree.InsertNewBranch(t.TagName);
                else
                    tree.GetParent();
            }

        }

        //Writes the output into a file. 
        public void WriteOutput(string path, string newFileName)
            =>  File.WriteAllText(Path.Combine(path, newFileName), Output.ToString());
        
    }
}
