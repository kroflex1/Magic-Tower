using System.Collections.Generic;

namespace MagicTower.Model
{
    public class Level
    {
        public readonly List<Room> Rooms;

        public Level(List<Room> rooms)
        {
            Rooms = rooms;
        }
    }
}