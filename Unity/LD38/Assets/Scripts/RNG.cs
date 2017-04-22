using Match3.Core.Domain;

public class RNG : IRandom
{

    private static RNG instance = null;

    public static IRandom Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new RNG();
            }

            return instance;
        }
    }

    private RNG()
    {
    }

    public int GetRandomNumber(int inclusiveFrom, int exclusiveTo)
    {
        return UnityEngine.Random.Range(inclusiveFrom, exclusiveTo - 1);
    }

    public int GetRandomNumber(int exclusiveTo)
    {
        return GetRandomNumber(0, exclusiveTo);
    }
}
