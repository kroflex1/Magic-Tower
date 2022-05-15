using System;
using MagicTower.Model.Magic;

namespace MagicTower.Model.EnemiesModels
{
    public abstract class Enemy : IGameObject
    {
        public int PosX { get; private set; }
        public int PosY { get; private set; }
        public int HitboxWidth { get; private set; }
        public int HitboxHeight { get; private set; }

        public int Speed
        {
            get => speed;
            set
            {
                if (value >= 0)
                    speed = value;
                else
                    throw new ArgumentException("Скорость не может быть меньше нуля");
            }
        }

        public int Damage
        {
            get => damage;
            set
            {
                if (value > 0)
                    damage = value;
                else
                    throw new ArgumentException("Урон не может быть меньше нуля");
            }
        }

        public Condition CurrentCondition { get; private set; }

        private int health;
        private int speed;
        private int damage;

        public Enemy(int posX, int posY, int hitboxWidth,
            int hitboxHeight, int health, int speed, int damage)
        {
            PosX = posX;
            PosY = posY;
            HitboxHeight = hitboxHeight;
            HitboxWidth = hitboxWidth;
            this.health = health;
            Speed = speed;
            Damage = damage;
            CurrentCondition = Condition.Alive;
        }

        public void MoveTo(int targetPosX, int targetPosY)
        {
            if (PosX != targetPosX || PosY != targetPosY)
            {
                var DirectionVectorToTarget = new Vector(PosX, PosY, targetPosX, targetPosY);
                DirectionVectorToTarget.SetLength(Speed);
                PosX += DirectionVectorToTarget.X;
                PosY += DirectionVectorToTarget.Y; 
            }
        }

        public void OnCollisionEnter(IGameObject gameObject)
        {
            if (gameObject is Magic.Magic)
            {
                Magic.Magic magic = gameObject as Magic.Magic;
                GetDamaged(magic.Damage);
            }
        }

        private void GetDamaged(int amountOfDamage)
        {
            health -= amountOfDamage;
            if (health <= 0)
                CurrentCondition = Condition.Destroyed;
        }
    }
}