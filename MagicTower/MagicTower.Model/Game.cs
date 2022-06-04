using System.Collections.Generic;
using MagicTower.Model.EnemiesModels;
using MagicTower.Model.Magic;

namespace MagicTower.Model
{
    public class Game
    {
        public Player Player { get; }
        public Arena Arena { get; private set; }
        public int IntervalBetweenWaves { get; private set; }
        private int[] windowSize;
        private int currentDifficulty;

        public Game(int windowWidth, int windowHeight)
        {
            windowSize = new[] {windowWidth, windowHeight};
            Player = new Player(600, 500, windowWidth, windowHeight);
            Arena = new Arena(windowSize[0], windowSize[1], Player);
            IntervalBetweenWaves = 3000;
            currentDifficulty = 1;
        }

        public void Update()
        {
            Arena.Update();
        }

        public void SpawnMagic(int targetX, int targetY)
        {
            Player.AttackTo(targetX, targetY);
        }

        public void SummonWaveOfEnemies()
        {
            Arena.SpawnRandomEnemies(currentDifficulty * 2);
        }
        
    }
}