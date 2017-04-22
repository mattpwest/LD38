using System;
using Match3.Core.Domain;

namespace Match3.Core.UI.Presenters
{
    public class RandomBoardFactory : BoardFactory
    {
        private readonly RandomTileGenerator randomTileGenerator;

        public RandomBoardFactory(RandomTileGenerator randomTileGenerator)
        {
            this.randomTileGenerator = randomTileGenerator;
        }

        protected override void GenerateTiles(int width, int height, Action<int, int, Tile> setTile)
        {
            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    setTile(x, y, this.randomTileGenerator.GenerateTile());
                }
            }
        }
    }
}
