using System;
using System.Collections.Generic;
using System.Text;

namespace BankApplication
{
    public enum InvestmentType
    {
        individual,
        corporate
    }

    public class InvestmentAccount : Account
    {
        public InvestmentType type { get; set; }
        public double WithdrawLimit { get; set; }
        public InvestmentAccount(long AccountNumber, string Owner, double Balance, InvestmentType type, double WithdrawLimit = 500) : base(AccountNumber, Owner, Balance)
        {
            this.type = type;
            this.WithdrawLimit = WithdrawLimit;
        }

        public override bool Withdraw(double amount)
        {
            if (type == InvestmentType.individual && amount > WithdrawLimit)
            {
                throw new ArgumentException("Withdrawal limit is " + WithdrawLimit);
            }

            return base.Withdraw(amount);
        }
    }
}
