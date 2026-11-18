using G_ASP_NET_83_EFCore04.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_ASP_NET_83_EFCore04.Services
{
    public static class ListCustomers
    {
        public static void ListCustomersWAccounts(NBMSDbcontext context)
        {

            try
            {
                Console.WriteLine("===================== List of Customers With Their Accounts =====================");
                Console.WriteLine("");

                var customers = context.Customers.Include(c => c.customerAccounts)
                                   .ThenInclude(ca => ca.Accounts)
                                   .ToList();

                if (customers.Count == 0)
                {
                    Console.WriteLine("No customers found.");
                    return;
                }

                foreach (Customer customer in customers)
                {
                    Console.WriteLine("\n==================================");
                    Console.WriteLine($"Customer ID: {customer.Id}");
                    Console.WriteLine($"Full Name: {customer.FullName}");
                    Console.WriteLine($"National ID: {customer.NationalId}");
                    Console.WriteLine($"Email: {customer.Email}");
                    Console.WriteLine($"Phone: {customer.PhoneNumber}");
                    Console.WriteLine($"Customer Type: {customer.CustomerType}");

                    if (customer.customerAccounts.Count == 0)
                    {
                        Console.WriteLine("No accounts found for this customer.");
                        continue;
                    }

                    Console.WriteLine("\nAccounts:");
                    Console.WriteLine("----------------------------------");

                    foreach (CustomerAccount ca in customer.customerAccounts)
                    {
                        Console.WriteLine($"Account Number: {ca.AccountNumber}");
                        Console.WriteLine($"Balance: {ca.Accounts.CurrentBalance:C}");
                        Console.WriteLine($"Type: {ca.Accounts.AccountType}");
                        Console.WriteLine($"Opening Date: {ca.Accounts.OpeningDate:yyyy-MM-dd}");

                        Console.WriteLine($"Ownership: {ca.OwnershipRole}");
                        Console.WriteLine($"Status: {ca.AccountStatus}");
                        Console.WriteLine($"Ownership Start Date: {ca.OwnershipStartDate:yyyy-MM-dd}");
                        Console.WriteLine("----------------------------------");
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");

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
