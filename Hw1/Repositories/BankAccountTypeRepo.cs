using Hw1.Data;
using Hw1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hw1.Repositories
{
    public class BankAccountTypeRepo
    {
        ApplicationDbContext db;

        public BankAccountTypeRepo(ApplicationDbContext context)
        {
            db = context;
        }

        public int Create(string type, decimal balance)
        {
            BankAccount bankAccount = new BankAccount()
            {
                accountType = type,
                balance = balance
            };

            db.BankAccounts.Add(bankAccount);
            db.SaveChanges();

            return bankAccount.accountNum;
        }

        public bool Update(int accountNum, decimal balance)
        {
            BankAccount bankAccount = db.BankAccounts.Where(b => b.accountNum == accountNum).FirstOrDefault();
            bankAccount.balance = balance;
            db.SaveChanges();
            return true;
        }
    
    }
}
