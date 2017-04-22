using Match3.Core.Domain;

namespace Match3.Core.UI.Presenters
{
    public class RandomTileGenerator : ITileGenerator
    {
        private readonly Tile[] tiles;
        private readonly IRandom random;

        public RandomTileGenerator(IRandom random, params string[] tileTypes)
        {
            this.random = random;

            this.tiles = new Tile[tileTypes.Length];

            for (var i = 0; i < tileTypes.Length; i++)
            {
                this.tiles[i] = new Tile(tileTypes[i]);
            }
        }

        public Tile GenerateTile()
        {
            var tileIndex = this.random.GetRandomNumber(this.tiles.Length);
            return this.tiles[tileIndex];
        }
    }
}
