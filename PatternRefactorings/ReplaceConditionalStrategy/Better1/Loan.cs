using System;
using System.Collections.Generic;
using System.Linq;

namespace PatternRefactorings.ReplaceConditionalStrategy.Better1
{
    // Create new CapitalStrategy class and Copy Method to it
    // Pass in a loan instance.  In this case, as a method parameter, but as a constructor parameter would also work
    // Note we had to make many fields non-private (internal)
    public class CapitalStrategy
    {
        public decimal Capital(Loan loan)
        {
            if (loan.Expiry == null && loan.Maturity != null)
                return loan.Commitment * DurationFor(loan) * RiskFactorFor(loan);
            if (loan.Expiry != null && loan.Maturity == null)
            {
                if (loan.UnusedPercentage != 1.0m)
                    return loan.Commitment * loan.UnusedPercentage * 
                        DurationFor(loan) * RiskFactorFor(loan);
                else
                    return (loan.Outstanding * DurationFor(loan) * RiskFactorFor(loan)) + 
                        (loan.UnusedRiskAmount() * DurationFor(loan) * UnusedRiskFactorFor(loan));
            }
            return 0.0m;
        }

        private decimal UnusedRiskFactorFor(Loan loan)
        {
            // call RiskFactor service
            return 1m;
        }


        private decimal RiskFactorFor(Loan loan)
        {
            // call RiskFactor service
            return 1m;
        }

        private decimal DurationFor(Loan loan)
        {
            if (loan.Expiry == null && loan.Maturity != null)
                return WeightedAverageDurationFor(loan);
            else if (loan.Expiry != null && loan.Maturity == null)
                return YearsTo(loan.Expiry.Value);
            return 0.0m;
        }

        private decimal WeightedAverageDurationFor(Loan loan)
        {
            decimal duration = 0.0m;
            decimal weightedAverage = 0.0m;
            decimal sumOfPayments = 0.0m;
            foreach (var payment in loan.Payments)
            {
                sumOfPayments += payment.Amount;
                weightedAverage += YearsTo(payment.Date) * payment.Amount;
            }
            if (loan.Commitment != 0.0m)
                duration = weightedAverage / sumOfPayments;
            return duration;
        }

        private decimal YearsTo(DateTime endDate)
        {
            DateTime startDate = DateTime.Today;
            return (endDate - startDate).Days / 365;
        }

    }

    // Loan retains a simple delegating Capital() method
    public class Loan
    {
        private CapitalStrategy _capitalStrategy;
        internal decimal Outstanding;
        internal decimal Commitment;
        internal decimal UnusedPercentage;
        internal readonly DateTime? Expiry;
        internal readonly DateTime? Maturity;
        internal IEnumerable<Payment> Payments;

        public decimal Capital()
        {
            return _capitalStrategy.Capital(this);
            
        }

        internal decimal UnusedRiskAmount()
        {
            return (Commitment - Outstanding);
        }


    }

    public class Payment
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
