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
        public double PosX { get; private set; }
        public double PosY { get; private set; }

        public int CurrentHealth
        {
            get => CurrentHealth;
            private set
            {
                if (value + CurrentHealth <= maxHealth)
                    CurrentHealth += value;
                CurrentHealth = maxHealth;
            }
        }

        public double Speed { get; private set; }

        private int maxHealth;

        public Player(int maxHealth, double speed)
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
                    PosY += Speed;
                    break;
                case Directions.Down:
                    PosY += Speed;
                    break;
            }
        }

        public void ChangePosition(double x, double y)
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