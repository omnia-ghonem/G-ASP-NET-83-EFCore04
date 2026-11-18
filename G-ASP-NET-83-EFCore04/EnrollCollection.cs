using G_ASP_NET_83_EFCore03;
using G_ASP_NET_83_EFCore04.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace G_ASP_NET_83_EFCore04
{
    public static class EnrollCollection
    {

        public static void AddList(NBMSDbcontext context, List<Branch> branches)
        {

            try
            {

                if (!Check.IsEntityRegistered<Branch>(context))
                {
                    throw new InvalidOperationException(
                        "Branch is not registered in EF Core.");
                }


                if (branches == null || branches.Count == 0)
                {
                    throw new ArgumentException("Branch list cannot be null or empty.");
                }

                List<Branch> ValidBranches = new List<Branch>();

                foreach (Branch branch in branches)
                {
                    try
                    {
                        Check.ValidBranch(context, branch);
                        ValidBranches.Add(branch);
                    }
                    catch (ArgumentNullException ex)
                    {
                        Console.WriteLine($"{ex.Message}");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"{ex.Message}");
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine($"{ex.Message}");

                    }

                }
                ValidBranches = ValidBranches.GroupBy(b => b.Code, StringComparer.OrdinalIgnoreCase)
                                            .Select(group => group.First())
                                            .ToList();
                // or :
                // ValidBranches = ValidBranches
                //.DistinctBy(b => b.Code, StringComparer.OrdinalIgnoreCase)
                //.ToList();
                var existingInTable = context.Branches.Select(b => b.Code)
                                               .ToHashSet(StringComparer.OrdinalIgnoreCase);

                var toAdd = ValidBranches.Where(b => !existingInTable.Contains(b.Code)).ToList();

                if (toAdd.Count > 0)
                {
                    context.Branches.AddRange(toAdd);
                    context.SaveChanges();
                    // Report results
                    int skipped = branches.Count - toAdd.Count;
                    Console.WriteLine($"Inserted: {toAdd.Count}, Skipped: {skipped}");
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
        }
    

        public static void AddList(NBMSDbcontext context, List<Manager> Managers)
        {

            try
            {

                if (!Check.IsEntityRegistered<Manager>(context))
                {
                    throw new InvalidOperationException(
                        "Branch is not registered in EF Core.");
                }


                if (Managers == null || Managers.Count == 0)
                {
                    throw new ArgumentException("Branch list cannot be null or empty.");
                }

                List<Manager> ValidManagers = new List<Manager>();


                foreach (Manager manager in Managers)
                {
                    try
                    {
                        Check.ValidManager(context, manager);
                        ValidManagers.Add(manager);
                    }
                    catch (ArgumentNullException ex)
                    {
                        Console.WriteLine($"{ex.Message}");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"{ex.Message}");
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine($"{ex.Message}");

                    }

                }
                ValidManagers = ValidManagers.DistinctBy(m => m.Email, StringComparer.OrdinalIgnoreCase)
                                            .DistinctBy(m => m.BranchCode, StringComparer.OrdinalIgnoreCase)
                                            .ToList();
                // Check both conditions

                var existingEmails = context.Managers.Select(m => m.Email)
                                                    .ToHashSet(StringComparer.OrdinalIgnoreCase);


                var existingBranches = context.Managers.Select(m => m.BranchCode)
                                                        .ToHashSet(StringComparer.OrdinalIgnoreCase);

                
                var toAdd = ValidManagers.Where(m => !existingEmails.Contains(m.Email) &&  !existingBranches.Contains(m.BranchCode)).ToList();
                // or :
                // ValidBranches = ValidBranches
                //.DistinctBy(b => b.Code, StringComparer.OrdinalIgnoreCase)
                //.ToList();

                if (toAdd.Count > 0)
                {
                    context.Managers.AddRange(toAdd);
                    context.SaveChanges();
                        // Report results
                    int skipped = Managers.Count - toAdd.Count;
                    Console.WriteLine($"Inserted: {toAdd.Count}, Skipped: {skipped}");
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
        }
    }
}
