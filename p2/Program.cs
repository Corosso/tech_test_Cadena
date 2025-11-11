using System;
using System.Diagnostics;
using System.Text;

namespace PalindromeChecker
{
    class Program
    {
        /// <summary>
        /// Checks if a string is a palindrome using Two-Pointer technique.
        /// Time Complexity: O(n)
        /// Space Complexity: O(1) - No additional data structures created
        /// 
        /// This is the most efficient approach for large strings as it:
        /// - Avoids creating a reversed copy of the string
        /// - Uses constant space (only two pointers)
        /// - Single pass through the string
        /// - Early termination on mismatch
        /// </summary>
        /// <param name="input">String to check</param>
        /// <returns>True if palindrome, False otherwise</returns>
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
        
        /// <summary>
        /// Alternative approach using StringBuilder (for comparison).
        /// Time Complexity: O(n)
        /// Space Complexity: O(n) - Creates cleaned string
        /// 
        /// This approach is LESS efficient for large strings because:
        /// - Creates a new string in memory
        /// - Requires additional space proportional to input size
        /// - Two passes: one to clean, one to compare
        /// 
        /// Use this only if you need the cleaned string for other purposes.
        /// </summary>
        public static bool IsPalindromeWithStringBuilder(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Input cannot be null");
            
            if (string.IsNullOrWhiteSpace(input))
                return true;
            
            // Clean the string - O(n) space
            StringBuilder cleaned = new StringBuilder();
            foreach (char c in input)
            {
                if (char.IsLetterOrDigit(c))
                {
                    cleaned.Append(char.ToLower(c));
                }
            }
            
            // Check palindrome
            string cleanedStr = cleaned.ToString();
            int left = 0;
            int right = cleanedStr.Length - 1;
            
            while (left < right)
            {
                if (cleanedStr[left] != cleanedStr[right])
                {
                    return false;
                }
                left++;
                right--;
            }
            
            return true;
        }
        
        /// <summary>
        /// Measures and compares performance of both algorithms
        /// </summary>
        static void ComparePerformance(string testString, string description)
        {
            Console.WriteLine($"\n{new string('=', 50)} {description} {new string('=', 50)}");
            Console.WriteLine($"String length: {testString.Length:N0} characters");
            
            Stopwatch sw = new Stopwatch();
            
            // Test Two-Pointer approach
            sw.Start();
            bool result1 = IsPalindrome(testString);
            sw.Stop();
            long twoPointerTime = sw.ElapsedTicks;
            
            // Test StringBuilder approach
            sw.Restart();
            bool result2 = IsPalindromeWithStringBuilder(testString);
            sw.Stop();
            long stringBuilderTime = sw.ElapsedTicks;
            
            Console.WriteLine($"\nALGORITHM COMPARISON:");
            Console.WriteLine($"   Two-Pointer (O(1) space):    {twoPointerTime} ticks - Result: {result1}");
            Console.WriteLine($"   StringBuilder (O(n) space):  {stringBuilderTime} ticks - Result: {result2}");
            Console.WriteLine($"   Performance gain: {(stringBuilderTime / (double)twoPointerTime):F2}x faster");
            Console.WriteLine($"   Memory efficiency: Two-Pointer uses CONSTANT space vs LINEAR space");
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
            
            // Test 5: Performance comparison with medium string
            string mediumString = new string('a', 10000) + new string('b', 10000) + 
                                 new string('b', 10000) + new string('a', 10000);
            ComparePerformance(mediumString, "MEDIUM STRING (40,000 chars)");
            
            // Test 6: Performance comparison with large string
            StringBuilder largeBuilder = new StringBuilder();
            for (int i = 0; i < 100000; i++)
            {
                largeBuilder.Append("Able was I ere I saw Elba ");
            }
            string largeString = largeBuilder.ToString();
            ComparePerformance(largeString, "LARGE STRING (2.7M+ chars)");
            
            // Test 7: Error handling
            Console.WriteLine("\nTEST 7: Error handling");
            try
            {
                IsPalindrome(null!);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Error caught correctly: {ex.Message}");
            }
            
            // Test 8: Edge cases
            Console.WriteLine("\nTEST 8: Edge cases");
            Console.WriteLine($"Empty string: {IsPalindrome("")}");
            Console.WriteLine($"Single char: {IsPalindrome("a")}");
            Console.WriteLine($"Only spaces: {IsPalindrome("   ")}");
            Console.WriteLine($"Only punctuation: {IsPalindrome("!!!???")}");
            
            Console.WriteLine("\n" + new string('=', 70));
            Console.WriteLine("EFFICIENCY ANALYSIS FOR LARGE STRINGS:");
            Console.WriteLine(new string('=', 70));
            Console.WriteLine("\nRECOMMENDED: Two-Pointer Algorithm");
            Console.WriteLine("   - Time Complexity: O(n) - single pass");
            Console.WriteLine("   - Space Complexity: O(1) - constant space");
            Console.WriteLine("   - Early termination on mismatch");
            Console.WriteLine("   - No additional memory allocation");
            Console.WriteLine("   - Best for: Strings of ANY size (KB to GB)");
            
            Console.WriteLine("\nALTERNATIVE: StringBuilder Approach");
            Console.WriteLine("   - Time Complexity: O(n) - two passes");
            Console.WriteLine("   - Space Complexity: O(n) - creates copy");
            Console.WriteLine("   - Requires memory for cleaned string");
            Console.WriteLine("   - Use only if: You need the cleaned string elsewhere");
            
            Console.WriteLine("\nFOR EXTREMELY LARGE STRINGS (GB+):");
            Console.WriteLine("   - Consider streaming approach");
            Console.WriteLine("   - Process chunks instead of entire string");
            Console.WriteLine("   - Use Memory-Mapped Files for files larger than RAM");
            Console.WriteLine("   - Implement parallel comparison for multi-core optimization");
            
            Console.WriteLine("\n" + new string('=', 70));
            Console.WriteLine("ANALYSIS COMPLETE");
            Console.WriteLine(new string('=', 70));
            
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}