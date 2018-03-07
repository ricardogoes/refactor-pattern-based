using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatternRefactorings.MoveAccumulationVisitor.Better2
{
    //
    // STEP 2. Extract Method on the first type of accumulation (StringNode)
    //
    public class TextExtractor
    {
        public List<Node> Nodes;
        private bool _isPreTag = false;
        private bool _isScriptTag = false;
        private readonly StringBuilder _sb = new StringBuilder();

        // Accumulator Method
        public string ExtractText()
        {
            foreach (var node in Nodes)
            {
                if (node is StringNode)
                {
                    AcceptStringNode(node as StringNode);
                    //var stringNode = node as StringNode;
                    //if (!_isScriptTag)
                    //{
                    //    if (_isPreTag)
                    //    {
                    //        _sb.Append(stringNode.GetText());
                    //    }
                    //    else
                    //    {
                    //        // details omitted
                    //    }
                    //}
                }
                else if (node is LinkTag)
                {
                    var linkTag = node as LinkTag;
                    if (_isPreTag)
                    {
                        _sb.Append(linkTag.GetLinkText());
                    }
                    if (GetLinks())
                    {
                        _sb.Append("<");
                        _sb.Append(linkTag.GetLink());
                        _sb.Append(">");
                    }
                }
                else if (node is EndTag)
                {
                    var endTag = node as EndTag;
                    string tagName = endTag.GetTagName();
                    if (tagName.ToUpper() == "PRE")
                    {
                        _isPreTag = false;
                    }
                    else if(tagName.ToUpper() == "SCRIPT")
                    {
                        _isScriptTag = false;
                    }
                }
                else if (node is Tag)
                {
                    var tag = node as Tag;
                    string tagName = tag.GetTagName();
                    if (tagName.ToUpper() == "PRE")
                    {
                        _isPreTag = true;
                    }
                    else if (tagName.ToUpper() == "SCRIPT")
                    {
                        _isScriptTag = true;
                    }
                }
            }
            return _sb.ToString();
        }
  
        private void AcceptStringNode(StringNode stringNode)
        {
            //var stringNode = node as StringNode;
            if (!_isScriptTag)
            {
                if (_isPreTag)
                {
                    _sb.Append(stringNode.GetText());
                }
                else
                {
                    // details omitted
                }
            }
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
