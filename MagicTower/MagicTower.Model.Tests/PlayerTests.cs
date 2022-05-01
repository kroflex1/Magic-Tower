using System;
using NUnit.Framework;

namespace MagicTower.Model.Tests
{
    [TestFixture]
    public class PlayerTests
    {
        [TestCase(Direction.Right, 1, 1, 2, 1)]
        [TestCase(Direction.Left, 1, 1, 0, 1)]
        [TestCase(Direction.Up, 1, 1, 1, 0)]
        [TestCase(Direction.Down, 1, 1, 1, 2)]
        public void PlayerShouldGoToDirection(Direction direction, int startX, int startY, int endX, int endY)
        {
            var player = CreatePlayer(startX, startY);
            player.Move(direction);
            Assert.AreEqual((endX, endY), (player.PosX, player.PosY));
        }

        [TestCase(Direction.Right, Direction.Up, 1, 1, 2, 0)]
        [TestCase(Direction.Right, Direction.Down, 1, 1, 2, 2)]
        [TestCase(Direction.Left, Direction.Up, 1, 1, 0, 0)]
        [TestCase(Direction.Left, Direction.Down, 1, 1, 0, 2)]
        public void PlayerShoulGoToDiagonal(Direction firstDirection, Direction secondDirection, int startX, int startY,
            int endX, int endY)
        {
            var player = CreatePlayer(startX, startY);
            player.Move(firstDirection);
            player.Move(secondDirection);
            Assert.AreEqual((endX, endY), (player.PosX, player.PosY));
        }

        [TestCase(Direction.Right, 3, 0)]
        [TestCase(Direction.Left, 0, 0)]
        [TestCase(Direction.Up, 0, 0)]
        [TestCase(Direction.Down, 0, 3)]
        public void PlayerCantGoToBeyondBounds(Direction direction, int startX, int startY)
        {
            var player = CreatePlayer(startX, startY);
            player.Move(direction);
            Assert.AreEqual((startX, startY), (player.PosX, player.PosY));
        }
        
        private Player CreatePlayer(int startPosX, int startPosY)
        {
            var room = new Room(3, 3);
            var player = new Player(startPosX, startPosY, room, 1, 1);
            return player;
        }
    }
}