using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Diagnostics;

namespace RandomQuiz.Util
{
    public static class RandomGen
    {
        internal static Queue<int> alreadyGenerated = new();

        /// <summary>
        /// Generates a random number. It is implemented in such a way that it doesn't
        /// generate the same number consecutively.
        /// </summary>
        /// <param name="lower">The lower bound, inclusive.</param>
        /// <param name="upper">The higher bound, inclusive.</param>
        /// <returns>A random <see cref="int"/>.</returns>
        public static int Generate(int lower, int upper)
        {
            int number = 0;
            bool isReadyToGo = false;
            updateQueue(lower, upper);
            while (!isReadyToGo)
            {
                number = generateRandom(lower, upper);
                isReadyToGo = !calledRecently(number);
            }
            alreadyGenerated.Enqueue(number);
            return number;
        }

        private static bool calledRecently(int number)
        {
            if (alreadyGenerated.Contains(number))
                return true;
            return false;
        }

        private static int generateRandom(int lower, int upper)
        {
            byte[] rndBytes = new byte[4];
            using var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(rndBytes);
            double decimalRandom = Math.Abs((double)BitConverter.ToInt32(rndBytes, 0) / (double)int.MaxValue);
            return Convert.ToInt32((decimalRandom * (upper - lower)) + lower);
        }

        private static void updateQueue(int lower, int upper)
        {
            int allowedCount = (upper - (lower - 1)) / 2;
            while (alreadyGenerated.Count > allowedCount)
            {
                alreadyGenerated.Dequeue();
            }
        }
    }
}
