using System;
using System.Linq;

namespace PatternRefactorings.IntroduceNullObject
{
    public class StockingPlan
    {
        public string Name { get; private set; }

        private StockingPlan(string name)
        {
            this.Name = name;
        }

        public static StockingPlan Basic()
        {
            return new StockingPlan("Basic");
        }

        public static StockingPlan Monthly()
        {
            return new StockingPlan("Monthly");
        }

        public static StockingPlan Custom()
        {
            return new StockingPlan("Custom");
        }

        public static StockingPlan None()
        {
            return new StockingPlan("None");
        }
    }
}
