using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MagicTower.Model;
using MagicTower.Model.Magic;
using MagicTower.Model.MagicModels;

namespace MagicTower
{
    public class MagicView
    {
        private Room room;
        private Dictionary<Type, Image> imagesForMagic;

        public MagicView(Room room)
        {
            this.room = room;
            SetImagesForMagic();
        }

        public void Draw(Graphics e)
        {
            foreach (var magic in room.MagicInRoom)
            {
                var pos = new Point(magic.PosX, magic.PosY);
                e.DrawImage(imagesForMagic[magic.GetType()], pos);
            }
        }

        private void SetImagesForMagic()
        {
            imagesForMagic = new Dictionary<Type, Image>();
            imagesForMagic[typeof(FireBall)] =
                Image.FromFile(
                    @"C:\Users\Kroflex\Desktop\Magic-Tower\MagicTower\MagicTower\Sprites\MagicSprites\fireBall.png");
            imagesForMagic[typeof(IceBall)] =
                Image.FromFile(
                    @"C:\Users\Kroflex\Desktop\Magic-Tower\MagicTower\MagicTower\Sprites\MagicSprites\IceBall.png");
            imagesForMagic[typeof(IceShard)] =
                Image.FromFile(
                    @"C:\Users\Kroflex\Desktop\Magic-Tower\MagicTower\MagicTower\Sprites\MagicSprites\IceShard.png");
            imagesForMagic[typeof(DuplicateSphere)] = Image.FromFile(
                @"C:\Users\Kroflex\Desktop\Magic-Tower\MagicTower\MagicTower\Sprites\MagicSprites\DuplicateSphere.png");
        }
    }
}