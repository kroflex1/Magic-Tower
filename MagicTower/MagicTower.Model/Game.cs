using System.Collections.Generic;

namespace MagicTower.Model
{
    public class Game
    {
        public Player Player { get; }
        private int[] windowSize;
        private List<Level> levels;
        private Room currentRoom;

        public Game(int windowWidth, int windowHeight)
        {
            windowSize = new[] {windowWidth, windowHeight};
            SetLevels();
            Player = new Player(0, 0, 10, 1, currentRoom);
        }

        private void SetLevels()
        {
            var room = new Room(windowSize[0], windowSize[1]);
            levels = new List<Level>() {new Level(new List<Room>() {room})};
            currentRoom = levels[0].Rooms[0];
        }

    }
}

