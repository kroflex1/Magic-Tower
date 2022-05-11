using System;
using MagicTower.Model.EnemiesModels;

namespace MagicTower.Model.Magic
{
    public abstract class Magic : IGameObject
    {
        public int PosX { get; private set; }
        public int PosY { get; private set; }
        public int HitboxWidth { get; }
        public int HitboxHeight { get; }
        public (int X, int Y) DirectionVector { get; private set; }

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
            get => damage;
            set
            {
                if (value >= 0)
                    damage = value;
            }
        }


        public Condition CurrentCondition { get; private set; }
        protected int speed;
        protected int damage;

        public Magic(int startX, int startY, int endX, int endY,  int hitboxWidth,
            int hitboxHeight, int speed, int damage)
        {
            PosX = startX;
            PosY = startY;
            HitboxHeight = hitboxHeight;
            HitboxWidth = hitboxWidth;
            Speed = speed;
            Damage= damage;
            CurrentCondition = Condition.Alive;
            CalculateDirectionVector(startX, startY, endX, endY);
        }

        public void TakeStep()
        {
            PosX += DirectionVector.X;
            PosY += DirectionVector.Y;
        }


        public void OnCollisionEnter(IGameObject gameObject)
        {
            if (gameObject is Enemy)
                CurrentCondition = Condition.Destroyed;
        }

        private void CalculateDirectionVector(double startX, double startY, double endX, double endY)
        {
            var vector = (endX - startX, endY - startY);
            var vectorLength = Math.Sqrt(vector.Item1 * vector.Item1 + vector.Item2 * vector.Item2);
            DirectionVector = ((int) Math.Round(Speed * vector.Item1 / vectorLength),
                (int) Math.Round(Speed * vector.Item2 / vectorLength));
        }
    }
}