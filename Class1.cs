﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OOPConcepts
{
    class Tag
    {
        string Value;
        public string TagName { get; set; }

        enum TagType
        {
            StartTag,
            EndTag,
            SelfContained
        }

        bool hasStyles;

        TagType type { get; set; }
        int StartIndex { get; set; }
        int EndIndex { get; set; }
        int SpaceIndex { get; set; }
        int SlashIndex { get; set; }

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

            else if (SlashIndex == -1)
            {
                type = TagType.StartTag;
                if (SpaceIndex != -1)
                    hasStyles = true;
                else hasStyles = false;
            }

            else
            {
                type = TagType.SelfContained;
                if (SpaceIndex != -1)
                    hasStyles = true;
                else hasStyles = false;
            }

            SetTagName();
        }

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

    class TagParser
    {
        List<Tag> tags = new List<Tag>();
        List<string> tagNames = new List<string>();

        string Input = default;
        string Output = default;

        public TagParser (string input)
        {
            tags.Clear();
            tagNames.Clear();
            Input = input;
            StringBuilder Output = new StringBuilder(Input);

            int start = 0, end = 0, slash = 0, space = 0;

            while (end != (Input[Input.Length - 1] != '>' ? Input.Length - 2 : Input.Length - 1))
            {
                start = Input.IndexOf('<', end);
                end = Input.IndexOf('>', start);
                slash = Input.IndexOf('/', start, end - start);
                space = Input.IndexOf(' ', start, end - start);

                string value = default;
                value = Input.Substring(start, end - start);
                Tag t = new Tag(value, start, end, space, slash);
                tags.Add(t);
                tagNames.Add(t.TagName);
                Output = Output.Replace(value, t.ToString());
            }
        }

        public void WriteOutput(string path, string newFileName)
        {
            File.WriteAllText(Path.Combine(path, newFileName), Output);
            Console.Write(Output);
            return;
        }
    }
}