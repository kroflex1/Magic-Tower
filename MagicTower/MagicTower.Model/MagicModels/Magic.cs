using MagicTower.Model.EnemiesModels;
using MagicTower.Model.Magic;

namespace MagicTower.Model.MagicModels
{
    public abstract class Magic : IGameObject
    {
        public int PosX { get; protected set; }
        public int PosY { get; protected set; }
        public int HitboxWidth { get; }
        public int HitboxHeight { get; }
        public Vector DirectionVector { get; protected set; }

        public int Speed
        {
            get => speed;
            set
            {
                if (value >= 0)
                    speed = value;
            }
        }

        public int Damage
        {
            get => damage;
            set
            {
                if (value >= 0)
                    damage = value;
            }
        }

        public int ManaCost
        {
            get => manaCost;
            set
            {
                if (value >= 0)
                    manaCost = value;
            }
        }

        public Condition CurrentCondition { get; protected set; }
        public delegate void MagicHandler(Magic magic);
        public abstract event MagicHandler CreateNewMagic;

        protected int speed;
        protected int damage;
        protected int manaCost;

        public Magic(int startX, int startY, int endX, int endY, int hitboxWidth,
            int hitboxHeight, int speed, int damage, int manaCost)
        {
            PosX = startX;
            PosY = startY;
            HitboxHeight = hitboxHeight;
            HitboxWidth = hitboxWidth;
            Speed = speed;
            Damage = damage;
            ManaCost = manaCost;
            CurrentCondition = Condition.Alive;
            CalculateDirectionVector(startX, startY, endX, endY);
        }

        public virtual void TakeStep()
        {
            PosX += DirectionVector.X;
            PosY += DirectionVector.Y;
        }


        public virtual void OnCollisionEnter(IGameObject gameObject)
        {
            if (gameObject is Enemy || gameObject is DuplicateSphere)
                CurrentCondition = Condition.Destroyed;
        }

        private void CalculateDirectionVector(int startX, int startY, int endX, int endY)
        {
            DirectionVector = new Vector(startX, startY, endX, endY);
            DirectionVector.SetLength(Speed);
        }
    }
}