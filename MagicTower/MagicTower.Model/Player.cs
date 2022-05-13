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
        private Room currentRoom;
        private int maxHealth;
        private int currentHealth;
        private int speed;
        private List<MagicType> learnedMagic;
        private MagicType currentMagic;


        public Player(int startPosX, int startPosY, int windowWidth, int windowHeight) : this(startPosX, startPosY, 32, 56,
            10, 10, windowWidth,  windowHeight)
        {
        }


        public Player(int startPosX, int startPosY, int hitboxWidth, int hitboxHeight, int maxHealth,
            int speed, int windowHeight, int windowSize)
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
        }

        public void GoToNewRoom(Room room)
        {
            
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
            PosX += Speed * (int) HorizontalMovement;
            PosY += Speed * (int) VerticalMovement;
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

        public void GetDamaged(int amountOfDamage)
        {
            if (amountOfDamage < 0)
                throw new ArgumentException("Наносимый урон не может быть меньше нуля");
            CurrentHealth -= amountOfDamage;
        }
        
    }
}