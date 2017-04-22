using System;
using Match3.Core.Domain;

namespace Match3.Core.Tests.Domain.Stubs
{
    public class StubBoardFactory : BoardFactory
    {
        protected override void GenerateTiles(int width, int height, Action<int, int, Tile> setTile)
        {
            for(var x = 0; x < width; x++)
            {
                for(var y = 0; y < height; y++)
                {
                    var tile = new Tile(Guid.NewGuid().ToString());
                    setTile(x, y, tile);
                }
            }
        }
    }
}
