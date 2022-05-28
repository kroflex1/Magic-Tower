using NUnit.Framework;

namespace MagicTower.Model.Tests
{
    [TestFixture]
    public class VectorTests
    {
        [TestCase(1, 0)]
        [TestCase(0, 1)]
        [TestCase(-1, 0)]
        [TestCase(0, -1)]
        [TestCase(1, 1)]
        [TestCase(-1, 0)]
        [TestCase(-1, -1)]
        [TestCase(1, -1)]
        [TestCase(2, 3)]
        [TestCase(10, 1)]
        [TestCase(-15, -3)]
        [TestCase(4, 2)]
        public void CreateSimpleVectors(int endX, int endY)
        {
            var vector = new Vector(0, 0, endX, endY);
            Assert.AreEqual((endX, endY), (vector.X, vector.Y));
        }

        [TestCase(4, 5, 5, 5, 1, 0)]
        [TestCase(-3, -7, -3, -6, 0, 1)]
        [TestCase(8, 6, 7, 6, -1, 0)]
        [TestCase(12, 15, 12, 14, 0, -1)]
        public void CreateSimpleVectorsFromNotZeroPoint(int startX, int startY, int endX, int endY, int shouldX,
            int shouldY)
        {
            var vector = new Vector(0, 0, endX, endY);
            Assert.AreEqual((endX, endY), (vector.X, vector.Y));
        }

        [TestCase(4, 5, 6, 8, 2, 3)]
        [TestCase(-3, -7, -9, 4, -6, 11)]
        [TestCase(8, 6, 18, 8, 10, 2)]
        [TestCase(12, 15, 15, 7, 3, -8)]
        [TestCase(-54, -20, -44, -18, 10, 2)]
        [TestCase(-34, 13, -31, 5, 3, -8)]
        [TestCase(34, 12, 44, 14, 10, 2)]
        [TestCase(-5, 12, -2, 4, 3, -8)]
        public void CreateNotSimpleVectors(int startX, int startY, int endX, int endY, int exceptedX, int exceptedY)
        {
            var vector = new Vector(startX, startY, endX, endY);
            Assert.AreEqual((exceptedX, exceptedY), (vector.X, vector.Y));
        }

        [TestCase(5, 0, 1, 0)]
        [TestCase(0, 7, 0, 1)]
        [TestCase(-6, 0, -1, 0)]
        [TestCase(0, -17,0,-1)]
        [TestCase(1, 1, 1, 1)]
        [TestCase(2, 2, 1, 1)]
        [TestCase(2, 4, 0, 1)]
        public void GetUnitVector(int endX, int endY, int exceptedX, int exceptedY)
        {
            var vector = new Vector(endX, endY);
            vector.BecomeUnitVector();
            Assert.AreEqual((exceptedX, exceptedY), (vector.X, vector.Y));
        }

        [TestCase(5, 0, 1, 1, 0)]
        [TestCase(0, 4, 1, 0, 1)]
        [TestCase(-8, 0, 1, -1, 0)]
        [TestCase(0, -7, 1, 0, -1)]
        public void CreateSimpleVectorWithSpecifiedLength(int endX, int endY, int length, int exceptedX, int exceptedY)
        {
            var vector = new Vector(0, 0, endX, endY);
            vector.SetLength(length);
            Assert.AreEqual((exceptedX, exceptedY), (vector.X, vector.Y));
        }

        [Test]
        public void CreateZeroVactor()
        {
            var vector = new Vector(0, 0, 0, 0);
            Assert.AreEqual((0, 0), (vector.X, vector.Y));
        }
    }
}