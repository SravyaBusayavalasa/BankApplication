using System;
using System.Collections.Generic;
using System.Text;

namespace BankApplication
{
    public class Bank
    {
        public string Name { get; set; }
        public List<Account> Accounts { get; set; }
        public Bank(string Name, List<Account> Accounts)
        {
            this.Name = Name;
            this.Accounts = Accounts;
        }
    }
}
