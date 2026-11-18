using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_ASP_NET_83_EFCore04.Models
{

    public enum CustomerType
    {
        Individual,
        Business
    }
    public class Customer
    {
        public int Id { get; set; }

        public string Address { get; set; }

        public DateTime DateOfBirth { get; set; }

        public String FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public CustomerType CustomerType { get; set; }

        public string NationalId { get; set; }


        // Account Relationship (Have)

        public List<CustomerAccount> customerAccounts { get; set; } = new List<CustomerAccount>();
    }
}
