namespace MagicTower.Model.Items
{
    public class MagicMushroom : Item
    {
        private const int AmountMaxHealthIncrease = 6;
        public MagicMushroom(int posX, int posY) : base(posX, posY, 32, 32)
        {
        }

        protected override void UpgradePlayer(Player player)
        {
            player.IncreaseMaxHealth(AmountMaxHealthIncrease);
        }
    }
}