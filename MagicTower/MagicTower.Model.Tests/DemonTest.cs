using MagicTower.Model.EnemiesModels;
using MagicTower.Model.MagicModels;
using NUnit.Framework;

namespace MagicTower.Model.Tests
{
    [TestFixture]
    public class DemonTest
    {
        private const int roomWidth = 1920;
        private const int roomHeight = 1080;

        [Test]
        public void DemonShouldSpawnLittleDemonsAfterDeath()
        {
            var room = GetRoomPreset();
            var demon = new Demon(roomWidth / 2, roomHeight / 2);
            var iceBall = new IceBall(roomWidth / 2, roomHeight / 2, roomWidth / 2, roomHeight / 2);
            room.SpawnEnemy(demon);
            for (int i = 0; i < 3; i++)
                demon.OnCollisionEnter(iceBall);
            room.Update();
            Assert.AreEqual(3, room.AliveEnemiesInRoom.Count);
        }

        private Arena GetRoomPreset()
        {
            var player = new Player(0, 0, roomWidth, roomHeight);
            var room = new Arena(roomWidth, roomHeight, player);
            return room;
        }
    }
}