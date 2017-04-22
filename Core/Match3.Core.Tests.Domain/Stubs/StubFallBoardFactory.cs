using System;
using Match3.Core.Domain;

namespace Match3.Core.Tests.Domain.Stubs
{
    public class StubFallBoardFactory : BoardFactory
    {
        protected override void GenerateTiles(int width, int height, Action<int, int, Tile> setTile)
        {
            if (width != 2 || height != 4)
            {
                throw new InvalidOperationException("This test factory is only for boards with a width of 2 and height of 4");
            }

            for (int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    if (y == 1 || y == 2 && x == 1)
                    {
                        continue;
                    }

                    setTile(x, y, new Tile(Guid.NewGuid().ToString()));
                }
            }
        }
    }
}
