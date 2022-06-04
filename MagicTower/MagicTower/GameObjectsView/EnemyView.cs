using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MagicTower.Model;
using MagicTower.Model.EnemiesModels;
using MagicTower.Model.Magic;

namespace MagicTower
{
    public class EnemyView
    {
        private Arena _arena;
        private Dictionary<Type, Image> imagesForEnemies;

        public EnemyView(Arena arena)
        {
            this._arena = arena;
            SetImagesForMagic();
        }

        public void Draw(Graphics e)
        {
            foreach (var enemy in _arena.AliveEnemiesInRoom)
            {
                var pos = new Point(enemy.PosX, enemy.PosY);
                e.DrawImage(imagesForEnemies[enemy.GetType()], pos);
            }
        }

        private void SetImagesForMagic()
        {
            imagesForEnemies = new Dictionary<Type, Image>();
            imagesForEnemies[typeof(Demon)] = Image.FromFile(@"Sprites/Enemies/demon.png");
            imagesForEnemies[typeof(LittleDemon)] = Image.FromFile(@"Sprites/Enemies/littleDemon.png");
        }
    }
}