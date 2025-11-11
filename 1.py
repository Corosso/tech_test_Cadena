import time
from typing import List

def is_prime_linear(n: int) -> bool:
    if n < 2:
        return False
    if n == 2:
        return True
    if n % 2 == 0:
        return False
    
    # Check odd divisors up to sqrt(n)
    i = 3
    while i * i <= n:
        if n % i == 0:
            return False
        i += 2
    return True


def sieve_of_eratosthenes(max_num: int) -> set:

    if max_num < 2:
        return set()
    
    # Create boolean array and initialize all entries as True
    is_prime = [True] * (max_num + 1)
    is_prime[0] = is_prime[1] = False
    
    p = 2
    while p * p <= max_num:
        if is_prime[p]:
            # Mark all multiples of p as not prime
            for i in range(p * p, max_num + 1, p):
                is_prime[i] = False
        p += 1
    
    # Collect all prime numbers
    return {num for num in range(2, max_num + 1) if is_prime[num]}


def sum_of_primes(numbers: List[int]) -> tuple:
    
    # Error handling
    if not isinstance(numbers, list):
        raise TypeError("Input must be a list")
    
    if len(numbers) == 0:
        raise ValueError("List cannot be empty")
    
    for num in numbers:
        if not isinstance(num, int):
            raise TypeError(f"All elements must be integers. Found: {type(num).__name__}")
    
    # Start timing
    start_time = time.perf_counter()
    
    # Analyze input characteristics
    list_size = len(numbers)
    max_number = max(numbers) if numbers else 0
    has_five_digits = max_number >= 100000
    
    # Algorithm selection
    if list_size >= 1000000:
        algorithm_used = "SEGMENTED SIEVE (NOT IMPLEMENTED - Recommended for 1M+ elements)"
        # Note: For production, implement segmented sieve here
        # Falling back to standard sieve for demonstration
        print(f"\n   RECOMMENDATION: For {list_size:,} elements, implement Segmented Sieve")
        print(f"    - Maintains O(n log log n) time complexity")
        print(f"    - Reduces memory usage from O(max_num) to O(√max_num)")
        print(f"    - Processing with standard Sieve for now...\n")
        
        positive_nums = [n for n in numbers if n > 0]
        if positive_nums:
            primes_set = sieve_of_eratosthenes(max(positive_nums))
            prime_sum = sum(num for num in numbers if num in primes_set)
        else:
            prime_sum = 0
        algorithm_used = "Sieve of Eratosthenes (Segmented Sieve recommended)"
    
    elif list_size < 10000 and not has_five_digits:
        # Use linear search for small lists
        algorithm_used = "Linear Search"
        prime_sum = sum(num for num in numbers if is_prime_linear(num))
    
    else:
        # Use Sieve of Eratosthenes for larger lists or big numbers
        algorithm_used = "Sieve of Eratosthenes"
        positive_nums = [n for n in numbers if n > 0]
        if positive_nums:
            primes_set = sieve_of_eratosthenes(max(positive_nums))
            prime_sum = sum(num for num in numbers if num in primes_set)
        else:
            prime_sum = 0
    
    # End timing
    end_time = time.perf_counter()
    execution_time = (end_time - start_time) * 1000  # Convert to milliseconds
    
    return prime_sum, algorithm_used, execution_time


# Example usage and testing
if __name__ == "__main__":
    print("=" * 70)
    print("PRIME NUMBER SUM - ALGORITHM PERFORMANCE ANALYSIS")
    print("=" * 70)
    
    # Test 1: Small list (Linear Search expected)
    print("\n TEST 1: Small list (< 10,000 elements, < 5 digits)")
    test_list_1 = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
    result, algorithm, exec_time = sum_of_primes(test_list_1)
    print(f"Input: {test_list_1}")
    print(f"Sum of primes: {result}")
    print(f"Algorithm used: {algorithm}")
    print(f"Execution time: {exec_time:.4f} ms")
    
    # Test 2: Medium list with large numbers (Sieve expected)
    print("\n TEST 2: List with 5+ digit numbers (Sieve of Eratosthenes expected)")
    test_list_2 = [99991, 100003, 100019, 100043, 50000, 60000]
    result, algorithm, exec_time = sum_of_primes(test_list_2)
    print(f"Input size: {len(test_list_2)} elements")
    print(f"Max number: {max(test_list_2):,}")
    print(f"Sum of primes: {result:,}")
    print(f"Algorithm used: {algorithm}")
    print(f"Execution time: {exec_time:.4f} ms")
    
    # Test 3: Large list (Sieve expected)
    print("\n TEST 3: Large list (> 10,000 elements)")
    test_list_3 = list(range(1, 15001))
    result, algorithm, exec_time = sum_of_primes(test_list_3)
    print(f"Input size: {len(test_list_3):,} elements")
    print(f"Max number: {max(test_list_3):,}")
    print(f"Sum of primes: {result:,}")
    print(f"Algorithm used: {algorithm}")
    print(f"Execution time: {exec_time:.4f} ms")

    # Test 4: Demonstrate segmented sieve recommendation
    print("\n TEST 4: Very large list (1M+ elements - Segmented Sieve recommended)")
    test_list_4 = list(range(1, 1000001))
    result, algorithm, exec_time = sum_of_primes(test_list_4)
    print(f"Input size: {len(test_list_4):,} elements")
    print(f"Max number: {max(test_list_4):,}")
    print(f"Sum of primes: {result:,}")
    print(f"Algorithm used: {algorithm}")
    print(f"Execution time: {exec_time:.4f} ms")
    
    # Error handling demonstration
    print("\n TEST 5: Error handling")
    try:
        sum_of_primes([1, 2, "three", 4])
    except TypeError as e:
        print(f"✓ Error caught correctly: {e}")
    
    print("\n" + "=" * 70)
    print("ANALYSIS COMPLETE")
    print("=" * 70)

# /*   _________        _________
# //  /  _______|      / _______ \
# //  |  |            | | x   x | |
# //  |  |            | |  x x  | |
# //  |  |            | |   +   | |
# //  |  |            | |   +   | |
# //  |  |            | |  x x  | |
# //  |  |_______     | |_x___x_| |
# //  \__________|     \_________/
# //   _________        _________
# //  |    __   \      / _______ \
# //  |   |  |   |    | | x   x | |
# //  |   |__|   |    | |  x x  | |
# //  |   __   __|    | |   +   | |
# //  |  |  \  \      | |   +   | |
# //  |  |   \  \     | |  x x  | |
# //  |  |    \  \    | |_x___x_| |
# //  |__|     \__\    \_________/
