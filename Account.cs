using System;
using System.Collections.Generic;
using System.Text;

namespace BankApplication
{
    public abstract class Account
    {
        public long AccountNumber { get; set; }
        public string Owner { get; set; }
        public double Balance { get; set; }

        public Account(long AccountNumber, string Owner, double Balance)
        {
            this.AccountNumber = AccountNumber;
            this.Owner = Owner;
            this.Balance = Balance;
        }

        public virtual bool Deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Amount should be > 0");
            }
            Balance += amount;
            return true;
        }
        public virtual bool Withdraw(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Amount should be > 0");
            }
            if (amount > Balance)
            {
                throw new ArgumentException("Insufficient funds");
            }
            Balance -= amount;
            return true;
        }
        public virtual bool Transfer(double amount, Account destination)
        {
            bool IsSuccess = false;
            try
            {
                IsSuccess = this.Withdraw(amount);

                if (IsSuccess)
                {
                    destination.Deposit(amount);
                }

                return true;
            }
            catch (Exception ex)
            {
                if (IsSuccess)
                    this.Deposit(amount);

                Console.WriteLine(ex.Message);
                throw;
            }

        }
    }
}
