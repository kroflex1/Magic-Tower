using System;

namespace MagicTower.Model.Magic
{
    public abstract class Magic : IGameObject
    {
        public int PosX { get; private set; }
        public int PosY { get; private set; }
        public int HitboxWidth { get; }
        public int HitboxHeight { get; }
        public (int X, int Y) DirectionVector { get; set; }

        public int Speed
        {
            get => speed;
            set
            {
                if (value >= 0)
                    speed = value;
            }
        }

        public int Damage
        {
            get => speed;
            set
            {
                if (value >= 0)
                    damage = value;
            }
        }

        public readonly Room room;
        protected int speed;
        protected int damage;

        public Magic(int startX, int startY, int endX, int endY, Room currentRoom, int hitboxWidth,
            int hitboxHeight, int speed, int damage)
        {
            PosX = startX;
            PosY = startY;
            room = currentRoom;
            HitboxHeight = hitboxHeight;
            HitboxWidth = hitboxWidth;
            this.speed = speed;
            this.damage = damage;
            CalculateDirectionVector(startX, startY, endX, endY);
        }

        public void TakeStep()
        {
            PosX += DirectionVector.X;
            PosY += DirectionVector.Y;
            if (!room.InBounds(PosX, PosY))
                room.beyondBoundsMagic.Add(this);
        }


        public void OnCollisionEnter(IGameObject gameObject)
        {
            throw new System.NotImplementedException();
        }

        private void CalculateDirectionVector(double startX, double startY, double endX, double endY)
        {
            var vector = (endX - startX, endY - startY);
            var vectorLength = Math.Sqrt(vector.Item1 * vector.Item1 + vector.Item2 * vector.Item2);
            DirectionVector = ((int) Math.Round(speed * vector.Item1 / vectorLength),
                (int) Math.Round(speed * vector.Item2 / vectorLength));
        }
    }
}