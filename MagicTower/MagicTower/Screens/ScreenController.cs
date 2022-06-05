using System;
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
            pauseScreen = new PauseScreen();
            ConnectScreens();
        }

        public void Run()
        {
            Application.Run(startScreen);
        }

        private void ConnectScreens()
        {
            if (gameScreen == null || startScreen == null || pauseScreen == null)
                throw new ArgumentException("Экраны не могут быть нулевыми");
            startScreen.SetGameScreen(gameScreen);
            pauseScreen.SetStartAndGameScreen(startScreen, gameScreen);
            gameScreen.SetPauseScreen(pauseScreen);
            gameScreen.SetStartScreen(startScreen);
        }
    }
}