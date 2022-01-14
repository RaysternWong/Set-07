using System;
using System.Collections.Generic;

namespace SeriesSum
{
    /**
     * ================================ ATTENTION PLEASE ================================
     *
     * Your ONLY task is to implement the following two methods:
     * 1. PrintSeries
     * 2. MultiSum
     *
     * You are ALLOWED to
     * 1. Add new method(s) in this file.
     * 2. Add additional test case(s) in Main.
     *
     * You are NOT ALLOWED to
     * 1. Add any new method in other files.
     * 2. Change signature of any existing methods.
     *    Method signature includes
     *    - method's name
     *    - return type
     *    - number of parameters
     *    - parameters' type
     *    - access modifier
     *
     * --- Please make sure your code is error-free when built.
     */
    public class Program
    {
        /**
         * You are allowed to add your own test case(s) in Main
         */
        public static void Main(string[] args)
        {
            Console.WriteLine("PrintSeries(16, 1, 0)  :" + PrintSeries(16, 1, 0));
            Console.WriteLine("PrintSeries(16, 1, -4) :" + PrintSeries(16, 1, -4));
            Console.WriteLine("PrintSeries(16, 1, -5) :" + PrintSeries(16, 1, -5));
            Console.WriteLine("PrintSeries(10, 3, 0)  :" + PrintSeries(10, 3, 0));
            Console.WriteLine("PrintSeries(10, 3, -1) :" + PrintSeries(10, 3, -1));
            Console.WriteLine("PrintSeries(10, 3, -2) :" + PrintSeries(10, 3, -2));
            Console.WriteLine();
            Console.WriteLine("MultiSum(4, 1) :" + MultiSum(4, 1));
            Console.WriteLine("MultiSum(3, 2) :" + MultiSum(3, 2));
            Console.WriteLine("MultiSum(1, 3) :" + MultiSum(1, 3));
            Console.WriteLine("MultiSum(2, 3) :" + MultiSum(2, 3));
            Console.WriteLine("MultiSum(4, 2) :" + MultiSum(4, 2));
        }

        /**
         * Print Series
         *
         * Given a number n, starting gap, and lowest displayed number,
         * return following pattern.
         *
         * Example:
         * {1}
         * n = 10
         * starting gap = 3
         * lowest number = 0
         *
         * 10 7 3 7 10
         *
         * {2}
         * n = 16
         * starting gap = 1
         * lowest number = 0
         *
         * 16 15 13 10 6 1 6 10 13 15 16
         */
        public static string PrintSeries(int n, int startingGap, int lowestNumber)
        {
            List<int> numList = new List<int>();
 
            while(n >= lowestNumber)
            {
                numList.Add(n);
                n -= startingGap;
                startingGap++;
            }

            List<int> reverseNumList = new List<int>(numList);
            reverseNumList.Reverse();
            reverseNumList.RemoveAt(0);

            return $"{string.Join(" ", numList)} {string.Join(" ", reverseNumList)} "  ;
        }

        /**
         * Multi Sum
         *
         * Given a number n and count m,
         * find m-th summation of first n natural numbers.
         *
         * Example:
         * {1}
         * SUM(4, 1) = 10
         * Explanation: 1 + 2 + 3 + 4 = 10
         *
         * {2}
         * SUM(3, 2) = 21
         * Explanation: SUM(3, 2) = SUM(SUM(3, 1), 1)
         *                        = SUM(6, 1)
         *                        = 21
         *
         * {3}
         * SUM(1, 3) = 1
         * Explanation: SUM(3, 2) = SUM(SUM(SUM(1,1), 1), 1)
         *                        = SUM(SUM(1, 1), 1)
         *                        = SUM(1, 1)
         *						  = 1
         *
         * Constraints:
         * - 1 <= m <= 4
         * - 1 <= n <= 20
         */
        public static int MultiSum(int n, int m)
        {
            int sum = 0;

            while (n != 0)
            {
                sum += n;
                n--;
            }
            m--;

            return m != 0 ? MultiSum(sum, m) : sum;
        }
    }
}
