using G_ASP_NET_83_EFCore03;
using G_ASP_NET_83_EFCore04.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_ASP_NET_83_EFCore04.Services
{
    public static class UpdateAccountStatus
    {
        public static void Update(NBMSDbcontext context)
        {

            try
            {
                Console.WriteLine("===================== Update Account Status =====================");
                Console.WriteLine("");

                int AccountNumber = UserInput.ReadInteger("Enter Account Number: ");
                // int CustomerId = UserInput.ReadInteger("Enter Customer ID: ");
                // AccountStatus status = UserInput.ReadTypebyChoice<AccountStatus>();
                if (!Check.IsRowExists<CustomerAccount>(context, c => c.AccountNumber == AccountNumber))
                    throw new InvalidOperationException($"An account with number '{AccountNumber}' doesn't connect to customer.");


                var affectedRows = context.CustomerAccounts
                .Where(ca => ca.AccountNumber == AccountNumber)
                .ExecuteUpdate(setters => setters.SetProperty(
                        ca => ca.AccountStatus,
                        ca => ca.AccountStatus == AccountStatus.Active ? AccountStatus.Closed : AccountStatus.Active
                ));

                if (affectedRows == 0)
                {
                    Console.WriteLine("No account ownership found for this customer.");
                    return;
                }

                // Retrieves only the AccountStatus property.
                AccountStatus newStatus = context.CustomerAccounts
                .Where(ca => ca.AccountNumber == AccountNumber)
                .Select(ca => ca.AccountStatus)
                .FirstOrDefault();

                Console.WriteLine($"Account status updated to {newStatus} successfully.");

            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            finally
            {
                Console.WriteLine();
                Console.WriteLine("Press Enter to return to the Main Menu...");
                Console.ReadLine();
            }

        }
    }
}
