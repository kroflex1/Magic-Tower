using System;
using System.Collections.Generic;
using System.Linq;
using MagicTower.Model.EnemiesModels;
using MagicTower.Model.Magic;

namespace MagicTower.Model
{
    public class Room
    {
        public readonly int Width;
        public readonly int Height;
        public readonly List<Magic.Magic> MagicInRoom;
        public readonly List<Enemy> AliveEnemiesInRoom;
        public Player Player { get;}
       
        private readonly List<Magic.Magic> destroyedMagic;
        private readonly List<Enemy> destroyedEnemies;

        public Room(int width, int height, Player player)
        {
            Width = width;
            Height = height;
            Player = player;
            MagicInRoom = new List<Magic.Magic>();
            AliveEnemiesInRoom = new List<Enemy>();
            destroyedMagic = new List<Magic.Magic>();
            destroyedEnemies = new List<Enemy>();
        }

        public void Update()
        {
            ChangeGameObjectsPosition();
            CollisionController.CheckGameObjectsForCollisions(this);
            DeleteAllExcessGameObjects();
        }

        public void SpawnMagic(int tagetX, int targetY)
        {
            MagicInRoom.Add(new FireBall(Player.PosX, Player.PosY, tagetX, targetY));
        }
        
        public void SpawnEnemy(int posX, int posY)
        {
            AliveEnemiesInRoom.Add(new Demon(posX, posY));
        }

        private bool InBounds(int x, int y)
        {
            if (x >= 0 && x <= Width && y >= 0 && y <= Height)
                return true;
            return false;
        }


        private void ChangeGameObjectsPosition()
        {
            Player.Move();
            foreach (var magic in MagicInRoom)
                magic.TakeStep();
            foreach (var enemy in AliveEnemiesInRoom)
                enemy.MoveTo(Player.PosX, Player.PosY);
        }

        private void DeleteAllExcessGameObjects()
        {
            FindDestroydMagic();
            FindDestroydEnemies();
            foreach (var magic in destroyedMagic)
                MagicInRoom.Remove(magic);
            foreach (var deadEnemy in destroyedEnemies)
                AliveEnemiesInRoom.Remove(deadEnemy);
        }

        private void FindDestroydMagic()
        {
            foreach (var magic in MagicInRoom)
            {
                if (magic.CurrentCondition == Condition.Destroyed || !InBounds(magic.PosX, magic.PosY))
                    destroyedMagic.Add(magic);
            }
        }

        private void FindDestroydEnemies()
        {
            foreach (var enemy in AliveEnemiesInRoom)
            {
                if (enemy.CurrentCondition == Condition.Destroyed)
                    destroyedEnemies.Add(enemy);
            }
        }
    }
}