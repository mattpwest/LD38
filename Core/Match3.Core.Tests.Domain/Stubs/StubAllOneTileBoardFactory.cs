using System;
using Match3.Core.Domain;

namespace Match3.Core.Tests.Domain.Stubs
{
    public class StubAllOneTileBoardFactory : BoardFactory
    {
        private readonly Tile tile;

        public StubAllOneTileBoardFactory()
        {
            this.tile = new Tile(Guid.Empty.ToString());
        }

        protected override void GenerateTiles(int width, int height, Action<int, int, Tile> setTile)
        {
            if(width > 2 || height > 2)
            {
                throw new InvalidOperationException("This test factory is only for boards with a width and height of 2 or less");
            }

            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    setTile(x, y, this.tile);
                }
            }
        }
    }
}
