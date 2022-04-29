using System;

namespace MagicTower.Model.Tests
{
    public class TestMagic : Magic.Magic
    {
        public TestMagic(int startX, int startY, int endX, int endY, Room currentRoom, int speed) : base(startX,
            startY, endX, endY, currentRoom, 5, 5, speed, 1)
        {
        }
    }
}