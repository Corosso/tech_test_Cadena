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
            Console.WriteLine(new string('=', 70));
            Console.WriteLine("PALINDROME CHECKER - EFFICIENT ALGORITHM ANALYSIS");
            Console.WriteLine(new string('=', 70));

            // Test 1: Example from problem
            Console.WriteLine("\nTEST 1: Example case");
            string test1 = "A man a plan a canal Panama";
            Console.WriteLine($"Input: \"{test1}\"");
            Console.WriteLine($"Output: {IsPalindrome(test1)}");
            Console.WriteLine($"Algorithm: Two-Pointer (O(n) time, O(1) space)");

            // Test 2: Simple palindrome
            Console.WriteLine("\nTEST 2: Simple palindrome");
            string test2 = "racecar";
            Console.WriteLine($"Input: \"{test2}\"");
            Console.WriteLine($"Output: {IsPalindrome(test2)}");

            // Test 3: Not a palindrome
            Console.WriteLine("\nTEST 3: Not a palindrome");
            string test3 = "hello world";
            Console.WriteLine($"Input: \"{test3}\"");
            Console.WriteLine($"Output: {IsPalindrome(test3)}");

            // Test 4: With punctuation
            Console.WriteLine("\nTEST 4: Complex with punctuation");
            string test4 = "Was it a car or a cat I saw?";
            Console.WriteLine($"Input: \"{test4}\"");
            Console.WriteLine($"Output: {IsPalindrome(test4)}");


            // Test 5: Edge cases
            Console.WriteLine("\nTEST 8: Edge cases");
            Console.WriteLine($"Empty string: {IsPalindrome("")}");
            Console.WriteLine($"Single char: {IsPalindrome("a")}");
            Console.WriteLine($"Only spaces: {IsPalindrome("   ")}");
            Console.WriteLine($"Only punctuation: {IsPalindrome("!!!???")}");



            Console.WriteLine("ANALYSIS COMPLETE");


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