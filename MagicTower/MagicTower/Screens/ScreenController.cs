using System.Drawing;
using System.Windows.Forms;

namespace MagicTower
{
    public class ScreenController
    {
        private readonly StartScreen startScreen;
        private readonly GameScreen gameScreen;
        private readonly PauseScreen pauseScreen;

        public ScreenController()
        {
            gameScreen = new GameScreen();
            startScreen = new StartScreen();
            pauseScreen = new PauseScreen(startScreen, gameScreen);
            startScreen.Closed +=(sender, args) =>
            {
                gameScreen.Show();
            };
        }

        public void Run()
        {
            Application.Run(gameScreen);
        }
    }
}