using System;
using System.Collections.Generic;
using System.Linq;

namespace PatternRefactorings.ReplaceConditionalStrategy.Bad
{
    // Example from Refactoring to Patterns, translated to C#
    public class Loan
    {
        private decimal _outstanding;
        private decimal _commitment;
        private decimal _unusedPercentage;
        private readonly DateTime? _expiry;
        private readonly DateTime? _maturity;
        private IEnumerable<Payment> _payments;

        public decimal Capital()
        {
            // this indicates a term loan
            if (_expiry == null && _maturity != null)
                return _commitment * Duration() * RiskFactor();
            if (_expiry != null && _maturity == null)
            {
                if (_unusedPercentage != 1.0m)
                    return _commitment * _unusedPercentage * Duration() * RiskFactor();
                else
                    return (OutstandingRiskAmount() * Duration() * RiskFactor()) + 
                        (UnusedRiskAmount() * Duration() * UnusedRiskFactor());
            }
            return 0.0m;
        }
  
        private decimal UnusedRiskFactor()
        {
            // call RiskFactor service
            return 1m;
        }

        private decimal UnusedRiskAmount()
        {
            return (_commitment - _outstanding);
        }

        private decimal OutstandingRiskAmount()
        {
            return _outstanding;
        }

        private decimal RiskFactor()
        {
            // call RiskFactor service
            return 1m;
        }

        private decimal Duration()
        {
            if (_expiry == null && _maturity != null)
                return WeightedAverageDuration();
            else if (_expiry != null && _maturity == null)
                return YearsTo(_expiry.Value);
            return 0.0m;
        }
  
        private decimal WeightedAverageDuration()
        {
            decimal duration = 0.0m;
            decimal weightedAverage = 0.0m;
            decimal sumOfPayments = 0.0m;
            foreach (var payment in _payments)
            {
                sumOfPayments += payment.Amount;
                weightedAverage += YearsTo(payment.Date) * payment.Amount;
            }
            if (_commitment != 0.0m)
                duration = weightedAverage / sumOfPayments;
            return duration;
        }

        private decimal YearsTo(DateTime endDate)
        {
            DateTime startDate = DateTime.Today;
            return (endDate - startDate).Days / 365;
        }
    }

    public class Payment
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
