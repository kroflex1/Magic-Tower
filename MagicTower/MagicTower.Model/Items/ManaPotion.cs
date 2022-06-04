namespace MagicTower.Model.Items
{
    public class ManaPotion: Item
    {
        private const int AmountOfMana = 3;
        public ManaPotion(int posX, int posY, int hitboxWidth, int hitboxHeight) : base(posX, posY, hitboxWidth, hitboxHeight)
        {
        }

        protected override void UpgradePlayer(Player player)
        {
            player.RestoreMana(AmountOfMana);
        }
    }
}