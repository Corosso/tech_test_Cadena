using System;
using System.Diagnostics;
using System.Text;

namespace PalindromeChecker
{
    class Program
    {
        public static bool IsPalindrome(string input)
        {
            // Error handling
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Input cannot be null");

            if (string.IsNullOrWhiteSpace(input))
                return true; // Empty or whitespace-only strings are considered palindromes

            int left = 0;
            int right = input.Length - 1;

            // Two-pointer approach - O(n) time, O(1) space
            while (left < right)
            {
                // Skip non-alphanumeric characters from left
                while (left < right && !char.IsLetterOrDigit(input[left]))
                {
                    left++;
                }

                // Skip non-alphanumeric characters from right
                while (left < right && !char.IsLetterOrDigit(input[right]))
                {
                    right--;
                }

                // Compare characters (case-insensitive)
                if (char.ToLower(input[left]) != char.ToLower(input[right]))
                {
                    return false; // Early termination - more efficient
                }

                left++;
                right--;
            }

            return true;
        }

        static void Main(string[] args)
        {
            Console.Write("\nWelcome to the Palindrome Checker for Cadena tech test");
            Console.Write("\nEnter a string to check if it's a palindrome: ");
            string input = Console.ReadLine();

            try
            {
                bool result = IsPalindrome(input);
                Console.WriteLine($"\nInput: \"{input}\"");
                Console.WriteLine($"Output: {result}");
                
                if (result)
                {
                    Console.WriteLine("✓ The string IS a palindrome!");
                }
                else
                {
                    Console.WriteLine("✗ The string is NOT a palindrome.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}

/*   _________        _________
//  /  _______|      / _______ \
//  |  |            | | x   x | |
//  |  |            | |  x x  | |
//  |  |            | |   +   | |
//  |  |            | |   +   | |
//  |  |            | |  x x  | |
//  |  |_______     | |_x___x_| |
//  \__________|     \_________/
//   _________        _________
//  |    __   \      / _______ \
//  |   |  |   |    | | x   x | |
//  |   |__|   |    | |  x x  | |
//  |   __   __|    | |   +   | |
//  |  |  \  \      | |   +   | |
//  |  |   \  \     | |  x x  | |
//  |  |    \  \    | |_x___x_| |
//  |__|     \__\    \_________/
*/