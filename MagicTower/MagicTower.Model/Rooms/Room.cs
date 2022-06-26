using System;
using System.Collections.Generic;
using MagicTower.Model.EnemiesModels;
using MagicTower.Model.Items;
using MagicTower.Model.Magic;

namespace MagicTower.Model
{
    public abstract class Room
    {
        protected readonly int width;
        protected readonly int height;
        public Player Player { get; }

        public readonly List<MagicModels.Magic> MagicInRoom;
        public readonly List<Enemy> AliveEnemiesInRoom;
        public readonly List<Item> ItemsInRoom;
        
        protected readonly List<Item> shouldAddToRoomItems;
        protected readonly List<MagicModels.Magic> shouldAddToRoomMagic;
        protected readonly List<Enemy> shouldAddToRoomEnemies;
        protected readonly List<Item> destroyedItems;
        protected readonly List<MagicModels.Magic> destroyedMagic;
        protected readonly List<Enemy> destroyedEnemies;
        
        public Room(int width, int height, Player player)
        {
            this.width = width;
            this.height = height;
            Player = player;
            
            ItemsInRoom = new List<Item>();
            MagicInRoom = new List<MagicModels.Magic>();
            AliveEnemiesInRoom = new List<Enemy>();
            
            shouldAddToRoomItems = new List<Item>();
            shouldAddToRoomMagic = new List<MagicModels.Magic>();
            shouldAddToRoomEnemies = new List<Enemy>();

            destroyedItems = new List<Item>();
            destroyedMagic = new List<MagicModels.Magic>();
            destroyedEnemies = new List<Enemy>();
        }

        public virtual void Update()
        {
            AddShouldGameObjectsToRoom();
            ChangeGameObjectsPosition();
            CollisionController.CheckGameObjectsForCollisions(this);
            DeleteAllExcessGameObjects();
        }
        
        public void SpawnItem(Item item)
        {
            shouldAddToRoomItems.Add(item);
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
            enemy.CreateNewItem += SpawnItem;
            Player.OnChangePosition += enemy.UpdatePlayerPosition;
        }
        

        protected virtual void AddShouldGameObjectsToRoom()
        {
            MoveGameObjectsFromListToAnother(shouldAddToRoomItems, ItemsInRoom);
            MoveGameObjectsFromListToAnother(shouldAddToRoomMagic, MagicInRoom);
            MoveGameObjectsFromListToAnother(shouldAddToRoomEnemies, AliveEnemiesInRoom);
        }

        protected virtual void ChangeGameObjectsPosition()
        {
            Player.Move();
            foreach (var magic in MagicInRoom)
                magic.TakeStep();
            foreach (var enemy in AliveEnemiesInRoom)
                enemy.Move();
        }

        protected virtual void DeleteAllExcessGameObjects()
        {
            FindDestroyedItems();
            FindDestroyedMagic();
            FindDestroyedEnemies();
            foreach (var item in destroyedItems)
                ItemsInRoom.Remove(item);
            foreach (var magic in destroyedMagic)
                MagicInRoom.Remove(magic);
            foreach (var deadEnemy in destroyedEnemies)
            {
                AliveEnemiesInRoom.Remove(deadEnemy);
                Player.OnChangePosition -= deadEnemy.UpdatePlayerPosition;
            }
        }
        
        protected bool InBounds(int x, int y)
        {
            if (x >= 0 && x <= width && y >= 0 && y <= height)
                return true;
            return false;
        }
        
        protected virtual void FindDestroyedItems()
        {
            foreach (var item in ItemsInRoom)
            {
                if (item.CurrentCondition == Condition.Destroyed)
                    destroyedItems.Add(item);
            }
        }

        protected virtual void FindDestroyedMagic()
        {
            foreach (var magic in MagicInRoom)
            {
                if (magic.CurrentCondition == Condition.Destroyed || !InBounds(magic.PosX, magic.PosY))
                    destroyedMagic.Add(magic);
            }
        }

        protected virtual void FindDestroyedEnemies()
        {
            foreach (var enemy in AliveEnemiesInRoom)
            {
                if (enemy.CurrentCondition == Condition.Destroyed)
                    destroyedEnemies.Add(enemy);
            }
        }
        private void MoveGameObjectsFromListToAnother<T>(List<T> from, List<T> to)
        {
            to.AddRange(from);
            from.Clear();
        }
    }
}