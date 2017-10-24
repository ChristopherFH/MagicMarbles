using System;

namespace MagicMarbles.Utils
{
    class RandomNumberGenerator
    {
        public static int Dice(int lowerBound, int upperBound)
        {
            return new Random(DateTime.Now.Millisecond).Next(lowerBound, upperBound);
        }
    }
}
