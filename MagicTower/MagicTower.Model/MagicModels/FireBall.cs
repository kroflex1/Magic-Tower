using System;

namespace MagicTower.Model.Magic
{
    public class FireBall : Magic
    {
        
        public FireBall(int startX, int startY, int endX, int endY, Room currentCurrentRoom) : base(startX, startY,
            endX, endY, currentCurrentRoom, 10, 10, 5, 10)
        {
        }
    }
}