using System;

namespace MagicTower.Model.Magic
{
    public class FireBall : Magic
    {
        public FireBall(int startX, int startY, int endX, int endY) : base(startX, startY,
            endX, endY, 10, 10, 5, 1)
        {
        }
    }
}