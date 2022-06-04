namespace MagicTower.Model.Items
{
    public class ВragonsEye:Item
    {

        private const int AmountDamageBonus = 3;
        public ВragonsEye(int posX, int posY, int hitboxWidth, int hitboxHeight) : base(posX, posY, hitboxWidth, hitboxHeight)
        {
        }

        protected override void UpgradePlayer(Player player)
        {
            player.IncreaseMagicDamage(AmountDamageBonus);
        }
    }
}