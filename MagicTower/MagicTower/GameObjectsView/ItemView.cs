using System;
using System.Collections.Generic;
using System.Drawing;
using MagicTower.Model;
using MagicTower.Model.Items;

namespace MagicTower
{
    public class ItemView : GameObjectView
    {
        public ItemView(Game gameModel) : base(gameModel)
        {
        }

        public override void Draw(Graphics graphics)
        {
            foreach (var item in gameModel.CurrentRoom.ItemsInRoom)
            {
                var pos = new Point(item.PosX, item.PosY);
                graphics.DrawImage(imagesForGameObjects[item.GetType()], pos);
            }
        }

        protected override void SetImagesForGameObjects()
        {
            imagesForGameObjects = new Dictionary<Type, Image>();
            imagesForGameObjects[typeof(HealingPotion)] =
                Image.FromFile(@"Sprites/Items/health_Potion.png");
            imagesForGameObjects[typeof(ManaPotion)] =
                Image.FromFile(@"Sprites/Items/mana_Potion.png");
            imagesForGameObjects[typeof(DragonsEye)] =
                Image.FromFile(@"Sprites/Items/dragons_Eye.png");
            imagesForGameObjects[typeof(MagicMushroom)] =
                Image.FromFile(@"Sprites/Items/magic_Mashroom.png");
            imagesForGameObjects[typeof(EldenRing)] =
                Image.FromFile(@"Sprites/Items/elden_Ring.png");
            imagesForGameObjects[typeof(Scroll)] =
                Image.FromFile(@"Sprites/Items/scroll.png");
        }
    }
}