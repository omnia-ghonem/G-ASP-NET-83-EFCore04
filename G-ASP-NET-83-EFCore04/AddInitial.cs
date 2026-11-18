using G_ASP_NET_83_EFCore03;
using G_ASP_NET_83_EFCore04.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace G_ASP_NET_83_EFCore04
{
    public static class AddInitialData
    {
  

        public static void AddInitial(NBMSDbcontext context, List<Branch> branches)
        {
                if (!Check.IsEntityRegistered<Branch>(context))
                {
                    Console.WriteLine($"Entity type {typeof(Branch).Name} is not a registered EF Core entity.");
                    return;
                }

                if (!(branches.Select(x => x.Code).Distinct(StringComparer.OrdinalIgnoreCase).Count() == branches.Count))
                { 
                    Console.WriteLine("Branch codes must be unique within the seed batch.");
                    return;
                }   

                if (!context.Branches.Any())
                {
                    EnrollCollection.AddList(context, branches);
                    //Console.WriteLine($"Inserted initial records for {typeof(Branch).Name} Table.");

                }
                else
                {
                    Console.WriteLine($"{typeof(Branch).Name} already contains records in the database.");
                }

              
            }

            public static void AddInitial(NBMSDbcontext context, List<Manager> Managers)
            {

                if (!Check.IsEntityRegistered<Manager>(context))
                {
                    Console.WriteLine($"Entity type {typeof(Manager).Name} is not a registered EF Core entity.");
                    return;
                }
                // Check that there are no duplicate Managers in same List
                if (!(Managers.Select(x => x.Email).Distinct(StringComparer.OrdinalIgnoreCase).Count() == Managers.Count))
                {
                    Console.WriteLine("Managers codes must be unique within the seed batch.");
                    return;
                }

                if (!(Managers.Select(x => x.BranchCode).Distinct(StringComparer.OrdinalIgnoreCase).Count() == Managers.Count))
                {
                    Console.WriteLine("Only one manager is allowed per branch.");
                    return;
                }


               // Check that each Branch has 1 Manager

                if (!context.Managers.Any())
                {
                    EnrollCollection.AddList(context, Managers);
                    //Console.WriteLine($"Inserted initial records for {typeof(Manager).Name} Table.");

                }
                else
                {
                    Console.WriteLine($"{typeof(Manager).Name} already contains records in the database.");
                }

     
            }

  
     

    }
}
