namespace MagicTower.Model.Items
{
    public class EldenRing : Artifact
    {
        private const int AmountIncreasedMaxHealth = 4;
        private const int AmountIncreasedMaxMana = 3;
        private const int AmountIncreasedBonusDamage = 2;

        public EldenRing(int posX, int posY) : base(posX, posY, 32, 32)
        {
        }

        protected override void UpgradePlayer(Player player)
        {
            player.IncreaseMaxHealth(AmountIncreasedMaxHealth);
            player.IncreaseMaxHealth(AmountIncreasedMaxMana);
            player.IncreaseMagicDamage(AmountIncreasedBonusDamage);
        }
    }
}