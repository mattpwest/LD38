using Match3.Core.Domain;

namespace Match3.Core.Tests.Domain.Stubs
{
    public class StubBoardFactory : AbstractBoardFactory
    {
        private readonly Tile stubTile = new Tile("test");

        protected override void GenerateTiles()
        {
            for(var x = 0; x < Board.Width; x++)
            {
                for(var y = 0; y < Board.Height; y++)
                {
                    SetTile(x, y, this.stubTile);
                }
            }
        }
    }
}
