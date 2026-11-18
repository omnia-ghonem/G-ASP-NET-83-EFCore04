using G_ASP_NET_83_EFCore03;
using G_ASP_NET_83_EFCore04.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace G_ASP_NET_83_EFCore04
{
    public static class UserInput
    {

        public static int ReadInteger(string message)
        {
            while (true)
            {
                Console.Write($"{message}");
                try
                {
                    string? input = Console.ReadLine();

                    // Check if the user entered nothing
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        throw new ArgumentException("Please enter number that is not empty or whitespace.");
                    }

                    if (!int.TryParse(input, out int number))
                    {
                        throw new ArgumentException("Please enter a valid integer.");
                    }

                    return number;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Please enter number that is not too large or small.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected Error: {ex.Message}");
                }

            }

        }


        public static int ReadIntegerInRange(string message, int min, int max)
        {


            while (true)
            {

                try
                {

                    int number = ReadInteger(message);
                    if (number < min || number > max)
                    {
                        throw new ArgumentException($"Please choose between {min} and {max}");
                    }

                    return number;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }

            }
         
        }

        public static decimal ReadDecimal(string message)
        {
            while (true)
            {
                Console.Write($"{message}");
                try
                {
                    string? input = Console.ReadLine();

                    // Check if the user entered nothing
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        throw new ArgumentException("Please enter number that is not empty or whitespace.");
                    }

                    if (!decimal.TryParse(input, out decimal number))
                    {
                        throw new ArgumentException("Please enter a valid decimal number.");
                    }

                    return number;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Please enter number that is not too large or small.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected Error: {ex.Message}");
                }

            }

        }
        public static string ReadString(string message)
        {


            while (true)
            {

                Console.Write($"{message}");
                try
                {
                    string? input = Console.ReadLine();
                    if (!Check.IsValidString(input))
                        throw new ArgumentException("Please enter text that is not empty or whitespace.");
                    
                    return input!.Trim();
                }

                catch (ArgumentException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected Error: {ex.Message}");
                }

            }
         
        }
        public static string ReadName(string message)
        {


            while (true)
            {

                Console.Write($"{message}");
                try
                {
                    string? input = Console.ReadLine();
                    if (!Check.IsValidString(input))
                        throw new ArgumentException("Please enter text that is not empty or whitespace.");
                    if (!(input!.All(c => char.IsLetter(c) || c==' ')))
                        throw new ArgumentException("Name must contain letters and spaces only.");
                    return input!.Trim();
                }

                catch (ArgumentException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected Error: {ex.Message}");
                }

            }
         
        }
        public static string ReadEmail(string message, int min = 1, int max = 200)
        {


            while (true)
            {

                Console.Write($"{message}");
                try
                {
                    string? input = Console.ReadLine();
                    Check.ValidEmail(input, min, max);

                    return input!.Trim();
                }

                catch (ArgumentException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected Error: {ex.Message}");
                }

            }
         
        }
        public static DateTime ReadDate(string message)
        {


            while (true)
            {

                Console.Write($"{message}");
                try
                {
                    string? input = Console.ReadLine();
                    if (!Check.IsValidString(input))
                        throw new ArgumentException("Please enter date that is not empty or whitespace.");
                    
                    if (!DateTime.TryParse(input, out DateTime date))
                        throw new ArgumentException("Please enter a valid date (2000-05-15).");

                    return date!;
                }
                catch (ArgumentException ex)
                {
                        Console.WriteLine($"Validation Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected Error: {ex.Message}");
                }

            }
         
        }
        public static string ReadPhoneNumber(string message)
        {


            while (true)
            {

                Console.Write($"{message}");
                try
                {
                    string? input = Console.ReadLine();
                    if (!Check.IsValidString(input))
                        throw new ArgumentException("Please enter phone number that is not empty or contains whitespace.");

                    input = input!.Trim();
                    Check.ValidPhoneNumber(input,11);

                    return input;
                }
                catch (ArgumentException ex)
                {
                        Console.WriteLine($"Validation Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected Error: {ex.Message}");
                }

            }
         
        }

        // ==================== Extract Date of Birth from Egyptian National ID ====================
        public static DateTime DOBFromNationalId(string? NationalId)
        {
            Check.ValidNationalId(NationalId, 14);
            int century = NationalId![0] == '3' ? 2000 : 1900;
            int year = century + int.Parse(NationalId.Substring(1, 2));
            int month = int.Parse(NationalId.Substring(3, 2));
            int day = int.Parse(NationalId.Substring(5, 2));
            DateTime.TryParse($"{year:D4}-{month:D2}-{day:D2}", out DateTime dateOfBirth);
            return dateOfBirth;
        }

        public static string ReadNationalId(string message)
        {


            while (true)
            {

                Console.Write($"{message}");
                try
                {
                    string? input = Console.ReadLine();
                    if (!Check.IsValidString(input))
                        throw new ArgumentException("Please enter national id that is not empty or contains whitespace.");

                    Check.ValidNationalId(input,14);

                    return input!.Trim();
                }
                catch (ArgumentException ex)
                {
                        Console.WriteLine($"Validation Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected Error: {ex.Message}");
                }

            }
         
        }
   
        public static T ReadType<T>(string message) where T: struct, Enum
        {

            
            while (true)
            {

                Console.Write($"{message}");
                try
                {
                    string? input = Console.ReadLine();
                    if (!Check.IsValidString(input))
                        throw new ArgumentException($"{typeof(T).Name} type cannot be empty or whitespace.");

                    input!.Trim();
                    if (!Enum.TryParse(input,out T type))
                        throw new ArgumentException($"{typeof(T).Name} type must be {string.Join("or", Enum.GetNames<T>())}.");

                    if(!Enum.IsDefined(type))
                        throw new ArgumentException($"{typeof(T).Name} type must be {string.Join("or", Enum.GetNames<T>())}.");

                    return type;
                }
                catch (ArgumentException ex)
                {
                        Console.WriteLine($"Validation Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected Error: {ex.Message}");
                }

            }
         
        }
        public static T ReadTypebyChoice<T>() where T: struct, Enum
        {

            
            while (true)
            {
                T[] values = Enum.GetValues<T>();

                // Display available enum values
                for (int i = 0; i < values.Length; i++)
                {
                    Console.WriteLine($"{i + 1}) {values[i]}");
                }

                Console.Write($"Enter {typeof(T).Name} Type (1-{values.Length}): ");
                try
                {
                    string? input = Console.ReadLine();
                    if (!Check.IsValidString(input))
                        throw new ArgumentException($"{typeof(T).Name} type cannot be empty or whitespace.");

                    input!.Trim();
                    if (!int.TryParse(input,out int choice))
                        throw new ArgumentException($"Please enter a valid integer.");

                    if (choice < 1 || choice > values.Length)
                        throw new ArgumentException($"Please enter a number between 1 and {values.Length}.");
                    return values[choice -1];
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
                catch (ArgumentException ex)
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
}
