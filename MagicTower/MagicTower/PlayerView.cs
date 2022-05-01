using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using MagicTower.Model;

namespace MagicTower
{
    public class PlayerView
    {
        public Direction imageDirection { get; private set; }
        private Image playerSprite;
        private Player player;

        public PlayerView(Player player)
        {
            this.player = player;
            playerSprite =
                Image.FromFile(@"C:\Users\Kroflex\Desktop\Magic-Tower\MagicTower\MagicTower\Sprites\player.png");
            imageDirection = Direction.Right;
        }

        public void Draw(Graphics e)
        {
            e.DrawImage(playerSprite, new Point(player.PosX, player.PosY));
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