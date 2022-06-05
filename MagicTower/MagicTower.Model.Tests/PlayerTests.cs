using System;
using System.Linq;
using MagicTower.Model.EnemiesModels;
using MagicTower.Model.MagicModels;
using NUnit.Framework;

namespace MagicTower.Model.Tests
{
    [TestFixture]
    public class PlayerTests
    {
        private const int roomWidth = 1920;
        private const int roomHeight = 1080;

        [TestCase(DirectionWeight.Positive, 1, 1, 2, 1)]
        [TestCase(DirectionWeight.Negative, 1, 1, 0, 1)]
        [TestCase(DirectionWeight.Neutral, 1, 1, 1, 1)]
        public void PlayerShouldGoHorizontalCorrect(DirectionWeight directionWeight, int startX, int startY, int endX,
            int endY)
        {
            var player = new Player(startX, startY, roomWidth, roomHeight);
            player.Speed = 1;
            player.HorizontalMoveDirection = directionWeight;
            player.Move();
            Assert.AreEqual((endX, endY), (player.PosX, player.PosY));
        }

        [TestCase(DirectionWeight.Positive, 1, 1, 1, 2)]
        [TestCase(DirectionWeight.Negative, 1, 1, 1, 0)]
        [TestCase(DirectionWeight.Neutral, 1, 1, 1, 1)]
        public void PlayerShouldGoVerticalCorrect(DirectionWeight directionWeight, int startX, int startY, int endX,
            int endY)
        {
            var player = new Player(startX, startY, roomWidth, roomHeight);
            player.Speed = 1;
            player.VerticalMoveDirection = directionWeight;
            player.Move();
            Assert.AreEqual((endX, endY), (player.PosX, player.PosY));
        }

        [TestCase(DirectionWeight.Positive, DirectionWeight.Positive, 1, 1, 2, 2)]
        [TestCase(DirectionWeight.Positive, DirectionWeight.Negative, 1, 1, 2, 0)]
        [TestCase(DirectionWeight.Negative, DirectionWeight.Positive, 1, 1, 0, 2)]
        [TestCase(DirectionWeight.Negative, DirectionWeight.Negative, 1, 1, 0, 0)]
        [TestCase(DirectionWeight.Neutral, DirectionWeight.Neutral, 1, 1, 1, 1)]
        public void PlayerShouldGoDiagonallyVCorrect(DirectionWeight horizontalDirectionWeight,
            DirectionWeight verticalDirectionWeight, int startX, int startY, int endX,
            int endY)
        {
            var player = new Player(startX, startY, roomWidth, roomHeight);
            player.Speed = 1;
            player.HorizontalMoveDirection = horizontalDirectionWeight;
            player.VerticalMoveDirection = verticalDirectionWeight;
            player.Move();
            Assert.AreEqual((endX, endY), (player.PosX, player.PosY));
        }


        [TestCase(DirectionWeight.Negative, DirectionWeight.Neutral, 0, 0)]
        [TestCase(DirectionWeight.Negative, DirectionWeight.Negative, 0, 0)]
        [TestCase(DirectionWeight.Positive, DirectionWeight.Neutral, roomWidth, 0)]
        [TestCase(DirectionWeight.Positive, DirectionWeight.Neutral, roomWidth, roomHeight)]
        [TestCase(DirectionWeight.Positive, DirectionWeight.Positive, roomWidth, 0)]
        [TestCase(DirectionWeight.Positive, DirectionWeight.Negative, roomWidth, roomHeight)]
        [TestCase(DirectionWeight.Neutral, DirectionWeight.Negative, 0, 0)]
        [TestCase(DirectionWeight.Negative, DirectionWeight.Negative, 0, 0)]
        [TestCase(DirectionWeight.Neutral, DirectionWeight.Positive, 0, roomHeight)]
        public void PlayerCantGoToBeyondBounds(DirectionWeight horizontalDirectionWeight,
            DirectionWeight verticalDirectionWeight, int startX, int startY)
        {
            var player = new Player(startX, startY, roomWidth, roomHeight);
            player.HorizontalMoveDirection = horizontalDirectionWeight;
            player.VerticalMoveDirection = verticalDirectionWeight;
            player.Move();
            Assert.AreEqual((startX, startY), (player.PosX, player.PosY));
        }
        

        [Test]
        public void PlayerCanLearnNewMagic()
        {
            var player = new Player(0, 0, roomWidth, roomHeight);
            var countLearnedMagic = player.LearnedMagic.Count;
            player.LearnNewMagic(typeof(TestMagic));
            Assert.AreEqual(countLearnedMagic + 1, player.LearnedMagic.Count);
        }

        
        [Test]
        public void LastLearnMagicShouldBeLastInList()
        {
            var player = new Player(0, 0, roomWidth, roomHeight);
            player.LearnNewMagic(typeof(TestMagic));
            Assert.AreEqual(player.LearnedMagic[player.LearnedMagic.Count - 1], typeof(TestMagic));
        }

        /*
        [Test]
        public void PlayerLosesManaWhenCreatedMagic()
        {
            var player = new Player(0, 0,  roomWidth, roomHeight);
            var room = new Arena(roomWidth, roomHeight, player);
            var iceBall = new IceBall(0, 0, 1, 1);
            player.ChangeCurrentMagic(1);
            player.CreateMagic(5, 5);
            Assert.AreEqual(player.MaxMana  - iceBall.ManaCost, player.CurrentMana);
        }

        [Test]
        public void PlayerLosesManaWhenCreateALotOfMagic()
        {
            var player = new Player(0, 0,  roomWidth, roomHeight);
            var room = new Arena(roomWidth, roomHeight, player);
            player.ChangeCurrentMagic(1);
            for (int i = 0; i < player.MaxMana; i++)
                player.CreateMagic(5, 5);
            
            Assert.AreEqual(0, player.CurrentMana);
        }*/

        [Test]
        public void CorrectGetDamage()
        {
            var demon = new Demon(0, 0);
            var player = new Player(0, 0, roomWidth, roomHeight);
            player.OnCollisionEnter(demon);
            Assert.AreEqual(player.MaxHealth - demon.Damage, player.CurrentHealth);
        }
    }
}