using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using MagicTower.Model;
using MagicTower.Model.Magic;

namespace MagicTower
{
    public partial class GameForm : Form
    {
        private Game gameModel;
        private PlayerView playerView;
        private MagicView magicView;

        public GameForm()
        {
            InitializeComponent();
            Size = new Size(400, 400);
            gameModel = new Game(Width, Height);
            playerView = new PlayerView(gameModel.Player);
            magicView = new MagicView(gameModel.currentRoom);

            var timer = new Timer();
            timer.Interval = 33;
            timer.Tick += (sender, args) =>
            {
                Invalidate();
            };
            timer.Start();
        }

        protected override void OnLoad(EventArgs e)
        {
            Text = "Magic Tower";
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            playerView.Draw(e.Graphics);
            magicView.Draw(e.Graphics);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
                gameModel.Player.Move(Direction.Left);
            if (e.KeyCode == Keys.D)
                gameModel.Player.Move(Direction.Right);
            if (e.KeyCode == Keys.W)
                gameModel.Player.Move(Direction.Up);
            if (e.KeyCode == Keys.S)
                gameModel.Player.Move(Direction.Down);
        }

        protected override void OnMouseMove(MouseEventArgs mouse)
        {
            if (mouse.X > gameModel.Player.PosX && playerView.imageDirection == Direction.Left
                || mouse.X < gameModel.Player.PosX && playerView.imageDirection == Direction.Right)
            {
                playerView.FlipImage();
                Invalidate();
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            gameModel.SpawnMagic(e.X, e.Y);
        }
    }

    public class PlayerView
    {
        public readonly Image playerSprite;
        public Direction imageDirection { get; set; }
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
                    @"C:\Users\Kroflex\Desktop\Magic-Tower\MagicTower\MagicTower\Sprites\MagicSprites\FireBall.png");
        }
    }
}