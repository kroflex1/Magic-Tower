using System;
using MagicTower.Model;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        [TestCase(Direction.Right, 1, 1, 2, 1)]
        [TestCase(Direction.Left, 1, 1, 0, 1)]
        [TestCase(Direction.Up, 1, 1, 1, 0)]
        [TestCase(Direction.Down, 1, 1, 1, 2)]
        public void PlayerShuoldGoToDirection(Direction direction, int startX, int startY, int endX, int endY)
        {
            var player = CreatePlayerAndMoveTo(direction, startX, startY);
            Assert.AreEqual((endX, endY), (player.PosX, player.PosY));
        }
        
        private PlayerModel CreatePlayerAndMoveTo(Direction direction, int startPosX, int startPosY)
        {
            var room = new Room(10, 10);
            var player = new PlayerModel(startPosX, startPosY ,100, 1, room);
            player.Move(direction);
            return player;
        }
    }
}