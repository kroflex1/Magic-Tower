using System;
using System.Collections.Generic;
using System.Linq;
using MagicTower.Model.EnemiesModels;
using MagicTower.Model.Magic;

namespace MagicTower.Model
{
    public class Arena : Room
    {
        public readonly List<MagicModels.Magic> MagicInRoom;
        public readonly List<Enemy> AliveEnemiesInRoom;

        private List<Type> typesOfEnemies;
        private readonly List<MagicModels.Magic> shouldAddToRoomMagic;
        private readonly List<Enemy> shouldAddToRoomEnemies;
        private readonly List<MagicModels.Magic> destroyedMagic;
        private readonly List<Enemy> destroyedEnemies;
        private Random random;

        public Arena(int width, int height, Player player) : base(width, height, player)
        {
            Player.OnCreateNewMagic += SpawnMagic;

            SetAvailableTypesOfEnemies();
            MagicInRoom = new List<MagicModels.Magic>();
            AliveEnemiesInRoom = new List<Enemy>();

            shouldAddToRoomMagic = new List<MagicModels.Magic>();
            shouldAddToRoomEnemies = new List<Enemy>();

            destroyedMagic = new List<MagicModels.Magic>();
            destroyedEnemies = new List<Enemy>();

            random = new Random();
        }

        public override void Update()
        {
            AddShouldGameObjectsToRoom();
            ChangeGameObjectsPosition();
            CollisionController.CheckGameObjectsForCollisions(this);
            DeleteAllExcessGameObjects();
        }

        public void SpawnMagic(MagicModels.Magic magic)
        {
            shouldAddToRoomMagic.Add(magic);
            magic.CreateNewMagic += SpawnMagic;
        }

        public void SpawnEnemy(Enemy enemy)
        {
            shouldAddToRoomEnemies.Add(enemy);
            enemy.UpdatePlayerPosition(Player.PosX, Player.PosY);
            enemy.CreateNewEnemy += SpawnEnemy;
            Player.OnChangePosition += enemy.UpdatePlayerPosition;
        }

        private void SetAvailableTypesOfEnemies()
        {
            typesOfEnemies = new List<Type>()
            {
                typeof(Demon),
                typeof(LittleDemon)
            };
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

        private bool InBounds(int x, int y)
        {
            if (x >= 0 && x <= width && y >= 0 && y <= height)
                return true;
            return false;
        }

        private void AddShouldGameObjectsToRoom()
        {
            MoveGameObjectsFromListToAnother(shouldAddToRoomMagic, MagicInRoom);
            MoveGameObjectsFromListToAnother(shouldAddToRoomEnemies, AliveEnemiesInRoom);
        }

        protected override void ChangeGameObjectsPosition()
        {
            Player.Move();
            foreach (var magic in MagicInRoom)
                magic.TakeStep();
            foreach (var enemy in AliveEnemiesInRoom)
                enemy.Move();
        }

        private void DeleteAllExcessGameObjects()
        {
            FindDestroyedMagic();
            FindDestroyedEnemies();
            foreach (var magic in destroyedMagic)
                MagicInRoom.Remove(magic);
            foreach (var deadEnemy in destroyedEnemies)
            {
                AliveEnemiesInRoom.Remove(deadEnemy);
                Player.OnChangePosition -= deadEnemy.UpdatePlayerPosition;
            }
        }

        private void MoveGameObjectsFromListToAnother<T>(List<T> from, List<T> to)
        {
            to.AddRange(from);
            from.Clear();
        }

        private void FindDestroyedMagic()
        {
            foreach (var magic in MagicInRoom)
            {
                if (magic.CurrentCondition == Condition.Destroyed || !InBounds(magic.PosX, magic.PosY))
                    destroyedMagic.Add(magic);
            }
        }

        private void FindDestroyedEnemies()
        {
            foreach (var enemy in AliveEnemiesInRoom)
            {
                if (enemy.CurrentCondition == Condition.Destroyed)
                    destroyedEnemies.Add(enemy);
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