namespace MagicTower.Model.Items
{
    public class DragonsEye:Item
    {

        private const int AmountDamageBonus = 3;
        public DragonsEye(int posX, int posY) : base(posX, posY, 64, 64)
        {
        }

        protected override void UpgradePlayer(Player player)
        {
            player.IncreaseMagicDamage(AmountDamageBonus);
        }
    }
}