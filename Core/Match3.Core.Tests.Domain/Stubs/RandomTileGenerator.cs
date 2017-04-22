using System;
using Match3.Core.Domain;

namespace Match3.Core.Tests.Domain.Stubs
{
    public class RandomTileGenerator : ITileGenerator
    {
        public Tile GenerateTile()
        {
            return new Tile(Guid.NewGuid().ToString());
        }
    }
}
