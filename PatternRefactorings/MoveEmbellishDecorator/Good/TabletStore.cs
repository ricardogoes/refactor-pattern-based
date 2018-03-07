using System;
using System.Linq;

namespace PatternRefactorings.MoveEmbellishDecorator.Good
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

        // This is limited as we add more options - move to a factory
        //public static Tablet WithCellularChip()
        //{
        //    return new CellularChipTabletDecorator(new Tablet());
        //}
    }

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

    public class SpaceGrayTabletDecorator : Tablet
    {
        private readonly Tablet _delegateField;

        public SpaceGrayTabletDecorator(Tablet tablet)
        {
            _delegateField = tablet;
        }

        public override string Description()
        {
            return "Space Gray " + _delegateField.Description();
        }
    }

    public class SilverTabletDecorator : Tablet
    {
        private readonly Tablet _delegateField;

        public SilverTabletDecorator(Tablet tablet)
        {
            _delegateField = tablet;
        }

        public override string Description()
        {
            return "Silver " + _delegateField.Description();
        }
    }

    public class TabletStore
    {
        // this method is our factory method for now, if it gets more responsibilities, this
        // logic can move to a separate factory class
        public Tablet BuyTablet(bool withCellular, bool isGray)
        {
            var tablet = new Tablet(); // base version
            if (withCellular)
            {
                tablet = new CellularChipTabletDecorator(tablet);
            }
            if (isGray)
            {
                tablet = new SpaceGrayTabletDecorator(tablet);
            }
            else
            {
                tablet = new SilverTabletDecorator(tablet);
            }
            return tablet;
        }
    }
}
