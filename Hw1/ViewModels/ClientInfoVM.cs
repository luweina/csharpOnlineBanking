using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Hw1.ViewModels
{
    public class ClientInfoVM
    {
        [DisplayName("Client Id")]
        public int ClientId { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Account Number")]
        public int AccountNum { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Account Type")]
        public string AccountType { get; set; }

        [DisplayName("Balance")]
        public decimal Balance { get; set; }
    }
}
