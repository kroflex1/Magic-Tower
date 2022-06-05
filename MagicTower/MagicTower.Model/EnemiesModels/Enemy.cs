using System;
using System.Collections.Generic;
using MagicTower.Model.Items;
using MagicTower.Model.Magic;

namespace MagicTower.Model.EnemiesModels
{
    public abstract class Enemy : IGameObject
    {
        public int PosX { get; private set; }
        public int PosY { get; private set; }
        public int HitboxWidth { get; private set; }
        public int HitboxHeight { get; private set; }
        public Condition CurrentCondition { get; private set; }

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

        public int Score { get; protected set; }

        public delegate void EnemySpawnHandler(Enemy magic);
        public abstract event EnemySpawnHandler CreateNewEnemy;
        public delegate void ItemHandler(Item item);
        public  event ItemHandler CreateNewItem;

        private List<Type> fallingItemsAfterDeath;
        private int health;
        private int speed;
        private int damage;
        private int playerPosX;
        private int playerPosY;

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
            SetFallingItemsAfterDeath();
        }

        public void Move()
        {
            if (PosX != playerPosX || PosY != playerPosY)
            {
                var directionVectorToTarget = new Vector(PosX, PosY, playerPosX, playerPosY);
                directionVectorToTarget.SetLength(Speed);
                PosX += directionVectorToTarget.X;
                PosY += directionVectorToTarget.Y;
            }
        }

        public void UpdatePlayerPosition(int playerPosX, int playerPosY)
        {
            this.playerPosX = playerPosX;
            this.playerPosY = playerPosY;
        }

        public void OnCollisionEnter(IGameObject gameObject)
        {
            if (gameObject is MagicModels.Magic)
            {
                MagicModels.Magic magic = gameObject as MagicModels.Magic;
                GetDamaged(magic.Damage);
            }
        }

        protected  virtual void Die()
        {
            var random = new Random();
            if (CreateNewItem != null)
            {
                var typeOfItem = fallingItemsAfterDeath[random.Next(0, fallingItemsAfterDeath.Count)];
                if(random.Next(0,10) <=2)
                    CreateNewItem((Item)Activator.CreateInstance(typeOfItem, PosX, PosY));
            }

            CurrentCondition = Condition.Destroyed;
        }

        private void GetDamaged(int amountOfDamage)
        {
            health -= amountOfDamage;
            if (health <= 0)
                Die();
        }

        private void SetFallingItemsAfterDeath()
        {
            fallingItemsAfterDeath = new List<Type>()
            {
                typeof(HealingPotion),
                typeof(ManaPotion)
            };
        }
    }
}