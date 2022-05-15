using System.Collections.Generic;
using MagicTower.Model.EnemiesModels;
using MagicTower.Model.Magic;

namespace MagicTower.Model
{
    public class Game
    {
        public Player Player { get; }
        public Room CurrentRoom { get; private set; }
        private int[] windowSize;
        private List<Room> rooms;

        public Game(int windowWidth, int windowHeight)
        {
            windowSize = new[] {windowWidth, windowHeight};
            Player = new Player(0, 0, windowWidth, windowHeight);
            SetRooms();
        }

        public void Update()
        {
            CurrentRoom.Update();
        }

        public void SpawnMagic(int tagetX, int targetY)
        {
            CurrentRoom.SpawnMagic(tagetX,targetY);
        }   

        public void SpawnEnemy(int posX, int posY)
        {
            CurrentRoom.SpawnEnemy(posX,posY);
        }

        private void SetRooms()
        {
            var room = new Room(windowSize[0], windowSize[1], Player);
            rooms = new List<Room>() {room};
            CurrentRoom = rooms[0];
        }
    }
}