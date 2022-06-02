using System;
using System.Collections.Generic;
using MagicTower.Model.EnemiesModels;
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

        public int CurrentHealth
        {
            get => currentHealth;
            private set
            {
                if (value > 0)
                    currentHealth = value;
            }
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

        public MovementWeight HorizontalMovement;
        public MovementWeight VerticalMovement;
        public int MaxHealth { get; private set; }
        public int MaxMana { get; private set; }
        public List<Type> LearnedMagic { get; private set; }

        public delegate void PosHandler(int playerPosX, int playerPosY);

        public delegate void MagicHandler(MagicModels.Magic magic);

        public event PosHandler OnChangePosition;
        public event MagicHandler OnCreateNewMagic;


        private int currentHealth;
        private int currentMana;
        private int speed;
        private Type currentMagic;
        private readonly int windowWidth;
        private readonly int windowHeight;

        public Player(int startPosX, int startPosY, int windowWidth, int windowHeight) : this(startPosX, startPosY, 32,
            56, 10, 10, 10, windowWidth, windowHeight)
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
            HorizontalMovement = MovementWeight.Neutral;
            VerticalMovement = MovementWeight.Neutral;

            SetStartLearnedMagic();
            currentMagic = LearnedMagic[0];

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
            if (InBounds(PosX + Speed * (int) HorizontalMovement, PosY + Speed * (int) VerticalMovement))
            {
                PosX += Speed * (int) HorizontalMovement;
                PosY += Speed * (int) VerticalMovement;
                if (OnChangePosition != null)
                    OnChangePosition(PosX, PosY);
            }
        }

        public void AttackTo(int targetX, int targetY)
        {
            var newMagic = (MagicModels.Magic) Activator.CreateInstance(currentMagic, PosX, PosY, targetX, targetY);
            if (CurrentMana - newMagic.ManaCost > 0 && OnCreateNewMagic != null)
            {
                CurrentMana -= newMagic.ManaCost;
                OnCreateNewMagic(newMagic);
            }
        }

        public void LearnNewMagic(Type newMagicType)
        {
            if (!LearnedMagic.Contains(newMagicType))
                LearnedMagic.Add(newMagicType);
        }

        public void ChangeCurrentMagic(int magicId)
        {
            if (magicId >= 0 && magicId < LearnedMagic.Count)
                currentMagic = LearnedMagic[magicId];
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
                typeof(DuplicateSphere)
            };
        }
    }
}