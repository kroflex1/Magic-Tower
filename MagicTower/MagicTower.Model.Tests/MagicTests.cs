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
        public void DirectionVectorFromZeroPointWithSpeedOne(int endX, int endY)
        {
            var magic = CreateTestMagic(0, 0, endX, endY, 1);
            Assert.AreEqual((endX, endY), (magic.DirectionVector.X, magic.DirectionVector.Y));
        }

        [TestCase(1, 1, 1, 1)]
        [TestCase(-1, 1, -1, 1)]
        [TestCase(-1, -1, -1, -1)]
        [TestCase(1, -1, 1, -1)]
        public void DiagonalDirectionVectorFromZeroPointWithSpeedOne(int endX, int endY, int directionX,
            int directionY)
        {
            var magic = CreateTestMagic(0, 0, endX, endY, 1);
            Assert.AreEqual(directionX, magic.DirectionVector.X, 1e10);
            Assert.AreEqual(directionY, magic.DirectionVector.Y, 1e10);
        }


        [TestCase(1, 1, 10, 1, 1, 0)]
        [TestCase(-2, -2, -2, 5, 0, 1)]
        [TestCase(-5, 3, -8, 3, -1, 0)]
        [TestCase(0, 5, 0, -5, 0, -1)]
        public void DirectionVectorFromNotZeroPointWithSpeedOne(int startX, int startY, int endX, int endY,
            int directionX, int directionY)
        {
            var magic = CreateTestMagic(startX, startY, endX, endY, 1);
            Assert.AreEqual((directionX, directionY), (magic.DirectionVector.X,magic.DirectionVector.Y));
        }

        [TestCase(2, 2, 8, 8, 1, 1)]
        [TestCase(2, 2, 0, 0, -1, -1)]
        [TestCase(2, 2, 0, 4, -1, 1)]
        [TestCase(2, 2, 4, 0, 1, -1)]
        public void DiagonalDirectionVectorFromNotZeroPointWithSpeedOne(int startX, int startY, int endX, int endY,
            int directionX, int directionY)
        {
            var magic = CreateTestMagic(startX, startY, endX, endY, 1);
            Assert.AreEqual(directionX, magic.DirectionVector.X, 1e10);
            Assert.AreEqual(directionY, magic.DirectionVector.Y, 1e10);
        }

        [TestCase(1, 0)]
        [TestCase(0, 1)]
        [TestCase(-1, 0)]
        [TestCase(0, -1)]
        public void DoHorizontalOrVerticalStepWithSpeedOne(int endX, int endY)
        {
            var magic = CreateTestMagic(0, 0, endX, endY, 1);
            magic.TakeStep();
            Assert.AreEqual(endX, magic.PosX);
            Assert.AreEqual(endY, magic.PosY);
        }

        [TestCase(1, 0, 2)]
        [TestCase(0, 1, 2)]
        [TestCase(-1, 0, 2)]
        [TestCase(0, -1, 2)]
        public void DoHorizontalOrVerticalStepsWithSpeedOne(int endX, int endY, int amountSteps)
        {
            var magic = CreateTestMagic(0, 0, endX, endY, 1);
            for (int i = 0; i < amountSteps; i++)
                magic.TakeStep();
            Assert.AreEqual(endX * amountSteps, magic.PosX);
            Assert.AreEqual(endY * amountSteps, magic.PosY);
        }

        [TestCase(1, 0, 5)]
        [TestCase(0, 1, 10)]
        [TestCase(-1, 0, 4)]
        [TestCase(0, -1, 5)]
        public void DoHorizontalOrVerticalStepWithDifferentSpeeds(int endX, int endY, int speed)
        {
            var magic = CreateTestMagic(0, 0, endX, endY, speed);
            magic.TakeStep();
            Assert.AreEqual(endX * speed, magic.PosX);
            Assert.AreEqual(endY * speed, magic.PosY);
        }


        private TestMagic CreateTestMagic(int startX, int startY, int endX, int endY, int speed)
        {
            return new TestMagic(startX, startY, endX, endY, speed);
        }
    }
}