using MagicTower.Model.Magic;

namespace MagicTower.Model
{
    public abstract class Room
    {
        protected readonly int width;
        protected readonly int height;
        public Player Player { get; }

        public Room(int width, int height, Player player)
        {
            this.width = width;
            this.height = height;
            Player = player;
        }

        public virtual void Update()
        {
            ChangeGameObjectsPosition();
        }
        
        protected virtual void ChangeGameObjectsPosition()
        {
            Player.Move();
        }
    }
}