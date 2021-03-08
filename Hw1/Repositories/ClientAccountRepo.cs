using Hw1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hw1.Repositories
{
    public class ClientAccountRepo
    {

        ApplicationDbContext db;

        public ClientAccountRepo(ApplicationDbContext context)
        {
            db = context;
        }

        public bool create(int accountId, int clientId)
        {
            ClientAccount clientAccount = new ClientAccount()
            {
                accountNum = accountId,
                clientID = clientId
            };

            db.ClientAccounts.Add(clientAccount);
            db.SaveChanges();

            return true;
        }
    }
}
