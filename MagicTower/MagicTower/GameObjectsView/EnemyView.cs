using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MagicTower.Model;
using MagicTower.Model.EnemiesModels;
using MagicTower.Model.Magic;

namespace MagicTower
{
    public class EnemyView:GameObjectView
    {

        public EnemyView(Game gameModel) : base(gameModel)
        {
        }

        public override void Draw(Graphics graphics)
        {
            foreach (var enemy in gameModel.CurrentRoom.AliveEnemiesInRoom)
            {
                var pos = new Point(enemy.PosX, enemy.PosY);
                graphics.DrawImage(imagesForGameObjects[enemy.GetType()], pos);
            }
        }

        protected override void SetImagesForGameObjects()
        {
            imagesForGameObjects = new Dictionary<Type, Image>();
            imagesForGameObjects[typeof(Demon)] = Image.FromFile(@"Sprites/Enemies/demon.png");
            imagesForGameObjects[typeof(LittleDemon)] = Image.FromFile(@"Sprites/Enemies/littleDemon.png");
        }
    }
}