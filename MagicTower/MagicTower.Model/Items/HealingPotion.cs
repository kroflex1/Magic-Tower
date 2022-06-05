namespace MagicTower.Model.Items
{
    public class HealingPotion : Item
    {
        private const int AmountOfHealth = 4;

        public HealingPotion(int posX, int posY) : base(posX, posY, 32,
            32)
        {
        }

        protected override void UpgradePlayer(Player player)
        {
            player.Heal(AmountOfHealth);
        }
    }
}