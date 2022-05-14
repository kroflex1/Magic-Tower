using System;
using System.Collections.Generic;
using System.Linq;
using MagicTower.Model.EnemiesModels;
using MagicTower.Model.Magic;

namespace MagicTower.Model
{
    public class Player : IGameObject
    {
        public int PosX { get; private set; }
        public int PosY { get; private set; }
        public int HitboxWidth { get; }
        public int HitboxHeight { get; }

        public int CurrentHealth
        {
            get { return currentHealth; }
            private set
            {
                if (value > 0)
                    currentHealth = maxHealth;
                currentHealth = value;
            }
        }

        public int Speed
        {
            get => speed;
            set
            {
                if (value > 0)
                    speed = value;
            }
        }

        public MovementWeight HorizontalMovement;
        public MovementWeight VerticalMovement;
        private int maxHealth;
        private int currentHealth;
        private int speed;
        private List<MagicType> learnedMagic;
        private MagicType currentMagic;
        private int windowWidth;
        private int windowHeight;


        public Player(int startPosX, int startPosY, int windowWidth, int windowHeight) : this(startPosX, startPosY, 32,
            56, 10, 10, windowWidth, windowHeight)
        {
        }


        public Player(int startPosX, int startPosY, int hitboxWidth, int hitboxHeight, int maxHealth,
            int speed, int windowWidth, int windowHeight)
        {
            PosX = startPosX;
            PosY = startPosY;

            HitboxWidth = hitboxWidth;
            HitboxHeight = hitboxHeight;

            this.maxHealth = maxHealth;
            CurrentHealth = maxHealth;

            Speed = speed;
            HorizontalMovement = MovementWeight.Neutral;
            VerticalMovement = MovementWeight.Neutral;

            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
        }

        public void OnCollisionEnter(IGameObject gameObject)
        {
            if (gameObject.GetType() == typeof(Enemy))
            {
                Enemy enemy = gameObject as Enemy;
                GetDamaged(enemy.Damage);
                
            }
        }

        public void Move()
        {
            if (InBounds(PosX + Speed * (int) HorizontalMovement, PosY + Speed * (int) VerticalMovement))
            {
                PosX += Speed * (int) HorizontalMovement;
                PosY += Speed * (int) VerticalMovement;
            }
        }

        // public void AttackTo(int targetX, int targetY)
        // {
        //     var fireBall = new FireBall(PosX, PosY, targetX, targetY, currentRoom);
        //     currentRoom.MagicInRoom.Add(fireBall);
        // }

        public void LearnNewMagic(MagicType newMagicType)
        {
            if (!learnedMagic.Contains(newMagicType))
                learnedMagic.Add(newMagicType);
        }

        public void ChangeCurrentMagic(int magicId)
        {
            currentMagic = learnedMagic[magicId - 1];
        }

        public void Heal(int amountOfHealth)
        {
            if (amountOfHealth < 0)
                throw new ArgumentException("Прибавляемое здоровье не может быть меньше нуля");
            CurrentHealth += amountOfHealth;
        }

        private void GetDamaged(int amountOfDamage)
        {
            if (amountOfDamage < 0)
                throw new ArgumentException("Наносимый урон не может быть меньше нуля");
            CurrentHealth -= amountOfDamage;
        }

        private void FindReboundTrajectory(int enemyPosX, int enemyPosY)
        {
            
        }

        private bool InBounds(int x, int y)
        {
            if (x >= 0 && x <= (windowWidth - HitboxWidth) && y >= 0 && y <= (windowHeight - HitboxWidth))
                return true;
            return false;
        }
    }
}