using System;
using System.Collections.Generic;
using MagicTower.Model.EnemiesModels;
using MagicTower.Model.Items;
using MagicTower.Model.Magic;
using MagicTower.Model.MagicModels;

namespace MagicTower.Model
{
    public class Player : IGameObject
    {
        public int PosX { get; private set; }
        public int PosY { get; private set; }
        public int HitboxWidth { get; }
        public int HitboxHeight { get; }
        public Condition CurrentCondition { get; private set; }

        public int CurrentHealth
        {
            get => currentHealth;
            private set => currentHealth = value;
        }

        public int CurrentMana
        {
            get => currentMana;
            private set
            {
                if (value >= 0)
                    currentMana = value;
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

        public bool IsAlive
        {
            get => CurrentHealth > 0;
        }

        public int CurrentRateOfFire { get; private set; }

        public DirectionWeight HorizontalMoveDirection;
        public DirectionWeight VerticalMoveDirection;
        public DirectionWeight HorizontalAttackDirection;
        public DirectionWeight VerticalAttackDirection;
        public int MaxHealth { get; private set; }
        public int MaxMana { get; private set; }
        public List<Type> LearnedMagic { get; private set; }
        public delegate void PosHandler(int playerPosX, int playerPosY);
        public event PosHandler OnChangePosition;
        
        
        private int currentHealth;
        private int currentMana;
        private int speed;
        private Type currentMagic;
        private int magicDamageBonus;
        private readonly int windowWidth;
        private readonly int windowHeight;

        public Player(int startPosX, int startPosY, int windowWidth, int windowHeight) : this(startPosX, startPosY, 42,
            58, 14, 10, 15, windowWidth, windowHeight)
        {
        }

        public Player(int startPosX, int startPosY, int hitboxWidth, int hitboxHeight, int maxHealth, int maxMana,
            int speed, int windowWidth, int windowHeight)
        {
            PosX = startPosX;
            PosY = startPosY;

            HitboxWidth = hitboxWidth;
            HitboxHeight = hitboxHeight;

            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;

            MaxMana = maxMana;
            currentMana = maxMana;

            Speed = speed;
            HorizontalMoveDirection = DirectionWeight.Neutral;
            VerticalMoveDirection = DirectionWeight.Neutral;

            HorizontalAttackDirection = DirectionWeight.Neutral;
            VerticalAttackDirection = DirectionWeight.Neutral;

            SetStartLearnedMagic();
            currentMagic = LearnedMagic[0];
            magicDamageBonus = 0;
            UpdateRateOfFire();

            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
        }

        public void OnCollisionEnter(IGameObject gameObject)
        {
            if (gameObject is Enemy)
            {
                Enemy enemy = gameObject as Enemy;
                GetDamaged(enemy.Damage);
            }
        }

        public void Move()
        {
            if (InBounds(PosX + Speed * (int) HorizontalMoveDirection, PosY + Speed * (int) VerticalMoveDirection))
            {
                PosX += Speed * (int) HorizontalMoveDirection;
                PosY += Speed * (int) VerticalMoveDirection;
            }

            if (OnChangePosition != null)
                OnChangePosition(PosX, PosY);
        }

        public MagicModels.Magic CreateMagic()
        {
            if (VerticalAttackDirection == DirectionWeight.Neutral &&
                HorizontalAttackDirection == DirectionWeight.Neutral)
                return null;

            var newMagic = (MagicModels.Magic) Activator.CreateInstance(currentMagic, PosX, PosY,
                PosX + HorizontalAttackDirection, PosY + VerticalAttackDirection);
            if (CurrentMana - newMagic.ManaCost >= 0)
            {
                newMagic.Damage += magicDamageBonus;
                CurrentMana -= newMagic.ManaCost;
                return newMagic;
            }

            return null;
        }

        public void LearnNewMagic(Type newMagicType)
        {
            if (!LearnedMagic.Contains(newMagicType))
                LearnedMagic.Add(newMagicType);
        }

        public void ChangeCurrentMagic(int magicId)
        {
            if (magicId >= 0 && magicId < LearnedMagic.Count)
            {
                currentMagic = LearnedMagic[magicId];
                UpdateRateOfFire();
            }
        }

        public void Heal(int amountOfHealth)
        {
            if (amountOfHealth < 0)
                throw new ArgumentException("Прибавляемое здоровье не может быть меньше нуля");
            CurrentHealth += amountOfHealth;
        }

        public void RestoreMana(int amountOfMana)
        {
            if (amountOfMana < 0)
                throw new ArgumentException("Прибавляемая мана не может быть меньше нуля");
            CurrentMana += amountOfMana;
        }

        public void IncreaseMagicDamage(int plusDamage)
        {
            if (magicDamageBonus + plusDamage >= 5)
                magicDamageBonus = 5;
            magicDamageBonus += plusDamage;
        }

        public void IncreaseMaxHealth(int amountHP)
        {
            if (MaxHealth + amountHP > 28)
                MaxHealth = 28;
            else
                MaxHealth += amountHP;
        }

        public void IncreaseSpeed(int amountSpeed)
        {
            if (speed + amountSpeed > 45)
                speed = 45;
            else
                speed += amountSpeed;
        }

        public void IncreaseMaxMana(int amountMana)
        {
            if (MaxMana + amountMana > 20)
                MaxMana = MaxMana;
            else
                MaxMana += amountMana;
        }

        private void GetDamaged(int amountOfDamage)
        {
            if (amountOfDamage < 0)
                throw new ArgumentException("Наносимый урон не может быть меньше нуля");
            CurrentHealth -= amountOfDamage;
        }

        private bool InBounds(int x, int y)
        {
            if (x >= 0 && x <= (windowWidth - HitboxWidth) && y >= 0 && y <= (windowHeight - HitboxWidth))
                return true;
            return false;
        }

        private void SetStartLearnedMagic()
        {
            LearnedMagic = new List<Type>()
            {
                typeof(FireBall),
                typeof(IceBall),
            };
        }

        private void UpdateRateOfFire()
        {
            CurrentRateOfFire = ((MagicModels.Magic) Activator.CreateInstance(currentMagic, 0, 0, 0, 0)).RateOfFire;
        }
    }
}