using System;
using System.Collections.Generic;
using System.Linq;
using MagicTower.Model.EnemiesModels;
using MagicTower.Model.Items;
using MagicTower.Model.Magic;

namespace MagicTower.Model
{
    public class Arena : Room
    {
        public int Score { get; private set; }
        
        private List<Type> typesOfEnemies;
        private Random random;

        public Arena(int width, int height, Player player) : base(width, height, player)
        {
            SetAvailableTypesOfEnemies();
            random = new Random();
        }
        
        public void SpawnRandomEnemies(int numberOfEnemies)
        {
            for (int i = 0; i < numberOfEnemies; i++)
            {
                var spawnPoint = GerRandomSpawnPointForEnemy();
                var enemy = (Enemy) Activator.CreateInstance(typesOfEnemies[random.Next(0, typesOfEnemies.Count)],
                    spawnPoint.X, spawnPoint.Y);
                SpawnEnemy(enemy);
            }
        }

        public void DestroyAllEnemies()
        {
            AliveEnemiesInRoom.Clear();
            shouldAddToRoomEnemies.Clear();
        }
        
        protected override void FindDestroyedEnemies()
        {
            foreach (var enemy in AliveEnemiesInRoom)
            {
                if (enemy.CurrentCondition == Condition.Destroyed)
                {
                    Score += enemy.Score;
                    destroyedEnemies.Add(enemy);
                }
            }
        }
        
        private void SetAvailableTypesOfEnemies()
        {
            typesOfEnemies = new List<Type>()
            {
                typeof(Demon),
                typeof(LittleDemon)
            };
        }

        private (int X, int Y) GerRandomSpawnPointForEnemy()
        {
            var border = 100;
            if (random.Next(0, 2) == 0)
            {
                var x = random.Next(0, width);
                var y = random.GetRandomNumberFromTwoRange((-border * 2, -border), (height, height + border));
                return (x, y);
            }
            else
            {
                var x = random.GetRandomNumberFromTwoRange((-border * 2, -border), (width, width + border));
                var y = random.Next(0, height);
                return (x, y);
            }
        }
    }

    public static class RandomExpansion
    {
        public static int GetRandomNumberFromTwoRange(this Random random, (int minValue, int maxValue) firstRange,
            (int minValue, int maxValue) secondRange)
        {
            if (firstRange.maxValue > secondRange.minValue)
                throw new ArgumentException(
                    "Максимальное значение первого диапозона долно быть меньша минимального значения второго диапозона");
            if (random.Next(0, 2) == 0)
                return random.Next(firstRange.minValue, firstRange.maxValue + 1);
            return random.Next(secondRange.minValue, secondRange.maxValue + 1);
        }
    }
}