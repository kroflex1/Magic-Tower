using System.Linq;
using MagicTower.Model.EnemiesModels;
using MagicTower.Model.Magic;
using MagicTower.Model.MagicModels;
using NUnit.Framework;

namespace MagicTower.Model.Tests
{
    [TestFixture]
    public class IceBallTests
    {
        private const int roomWidth = 1920;
        private const int roomHeight = 1080;

        [Test]
        public void ShouldCreateIceShards()
        {
            var room = GetSetupRoom();
            var iceBall = new IceBall(roomWidth / 2, roomHeight / 2, roomWidth / 2 + 1, roomHeight / 2);

            room.SpawnMagic(iceBall);
            room.SpawnEnemy(new LittleDemon(roomWidth / 2, roomHeight / 2));

            room.Update();
            room.Update();

            Assert.AreEqual(5, room.MagicInRoom.Count);
        }

        private Arena GetSetupRoom()
        {
            var player = new Player(roomWidth / 2, roomHeight / 2, roomWidth, roomHeight);
            var room = new Arena(roomWidth, roomHeight, player);
            return room;
        }
    }
}