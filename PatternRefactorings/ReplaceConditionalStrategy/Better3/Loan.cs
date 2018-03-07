using System;
using System.Collections.Generic;
using System.Linq;

namespace PatternRefactorings.ReplaceConditionalStrategy.Better3
{
    // Now replace conditional with polymorphism for Capital()
    public class CapitalStrategy
    {
        public virtual decimal Capital(Loan loan)
        {
            // Remove Term Loan case
            //if (loan.Expiry == null && loan.Maturity != null)
            //    return loan.Commitment * DurationFor(loan) * RiskFactorFor(loan);
            if (loan.Expiry != null && loan.Maturity == null)
            {
                if (loan.UnusedPercentage != 1.0m)
                    return loan.Commitment * loan.UnusedPercentage * DurationFor(loan) * RiskFactorFor(loan);
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

        protected decimal RiskFactorFor(Loan loan)
        {
            // call RiskFactor service
            return 1m;
        }

        protected virtual decimal DurationFor(Loan loan)
        {
            if (loan.Expiry != null && loan.Maturity == null)
                return YearsTo(loan.Expiry.Value);
            return 0.0m;
        }

        protected decimal YearsTo(DateTime endDate)
        {
            DateTime startDate = DateTime.Today;
            return (endDate - startDate).Days / 365;
        }
    }

    public class TermLoanCapitalStrategy : CapitalStrategy
    {
        public override decimal Capital(Loan loan)
        {
            return loan.Commitment * DurationFor(loan) * RiskFactorFor(loan);
        }

        protected override decimal DurationFor(Loan loan)
        {
            return WeightedAverageDurationFor(loan);
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
    }

    public class Loan
    {
        private CapitalStrategy _capitalStrategy;
        internal decimal Outstanding;
        internal decimal Commitment;
        internal decimal UnusedPercentage;
        internal readonly DateTime? Expiry;
        internal readonly DateTime? Maturity;
        internal IEnumerable<Payment> Payments;

        public Loan(CapitalStrategy capitalStrategy)
        {
            _capitalStrategy = capitalStrategy;
        }

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
