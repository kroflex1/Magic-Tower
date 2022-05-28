using System;

namespace MagicTower.Model.Tests
{
    public class TestMagic : MagicModels.Magic
    {
        public TestMagic(int startX, int startY, int endX, int endY, int speed) : base(startX,
            startY, endX, endY, 5, 5, speed, 1)
        {
        }
    }
}