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

        private int maxHealth;
        private int currentHealth;
        private int speed;
        private Room currentRoom;
        private List<MagicType> learnedMagic;
        private MagicType currentMagic;


        public Player(int startPosX, int startPosY, Room currentRoom) : this(startPosX, startPosY, currentRoom, 10, 10)
        {
        }


        public Player(int startPosX, int startPosY, Room currentRoom, int maxHealth, int speed)
        {
            PosX = startPosX;
            PosY = startPosY;
            this.currentRoom = currentRoom;
            this.maxHealth = maxHealth;
            CurrentHealth = maxHealth;
            Speed = speed;
        }

        public void OnCollisionEnter(IGameObject gameObject)
        {
            if (gameObject.GetType() == typeof(Enemy))
            {
                Enemy enemy = gameObject as Enemy;
                GetDamaged(enemy.Damage);
            }
        }

        public void Move(Direction direction)
        {
            if (direction == Direction.Right && currentRoom.InBounds(PosX + Speed, PosY))
                PosX += Speed;
            else if (direction == Direction.Left && currentRoom.InBounds(PosX - Speed, PosY))
                PosX -= Speed;
            else if (direction == Direction.Up && currentRoom.InBounds(PosX, PosY - speed))
                PosY -= Speed;
            else if (direction == Direction.Down && currentRoom.InBounds(PosX, PosY + speed))
                PosY += Speed;
        }

        public void AttackTo(int targetX, int targetY)
        {
            var fireBall = new FireBall(PosX, PosY, targetX, targetY, currentRoom);
            currentRoom.MagicInRoom.Add(fireBall);
        }

        public void LearnNewMagic(MagicType newMagicType)
        {
            if(!learnedMagic.Contains(newMagicType))
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

        public bool IsPlayerAlive()
        {
            return CurrentHealth > 0;
        }
    }
}