using System.Collections.Generic;
using MagicTower.Model.EnemiesModels;
using MagicTower.Model.Magic;

namespace MagicTower.Model
{
    public class Game
    {
        public Player Player { get; private set; }
        public Room CurrentRoom { get; private set; }
        public int IntervalBetweenWaves { get; private set; }

        public int CurrentRateOfFire { get => Player.CurrentRateOfFire; }
        
        private int[] windowSize;
        private Arena arena;
        private TreasureRoom treasureRoom;
        private int currentDifficulty;
        private int scoreForTreasureRoom;
       

        public Game(int windowWidth, int windowHeight)
        {
            windowSize = new[] {windowWidth, windowHeight};

            Player = new Player(600, 500, windowWidth, windowHeight);
            arena = new Arena(windowSize[0], windowSize[1], Player);
            treasureRoom = new TreasureRoom(windowSize[0], windowSize[1], Player);
            CurrentRoom = arena;

            IntervalBetweenWaves = 2000;
            currentDifficulty = 2;
            scoreForTreasureRoom = 400;
        }

        public void Update()
        {
            if (CurrentRoom is Arena && arena.Score >= scoreForTreasureRoom)
            {
                CurrentRoom = treasureRoom;
                treasureRoom.UpdateTreasures();
                scoreForTreasureRoom *= 2;
            }
            
            else if (CurrentRoom is TreasureRoom && treasureRoom.IsEmpty)
            {
                CurrentRoom = arena;
                arena.DestroyAllEnemies();
            }
            CurrentRoom.Update();
        }
        
        public void SummonWaveOfEnemies()
        {
            if (CurrentRoom is Arena)
                arena.SpawnRandomEnemies(currentDifficulty * 2);
        }

        public int GetScore()
        {
            return arena.Score;
        }

        public void SpawnMagic()
        {
            var newMagic = Player.CreateMagic();
            if(newMagic != null)
                CurrentRoom.SpawnMagic(Player.CreateMagic());
        }

        public void Restart()
        {
            Player = new Player(600, 500,windowSize[0] , windowSize[1]);
            arena = new Arena(windowSize[0], windowSize[1], Player);
            treasureRoom = new TreasureRoom(windowSize[0], windowSize[1], Player);
            CurrentRoom = arena;

            IntervalBetweenWaves = 2000;
            currentDifficulty = 2;
            scoreForTreasureRoom = 400;
        }
    }
}