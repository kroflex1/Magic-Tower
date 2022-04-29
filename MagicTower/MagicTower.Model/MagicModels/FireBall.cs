using System;

namespace MagicTower.Model.Magic
{
    public class FireBall : Magic
    {
        
        public FireBall(int startX, int startY, int endX, int endY, Room currentRoom) : base(startX, startY,
            endX, endY, currentRoom, 10, 10, 5, 10)
        {
        }
    }
}