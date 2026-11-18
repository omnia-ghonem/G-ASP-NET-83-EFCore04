using G_ASP_NET_83_EFCore04.Models;
using G_ASP_NET_83_EFCore04.Services;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
//using G_ASP_NET_83_EFCore04.Services;

namespace G_ASP_NET_83_EFCore04
{
    internal class Program
    {

 
        static void Main(string[] args)
        {




            using NBMSDbcontext context = new NBMSDbcontext();

            try
            {
                //context.Database.Migrate();

                #region Add Initial Values

                //Console.WriteLine("============================= INITIAL Data =============================");


                //// ==============================
                //// 1. Create 2 Branches
                //// ==============================

                //List<Branch> branches = new List<Branch>()
                //{
                //    new Branch{ Code = "BR001", Name = "Cairo Branch",Address = "Nasr City, Cairo, Egypt", PhoneNumber = "01012345678" },
                //    new Branch{   Code = "BR002", Name = "Alexandria Branch",  Address = "Smouha, Alexandria, Egypt", PhoneNumber = "01112345678" }
                //};

                //// Insert branches first
                //AddInitialData.AddInitial(context, branches);


                //// ==============================
                //// 2. Create 2 Managers
                //// ==============================

                //List<Manager> managers = new List<Manager>()
                //{
                //    new Manager { FullName = "Ahmed Mohamed", Email = "ahmed.manager@gmail.com", PhoneNumber = "01098765432", BranchCode = "BR001", HireDate = new DateTime(2023, 5, 10)},
                //    new Manager{  FullName = "Mona Hassan",   Email = "mona.manager@gmail.com",   PhoneNumber = "01198765432",   BranchCode = "BR002", HireDate = new DateTime(2024, 3, 15) }
                //};

                //// Insert managers after branches
                //AddInitialData.AddInitial(context, managers);
                #endregion

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("========== NATIONAL BANK ==========");
                    Console.WriteLine("1) Add a new Customer");
                    Console.WriteLine("2) Open an Account for a Customer");
                    Console.WriteLine("3) Update Account Status");
                    Console.WriteLine("4) Remove an Account from a Customer");
                    Console.WriteLine("5) List all Customers (with accounts)");
                    Console.WriteLine("0) Exit");
                    Console.WriteLine("===================================");


                    int choice = UserInput.ReadIntegerInRange("Enter Choice (From 0 to 5): ",0,5);
                    if (choice == 0)
                    {
                        Console.WriteLine("Thank you for using National Bank. Goodbye!");
                        Environment.Exit(0); // The using statements in each operation close their DbContext.
                    }
                    Console.Clear();
                    try
                    {
                        switch (choice)
                        {

                            case 1:
                                AddCustomer.Add(context);
                                break;
                            case 2:
                                OpenAccount.Add(context);
                                break;
                            case 3:
                                UpdateAccountStatus.Update(context);
                                break;
                            case 4:
                                RemoveAccount.RemoveFromCustomer(context);
                                break;
                            case 5:
                                ListCustomers.ListCustomersWAccounts(context);
                                break;

                        }


                    }
                    catch (DbUpdateException)
                    {
                        Console.WriteLine("SQL Server rejected this change. Check foreign keys and unique constraints.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Operation failed: {ex.Message}");
                    }
                    Console.Clear();
                    //ReturnToMainMenu.Return();

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Type: {ex.GetType().Name}");
                Console.WriteLine($"Error Message: {ex.Message}");
                Console.WriteLine($"Full Error: {ex}");
            }

        }
    }
}
