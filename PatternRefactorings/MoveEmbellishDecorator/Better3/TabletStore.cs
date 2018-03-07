using System;
using System.Linq;

namespace PatternRefactorings.MoveEmbellishDecorator.Better3
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

        public static Tablet WithCellularChip()
        {
            return new CellularChipTabletDecorator(new Tablet());
        }
    }

    /// <summary>
    /// Extract Parameter and set field via Constructor
    /// </summary>
    public class CellularChipTabletDecorator : Tablet
    {
        private readonly Tablet _delegateField;

        public CellularChipTabletDecorator(Tablet tablet)
        {
            _delegateField = tablet;
        }

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
                return Tablet.WithCellularChip();
            }
            return new Tablet();
        }
    }
}
