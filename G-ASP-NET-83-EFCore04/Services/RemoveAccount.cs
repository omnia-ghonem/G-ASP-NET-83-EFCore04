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
    public static class RemoveAccount
    {

        public static void RemoveFromCustomer(NBMSDbcontext context)
        {

            try
            {
                Console.WriteLine("===================== Remove Account =====================");
                Console.WriteLine("");

                int AccountNumber = UserInput.ReadInteger("Enter Account Number: ");
                if (!Check.IsRowExists<Account>(context, a => a.AccountNumber == AccountNumber))
                    throw new InvalidOperationException($"An account with number '{AccountNumber}' doesn't exists.");

                int CustomerId = UserInput.ReadInteger("Enter Customer ID: ");
                if (!Check.IsRowExists<Customer>(context, c => c.Id == CustomerId))
                    throw new InvalidOperationException($"A customer with Id '{CustomerId}' doesn't exists.");
                
                
                if (!Check.IsRowExists<CustomerAccount>(context, ca => ca.AccountNumber == AccountNumber && ca.CustomerId == CustomerId))
                    throw new InvalidOperationException($"A customer with Id '{CustomerId}' hasn't account with number '{AccountNumber}'.");


                var customerOwnership = context.CustomerAccounts.Where(ca => ca.CustomerId == CustomerId && ca.AccountNumber == AccountNumber)
                                                                .Select(ca => ca.OwnershipRole)
                                                                .FirstOrDefault();


                int affectedCustomerAccountRows = context.CustomerAccounts
                .Where(ca => ca.AccountNumber == AccountNumber && ca.CustomerId == CustomerId)
                .ExecuteDelete();

                var IsRowRemain = context.CustomerAccounts.FirstOrDefault(ca => ca.AccountNumber == AccountNumber);


                if (customerOwnership == OwnershipRole.Primary)
                {
                    if (IsRowRemain == null)
                    {
                        int affectedAccountRows = context.Accounts
                           .Where(ca => ca.AccountNumber == AccountNumber)
                           .ExecuteDelete();

                    }
                    else
                    {

                        int UpdatedRows = context.CustomerAccounts
                            .Where(ca => ca.AccountNumber == AccountNumber)
                            .OrderBy(ca => ca.OwnershipStartDate)
                            .ExecuteUpdate(setters => setters.SetProperty(
                                                            ca => ca.OwnershipRole,
                                                            OwnershipRole.Primary
                                                            )
                             );
                    }

                }

                //if (affectedRows == 0)
                //{
                //    Console.WriteLine("No account ownership found for this customer.");
                //    return;
                //}

                Console.WriteLine($"Account {AccountNumber} was successfully removed from Customer {CustomerId}.");
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
