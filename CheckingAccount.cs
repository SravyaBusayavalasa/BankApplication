using System;
using System.Collections.Generic;
using System.Text;

namespace BankApplication
{
    public class CheckingAccount : Account
    {
        public CheckingAccount(long AccountNumber, string Owner, double Balance) : base(AccountNumber, Owner, Balance)
        {

        }
    }
}
