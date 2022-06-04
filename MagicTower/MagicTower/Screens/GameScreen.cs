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

        private Game gameModel;
        private PlayerView playerView;
        private MagicView magicView;
        private EnemyView enemyView;
        private PlayerUI playerUi;
        private PauseScreen pauseScreen;
        
        public GameScreen()
        {
            InitializeComponent();

            SetWindowConfigurations();
            gameModel = new Game(Width, Height);
            playerUi = new PlayerUI(Width, Height, gameModel.Player);
            SetViewObjects();
            
            TimerUpdate = new Timer();
            TimerUpdate.Interval = 35;
            TimerUpdate.Tick += (sender, args) => gameModel.Update();
            TimerUpdate.Tick += (sender, args) => Invalidate();

            TimerWave = new Timer();
            TimerWave.Interval = gameModel.IntervalBetweenWaves;
            TimerWave.Tick += (sender, args) => gameModel.SummonWaveOfEnemies();
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
            playerUi.Draw(e.Graphics);
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
                TimerUpdate.Stop();
                TimerWave.Stop();
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
                playerView.FlipImage();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            gameModel.SpawnMagic(e.X, e.Y);
        }

        private void SetViewObjects()
        {
            playerView = new PlayerView(gameModel.Player);
            magicView = new MagicView(gameModel.Arena);
            enemyView = new EnemyView(gameModel.Arena);
        }

        private void SetWindowConfigurations()
        {
            Size = Screen.PrimaryScreen.Bounds.Size;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            BackgroundImage =
                Image.FromFile(
                    @"C:\Users\Kroflex\Desktop\Magic-Tower\MagicTower\MagicTower\Sprites\Backgrounds\GameBackground.png");
        }
    }
}