using System;

namespace MagicTower.Model
{
    public enum Directions
    {
        Up,
        Down,
        Left,
        Right
    }

    public class Player
    {
        public int PosX { get; private set; }
        public int PosY { get; private set; }

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

        public Player(int maxHealth, int speed)
        {
            this.maxHealth = maxHealth;
            CurrentHealth = maxHealth;
            Speed = speed;
        }

        public void Move(Directions direction)
        {
            switch (direction)
            {
                case Directions.Right:
                    PosX += Speed;
                    break;
                case Directions.Left:
                    PosX -= Speed;
                    break;
                case Directions.Up:
                    PosY -= Speed;
                    break;
                case Directions.Down:
                    PosY += Speed;
                    break;
            }
        }

        public void ChangePosition(int x, int y)
        {
            PosX = x;
            PosY = y;
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