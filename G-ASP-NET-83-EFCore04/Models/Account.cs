using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_ASP_NET_83_EFCore04.Models
{

    public enum AccountType
    { 
        Current,
        Savings,
        Business
    
    }
    public class Account
    {

        public int AccountNumber { get; set; }
        public decimal CurrentBalance { get; set; }
        public AccountType AccountType { get; set; }
        public DateTime OpeningDate { get; set; }

        // Branch Relationship (Belong)
        public string BranchCode{ get; set; }
        public Branch Branch { get; set; }


        // Customer Relationship (Have)
        public List<CustomerAccount> customerAccounts { get; set; } = new List<CustomerAccount>();

        // Transaction Relationship (Related)

        public List<Transaction> Transactions { get; set; } = new List<Transaction>();

    }
}
