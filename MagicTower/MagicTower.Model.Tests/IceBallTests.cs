using System.Linq;
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
        public void ShouldCreateCorrectIceShards()
        {
            var room = GetSetupRoom();
            var iceBall = new IceBall(1, 1, 2, 2);
            room.SpawnMagic(iceBall);
            room.SpawnEnemy(2,2);
            
            iceBall.OnCollisionEnter(room.AliveEnemiesInRoom[0]);
            Assert.AreEqual(5, room.MagicInRoom.Count);
        }

        private Room GetSetupRoom()
        {
            var player = new Player(roomWidth / 2, roomHeight / 2, roomWidth, roomHeight);
            var room = new Room(roomWidth, roomHeight, player);
            return room;
        }
    }
}