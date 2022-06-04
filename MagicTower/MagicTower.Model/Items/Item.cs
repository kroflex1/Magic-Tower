using MagicTower.Model.Magic;

namespace MagicTower.Model.Items
{
    public abstract class Item : IGameObject
    {
        private IGameObject _gameObjectImplementation;
        public int PosX { get; private set; }
        public int PosY { get; private set; }
        public int HitboxWidth { get; private set; }
        public int HitboxHeight { get; private set; }
        public Condition CurrentCondition { get; private set; }
        
        public string Description { get; private set; }

        public Item(int posX, int posY, int hitboxWidth, int hitboxHeight)
        {
            PosX = posX;
            PosY = posY;
            HitboxWidth = hitboxWidth;
            HitboxHeight = hitboxHeight;
            CurrentCondition = Condition.Alive;
        }

        public void OnCollisionEnter(IGameObject gameObject)
        {
            if (gameObject is Player)
            {
                UpgradePlayer((Player)gameObject);
                CurrentCondition = Condition.Destroyed;
            }
        }

        protected virtual void UpgradePlayer(Player player)
        {
            
        }
    }
}