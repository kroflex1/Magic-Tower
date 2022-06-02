using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MagicTower.Model;

namespace MagicTower
{
    public partial class GameScreen : Form
    {
        private Game gameModel;
        private PlayerView playerView;
        private MagicView magicView;
        private EnemyView enemyView;
        private PauseScreen pauseScreen;
        public GameScreen()
        {
            InitializeComponent();
            SetWindowConfigurations();
            gameModel = new Game(Width, Height);
            SetViewObjects();

            var playerHealthLabel = new Label()
            {
                Location = new Point(0, 0),
                Size = new Size(100, 100),
                Text = "Player Health:" + gameModel.Player.CurrentHealth
            };
            Controls.Add(playerHealthLabel);
            
            var timer = new Timer();
            timer.Interval = 30;
            timer.Tick += (sender, args) => { gameModel.Update(); };
            timer.Tick += (sender, args) => { UpdateLabels(playerHealthLabel); };
            timer.Tick += (sender, args) => { Invalidate(); };
            timer.Start();
        }

        public void SetPauseScreen(PauseScreen pauseScreen)
        {
            this.pauseScreen = pauseScreen;
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
            else if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)
                gameModel.Player.ChangeCurrentMagic(e.KeyData.ToString()[1] - '0' - 1);
            
            else if (e.KeyCode == Keys.Escape)
            {
                Hide();
                pauseScreen.Show();
            }
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
        
        private void SetViewObjects()
        {
            playerView = new PlayerView(gameModel.Player);
            magicView = new MagicView(gameModel.CurrentRoom);
            enemyView = new EnemyView(gameModel.CurrentRoom);
        }

        private void SetWindowConfigurations()
        {
            Size = Screen.PrimaryScreen.Bounds.Size;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            BackgroundImage = Image.FromFile(@"C:\Users\Kroflex\Desktop\Magic-Tower\MagicTower\MagicTower\Sprites\Backgrounds\GameBackground.png");
        }

        private void UpdateLabels(Label playerHealth)
        {
            playerHealth.Text = "Player Health:" + gameModel.Player.CurrentHealth.ToString();
        }
        
        private void DrawPlayerUI()
        {
            
        }
    }
}