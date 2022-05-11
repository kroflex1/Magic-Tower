using System;
using System.CodeDom;
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
        public readonly List<Magic.Magic> ShouldDisappearMagic;
        public readonly List<Enemy> DeadEnemiesInRoom;

        public Room(int width, int height)
        {
            Width = width;
            Height = height;
            MagicInRoom = new List<Magic.Magic>();
            AliveEnemiesInRoom = new List<Enemy>();
            ShouldDisappearMagic = new List<Magic.Magic>();
            DeadEnemiesInRoom = new List<Enemy>();
        }

        public void Update()
        {
            foreach (var magic in MagicInRoom)
                magic.TakeStep();
            foreach (var enemy in AliveEnemiesInRoom)
                enemy.TakeStep();
            CollisionController.CheckForCollisions(MagicInRoom, AliveEnemiesInRoom);

            foreach (var magic in ShouldDisappearMagic)
                MagicInRoom.Remove(magic);
            foreach (var deadEnemy in DeadEnemiesInRoom)
                AliveEnemiesInRoom.Remove(deadEnemy);
        }

        public bool InBounds(int x, int y)
        {
            if (x >= 0 && x <= Width && y >= 0 && y <= Height)
                return true;
            return false;
        }
    }
}