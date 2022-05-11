using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using MagicTower.Model;
using System.Windows.Input;
using MagicTower.Model.EnemiesModels;
using MagicTower.Model.Magic;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;


namespace MagicTower
{
    public partial class GameForm : Form
    {
        private Game gameModel;
        private PlayerView playerView;
        private MagicView magicView;
        private EnemyView enemyView;


        public GameForm()
        {
            InitializeComponent();
            Size = new Size(1920, 1080);
            KeyPreview = true;
            gameModel = new Game(Width, Height);
            playerView = new PlayerView(gameModel.Player);
            magicView = new MagicView(gameModel.currentRoom);
            enemyView = new EnemyView(gameModel.currentRoom);

            gameModel.SpawnEnemy(200, 200);

            var timer = new Timer();
            timer.Interval = 33;
            timer.Tick += (sender, args) => { gameModel.currentRoom.Update(); };
            timer.Tick += (sender, args) => { Invalidate(); };
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
            enemyView.Draw(e.Graphics);
        }


        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.A) && Keyboard.IsKeyDown(Key.W))
            {
                gameModel.Player.Move(Direction.Left);
                gameModel.Player.Move(Direction.Up);
            }
            else if (Keyboard.IsKeyDown(Key.D) && Keyboard.IsKeyDown(Key.W))
            {
                gameModel.Player.Move(Direction.Right);
                gameModel.Player.Move(Direction.Up);
            }
            else if (Keyboard.IsKeyDown(Key.A) && Keyboard.IsKeyDown(Key.S))
            {
                gameModel.Player.Move(Direction.Left);
                gameModel.Player.Move(Direction.Down);
            }
            else if (Keyboard.IsKeyDown(Key.D) && Keyboard.IsKeyDown(Key.S))
            {
                gameModel.Player.Move(Direction.Right);
                gameModel.Player.Move(Direction.Down);
            }
            else if (Keyboard.IsKeyDown(Key.A))
            {
                gameModel.Player.Move(Direction.Left);
            }
            else if (Keyboard.IsKeyDown(Key.D))
            {
                gameModel.Player.Move(Direction.Right);
            }
            else if (Keyboard.IsKeyDown(Key.W))
            {
                gameModel.Player.Move(Direction.Up);
            }
            else if (Keyboard.IsKeyDown(Key.S))
            {
                gameModel.Player.Move(Direction.Down);
            }
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
}