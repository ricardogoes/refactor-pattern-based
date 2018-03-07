using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatternRefactorings.MoveAccumulationVisitor.Good
{
    //
    // STEP 8: Extract visitor interface for TextExtractor
    // STEP 9: Use this interface for Accept() method parameter
    // Note how simple ExtractText is now, and overall cyclomatic complexity of every method involved in process
    //
    public interface INodeVisitor
    {
        void VisitTag(Tag tag);
        void VisitEndTag(EndTag endTag);
        void VisitLinkTag(LinkTag linkTag);
        void VisitStringNode(StringNode stringNode);
    }

    public class TextExtractor : INodeVisitor
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
        public abstract void Accept(INodeVisitor nodeVisitor);
    }

    public class StringNode : Node
    {
        public string GetText()
        {
            throw new NotImplementedException();
        }

        public override void Accept(INodeVisitor nodeVisitor)
        {
            nodeVisitor.VisitStringNode(this);
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

        public override void Accept(INodeVisitor nodeVisitor)
        {
            nodeVisitor.VisitLinkTag(this);
        }
    }

    public class EndTag : Node
    {
        public string GetTagName()
        {
            throw new NotImplementedException();
        }

        public override void Accept(INodeVisitor nodeVisitor)
        {
            nodeVisitor.VisitEndTag(this);
        }
    }

    public class Tag : Node
    {
        public string GetTagName()
        {
            throw new NotImplementedException();
        }

        public override void Accept(INodeVisitor nodeVisitor)
        {
            nodeVisitor.VisitTag(this);
        }
    }
}
