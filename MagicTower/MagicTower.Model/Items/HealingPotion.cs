namespace MagicTower.Model.Items
{
    public class HealingPotion : Artifact
    {
        private const int AmountOfHealth = 4;

        public HealingPotion(int posX, int posY, int hitboxWidth, int hitboxHeight) : base(posX, posY, hitboxWidth,
            hitboxHeight)
        {
        }

        protected override void UpgradePlayer(Player player)
        {
            player.Heal(AmountOfHealth);
        }
    }
}