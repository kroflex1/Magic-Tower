﻿using System;
using NUnit.Framework;

namespace MagicTower.Model.Tests
{
    [TestFixture]
    public class Tests
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
            var player = CreatePlayer( startX, startY);
            player.Move(firstDirection);
            player.Move(secondDirection);
            Assert.AreEqual((endX, endY), (player.PosX, player.PosY));
        }

        private Player CreatePlayer(int startPosX, int startPosY)
        {
            var room = new Room(10, 10);
            var player = new Player(startPosX, startPosY, 1, 1, room);
            return player;
        }
    }
}