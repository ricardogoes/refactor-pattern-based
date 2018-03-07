using System;
using System.Linq;

namespace PatternRefactorings.MoveEmbellishDecorator.Bad
{
    public class Tablet
    {
        // This is a type code
        public bool IncludesCellularDataChip { get; set; }

        public string Description()
        {
            if (!IncludesCellularDataChip)
            {
                return "myTab";
            }
            return "myTab w/Cellular Data Chip";
        }

        public decimal Price()
        {
            if (!IncludesCellularDataChip)
            {
                return 300m;
            }
            return 450m;
        }
    }

    /// <summary>
    /// What should be the enclosure type?  Tablet.
    /// </summary>
    public class TabletStore
    {
        public Tablet BuyTablet(bool withCellular)
        {
            var tablet = new Tablet() { IncludesCellularDataChip = withCellular };

            return tablet;
        }
    }
}
