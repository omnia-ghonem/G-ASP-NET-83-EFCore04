using G_ASP_NET_83_EFCore03;
using G_ASP_NET_83_EFCore04.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_ASP_NET_83_EFCore04
{
    public static class EnrollRow
    {

        public static void EnrollBranch(NBMSDbcontext context, Branch branch)
        {
            //try
            //{
                if (!Check.IsEntityRegistered<Branch>(context))
                    throw new InvalidOperationException($"Entity type {typeof(Branch).Name} is not a registered EF Core entity.");
               
                Check.ValidBranch(context,branch);

                if (Check.IsRowExists<Branch>(context, b => EF.Functions.Collate( b.Code, "Latin1_General_100_CI_AS") == branch.Code))
                    throw new InvalidOperationException($"Branch with code '{branch.Code}' already exists.");
                
                context.Branches.Add(branch);
                context.SaveChanges();
                Console.WriteLine($"Branch {branch.Name} was added successfully");

            //}
            //catch (ArgumentException ex)
            //{
            //    Console.WriteLine($"{ex.Message}");
            //}
            //catch (InvalidOperationException ex)
            //{
            //    Console.WriteLine($"{ex.Message}");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Unexpected Error: {ex.Message}");
            //}

        }
        public static void EnrollCustomer(NBMSDbcontext context, Customer customer)
        {

                if (!Check.IsEntityRegistered<Customer>(context))
                    throw new InvalidOperationException($"Entity type {typeof(Customer).Name} is not a registered EF Core entity.");
   
                Check.ValidCustomer(context,customer);

                if (Check.IsRowExists<Customer>(context, c => EF.Functions.Collate(c.Email, "Latin1_General_100_CI_AS") == customer.Email))
                    throw new InvalidOperationException($"A customer with email '{customer.Email}' already exists.");
          
                if (Check.IsRowExists<Customer>(context, c => c.NationalId == customer.NationalId))
                    throw new InvalidOperationException($"A customer with National ID '{customer.NationalId}' already exists.");
 
                context.Customers.Add(customer);
                context.SaveChanges();
                Console.WriteLine($"Customer {customer.FullName} was added successfully");


        }
        public static int EnrollAccount(NBMSDbcontext context, Account account)
        {

            if (!Check.IsEntityRegistered<Account>(context))
                    throw new InvalidOperationException($"Entity type {typeof(Account).Name} is not a registered EF Core entity.");

            if (!Check.IsRowExists<Branch>(context, b => EF.Functions.Collate(b.Code, "Latin1_General_100_CI_AS") == account.BranchCode))
                throw new InvalidOperationException($"Branch with code '{account.BranchCode}' doesn't exists.");

            Check.ValidAccount(context,account);

                context.Accounts.Add(account);
                context.SaveChanges();
                Console.WriteLine($"Account {account.AccountNumber} was added successfully");
                return account.AccountNumber;   


        }
        public static void EnrollCustomerAccount(NBMSDbcontext context, CustomerAccount customeraccount)
        {

                if (!Check.IsEntityRegistered<CustomerAccount>(context))
                    throw new InvalidOperationException($"Entity type {typeof(CustomerAccount).Name} is not a registered EF Core entity.");

                Check.ValidCustomerAccount(context, customeraccount);


                if (!Check.IsRowExists<Account>(context, c => c.AccountNumber == customeraccount.AccountNumber))
                    throw new InvalidOperationException($"An account with number '{customeraccount.AccountNumber}' doesn't exists.");

                if (!Check.IsRowExists<Customer>(context, c => c.Id == customeraccount.CustomerId))
                    throw new InvalidOperationException($"A customer with Id '{customeraccount.CustomerId}' doesn't exists.");

                if (Check.IsRowExists<CustomerAccount>(context, c => c.AccountNumber == customeraccount.AccountNumber && c.CustomerId == customeraccount.CustomerId))
                    throw new InvalidOperationException($"Customer with Id {customeraccount.CustomerId} already opened the account {customeraccount.AccountNumber} as {customeraccount.OwnershipRole}");

                if (Check.IsRowExists<CustomerAccount>(context, c => c.AccountNumber == customeraccount.AccountNumber))
                    customeraccount.OwnershipRole = OwnershipRole.CoHolder;
                else
                    customeraccount.OwnershipRole = OwnershipRole.Primary;
           // OwnershipStartDate

                var account = context.Accounts.FirstOrDefault(a => a.AccountNumber == customeraccount.AccountNumber);

                if(account!.OpeningDate.Date > customeraccount.OwnershipStartDate.Date)
                     throw new ArgumentException($"Ownership Start Date {customeraccount.OwnershipStartDate.Date} must be greater than account opening date {account!.OpeningDate.Date}");

                context.CustomerAccounts.Add(customeraccount);
                context.SaveChanges();
                Console.WriteLine($"Customer with Id {customeraccount.CustomerId} open account {customeraccount.AccountNumber} as {customeraccount.OwnershipRole} successfully");


        }


    }
}
