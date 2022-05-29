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
            
            var duplicateShere = new DuplicateSphere(0, 0, 1, 1);
            var fireBall = new FireBall(2, 2, 1, 1);
            
            room.SpawnMagic(duplicateShere);
            room.SpawnMagic(fireBall);
            
            duplicateShere.OnCollisionEnter(fireBall);
            fireBall.OnCollisionEnter(fireBall);
            
            Assert.AreEqual(10, room.MagicInRoom.Count);
        }


        [Test]
        public void CantCreateDuplicatesOfDuplicateShere()
        {
            var room = GetSetupRoom();
            var firstDuplicateShere = new DuplicateSphere(0, 0, 1, 1);
            var secondDuplicateShere = new DuplicateSphere(2, 2, 1, 1);
            
            room.SpawnMagic(firstDuplicateShere);
            room.SpawnMagic(secondDuplicateShere);
            
            firstDuplicateShere.OnCollisionEnter(secondDuplicateShere);
            secondDuplicateShere.OnCollisionEnter(firstDuplicateShere);
            
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