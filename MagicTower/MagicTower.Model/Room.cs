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
        public readonly List<MagicModels.Magic> MagicInRoom;
        public readonly List<Enemy> AliveEnemiesInRoom;
        public Player Player { get;}
       
        private readonly List<MagicModels.Magic> destroyedMagic;
        private readonly List<Enemy> destroyedEnemies;

        public Room(int width, int height, Player player)
        {
            Width = width;
            Height = height;
            
            Player = player;
            Player.OnCreateNewMagic += SpawnMagic;
            
            MagicInRoom = new List<MagicModels.Magic>();
            AliveEnemiesInRoom = new List<Enemy>();
            destroyedMagic = new List<MagicModels.Magic>();
            destroyedEnemies = new List<Enemy>();
        }

        public void Update()
        {
            ChangeGameObjectsPosition();
            CollisionController.CheckGameObjectsForCollisions(this);
            DeleteAllExcessGameObjects();
        }
        
        
        public void SpawnMagic(MagicModels.Magic magic)
        {
            MagicInRoom.Add(magic);
            magic.CreateNewMagic += SpawnMagic;
        }
        
        public void SpawnEnemy(int posX, int posY)
        {
            var newEnemy = new Demon(posX, posY);
            AliveEnemiesInRoom.Add(newEnemy);
            newEnemy.UpdatePlayerPosition(Player.PosX, Player.PosY);
            Player.OnChangePosition += newEnemy.UpdatePlayerPosition;
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
                enemy.Move();
        }

        private void DeleteAllExcessGameObjects()
        {
            FindDestroydMagic();
            FindDestroydEnemies();
            foreach (var magic in destroyedMagic)
                MagicInRoom.Remove(magic);
            foreach (var deadEnemy in destroyedEnemies)
            {
                AliveEnemiesInRoom.Remove(deadEnemy);
                Player.OnChangePosition -= deadEnemy.UpdatePlayerPosition;
            }
                
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