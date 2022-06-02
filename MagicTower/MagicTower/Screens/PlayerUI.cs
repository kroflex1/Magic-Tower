using System;
using System.Collections.Generic;
using System.Drawing;
using MagicTower.Model;

namespace MagicTower
{
    public class PlayerUI
    {
        private Player player;
        private readonly int windowWidth;
        private readonly int windowHeight;
        private readonly Dictionary<int, Image> heartStatus;
        private readonly Dictionary<int, Image> manaStatus;

        public PlayerUI(int windowWidth, int windowHeight, Player player)
        {
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
            this.player = player;
            heartStatus = SetImagesForHeartStatus();
            manaStatus = SetImagesForManaStatus();
        }

        public void Draw(Graphics graphics)
        {
            DrawHearts(graphics);
            DrawMana(graphics);
        }

        private void DrawHearts(Graphics graphics)
        {
            var currnetStartPoint = new Point(4, 4);
            var remainsHealh = player.CurrentHealth;
            for (int i = 0; i < player.MaxHealth / 2; i++)
            {
                if (remainsHealh >= 2)
                {
                    graphics.DrawImage(heartStatus[2], currnetStartPoint);
                    remainsHealh -= 2;
                }
                else if (remainsHealh == 1)
                {
                    graphics.DrawImage(heartStatus[1], currnetStartPoint);
                    remainsHealh -= 1;
                }
                else
                    graphics.DrawImage(heartStatus[0], currnetStartPoint);

                if (i == 6)
                {
                    currnetStartPoint.X = 64;
                    currnetStartPoint.Y += heartStatus[0].Height + 32;
                }
                else
                    currnetStartPoint.X += 64;
            }
        }

        private void DrawMana(Graphics graphics)
        {
            var currnetStartPoint = new Point(64, windowHeight - 64);
            var remainsMana =  player.CurrentMana;
            for (int i = 0; i < player.MaxMana; i++)
            {
                if (remainsMana > 0)
                {
                    remainsMana--;
                    graphics.DrawImage(manaStatus[1], currnetStartPoint);
                }
                else
                    graphics.DrawImage(manaStatus[0], currnetStartPoint);
                currnetStartPoint.Y -= 64;
            }
        }

        private Dictionary<int, Image> SetImagesForHeartStatus()
        {
            var healtStatus = new Dictionary<int, Image>()
            {
                {0, Image.FromFile(@"C:\Users\Kroflex\Desktop\Magic-Tower\MagicTower\MagicTower\Sprites\UI\Hert\ui_heart_empty.png")},
                {1, Image.FromFile(@"C:\Users\Kroflex\Desktop\Magic-Tower\MagicTower\MagicTower\Sprites\UI\Hert\ui_heart_half.png")},
                {2, Image.FromFile(@"C:\Users\Kroflex\Desktop\Magic-Tower\MagicTower\MagicTower\Sprites\UI\Hert\ui_heart_full.png")}
            };
            return healtStatus;
        }

        private Dictionary<int, Image> SetImagesForManaStatus()
        {
            var manaStatus = new Dictionary<int, Image>()
            {
                {0, Image.FromFile(@"C:\Users\Kroflex\Desktop\Magic-Tower\MagicTower\MagicTower\Sprites\UI\Mana\ui_empty_mana.png")},
                {1, Image.FromFile(@"C:\Users\Kroflex\Desktop\Magic-Tower\MagicTower\MagicTower\Sprites\UI\Mana\ui_full_mana.png")}
            };
            return manaStatus;
        }
    }
}