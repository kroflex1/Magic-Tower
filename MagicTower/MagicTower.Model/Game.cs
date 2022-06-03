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
            Player = new Player(600, 500, windowWidth, windowHeight);
            SetRooms();
            SpawnEnemy(50, 50);
        }

        public void Update()
        {
            CurrentRoom.Update();
        }

        public void SpawnMagic(int tagetX, int targetY)
        {
            Player.AttackTo(tagetX, targetY);
        }

        public void SpawnEnemy(int posX, int posY)
        {
            CurrentRoom.SpawnEnemy(new Demon(100,100));
        }
        
        
        private void SetRooms()
        {
            var room = new Room(windowSize[0], windowSize[1], Player);
            rooms = new List<Room>() {room};
            CurrentRoom = rooms[0];
        }
    }
}