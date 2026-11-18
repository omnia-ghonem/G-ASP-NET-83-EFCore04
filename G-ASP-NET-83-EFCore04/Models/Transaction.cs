using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_ASP_NET_83_EFCore04.Models
{

    public enum TransactionType
    {
        Deposit,
        Withdrawal,
        TransferIn, 
        TransferOut


    }
    public class Transaction
    {

            public int TransactionNumber { get; set; }
            public DateTime TransactionDate { get; set; }
            public decimal Amount { get; set; }

            public TransactionType TransactionType { get; set; }
            public string? Note { get; set; }

            // Account Relationship (Related)

            public int AccountId { get; set; }
            public Account Account { get; set; }


    }
}
