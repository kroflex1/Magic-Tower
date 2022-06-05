namespace MagicTower.Model.Items
{
    public class ManaPotion: Item
    {
        private const int AmountOfMana = 3;
        public ManaPotion(int posX, int posY) : base(posX, posY, 32, 32)
        {
        }

        protected override void UpgradePlayer(Player player)
        {
            player.RestoreMana(AmountOfMana);
        }
    }
}