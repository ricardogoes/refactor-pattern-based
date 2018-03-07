using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatternRefactorings.MoveAccumulationVisitor.Better7
{
    //
    // STEP 6: Adjust interface on base Node class so they are consistent
    // STEP 7: Remove conditional logic in ExtractText and call Accept() polymorphically
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
                //if (node is StringNode)
                //{
                //    ((StringNode)node).Accept(this);
                //}
                //else if (node is LinkTag)
                //{
                //    ((LinkTag)node).Accept(this);
                //}
                //else if (node is EndTag)
                //{
                //    ((EndTag)node).Accept(this);
                //}
                //else if (node is Tag)
                //{
                //    ((Tag)node).Accept(this);
                //}
                node.Accept(this);
            }
            return _sb.ToString();
        }
  
        public void VisitTag(Tag tag)
        {
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

        public void VisitEndTag(EndTag endTag)
        {
            string tagName = endTag.GetTagName();
            if (tagName.ToUpper() == "PRE")
            {
                _isPreTag = false;
            }
            else if (tagName.ToUpper() == "SCRIPT")
            {
                _isScriptTag = false;
            }
        }
  
        public void VisitLinkTag(LinkTag linkTag)
        {
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

        public void VisitStringNode(StringNode stringNode)
        {
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

    public abstract class Node
    {
        public abstract void Accept(TextExtractor textExtractor);
    }

    public class StringNode : Node
    {
        public string GetText()
        {
            throw new NotImplementedException();
        }

        public override void Accept(TextExtractor textExtractor)
        {
            textExtractor.VisitStringNode(this);
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

        public override void Accept(TextExtractor textExtractor)
        {
            textExtractor.VisitLinkTag(this);
        }
    }

    public class EndTag : Node
    {
        public string GetTagName()
        {
            throw new NotImplementedException();
        }

        public override void Accept(TextExtractor textExtractor)
        {
            textExtractor.VisitEndTag(this);
        }
    }

    public class Tag : Node
    {
        public string GetTagName()
        {
            throw new NotImplementedException();
        }

        public override void Accept(TextExtractor textExtractor)
        {
            textExtractor.VisitTag(this);
        }
    }
}
