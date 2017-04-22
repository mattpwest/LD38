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

        protected override void GenerateTiles()
        {
            if(this.Board.Width > 2 || this.Board.Height > 2)
            {
                throw new InvalidOperationException("This test factory is only for boards with a width and height of 2 or less");
            }

            for(int x = 0; x < this.Board.Width; x++)
            {
                for(int y = 0; y < this.Board.Height; y++)
                {
                    this.SetTile(x, y, this.tile);
                }
            }
        }
    }
}
