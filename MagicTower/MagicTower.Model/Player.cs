using System;

namespace MagicTower.Model
{
    public class Player
    {
        public int PosX { get; private set; }
        public int PosY { get; private set; }
        private Room currentRoom;

        public int CurrentHealth
        {
            get { return currentHealth; }
            private set
            {
                if (value > maxHealth)
                    currentHealth = maxHealth;
                currentHealth = value;
            }
        }

        public int Speed { get; private set; }

        private int maxHealth;
        private int currentHealth;

        public Player(int startPosX, int startPosY, int maxHealth, int speed, Room currentRoom)
        {
            PosX = startPosX;
            PosY = startPosY;
            this.maxHealth = maxHealth;
            CurrentHealth = maxHealth;
            Speed = speed;
            this.currentRoom = currentRoom;
        }

        public void Move(Direction direction)
        {
            if (direction == Direction.Right && CanGoTo(PosX + Speed, PosY))
                PosX += Speed;
            else if (direction == Direction.Left && CanGoTo(PosX - Speed, PosY))
                PosX -= Speed;
            else if (direction == Direction.Up && CanGoTo(PosX, PosY - Speed))
                PosY -= Speed;
            else if (direction == Direction.Down && CanGoTo(PosX, PosY + Speed))
                PosY += Speed;
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

        private bool CanGoTo(int x, int y)
        {
            if (x >= 0 && x <= currentRoom.Width && y >= 0 && y <= currentRoom.Height)
                return true;
            return false;
        }
    }
}