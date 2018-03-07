using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatternRefactorings.MoveAccumulationVisitor.Bad
{
    public class TextExtractor
    {
        public List<Node> Nodes;

        // Accumulator Method
        public string ExtractText()
        {
            bool isPreTag = false;
            bool isScriptTag = false;
            StringBuilder sb = new StringBuilder();

            foreach (var node in Nodes)
            {
                if (node is StringNode)
                {
                    var stringNode = node as StringNode;
                    if (!isScriptTag)
                    {
                        if (isPreTag)
                        {
                            sb.Append(stringNode.GetText());
                        }
                        else
                        {
                            // details omitted
                        }
                    }
                }
                else if (node is LinkTag)
                {
                    var linkTag = node as LinkTag;
                    if (isPreTag)
                    {
                        sb.Append(linkTag.GetLinkText());
                    }
                    if (GetLinks())
                    {
                        sb.Append("<");
                        sb.Append(linkTag.GetLink());
                        sb.Append(">");
                    }
                }
                else if (node is EndTag)
                {
                    var endTag = node as EndTag;
                    string tagName = endTag.GetTagName();
                    if (tagName.ToUpper() == "PRE")
                    {
                        isPreTag = false;
                    }
                    else if(tagName.ToUpper() == "SCRIPT")
                    {
                        isScriptTag = false;
                    }
                }
                else if (node is Tag)
                {
                    var tag = node as Tag;
                    string tagName = tag.GetTagName();
                    if (tagName.ToUpper() == "PRE")
                    {
                        isPreTag = true;
                    }
                    else if (tagName.ToUpper() == "SCRIPT")
                    {
                        isScriptTag = true;
                    }
                }
            }
            return sb.ToString();
        }
  
        private bool GetLinks()
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }
    }

    public class Node
    {
    }

    public class StringNode : Node
    {
        public string GetText()
        {
            throw new NotImplementedException();
        }
    }

    public class LinkTag : Node
    {
        public string GetLink()
        {
            throw new NotImplementedException();
        }

        public string GetLinkText()
        {
            throw new NotImplementedException();
        }
    }

    public class EndTag : Node
    {
        public string GetTagName()
        {
            throw new NotImplementedException();
        }
    }

    public class Tag : Node
    {
        public string GetTagName()
        {
            throw new NotImplementedException();
        }
    }

}
