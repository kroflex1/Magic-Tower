using MagicTower.Model.Items;

namespace MagicTower.Model
{
    public class TreasureRoom:Room
    {
        private Item artifact;
        private Scroll scroll;
        private Rectangle artifactRectangle;
        private Rectangle scrollRectangle;
        
        public TreasureRoom(int width, int height, Player player) : base(width, height, player)
        {
            
        }

        public override void Update()
        {
            ChangeGameObjectsPosition();
        }

        public void UpdateContent()
        {
            
        }

        private void CheckForCollisions()
        {
            var playerRectangle = new Rectangle(Player.PosX, Player.PosY, Player.HitboxWidth, Player.HitboxHeight);
            if (CollisionController.IsIntersection(playerRectangle, artifactRectangle))
            {
                artifact.OnCollisionEnter(Player);
                Player.OnCollisionEnter(artifact);
            }
            else if (CollisionController.IsIntersection(playerRectangle, scrollRectangle))
            {
                scroll.OnCollisionEnter(Player);
                Player.OnCollisionEnter(scroll);
            }
        }

        private void DeleteTakenItems()
        {
            if (artifact.CurrentCondition == Condition.Destroyed)
                artifact = null;
            if (scroll.CurrentCondition == Condition.Destroyed)
                scroll = null;
        }
    }
    
}