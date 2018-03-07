using System;
using System.Linq;

namespace PatternRefactorings.MoveEmbellishDecorator.Better1
{
    public class Tablet
    {
        public virtual string Description()
        {
            return "myTab";
        }

        public virtual decimal Price()
        {
            return 300m;
        }
    }

    /// <summary>
    /// Replace Type Code with Subclasses
    /// Replace Conditional with Polymorphism
    /// </summary>
    public class TabletWithCellularChip : Tablet
    {
        public override string Description()
        {
            return base.Description() + " w/Cellular Data Chip";
        }

        public override decimal Price()
        {
            return base.Price() + 150m;
        }
    }

    public class TabletStore
    {
        public Tablet BuyTablet(bool withCellular)
        {
            if (withCellular)
            {
                return new TabletWithCellularChip();
            }
            return new Tablet();
        }
    }
}
