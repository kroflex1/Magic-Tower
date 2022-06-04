using System.Collections.Generic;
using MagicTower.Model.EnemiesModels;
using MagicTower.Model.Magic;
using NUnit.Framework;

namespace MagicTower.Model.Tests
{
    [TestFixture]
    public class CollisionControllerTests
    {
        private const int roomWidth = 1920;
        private const int roomHeight = 1080;

        [Test]
        public void RectanglesIntersectionFromZeroPoint()
        {
            var r1 = new CollisionController.Rectangle(0, 0, 5, 5);
            var r2 = new CollisionController.Rectangle(0, 0, 3, 3);
            Assert.AreEqual(true, CollisionController.IsIntersection(r1, r2));
        }

        [TestCase(2, 3, 3, 3)]
        [TestCase(2, 5, 3, 3)]
        [TestCase(9, 4, 3, 3)]
        [TestCase(9, 9, 3, 3)]
        [TestCase(6, 6, 3, 3)]
        public void RectanglesIntersection(int x, int y, int width, int height)
        {
            var r1 = new CollisionController.Rectangle(5, 5, 5, 5);
            var r2 = new CollisionController.Rectangle(x, y, width, height);
            Assert.AreEqual(true, CollisionController.IsIntersection(r1, r2));
        }

        [TestCase(0, 0, 1, 1)]
        [TestCase(5, 12, 3, 3)]
        [TestCase(12, 5, 3, 3)]
        [TestCase(12, 12, 3, 3)]
        [TestCase(5, 0, 3, 3)]
        public void RectanglesNotIntersection(int x, int y, int width, int height)
        {
            var r1 = new CollisionController.Rectangle(5, 5, 5, 5);
            var r2 = new CollisionController.Rectangle(x, y, width, height);
            Assert.AreEqual(false, CollisionController.IsIntersection(r1, r2));
        }

        // Magic: width = 5, height = 5, damage = 1
        // Enemy: width = 10, height = 10, health = 1
        [TestCase(0, 0, 1, 0)]
        public void MagicShouldIntersectEnemy(int magicPosX, int magicPosY, int enemyPosX, int enemyPosY)
        {
            var room = GetRoomPresetWithEnemyAndMagic(magicPosX, magicPosY, enemyPosX, enemyPosY);
            CollisionController.CheckGameObjectsForCollisions(room);
            room.Update();
            Assert.AreEqual(0, room.MagicInRoom.Count);
        }


        // Enemy: width = 10, height = 10, health = 1
        [Test]
        public void PlayerShouldGetDamageAfterCollisionWithEnemy()
        {
            var room = GetRoomPresetWithEnemyAndPlayer(0, 0, 1, 1);
            var playerHealthBeforeDamage = room.Player.CurrentHealth;
            CollisionController.CheckGameObjectsForCollisions(room);
            Assert.AreEqual(playerHealthBeforeDamage - room.AliveEnemiesInRoom[0].Damage, room.Player.CurrentHealth);
        }

        // Enemy: width = 10, height = 10
        // Player: width = 32, height = 56, speed = 10
        [Test]
        public void PlayerShouldGetDamageAfterHorizontalMovingAndCollisionWithEnemy()
        {
            var room = GetRoomPresetWithEnemyAndPlayer(0, 0, 54, 0);
            var playerHealthBeforeDamage = room.Player.CurrentHealth;
            room.Player.Speed = room.Player.HitboxWidth;
            room.Player.HorizontalMovement = MovementWeight.Positive;
            room.Player.Move();

            CollisionController.CheckGameObjectsForCollisions(room);
            Assert.AreEqual(playerHealthBeforeDamage - room.AliveEnemiesInRoom[0].Damage, room.Player.CurrentHealth);
        }

        private Arena GetRoomPresetWithEnemyAndMagic(int magicPosX, int magicPosY, int enemyPosX, int enemyPosY)
        {
            var player = new Player(1500, 900, roomWidth, roomHeight);
            var room = new Arena(roomWidth, roomHeight, player);
            var magic = new TestMagic(magicPosX, magicPosY, 10, 0, 1);
            var enemy = new TestEnemy(enemyPosX, enemyPosY);
            room.MagicInRoom.Add(magic);
            room.AliveEnemiesInRoom.Add(enemy);
            return room;
        }

        private Arena GetRoomPresetWithEnemyAndPlayer(int playerPosX, int playerPosY, int enemyPosX, int enemyPosY)
        {
            var player = new Player(playerPosX, playerPosY, roomWidth, roomHeight);
            var room = new Arena(roomWidth, roomHeight, player);
            var enemy = new TestEnemy(enemyPosX, enemyPosY);
            room.AliveEnemiesInRoom.Add(enemy);
            return room;
        }
    }
}