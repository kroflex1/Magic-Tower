using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MagicTower.Model;
using MagicTower.Model.Magic;

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
            room.Update();
            foreach (var magic in room.allMagicInRoom)
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
                    Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName,
                        "Sprites\\MagicSprites\\fireBall.png"));
        }
    }
}