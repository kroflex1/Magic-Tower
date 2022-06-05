using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MagicTower.Model;
using MagicTower.Model.Magic;
using MagicTower.Model.MagicModels;

namespace MagicTower
{
    public class MagicView : GameObjectView
    {

        

        public MagicView(Game gameModel) : base(gameModel)
        {
        }
        
        public override void Draw(Graphics graphics)
        {
            foreach (var magic in gameModel.CurrentRoom.MagicInRoom)
            {
                var pos = new Point(magic.PosX, magic.PosY);
                graphics.DrawImage(imagesForGameObjects[magic.GetType()], pos);
            }
        }

        protected override void SetImagesForGameObjects()
        {
            imagesForGameObjects = new Dictionary<Type, Image>();
            imagesForGameObjects[typeof(FireBall)] =
                Image.FromFile(
                    @"C:\Users\Kroflex\Desktop\Magic-Tower\MagicTower\MagicTower\Sprites\MagicSprites\fireBall.png");
            imagesForGameObjects[typeof(IceBall)] =
                Image.FromFile(
                    @"C:\Users\Kroflex\Desktop\Magic-Tower\MagicTower\MagicTower\Sprites\MagicSprites\IceBall.png");
            imagesForGameObjects[typeof(IceShard)] =
                Image.FromFile(
                    @"C:\Users\Kroflex\Desktop\Magic-Tower\MagicTower\MagicTower\Sprites\MagicSprites\IceShard.png");
            imagesForGameObjects[typeof(DuplicateSphere)] = Image.FromFile(
                @"C:\Users\Kroflex\Desktop\Magic-Tower\MagicTower\MagicTower\Sprites\MagicSprites\DuplicateSphere.png");
        }

     
    }
}