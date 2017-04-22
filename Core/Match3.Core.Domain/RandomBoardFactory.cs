namespace Match3.Core.Domain
{
    public class RandomBoardFactory : BoardFactory
    {
        private readonly Tile[] tiles;
        private readonly IRandom random;

        public RandomBoardFactory(IRandom random, params string[] tileTypes)
        {
            this.random = random;

            this.tiles = new Tile[tileTypes.Length];

            for(var i = 0; i < tileTypes.Length; i++)
            {
                this.tiles[i] = new Tile(tileTypes[i]);
            }
        }

        protected override void GenerateTiles()
        {
            for(int x = 0; x < this.Board.Width; x++)
            {
                for(int y = 0; y < this.Board.Height; y++)
                {
                    var tileIndex = this.random.GetRandomNumber(this.tiles.Length);
                    var tileToPlace = this.tiles[tileIndex];
                    this.SetTile(x, y, tileToPlace);
                }
            }
        }
    }
}
