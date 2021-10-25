using NUnit.Framework;
using RandomQuiz.Util;
using System.Diagnostics;
using System.Collections.Generic;

namespace RandomQuiz.Tests.Util
{
    [TestFixture]
    class RandomGenTests
    {
        [Test]
        public void Generate_NumbersAreInRange()
        {
            int lower = 1;
            int higher = 10;

            for (int i = 0; i < 1_000; i++)
            {
                int rnd = RandomGen.Generate(lower, higher);
                Assert.IsTrue(rnd >= lower && rnd <= higher);
            }
        }

        [Test]
        public void Generate_NumbersAreNotConsecutive()
        {
            int lower = 1;
            int upper = 3;
            int lastRandomNumber = 0;
            
            for (int i = 0; i < 1_000; i++)
            {
                int generatedNumber = RandomGen.Generate(lower, upper);
                Assert.IsTrue(generatedNumber != lastRandomNumber);
                lastRandomNumber = generatedNumber;
            }
        }

        [Test]
        public void Generate_NumbersHaveNotBeenCalledRecently()
        {
            int lower = 1;
            int upper = 10;
            Queue<int> beenGenerated = new Queue<int>(5);

            for (int i = 0; i < 1_000; i++)
            {
                int generatedNumber = RandomGen.Generate(lower, upper);
                Assert.IsTrue(!beenGenerated.Contains(generatedNumber));

                if (beenGenerated.Count == 5)
                    beenGenerated.Dequeue();
                beenGenerated.Enqueue(generatedNumber);
            }
        }
    }
}
