using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_ASP_NET_83_EFCore04.Models
{

    public enum OwnershipRole
    {
        Primary,
        CoHolder

    }

    public enum AccountStatus
    { 
        Active,
        Closed
    
    }
    public class CustomerAccount
    {
        public int CustomerId;
        public Customer Customers { get; set; }

        public int AccountNumber { get; set; }

        public Account Accounts { get; set; }
        public OwnershipRole OwnershipRole { get; set; }

        public AccountStatus AccountStatus { get; set; }

        public DateTime OwnershipStartDate { get; set; }

    }
}
