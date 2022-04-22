namespace SantaseCardGame.Core.Logic.Dealer
{
    using System;
    using System.Threading;

    public static class RandomGenerator
    {
        private static readonly ThreadLocal<Random> Random = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref location)));

        private static int location = Environment.TickCount;

        public static int Next(int minValue, int maxValue)
        {
            return Random.Value.Next(minValue, maxValue);
        }
    }
}
