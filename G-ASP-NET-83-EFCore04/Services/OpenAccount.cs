using G_ASP_NET_83_EFCore03;
using G_ASP_NET_83_EFCore04.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace G_ASP_NET_83_EFCore04.Services
{
    public static class OpenAccount
    {
        public static void Add(NBMSDbcontext context)
        {
            try
            {
                Console.WriteLine("===================== Add Account =====================");
                Console.WriteLine("");


                int ExistOrCreate = UserInput.ReadIntegerInRange("1) Create New Account \n2) Co-holder to excisting account \nChoose:   ", 1,2);


                // ==================== Declare Shared Variables ====================

                int AccountNumber = 0;
                Account? newaccount = null;
                AccountStatus AccountStatus = AccountStatus.Active;
                if (ExistOrCreate == 1)
                {

                    // ==================== Read Account Details ====================

                    string BranchCode = UserInput.ReadString("Enter Branch Code: ");
                    if (!Check.IsRowExists<Branch>(context, b => EF.Functions.Collate(b.Code, "Latin1_General_100_CI_AS") == BranchCode))
                        throw new InvalidOperationException($"Branch with code '{BranchCode}' doesn't exists.");

                    decimal CurrentBalance = UserInput.ReadDecimal("Enter account Balance: ");
                    DateTime OpeningDate = DateTime.Today;
                    AccountType AccountType = UserInput.ReadTypebyChoice<AccountType>();

                    // ==================== Create Account ====================

                    newaccount = new Account
                    {
                        CurrentBalance = CurrentBalance,
                        OpeningDate = OpeningDate,
                        AccountType = AccountType.Business,
                        BranchCode = BranchCode
                    };
                }
                else
                {
                    AccountNumber = UserInput.ReadInteger("Enter the account number: ");
                    var account = context.CustomerAccounts.FirstOrDefault( a => a.AccountNumber == AccountNumber);
                    if (account == null)
                        throw new ArgumentException($"Account of number {AccountNumber} hasn't owner");
                }


                // ==================== Read Customer Details ====================

                Console.WriteLine();
                int CutomerId = UserInput.ReadInteger("Enter Customer Id: ");
                OwnershipRole OwnershipRole = OwnershipRole.Primary;
                if (ExistOrCreate == 1)
                    AccountStatus = UserInput.ReadTypebyChoice<AccountStatus>();
                else
                    AccountStatus = context.CustomerAccounts.Where(ca => ca.AccountNumber == AccountNumber)
                                                            .Select(ca => ca.AccountStatus)
                                                            .FirstOrDefault();

                        
                DateTime OwnershipStartDate = DateTime.Today;





                // ==================== Begin Transaction ====================

                using var transaction = context.Database.BeginTransaction();

                try
                {

                    if (ExistOrCreate == 1)
                    {
                        // Step 1: Insert the account

                        AccountNumber = EnrollRow.EnrollAccount(context, newaccount!);
                        Console.WriteLine($"A new account of number {newaccount!.AccountNumber} is added successfuly.");

                    }
              
                    // Step 2: Create the first owner

                    CustomerAccount newcustomeraccount = new CustomerAccount 
                    { 
                        AccountNumber = AccountNumber, 
                        CustomerId = CutomerId, 
                        OwnershipRole = OwnershipRole, 
                        AccountStatus = AccountStatus, 
                        OwnershipStartDate = OwnershipStartDate 
                    };
                    EnrollRow.EnrollCustomerAccount(context, newcustomeraccount);
                    // Step 4: Both operations succeeded
                    transaction.Commit();

                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }



            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"{ex.Message}");
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


/*
 
 AccountStatus = context.CustomerAccounts
    .First(ca => ca.AccountNumber == AccountNumber)
    .AccountStatus;
works:

    Finds the first CustomerAccount matching AccountNumber.
    Retrieves the entire CustomerAccount entity from the database.
    Accesses its AccountStatus property in C#.

SQL:

SELECT TOP(1) *
FROM CustomerAccounts
WHERE AccountNumber = @AccountNumber;


AccountStatus = context.CustomerAccounts
    .Where(ca => ca.AccountNumber == AccountNumber)
    .Select(ca => ca.AccountStatus)
    .FirstOrDefault();

works:
Filters records by AccountNumber.
Selects only the AccountStatus column.
Retrieves its first value, or the default value if nothing matches.
 
SQL:
SELECT TOP(1) AccountStatus
FROM CustomerAccounts
WHERE AccountNumber = @AccountNumber;


 */