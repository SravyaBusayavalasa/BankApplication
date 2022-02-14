using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BankApplication.Test
{
    [TestClass]
    public class TransactionTest
    {
        Bank BankA, BankB;

        [TestInitialize]
        public void TestInitialize()
        {
            var accounts = new List<Account>
            {
                new CheckingAccount(123456780, "Owner-A-1", 2500.00),
                new InvestmentAccount(123456781, "Owner-A-2", 15000.00, InvestmentType.individual),
                new InvestmentAccount(123456782, "Owner-A-3", 300000.00, InvestmentType.corporate)
            };
            BankA = new Bank("Bank-A", accounts);

            accounts = new List<Account>
            {
                new CheckingAccount(123456780, "Owner-B-1", 2500.00),
                new InvestmentAccount(123456781, "Owner-B-2", 15000.00, InvestmentType.individual),
                new InvestmentAccount(123456782, "Owner-B-3", 300000.00, InvestmentType.corporate)
            };
            BankB = new Bank("Bank-B", accounts);
        }

        // Withdrawal -> -ve amount/Zero, >balance, <balance, >500 for individual investment

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Withdraw_NegativeAmount_Test()
        {
            BankA.Accounts[0].Withdraw(-250);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Withdraw_ZeroAmount_Test()
        {
            BankA.Accounts[0].Withdraw(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Withdraw_AmountMoreThanBalance_Test()
        {
            BankA.Accounts[0].Withdraw(3500);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Withdraw_IndividualInvestment_Test()
        {
            BankA.Accounts[1].Withdraw(1000);
        }

        [TestMethod]
        public void Withdraw_Success_Test()
        {
            Assert.IsTrue(BankA.Accounts[0].Withdraw(1000));
            Assert.AreEqual(BankA.Accounts[0].Balance, 1500);

            Assert.IsTrue(BankA.Accounts[1].Withdraw(500));
            Assert.AreEqual(BankA.Accounts[1].Balance, 14500);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Deposit_NegativeAmount_Test()
        {
            BankA.Accounts[0].Deposit(-250);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Deposit_ZeroAmount_Test()
        {
            BankA.Accounts[0].Deposit(0);
        }

        [TestMethod]
        public void Deposit_AmountMoreThanBalance_Test()
        {
            Assert.IsTrue(BankA.Accounts[2].Deposit(500));
            Assert.AreEqual(BankA.Accounts[2].Balance, 300500);
        }

        [TestMethod]
        public void Transfer_IntraBank_Test()
        {
            // Transfer -> Interbank, Intrabank
            Assert.IsTrue(BankA.Accounts[0].Transfer(1000, BankA.Accounts[1]));
            Assert.AreEqual(BankA.Accounts[0].Balance, 1500);
            Assert.AreEqual(BankA.Accounts[1].Balance, 16000);
        }

        [TestMethod]
        public void Transfer_InterBank_Test()
        {
            // Transfer -> Interbank, Intrabank
            Assert.IsTrue(BankA.Accounts[2].Transfer(100000, BankB.Accounts[2]));
            Assert.AreEqual(BankA.Accounts[2].Balance, 200000);
            Assert.AreEqual(BankB.Accounts[2].Balance, 400000);
        }
    }
}
