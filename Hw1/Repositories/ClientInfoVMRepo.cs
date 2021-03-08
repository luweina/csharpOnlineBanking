using Hw1.Data;
using Hw1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hw1.Repositories
{
    public class ClientInfoVMRepo
    {
        ApplicationDbContext db;
        public ClientInfoVMRepo(ApplicationDbContext context)
        {
            db = context;
        }
        public IQueryable<ClientInfoVM> GetAll()
        {
            var query = from ca in db.ClientAccounts
                        from c in db.Clients
                        where ca.clientID == c.clientID
                        select new
                        {
                            accId = ca.accountNum,
                            clientID = c.clientID,
                            lastName = c.lastName,
                            firstName = c.firstName,
                            email = c.email
                        };

            var join = from b in db.BankAccounts
                         from q in query
                         where b.accountNum == q.accId
                         select new ClientInfoVM()
                         {
                             ClientId = q.clientID,
                             LastName = q.lastName,
                             FirstName = q.firstName,
                             Email = q.email,
                             AccountNum = b.accountNum,
                             AccountType = b.accountType,
                             Balance = b.balance
                         };
            return join;
        }
        public bool create(ClientInfoVM ciVM)
        {
            ClientRepo clientRP = new ClientRepo(db);
            BankAccountTypeRepo bankRP = new BankAccountTypeRepo(db);
            ClientAccountRepo clientAccountRP = new ClientAccountRepo(db);

            int clientId = clientRP.Create(ciVM.LastName, ciVM.FirstName, ciVM.Email);
            int accountnum = bankRP.Create(ciVM.AccountType, ciVM.Balance);
            clientAccountRP.create(accountnum, clientId);

            return true;
        }
        public ClientInfoVM GetByEmail(string email)
        {
            var query = GetAll().Where(q => q.Email == email).FirstOrDefault();

            return query;
        }
        public ClientInfoVM Get(int clientId, int accountNum)
        {
            var query = GetAll()
                .Where(q => q.ClientId == clientId && q.AccountNum == accountNum)
                .FirstOrDefault();

            return query;

        }
        public bool Update(ClientInfoVM ciVM)
        {
            ClientRepo cRP = new ClientRepo(db);
            cRP.Update(ciVM.ClientId, ciVM.LastName, ciVM.FirstName);

            BankAccountTypeRepo bRP = new BankAccountTypeRepo(db);
            bRP.Update(ciVM.AccountNum, ciVM.Balance);
            return true;
        }
    }
}
