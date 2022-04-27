using System;
using System.CodeDom;
using MagicTower.Model.Magic;
using NUnit.Framework;

namespace MagicTower.Model.Tests
{
    [TestFixture]
    public class MagicTests
    {
        [TestCase(1, 0)]
        [TestCase(0, 1)]
        [TestCase(-1, 0)]
        [TestCase(0, -1)]
        public void SimpleDirectionVectorFromZeroPoint(double endX, double endY)
        {
            var magic = new FireBall(0, 0, endX, endY, 1);
            Assert.AreEqual((endX, endY), magic.DirectionVector);
        }

        [TestCase(1, 1, 0.7, 0.7)]
        [TestCase(-1, 1, -0.7, 0.7)]
        [TestCase(-1, -1, -0.7, -0.7)]
        [TestCase(1, -1, 0.7, -0.7)]
        public void DiagonalDirectionVectorFromZeroPoint(double endX, double endY, double directionX,
            double directionY)
        {
            var magic = new FireBall(0, 0, endX, endY, 1);
            Assert.AreEqual(directionX, magic.DirectionVector.X, 1e10);
            Assert.AreEqual(directionY, magic.DirectionVector.Y, 1e10);
        }


        [TestCase(1, 1, 10, 1, 1, 0)]
        [TestCase(-2, -2, -2, 5, 0, 1)]
        [TestCase(-5, 3, -8, 3, -1, 0)]
        [TestCase(0, 5, 0, -5, 0, -1)]
        public void SimpleDirectionVectorFromNotZeroPoint(double startX, double startY, double endX, double endY,
            double directionX, double directionY)
        {
            var magic = new FireBall(startX, startY, endX, endY, 1);
            Assert.AreEqual((directionX, directionY), magic.DirectionVector);
        }

        [TestCase(2, 2, 8, 8, 0.7, 0.7)]
        [TestCase(2, 2, 0, 0, -0.7, -0.7)]
        [TestCase(2, 2, 0, 4, -0.7, 0.7)]
        [TestCase(2, 2, 4, 0, 0.7, -0.7)]
        public void DiagonalDirectionVectorFromNotZeroPoint(double startX, double startY, double endX, double endY,
            double directionX, double directionY)
        {
            var magic = new FireBall(startX, startY, endX, endY, 1);
            Assert.AreEqual(directionX, magic.DirectionVector.X, 1e10);
            Assert.AreEqual(directionY, magic.DirectionVector.Y, 1e10);
        }

        [TestCase(1, 0)]
        [TestCase(0, 1)]
        [TestCase(-1, 0)]
        [TestCase(0, -1)]
        public void DoHorizontalOrVerticalStep(int endX, int endY)
        {
            var magic = new FireBall(0, 0, endX, endY, 1);
            magic.TakeStepInDirection();
            Assert.AreEqual(endX, magic.PosX);
            Assert.AreEqual(endY, magic.PosY);
        }
    }
}