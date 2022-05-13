using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using MagicTower.Model;
using System.Windows.Input;
using MagicTower.Model.EnemiesModels;
using MagicTower.Model.Magic;

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
            BackgroundImage =
                Image.FromFile(@"C:\Users\Kroflex\Desktop\Magic-Tower\MagicTower\MagicTower\Sprites\Background.png");
            KeyPreview = true;
            gameModel = new Game(1920, 1080);
            playerView = new PlayerView(gameModel.Player);
            magicView = new MagicView(gameModel.currentRoom);
            enemyView = new EnemyView(gameModel.currentRoom);

            gameModel.SpawnEnemy(200, 200);

            var timer = new Timer();
            timer.Interval = 10;
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
            if(e.KeyCode == Keys.A)
                gameModel.Player.HorizontalMovement = MovementWeight.Negative;
            else if (e.KeyCode == Keys.D)
                gameModel.Player.HorizontalMovement = MovementWeight.Positive;
            else if (e.KeyCode == Keys.W)
                gameModel.Player.VerticalMovement = MovementWeight.Negative;
            else if (e.KeyCode == Keys.S)
                gameModel.Player.VerticalMovement = MovementWeight.Positive;
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.A)
                gameModel.Player.HorizontalMovement = MovementWeight.Neutral;
            else if (e.KeyCode == Keys.D)
                gameModel.Player.HorizontalMovement = MovementWeight.Neutral;
            else if (e.KeyCode == Keys.W)
                gameModel.Player.VerticalMovement = MovementWeight.Neutral;
            else if (e.KeyCode == Keys.S)
                gameModel.Player.VerticalMovement = MovementWeight.Neutral;
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