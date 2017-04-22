namespace Match3.Core.UI.Presenters
{
    internal class Move
    {
        public int ToX { get; }
        public int ToY { get; }
        public int FromX { get; }
        public int FromY { get; }

        public Move(int toX, int toY, int fromX, int fromY)
        {
            this.ToX = toX;
            this.ToY = toY;
            this.FromX = fromX;
            this.FromY = fromY;
        }
    }
}
