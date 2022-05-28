using System.Drawing;
using System.Windows.Forms;

namespace MagicTower
{
    public partial class PauseScreen : Form
    {
        public PauseScreen(StartScreen startScreen, GameScreen gameScreen)
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
            };

            var backToMenuButton = new Button()
            {
                Location = new Point(Width / 2, Height / 2),
                Text = "Menu"
            };
            backToMenuButton.Click += (sender, args) =>
            {
                Hide();
                startScreen.Show();
            };
        }

        private void SetWindowConfigurations()
        {
            Size = Screen.PrimaryScreen.Bounds.Size;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            BackgroundImage =
                Image.FromFile(
                    @"C:\Users\Kroflex\Desktop\Magic-Tower\MagicTower\MagicTower\Sprites\Backgrounds\StartScreenBackground.jpg");
        }
    }
}