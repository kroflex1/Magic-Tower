using MagicTower.Model.MagicModels;
using NUnit.Framework;

namespace MagicTower.Model.Tests
{
    [TestFixture]
    public class DuplicateShereTests

    {
        private const int roomWidth = 1920;
        private const int roomHeight = 1080;

        [Test]
        public void ShouldCreateEightDuplicatesOfFireball()
        {
            var room = GetSetupRoom();

            var duplicateShere = new DuplicateSphere(roomWidth / 2, roomHeight / 2, 1, 0);
            var fireBall = new FireBall( roomWidth / 2, roomHeight / 2, -1, 0);

            room.SpawnMagic(duplicateShere);
            room.SpawnMagic(fireBall);

            room.Update();
            room.Update();

            Assert.AreEqual(8, room.MagicInRoom.Count);
        }
        
        [Test]
        public void ShouldCreateEightDuplicatesOfIceball()
        {
            var room = GetSetupRoom();

            var duplicateShere = new DuplicateSphere(roomWidth / 2, roomHeight / 2, 1, 0);
            var iceBall = new IceBall( roomWidth / 2, roomHeight / 2, -1, 0);

            room.SpawnMagic(duplicateShere);
            room.SpawnMagic(iceBall);

            room.Update();
            room.Update();

            Assert.AreEqual(8, room.MagicInRoom.Count);
        }

        [Test]
        public void DuplacatedMagicShouldSaveItsSpeed()
        {
            var room = GetSetupRoom();

            var duplicateShere = new DuplicateSphere(roomWidth / 2, roomHeight / 2, 1, 0);
            var fireBall = new FireBall( roomWidth / 2, roomHeight / 2, -1, 0);
            fireBall.Speed = 5;
            
            room.SpawnMagic(duplicateShere);
            room.SpawnMagic(fireBall);
            
            room.Update();
            room.Update();
            
            Assert.AreEqual(room.MagicInRoom[0].Speed, fireBall.Speed);
        }
        
        [Test]
        public void CantCreateDuplicatesOfDuplicateShere()
        {
            var room = GetSetupRoom();
            var firstDuplicateShere = new DuplicateSphere(0, 0, 1, 1);
            var secondDuplicateShere = new DuplicateSphere(2, 2, 1, 1);

            room.SpawnMagic(firstDuplicateShere);
            room.SpawnMagic(secondDuplicateShere);

            room.Update();
            room.Update();

            Assert.AreEqual(2, room.MagicInRoom.Count);
        }

        private Room GetSetupRoom()
        {
            var player = new Player(roomWidth / 2, roomHeight / 2, roomWidth, roomHeight);
            var room = new Room(roomWidth, roomHeight, player);
            return room;
        }
    }
}