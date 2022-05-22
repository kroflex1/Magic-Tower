using System;
using System.Drawing;
using System.Windows.Forms;
using MagicTower.Model;

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
            Size = Screen.PrimaryScreen.Bounds.Size;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            BackgroundImage = Image.FromFile(@"C:\Users\Kroflex\Desktop\GameSprites\Tiles\Background.png");

            gameModel = new Game(Width, Height);
            SetViewObjects();

            var playerHealthLabel = new Label();
            playerHealthLabel.Location = new Point(0, 0);
            playerHealthLabel.Size = new Size(50, 50);
            playerHealthLabel.Text = "Player Health:" + gameModel.Player.CurrentHealth.ToString();
            Controls.Add(playerHealthLabel);
            
            
            var timer = new Timer();
            timer.Interval = 25;
            timer.Tick += (sender, args) => { gameModel.Update(); };
            timer.Tick += (sender, args) => { UpdateLabels(playerHealthLabel); };
            timer.Tick += (sender, args) => { Invalidate(); };
            timer.Start();
        }

        private void SetViewObjects()
        {
            playerView = new PlayerView(gameModel.Player);
            magicView = new MagicView(gameModel.CurrentRoom);
            enemyView = new EnemyView(gameModel.CurrentRoom);
        }

        private void UpdateLabels(Label playerHealth)
        {
            playerHealth.Text = "Player Health:" + gameModel.Player.CurrentHealth.ToString();
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
            if (e.KeyCode == Keys.A)
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
            if (e.KeyCode == Keys.A)
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