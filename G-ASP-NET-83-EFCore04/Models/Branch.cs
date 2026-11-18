using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_ASP_NET_83_EFCore04.Models
{
    public class Branch 
    {

        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        // Manager Relationship
        public Manager Manager { get; set; }

        // Account Relationship
        public List<Account> Accounts { get; set; } = new List<Account>();


    }
}
