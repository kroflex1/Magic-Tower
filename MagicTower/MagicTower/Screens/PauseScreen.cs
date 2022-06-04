using System.Drawing;
using System.Windows.Forms;

namespace MagicTower
{
    public partial class PauseScreen : Form
    {
        private GameScreen gameScreen;
        private StartScreen startScreen;
        public PauseScreen()
        {
            InitializeComponent();
            SetWindowConfigurations();
            var continueGameButton = new Button()
            {
                Location = new Point(Width / 2, Height / 2),
                Text = "Continue"
            };
            continueGameButton.Click += (sender, args) =>
            {
                Hide();
                gameScreen.Show();
                gameScreen.TimerUpdate.Start();
                gameScreen.TimerWave.Start();   
            };

            var backToMenuButton = new Button()
            {
                Location = new Point(continueGameButton.Left, continueGameButton.Bottom + 5),
                Text = "Menu"
            };
            backToMenuButton.Click += (sender, args) =>
            {
                Hide();
                startScreen.Show();
            };
            
            Controls.Add(continueGameButton);
            Controls.Add(backToMenuButton);
        }

        public void SetStartAndGameScreen(StartScreen startScreen, GameScreen gameScreen)
        {
            this.startScreen = startScreen;
            this.gameScreen = gameScreen;
        }

        private void SetWindowConfigurations()
        {
            Size = Screen.PrimaryScreen.Bounds.Size;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            BackgroundImage =
                Image.FromFile(
                    @"C:\Users\Kroflex\Desktop\Magic-Tower\MagicTower\MagicTower\Sprites\Backgrounds\PauseBackground.jpg");
        }
    }
}