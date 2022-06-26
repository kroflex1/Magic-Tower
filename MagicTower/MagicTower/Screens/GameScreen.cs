using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MagicTower.Model;

namespace MagicTower
{
    public partial class GameScreen : Form
    {
        public Timer TimerUpdate { get; private set; }
        public Timer TimerWave { get; private set; }
        private Timer timeBetweenShots;

        private Game gameModel;
        private PlayerView playerView;
        private ItemView itemView;
        private MagicView magicView;
        private EnemyView enemyView;
        private PlayerUI playerUi;
        private Label scoreLabel;
        private PauseScreen pauseScreen;
        private StartScreen menuScreen;

        public GameScreen()
        {
            InitializeComponent();

            SetWindowConfigurations();
            gameModel = new Game(Width, Height);
            playerUi = new PlayerUI(Width, Height, gameModel.Player);
            SetViewObjects();
            SetTimers();
            SetScoreLabel();
        }

        public void RestartGame()
        {
            gameModel.Restart();
            TimerUpdate.Start();
            TimerWave.Start();
        }

        public void SetPauseScreen(PauseScreen pauseScreen)
        {
            this.pauseScreen = pauseScreen;
        }

        public void SetStartScreen(StartScreen startScreen)
        {
            menuScreen = startScreen;
        }

        protected override void OnLoad(EventArgs e)
        {
            Text = "Magic Tower";
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            itemView.Draw(e.Graphics);
            magicView.Draw(e.Graphics);
            enemyView.Draw(e.Graphics);
            playerView.Draw(e.Graphics);
            playerUi.Draw(e.Graphics);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            CheckForStartPlayerMovement(e);
            CheckForStartPlayerAttack(e);

            if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)
                gameModel.Player.ChangeCurrentMagic(e.KeyData.ToString()[1] - '0' - 1);
            else if (e.KeyCode == Keys.Escape)
                OpenPauseScreen();
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            CheckForStopPlayerMovement(e);
            CheckForStopPlayerAttack(e);
        }

        protected override void OnMouseMove(MouseEventArgs mouse)
        {
            if (mouse.X > gameModel.Player.PosX && playerView.imageDirection == Direction.Left
                || mouse.X < gameModel.Player.PosX && playerView.imageDirection == Direction.Right)
                playerView.FlipImage();
        }

        private void SetTimers()
        {
            TimerUpdate = new Timer();
            TimerUpdate.Interval = 60;
            TimerUpdate.Tick += (sender, args) =>
            {
                if (!gameModel.Player.IsAlive)
                {
                    TimerUpdate.Stop();
                    TimerWave.Stop();
                    ShowGameOver();
                }
                gameModel.SpawnMagic();
                gameModel.Update();
                Invalidate();
                UpdateScoreView();
            };

            TimerWave = new Timer();
            TimerWave.Interval = gameModel.IntervalBetweenWaves;
            TimerWave.Tick += (sender, args) => gameModel.SummonWaveOfEnemies();
        }

        private void CheckForStartPlayerMovement(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
                gameModel.Player.HorizontalMoveDirection = DirectionWeight.Negative;
            else if (e.KeyCode == Keys.D)
                gameModel.Player.HorizontalMoveDirection = DirectionWeight.Positive;
            else if (e.KeyCode == Keys.W)
                gameModel.Player.VerticalMoveDirection = DirectionWeight.Negative;
            else if (e.KeyCode == Keys.S)
                gameModel.Player.VerticalMoveDirection = DirectionWeight.Positive;
        }

        private void CheckForStartPlayerAttack(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                gameModel.Player.VerticalAttackDirection = DirectionWeight.Negative;
            else if (e.KeyCode == Keys.Down)
                gameModel.Player.VerticalAttackDirection = DirectionWeight.Positive;
            else if (e.KeyCode == Keys.Left)
                gameModel.Player.HorizontalAttackDirection = DirectionWeight.Negative;
            else if (e.KeyCode == Keys.Right)
                gameModel.Player.HorizontalAttackDirection = DirectionWeight.Positive;
        }

        private void CheckForStopPlayerMovement(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
                gameModel.Player.HorizontalMoveDirection = DirectionWeight.Neutral;
            else if (e.KeyCode == Keys.D)
                gameModel.Player.HorizontalMoveDirection = DirectionWeight.Neutral;
            else if (e.KeyCode == Keys.W)
                gameModel.Player.VerticalMoveDirection = DirectionWeight.Neutral;
            else if (e.KeyCode == Keys.S)
                gameModel.Player.VerticalMoveDirection = DirectionWeight.Neutral;
        }

        private void CheckForStopPlayerAttack(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                gameModel.Player.VerticalAttackDirection = DirectionWeight.Neutral;
            else if (e.KeyCode == Keys.Down)
                gameModel.Player.VerticalAttackDirection = DirectionWeight.Neutral;
            else if (e.KeyCode == Keys.Left)
                gameModel.Player.HorizontalAttackDirection = DirectionWeight.Neutral;
            else if (e.KeyCode == Keys.Right)
                gameModel.Player.HorizontalAttackDirection = DirectionWeight.Neutral;
        }

        private void OpenPauseScreen()
        {
            TimerUpdate.Stop();
            TimerWave.Stop();
            Hide();
            pauseScreen.Show();
        }

        private void OpenMenuScreen()
        {
            Hide();
            menuScreen.Show();
        }

        private void ShowGameOver()
        {
            var gameOverLabel = new Label()
            {
                Text = "GameOver",
                Location = new Point(Width / 2, Height / 2)
            };

            var menuButton = new Button()
            {
                Text = "Menu",
                Location = new Point(gameOverLabel.Left, gameOverLabel.Bottom + 20),
            };
            
            var restartGameButton = new Button()
            {
                Text = "Restart",
                Location = new Point(gameOverLabel.Left, menuButton.Bottom + 20),
            };
            
            menuButton.Click += (sender, args) =>
            {
                OpenMenuScreen();
                Controls.Remove(gameOverLabel);
                Controls.Remove(menuButton);
                Controls.Remove(restartGameButton);
            };
            
            restartGameButton.Click += (sender, args) =>
            {
                Controls.Remove(gameOverLabel);
                Controls.Remove(menuButton);
                Controls.Remove(restartGameButton);
                RestartGame();
            };

            Controls.Add(gameOverLabel);
            Controls.Add(restartGameButton);
            Controls.Add(menuButton);
        }

        private void UpdateScoreView()
        {
            scoreLabel.Text = "Score:" + gameModel.GetScore();
        }

        private void SetViewObjects()
        {
            playerView = new PlayerView(gameModel);
            itemView = new ItemView(gameModel);
            magicView = new MagicView(gameModel);
            enemyView = new EnemyView(gameModel);
        }

        private void SetScoreLabel()
        {
            scoreLabel = new Label()
            {
                Text = "Score:" + gameModel.GetScore(),
                AutoSize = false,
                TextAlign = ContentAlignment.TopCenter,
                Dock = DockStyle.None,
            };
            scoreLabel.Location = new Point(Width / 2 - scoreLabel.Width / 2, 10);
            Controls.Add(scoreLabel);
        }

        private void SetWindowConfigurations()
        {
            Size = Screen.PrimaryScreen.Bounds.Size;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            BackgroundImage = Image.FromFile(@"Sprites\Backgrounds\GameBackground.png");
        }
    }
}