using System;
using MagicTower.Model.EnemiesModels;

namespace MagicTower.Model.Items
{
    public class Scroll :Item
    {
        private Type magic;
        public Scroll(int posX, int posY, Type magicType) : base(posX, posY, 32, 32)
        {
            if (!magicType.IsSubclassOf(typeof(MagicModels.Magic)))
                throw new ArgumentException();
            else
                magic = magicType;
        }

        protected override void UpgradePlayer(Player player)
        {
            player.LearnNewMagic(magic);
        }
    }
}