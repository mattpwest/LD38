using System;
using Match3.Core.Domain;

namespace Match3.Core.Tests.Domain.Stubs
{
    internal class StubHorizontalMatchBoardFactory : BoardFactory
    {
        protected override void GenerateTiles(int width, int height, Action<int, int, Tile> setTile)
        {
            var matchTile = new Tile("match");

            for(var x = 0; x < width; x++)
            {
                for(var y = 0; y < height; y++)
                {
                    if(y == 0 && x < 3)
                    {
                        setTile(x, y, matchTile);
                    }
                    else
                    {
                        setTile(x, y, new Tile(Guid.NewGuid().ToString()));
                    }
                }
            }
        }
    }
}
