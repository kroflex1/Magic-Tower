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
        public void CorrectSimpleDirectionVectorFromZeroPoint(double endX, double endY)
        {
            var magic = new FireBall(0, 0, endX, endY, 1);
            Assert.AreEqual((endX, endY), magic.DirectionVector);
        }

        [TestCase(1, 1, 0.7, 0.7)]
        [TestCase(-1, 1, -0.7, 0.7)]
        [TestCase(-1, -1, -0.7, -0.7)]
        [TestCase(1, -1, 0.7, -0.7)]
        public void CorrectDiagonalDirectionVectorFromZeroPoint(double endX, double endY, double directionX,
            double directionY)
        {
            var magic = new FireBall(0, 0, endX, endY, 1);
            Assert.AreEqual(directionX, magic.DirectionVector.X, 1e10);
        }


        [TestCase(1, 0)]
        [TestCase(0, 1)]
        [TestCase(-1, 0)]
        [TestCase(0, -1)]
        public void CorrectDirectionVectorFromZeroPoint(double endX, double endY)
        {
            var magic = new FireBall(0, 0, endX, endY, 1);
            Assert.AreEqual((endX, endY), magic.DirectionVector);
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