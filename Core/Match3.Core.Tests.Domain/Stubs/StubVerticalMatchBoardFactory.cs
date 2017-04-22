using System;
using Match3.Core.Domain;

namespace Match3.Core.Tests.Domain.Stubs {
    internal class StubVerticalMatchBoardFactory : AbstractBoardFactory {

        protected override void GenerateTiles()
        {
            var matchTile = new Tile("match");

            for(var x = 0; x < Board.Width; x++)
            {
                for(var y = 0; y < Board.Height; y++)
                {
                    if(x == 0 && y < 3)
                    {
                        SetTile(x, y, matchTile);
                    }
                    else
                    {
                        SetTile(x, y, new Tile(Guid.NewGuid().ToString()));
                    }
                }
            }
        }
    }
}