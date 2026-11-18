using G_ASP_NET_83_EFCore04;
using G_ASP_NET_83_EFCore04.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace G_ASP_NET_83_EFCore03
{
    public static class Check
    {

        public static bool IsValidDateOfBirth(DateTime dateOfBirth,int minAge = 18)
        {
            if (dateOfBirth == DateTime.MinValue)
                return false;

            if (dateOfBirth.Date > DateTime.Today)
                return false;

            if (dateOfBirth.Date > DateTime.Today.AddYears(-minAge))
                return false;

            return true;
        }


        #region Contact and Identity Validation

        // ==================== Validate Customer Email Address ====================
        public static void ValidEmail(string? email, int min, int max)
        {

            if (!Check.IsValidString(email, min, max))
                throw new ArgumentException("Email must not be empty or contain whitespace.");
            email = email!.Trim();
            if (email.Contains(' '))
                throw new ArgumentException("Email cannot contain spaces.");

            if (!email.Contains('@'))
                throw new ArgumentException("Email must contain '@'.");

            if (email.Count(c => c == '@') != 1)
                throw new ArgumentException("Email must contain exactly one '@'.");

            string[] parts = email.Split('@');
            if (string.IsNullOrWhiteSpace(parts[0]))
                throw new ArgumentException("Email must contain a username before '@'.");

            if (string.IsNullOrWhiteSpace(parts[1]))
                throw new ArgumentException("Email must contain a domain after '@'.");

            if (!parts[1].EndsWith(".com"))
                throw new ArgumentException("Email domain must end with '.com' ");
        }

        // ==================== Validate Phone Number ====================
        public static void ValidPhoneNumber(string? phoneNo, int len)
        {

            if (!Check.IsValidString(phoneNo))
                throw new ArgumentException("Phone number must not be empty or contain whitespace.");
            phoneNo = phoneNo!.Trim();
            if (phoneNo.Contains(' '))
                throw new ArgumentException("Phone number cannot contain spaces.");

            if (phoneNo.Length != len)
                throw new ArgumentException($"Phone number must contain exactly {len} digits.");

            if (!phoneNo.All(c => c >= '0' && c <= '9'))
                throw new ArgumentException("Phone number must contain digits only.");

            if (!phoneNo.StartsWith("010") && !phoneNo.StartsWith("011") && !phoneNo.StartsWith("012") && !phoneNo.StartsWith("015"))
                throw new ArgumentException("Phone number must start with 010, 011, 012, or 015.");

        }



        // ==================== Validate Egyptian National ID Format ====================
        public static void ValidNationalId(string? NationalId, int len)
        {

            if (!Check.IsValidString(NationalId))
                throw new ArgumentException("National ID cannot be empty or whitespace.");
            NationalId = NationalId!.Trim();
            if (NationalId.Contains(' '))
                throw new ArgumentException("National ID cannot contain spaces.");

            if (NationalId.Length != len)
                throw new ArgumentException($"National ID must contain exactly {len} digits.");

            if (!NationalId.All(c => c >= '0' && c <= '9'))
                throw new ArgumentException("National ID must contain digits only.");

            if (NationalId[0] != '2' && NationalId[0] != '3')
                throw new ArgumentException("National ID must start with 2 or 3.");

        } 
        public static void ValidEnumType<T>(T Type) where T: struct, Enum
        {

            if (!Enum.IsDefined(Type))
                throw new ArgumentException($"{typeof(T).Name} type must be {string.Join("or", Enum.GetNames<T>())}.");


        } 
        #endregion

        #region Is String Valid
        public static bool IsValidString(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;
            else
                return true;

        }
        public static bool IsValidString(string? input, int minlen, int maxlen)
        {
            if (string.IsNullOrWhiteSpace(input) || input.Length < minlen || input.Length > maxlen)
                return false;
            else
                return true;

        }
        public static bool IsValidString(string? input, int exactlen)
        {
            if (string.IsNullOrWhiteSpace(input) || input.Length != exactlen)
                return false;
            else
                return true;

        } 
        #endregion

        #region Is Number Valid
        public static bool IsValidInteger(int number, int min = int.MinValue, int max = int.MaxValue)
        {

            if (number < min || number > max)
                return false;
            else
                return true;

        }
        public static bool IsValidDecimal(decimal number, decimal min = decimal.MinValue, decimal max = decimal.MaxValue)
        {

            if (number < min || number > max)
                return false;
            else
                return true;

        }
        #endregion

        #region EF Core Entity and Record Existence Checks


        // ==================== Check If Entity Is Registered in EF Core ====================
        public static bool IsEntityRegistered<T>(NBMSDbcontext context) where T : class
        {
            return context.Model.FindEntityType(typeof(T)) != null;
        }

        // ==================== Check If a Matching Record Exists in the Database ====================
        public static bool IsRowExists<T>(NBMSDbcontext context, Expression<Func<T, bool>> predicate) where T : class
        {
            if (!IsEntityRegistered<T>(context))
            {
                Console.WriteLine($"Entity type {typeof(T).Name} is not a registered EF Core entity.");
                return false;
            }
            return context.Set<T>().Any(predicate);
        } 
        #endregion

        public static void ValidBranch(NBMSDbcontext context, Branch branch)
        {
            if (branch == null)
                throw new ArgumentNullException(nameof(branch));

            if (!Check.IsValidString(branch.Name,1,150))
                throw new ArgumentException("Branch name cannot be null, empty, or whitespace.");
            
            if (!Check.IsValidString(branch.Address,1,250))
                throw new ArgumentException("Branch address cannot be null, empty, or whitespace.");

            if (!Check.IsValidString(branch.PhoneNumber, 11))
                throw new ArgumentException("Branch phone number must contain exactly 11 characters.");
            


        }
        public static void ValidManager(NBMSDbcontext context, Manager manager)
        {
            if (manager == null)
                throw new ArgumentNullException(nameof(manager));

            if (!Check.IsValidString(manager.FullName,1,150))
                throw new ArgumentException("Manager name cannot be null, empty, or whitespace.");

            ValidEmail(manager.Email, 1, 200);

            if (!Check.IsValidString(manager.PhoneNumber, 11))
                throw new ArgumentException("Manager phone number must contain exactly 11 characters.");
            
            if (!(manager.HireDate <= DateTime.Today))
                throw new ArgumentException("Manager hire date cannot be in the future.");


        }
        public static void ValidCustomer(NBMSDbcontext context, Customer customer)
        {

            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            if (!Check.IsValidString(customer.Address, 1, 250))
                throw new ArgumentException("Customer address cannot be null, empty, or whitespace.");

            if (!IsValidDateOfBirth(customer.DateOfBirth))
                throw new ArgumentException("Customer date of birth must be a valid date in the past and the age must be > 18.");

            if (!Check.IsValidString(customer.FullName, 1, 150))
                throw new ArgumentException("Customer full name is required and must be between 1 and 150 characters.");
            
            ValidEmail(customer.Email, 1, 200);

            if (!Check.IsValidString(customer.PhoneNumber, 11))
                throw new ArgumentException("Customer phone number must contain exactly 11 characters.");
            
            if (!Enum.IsDefined(customer.CustomerType))
                throw new ArgumentException("Customer type must be Individual or Business.");

            if (!Check.IsValidString(customer.NationalId, 14))
                throw new ArgumentException("Customer National Id number must contain exactly 14 characters.");


        }

        public static void ValidAccount(NBMSDbcontext context, Account account)
        {

            if (account == null)
                throw new ArgumentNullException(nameof(account));

            if (!IsValidDecimal(account.CurrentBalance , 0))
                throw new ArgumentException($"Account balance must be a non-negative decimal value within the allowed range (0 - {decimal.MaxValue}).");

            if (!Enum.IsDefined(account.AccountType))
                throw new ArgumentException("Account type must be Current, Business or Savings.");

            if (!(account.OpeningDate == DateTime.Today))
                throw new ArgumentException("Account open date must be today's date.");

            if (!Check.IsValidString(account.BranchCode))
                throw new ArgumentException("Branch code cannot be null, empty, or whitespace.");


        }
        public static void ValidCustomerAccount(NBMSDbcontext context, CustomerAccount customeraccount)
        {
            if (customeraccount == null)
                throw new ArgumentNullException(nameof(CustomerAccount));

            if (!Check.IsValidInteger(customeraccount.AccountNumber , 0))
                throw new ArgumentException($"Account number must be a non-negative within the allowed range (0 - {int.MaxValue}).");

            if (!IsValidDecimal(customeraccount.CustomerId , 0))
                throw new ArgumentException($"Account balance must be a non-negative decimal value within the allowed range (0 - {decimal.MaxValue}).");

            ValidEnumType(customeraccount.OwnershipRole);

            ValidEnumType(customeraccount.AccountStatus);

            if (!(customeraccount.OwnershipStartDate == DateTime.Today))
                throw new ArgumentException("Account open date must be today's date.");
       
        }
       
    }
}
