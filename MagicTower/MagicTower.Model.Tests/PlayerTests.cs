﻿using System;
using MagicTower.Model.EnemiesModels;
using NUnit.Framework;

namespace MagicTower.Model.Tests
{
    [TestFixture]
    public class PlayerTests
    {
        private const int roomWidth = 1920;
        private const int roomHeight = 1080;

        [TestCase(MovementWeight.Positive, 1, 1, 2, 1)]
        [TestCase(MovementWeight.Negative, 1, 1, 0, 1)]
        [TestCase(MovementWeight.Neutral, 1, 1, 1, 1)]
        public void PlayerShouldGoHorizontalCorrect(MovementWeight movementWeight, int startX, int startY, int endX,
            int endY)
        {
            var player = new Player(startX, startY, roomWidth, roomHeight);
            player.Speed = 1;
            player.HorizontalMovement = movementWeight;
            player.Move();
            Assert.AreEqual((endX, endY), (player.PosX, player.PosY));
        }

        [TestCase(MovementWeight.Positive, 1, 1, 1, 2)]
        [TestCase(MovementWeight.Negative, 1, 1, 1, 0)]
        [TestCase(MovementWeight.Neutral, 1, 1, 1, 1)]
        public void PlayerShouldGoVerticalCorrect(MovementWeight movementWeight, int startX, int startY, int endX,
            int endY)
        {
            var player = new Player(startX, startY, roomWidth, roomHeight);
            player.Speed = 1;
            player.VerticalMovement = movementWeight;
            player.Move();
            Assert.AreEqual((endX, endY), (player.PosX, player.PosY));
        }

        [TestCase(MovementWeight.Positive, MovementWeight.Positive, 1, 1, 2, 2)]
        [TestCase(MovementWeight.Positive, MovementWeight.Negative, 1, 1, 2, 0)]
        [TestCase(MovementWeight.Negative, MovementWeight.Positive, 1, 1, 0, 2)]
        [TestCase(MovementWeight.Negative, MovementWeight.Negative, 1, 1, 0, 0)]
        [TestCase(MovementWeight.Neutral, MovementWeight.Neutral, 1, 1, 1, 1)]
        public void PlayerShouldGoDiagonallyVCorrect(MovementWeight horizontalMovementWeight,
            MovementWeight verticalMovementWeight, int startX, int startY, int endX,
            int endY)
        {
            var player = new Player(startX, startY, roomWidth, roomHeight);
            player.Speed = 1;
            player.HorizontalMovement = horizontalMovementWeight;
            player.VerticalMovement = verticalMovementWeight;
            player.Move();
            Assert.AreEqual((endX, endY), (player.PosX, player.PosY));
        }


        [TestCase(MovementWeight.Negative, MovementWeight.Neutral, 0, 0)]
        [TestCase(MovementWeight.Negative, MovementWeight.Negative, 0, 0)]
        [TestCase(MovementWeight.Positive, MovementWeight.Neutral, roomWidth, 0)]
        [TestCase(MovementWeight.Positive, MovementWeight.Neutral, roomWidth, roomHeight)]
        [TestCase(MovementWeight.Positive, MovementWeight.Positive, roomWidth, 0)]
        [TestCase(MovementWeight.Positive, MovementWeight.Negative, roomWidth, roomHeight)]
        [TestCase(MovementWeight.Neutral, MovementWeight.Negative, 0, 0)]
        [TestCase(MovementWeight.Negative, MovementWeight.Negative, 0, 0)]
        [TestCase(MovementWeight.Neutral, MovementWeight.Positive, 0, roomHeight)]
        
        public void PlayerCantGoToBeyondBounds(MovementWeight horizontalMovementWeight,
            MovementWeight verticalMovementWeight, int startX, int startY)
        {
            var player = new Player(startX, startY, roomWidth, roomHeight);
            player.HorizontalMovement = horizontalMovementWeight;
            player.VerticalMovement = verticalMovementWeight;
            player.Move();
            Assert.AreEqual((startX, startY), (player.PosX, player.PosY));
        }

        [TestCase()]
        public void CorrectGetDamage()
        {
            var demon = new Demon(0,0); //damage = 
            var player = new Player(0, 0, roomWidth, roomHeight);
            player.OnCollisionEnter(demon);
            Assert.AreEqual(player.CurrentHealth - demon.Damage, player.CurrentHealth);
        }
    }
}