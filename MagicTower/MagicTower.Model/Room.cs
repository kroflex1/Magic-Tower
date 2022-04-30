using System;
using System.CodeDom;
using System.Collections.Generic;
using MagicTower.Model.Magic;

namespace MagicTower.Model
{
    public class Room
    {
        public readonly int Width;
        public readonly int Height;
        public readonly List<Magic.Magic> allMagicInRoom;
        public readonly List<Magic.Magic> beyondBoundsMagic;

        public Room(int width, int height)
        {
            Width = width;
            Height = height;
            allMagicInRoom = new List<Magic.Magic>();
            beyondBoundsMagic = new List<Magic.Magic>();
        }

        public void Update()
        {
            foreach (var magic in allMagicInRoom)
                magic.TakeStep();
            foreach (var magic in beyondBoundsMagic)
                allMagicInRoom.Remove(magic);
        }

        public bool InBounds(int x, int y)
        {
            if (x >= 0 && x <= Width && y >= 0 && y <= Height)
                return true;
            return false;
        }
    }
}