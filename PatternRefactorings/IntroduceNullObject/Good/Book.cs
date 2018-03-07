using System;

namespace PatternRefactorings.IntroduceNullObject.Good
{
    public class Book
    {
        public virtual string Title { get; set; }
        public virtual string Publisher { get; set; }
        public virtual decimal Price { get; set; }
        public virtual bool IsNull
        {
            get
            {
                return false;
            }
        }

        public virtual StockingPlan GetStockingPlan()
        {
            return StockingPlan.Basic();
        }
    }

    public class NullBook : Book
    {
        public override string Title
        {
            get
            {
                return "No Book Selected";
            }
            set
            {
            }
        }
        public override string Publisher
        {
            get
            {
                return "None";
            }
            set
            {
            }
        }
        public override decimal Price
        {
            get
            {
                return 0m;
            }
            set
            {
            }
        }
        public override bool IsNull
        {
            get
            {
                return true;
            }
        }

        public override StockingPlan GetStockingPlan()
        {
            return StockingPlan.None();
        }
    }

    public class Client
    {
        public Book FavoriteBook { get; set; }

        public void Main()
        {
            string title = FavoriteBook.Title;
            string publisher = FavoriteBook.Publisher;
            decimal price = FavoriteBook.Price;
            StockingPlan plan = FavoriteBook.GetStockingPlan();

            // if there were cases that only did something if not null, those would remain
            if(!FavoriteBook.IsNull)
            {
                // do something here
            }
        }
    }
}