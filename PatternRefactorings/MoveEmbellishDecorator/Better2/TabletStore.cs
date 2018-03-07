using System;
using System.Linq;

namespace PatternRefactorings.MoveEmbellishDecorator.Better2
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

        public static Tablet CreateTabletWithCellularChip()
        {
            return new TabletWithCellularChip();
        }
    }

    /// <summary>
    /// Replace Inheritance with Delegation
    /// </summary>
    public class TabletWithCellularChip : Tablet
    {
        private Tablet _delegateField = new Tablet(); // this works because Tablet is immutable

        public override string Description()
        {
            return _delegateField.Description() + " w/Cellular Data Chip";
        }

        public override decimal Price()
        {
            return _delegateField.Price() + 150m;
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
