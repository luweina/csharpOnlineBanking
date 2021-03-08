using Hw1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hw1.Repositories
{
    public class ClientRepo
    {

        ApplicationDbContext db;

        public ClientRepo(ApplicationDbContext context)
        {
            db = context;
        }


        public int Create(string LN, string FN, string email)
        {
            Client client = new Client()
            {
                lastName = LN,
                firstName = FN,
                email = email
            };

            db.Clients.Add(client);
            db.SaveChanges();

            return client.clientID;
        }

        public bool Update(int clientId, string LN, string FN)
        {

            Client client = db.Clients.Where(c => c.clientID == clientId).FirstOrDefault();
            client.lastName = LN;
            client.firstName = FN;

            db.SaveChanges();

            return true;
        }
    }

}
