using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
            var distanceBetweenHearts = heartStatus[0].Width + heartStatus[0].Width / 4;
            var currentStartPoint = new Point(4, 4);
            var remainsHealh = player.CurrentHealth;
            for (int i = 0; i < player.MaxHealth / 2; i++)
            {
                if (remainsHealh >= 2)
                {
                    graphics.DrawImage(heartStatus[2], currentStartPoint);
                    remainsHealh -= 2;
                }
                else if (remainsHealh == 1)
                {
                    graphics.DrawImage(heartStatus[1], currentStartPoint);
                    remainsHealh -= 1;
                }
                else
                    graphics.DrawImage(heartStatus[0], currentStartPoint);

                if (i == 6)
                {
                    currentStartPoint.X = 4;
                    currentStartPoint.Y += heartStatus[0].Height + heartStatus[0].Height/4;
                }
                else
                    currentStartPoint.X += distanceBetweenHearts;
            }
        }

        private void DrawMana(Graphics graphics)
        {
            var distanceBetweenMana = manaStatus[0].Height + manaStatus[0].Height / 4;
            var startY = heartStatus[0].Height * 2 +player.MaxMana *  distanceBetweenMana;
            var currentStartPoint = new Point(4, startY);
            var remainsMana = player.CurrentMana;
            for (int i = 0; i < player.MaxMana; i++)
            {
                if (remainsMana > 0)
                {
                    remainsMana--;
                    graphics.DrawImage(manaStatus[1], currentStartPoint);
                }
                else
                    graphics.DrawImage(manaStatus[0], currentStartPoint);
                currentStartPoint.Y -= distanceBetweenMana;
            }
        }
        
       

        private Dictionary<int, Image> SetImagesForHeartStatus()
        {
            var healtStatus = new Dictionary<int, Image>()
            {
                {0, Image.FromFile(@"Sprites\UI\Heart\ui_heart_empty.png")},
                {1, Image.FromFile(@"Sprites\UI\Heart\ui_heart_half.png")},
                {2, Image.FromFile(@"Sprites\UI\Heart\ui_heart_full.png")}
            };
            return healtStatus;
        }

        private Dictionary<int, Image> SetImagesForManaStatus()
        {
            var manaStatus = new Dictionary<int, Image>()
            {
                {0, Image.FromFile(@"Sprites\UI\Mana\ui_empty_mana.png")},
                {1, Image.FromFile(@"Sprites\UI\Mana\ui_full_mana.png")}
            };
            return manaStatus;
        }
    }
}