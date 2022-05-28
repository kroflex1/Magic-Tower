using System.Drawing;
using System.Windows.Forms;

namespace MagicTower
{
    public partial class StartScreen : Form
    {
        public StartScreen(GameScreen gameScreen)
        {
            InitializeComponent();
            SetWindowConfigurations();
            var startGameButton = new Button()
            {
                Location = new Point(Width / 2, Height / 2),
                Text = "Play"
            };
            startGameButton.Click += (sender, args) =>
            {
                Hide();
                gameScreen.Show();
            };
            
            
            var exitButton = new Button()
            {
                Location = new Point(startGameButton.Left, startGameButton.Bottom + 5),
                Text = "Exit",
            };
            exitButton.Click += (sender, args) => { Application.Exit(); };
            
            Controls.Add(startGameButton);
            Controls.Add(exitButton);
        }

        private void SetWindowConfigurations()
        {
            Size = Screen.PrimaryScreen.Bounds.Size;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            BackgroundImage = Image.FromFile(@"C:\Users\Kroflex\Desktop\Magic-Tower\MagicTower\MagicTower\Sprites\Backgrounds\StartScreenBackground.jpg");
        }
        
    }
}