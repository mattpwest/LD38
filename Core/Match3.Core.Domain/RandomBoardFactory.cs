using System;

namespace Match3.Core.Domain
{
    public class RandomBoardFactory : BoardFactory
    {
        private readonly Tile[] tiles;
        private readonly IRandom random;

        public RandomBoardFactory(IRandom random, params string[] tileTypes)
        {
            this.random = random;

            this.tiles = new Tile[tileTypes.Length];

            for(var i = 0; i < tileTypes.Length; i++)
            {
                this.tiles[i] = new Tile(tileTypes[i]);
            }
        }

        protected override void GenerateTiles(int width, int height, Action<int, int, Tile> setTile)
        {
            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    var tileIndex = this.random.GetRandomNumber(this.tiles.Length);
                    var tileToPlace = this.tiles[tileIndex];
                    setTile(x, y, tileToPlace);
                }
            }
        }
    }
}
