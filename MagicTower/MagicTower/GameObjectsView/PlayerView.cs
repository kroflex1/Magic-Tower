using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using MagicTower.Model;

namespace MagicTower
{
    public class PlayerView : GameObjectView
    {
        public Direction imageDirection { get; private set; }
        private Image playerSprite;
        private Player player;

        public PlayerView(Game gameModel) : base(gameModel)
        {
            playerSprite = Image.FromFile(@"Sprites\player.png");
            imageDirection = Direction.Right;
        }

        public override void Draw(Graphics graphics)
        {
            graphics.DrawImage(playerSprite, new Point(gameModel.Player.PosX, gameModel.Player.PosY));
        }
        
        
        public void FlipImage()
        {
            if (imageDirection == Direction.Right)
                imageDirection = Direction.Left;
            else
                imageDirection = Direction.Right;
            playerSprite.RotateFlip(RotateFlipType.Rotate180FlipY);
        }
    }
}