using System;
using Match3.Core.Domain;

namespace Match3.Core.Tests.Domain.Stubs {
    internal class StubHorizontalMatchBoardFactory : BoardFactory {

        protected override void GenerateTiles()
        {
            var matchTile = new Tile("match");

            for(var x = 0; x < Board.Width; x++)
            {
                for(var y = 0; y < Board.Height; y++)
                {
                    if(y == 0 && x < 3)
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