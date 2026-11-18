using G_ASP_NET_83_EFCore04.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G_ASP_NET_83_EFCore04;
using G_ASP_NET_83_EFCore03;
namespace G_ASP_NET_83_EFCore04.Services
{
    public static class AddCustomer
    {
        public static void Add(NBMSDbcontext context)
        {
            try 
            {
                Console.WriteLine("===================== Add Customer =====================");
                Console.WriteLine("");

                string FullName = UserInput.ReadName("Enter customer Full Name: ");
                string Address = UserInput.ReadString("Enter customer Address: ");
                //DateTime DateOfBirth = UserInput.ReadDate("Enter Date of Birth (YYYY-MM-DD): ");
                string Email = UserInput.ReadEmail("Enter customer Email: ");
                string PhoneNumber = UserInput.ReadPhoneNumber("Enter customer Phone Number: ");
                CustomerType CustomerType = UserInput.ReadTypebyChoice<CustomerType>();
                string NationalId = UserInput.ReadNationalId("Enter customer National Id: ");
                DateTime DateOfBirth = UserInput.DOBFromNationalId(NationalId);

                Customer newcustomer = new Customer { Address = Address, DateOfBirth = DateOfBirth, FullName = FullName, Email = Email, PhoneNumber = PhoneNumber, CustomerType = CustomerType, NationalId = NationalId };
                EnrollRow.EnrollCustomer(context, newcustomer);
                Console.WriteLine($"A new customer of id {newcustomer.Id} is added successfuly.");

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
