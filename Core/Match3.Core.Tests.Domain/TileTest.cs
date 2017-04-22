using Match3.Core.Domain;
using NUnit.Framework;

namespace Match3.Core.Tests.Domain
{
    [TestFixture]
    public class TileTest
    {
        [Test]
        public void TestTileCanBeCreated()
        {
            var tileType = "TestTile";
            var tile = new Tile(tileType);

            Assert.IsNotNull(tile);
            Assert.AreEqual(tileType, tile.Type);
        }
    }
}
