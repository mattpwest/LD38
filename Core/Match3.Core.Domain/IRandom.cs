namespace Match3.Core.Domain
{
    public interface IRandom
    {
        int GetRandomNumber(int inclusiveFrom, int exclusiveTo);

        int GetRandomNumber(int exclusiveTo);
    }
}
