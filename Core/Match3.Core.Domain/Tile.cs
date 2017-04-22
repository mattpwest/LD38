namespace Match3.Core.Domain
{
    public class Tile
    {
        public string Type { get; }

        public Tile(string tileType)
        {
            this.Type = tileType;
        }
    }
}
