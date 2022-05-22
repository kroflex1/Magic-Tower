using System;
using MagicTower.Model.EnemiesModels;

namespace MagicTower.Model.Magic
{
    public abstract class Magic : IGameObject
    {
        public int PosX { get; protected set; }
        public int PosY { get; protected set; }
        public int HitboxWidth { get; }
        public int HitboxHeight { get; }
        public Vector DirectionVector { get; protected set; }

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

        public virtual void TakeStep()
        {
            PosX += DirectionVector.X;
            PosY += DirectionVector.Y;
        }


        public virtual void OnCollisionEnter(IGameObject gameObject)
        {
            if (gameObject is Enemy)
                CurrentCondition = Condition.Destroyed;
        }

        private void CalculateDirectionVector(int startX, int startY, int endX, int endY)
        {
            DirectionVector = new Vector(startX, startY, endX, endY);
            DirectionVector.SetLength(Speed);
        }
    }
}