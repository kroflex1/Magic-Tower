using System.Drawing;
using System.Windows.Forms;

namespace MagicTower
{
    public partial class StartScreen : Form
    {
        private GameScreen gameScreen;
        private Button startGameButton;
        private Button tutorialButton;
        private Button exitButton;

        public StartScreen()
        {
            InitializeComponent();
            SetWindowConfigurations();
            
            startGameButton = new Button()
            {
                Location = new Point(Width / 2, Height / 2),
                Text = "Play"
            };
            startGameButton.Click += (sender, args) =>
            {
                Hide();
                gameScreen.Show();
                gameScreen.TimerUpdate.Start();
                gameScreen.TimerWave.Start();
            };

            tutorialButton = new Button()
            {
                Location = new Point(startGameButton.Left, startGameButton.Bottom + 5),
                Text = "Tutorial"
            };
            tutorialButton.Click += (sender, args) => { ShowTutorial(); };

            exitButton = new Button()
            {
                Location = new Point(startGameButton.Left, tutorialButton.Bottom + 5),
                Text = "Exit",
            };
            exitButton.Click += (sender, args) => { Application.Exit(); };

            Controls.Add(startGameButton);
            Controls.Add(tutorialButton);
            Controls.Add(exitButton);
        }

        public void SetGameScreen(GameScreen gameScreen)
        {
            this.gameScreen = gameScreen;
        }

        private void SetWindowConfigurations()
        {
            Size = Screen.PrimaryScreen.Bounds.Size;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            BackgroundImage = Image.FromFile(@"Sprites\Backgrounds\StartScreenBackground.jpg");
        }

        private void ShowTutorial()
        {
            BackgroundImage = Image.FromFile(@"Sprites\Backgrounds\tutorial.png");
            Controls.Remove(startGameButton);
            Controls.Remove(tutorialButton);
            Controls.Remove(exitButton);
            
            var closeTutorialButton = new Button()
            {
                Text = "Close",
            };
            closeTutorialButton.Location = new Point(Width - closeTutorialButton.Width, 0);
            closeTutorialButton.Click += (sender, args) =>
            {
                Controls.Remove(closeTutorialButton);
                Controls.Add(startGameButton);
                Controls.Add(tutorialButton);
                Controls.Add(exitButton);
                BackgroundImage = Image.FromFile(@"Sprites\Backgrounds\StartScreenBackground.jpg");
            };

            Controls.Add(closeTutorialButton);
        }
    }
}