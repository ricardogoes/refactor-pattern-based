using System;

namespace PatternRefactorings.IntroduceNullObject.Bad
{
    public class Book
    {
        public string Title { get; set; }
        public string Publisher { get; set; }
        public decimal Price { get; set; }

        public StockingPlan GetStockingPlan()
        {
            return StockingPlan.Basic();
        }
    }

    public class Client
    {
        public Book FavoriteBook { get; set; }

        public void Main()
        {
            string title;
            if (FavoriteBook == null)
            {
                title = "No Book Selected";
            }
            else
            {
                title = FavoriteBook.Title;
            }
            string publisher;
            if (FavoriteBook == null)
            {
                publisher = "None";
            }
            else
            {
                publisher = FavoriteBook.Publisher;
            }

            // if you prefer, you can do it this way as well
            decimal price = FavoriteBook != null ? FavoriteBook.Price : 0m;

            StockingPlan plan = FavoriteBook != null ? FavoriteBook.GetStockingPlan() : StockingPlan.None();
        }
    }
}