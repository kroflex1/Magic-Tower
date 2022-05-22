using MagicTower.Model.EnemiesModels;

namespace MagicTower.Model.Magic
{
    public class IceBall:Magic
    {
        public IceBall(int startX, int startY, int endX, int endY) : base(startX, startY, endX, endY, 48, 32, 10, 2)
        {
        }

        public override void OnCollisionEnter(IGameObject gameObject)
        {

        }
        
        
    }

    public class IceShard : Magic
    {
        public IceShard(int startX, int startY, int endX, int endY, int hitboxWidth, int hitboxHeight, int speed, int damage) : base(startX, startY, endX, endY, hitboxWidth, hitboxHeight, speed, damage)
        {
        }
    }
}