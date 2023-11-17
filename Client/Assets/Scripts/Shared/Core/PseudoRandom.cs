using System;

namespace Shared.Core
{
    public class PseudoRandom : IPseudoRandomService
    {
        public static int CreateTimeBasedSeed() => Environment.TickCount & Int32.MaxValue;

        private MersenneTwister random;

        public int Sequence { get; private set; }

        public ulong Seed { get { return System.Convert.ToUInt64(random.Seed); } }

        public PseudoRandom()
            : this(CreateTimeBasedSeed())
        { }

        public PseudoRandom(int seed)
        {
            random = new MersenneTwister(seed);
            Sequence = 0;
        }

        public PseudoRandom(int seed, int count)
        {
            Reset(seed, count);
        }

        public void SetSequence(int sequence)
        {
            if (sequence > Sequence)
            {
                int seq = System.Math.Max(0, sequence - Sequence);
                for (int i = 0; i < seq; ++i)
                {
                    random.Next();
                }
                this.Sequence = sequence;
            }
            else
            {
                Reset(random.Seed, sequence);
            }
        }

        public void Reset(int seed, int sequence)
        {
            random = new MersenneTwister(seed);
            for (int i = 0; i < sequence; ++i)
            {
                random.Next();
            }
            this.Sequence = sequence;
        }

        public void Reset(ulong seed, int sequence)
        {
            Reset(Convert.ToInt32(seed), sequence);
        }

        public int Next()
        {
            Sequence++;
            return random.Next();
        }

        public int Next(int maxValue)
        {
            Sequence++;
            return random.Next(maxValue);
        }

        public int Next(int minValue, int maxValue)
        {
            Sequence++;
            return random.Next(minValue, maxValue);
        }

        public int NextIncludeMax(int minValue, int maxValue)
        {
            Sequence++;
            return random.NextIncludeMax(minValue, maxValue);
        }

        public ulong NextULong() => throw new System.NotImplementedException();

        public float NextFloat()
        {
            Sequence++;
            return random.NextFloat();
        }

        public float NextFloat(bool includeOne)
        {
            Sequence++;
            return random.NextFloat(includeOne);
        }

        public float NextFloatPositive()
        {
            Sequence++;
            return random.NextFloatPositive();
        }

        public double NextDouble()
        {
            Sequence++;
            return random.NextDouble();
        }

        public override string ToString()
        {
            return $"[{GetType().ToString()}]:Seed:{Seed}/Sequence:{Sequence}";
        }
    }
}
