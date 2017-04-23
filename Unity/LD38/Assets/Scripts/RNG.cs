using Match3.Core.Domain;
using UnityEngine;

internal class RNG : IRandom
{
    private static RNG instance = null;
    private readonly int seed;

    internal int Seed
    {
        get
        {
            return this.seed;
        }
    }

    internal static RNG NewInstance()
    {
        return new RNG();
    }

    internal static RNG NewInstance(int seed)
    {
        return new RNG(seed);
    }

    private RNG()
    {
        this.seed = Random.Range(0, int.MaxValue);
        Random.InitState(this.seed);
    }

    private RNG(int seed)
        :this()
    {
        this.seed = seed;
    }

    public int GetRandomNumber(int inclusiveFrom, int exclusiveTo)
    {
        return Random.Range(inclusiveFrom, exclusiveTo);
    }

    public int GetRandomNumber(int exclusiveTo)
    {
        return GetRandomNumber(0, exclusiveTo);
    }
}
