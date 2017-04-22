using System;
using Match3.Core.Domain;

namespace Match3.Core.Tests.Domain.Stubs
{
    public class StubBoardFactory : BoardFactory
    {
        protected override void GenerateTiles()
        {
            for(var x = 0; x < Board.Width; x++)
            {
                for(var y = 0; y < Board.Height; y++)
                {
                    var tile = new Tile(Guid.NewGuid().ToString());
                    SetTile(x, y, tile);
                }
            }
        }
    }
}
