using System.Collections.Generic;

namespace MagicTower.Model
{
    public class Room
    {
        public readonly int Width;
        public readonly int Height;
        private List<Magic.Magic> allMagicInRoom;
        private Player player;

        public Room(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public void AddNewMagic(Magic.Magic magic)
        {
            allMagicInRoom.Add(magic);
        }
    }
}