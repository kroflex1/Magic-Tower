using System.Collections.Generic;
using MagicTower.Model.EnemiesModels;
using MagicTower.Model.Magic;

namespace MagicTower.Model
{
    public class Game
    {
        public Player Player { get; }
        public Room currentRoom;
        private int[] windowSize;
        private List<Level> levels;
        
        public Game(int windowWidth, int windowHeight)
        {
            windowSize = new[] {windowWidth, windowHeight};
            SetLevels();
            Player = new Player(0, 0, currentRoom);
        }

        public void SpawnMagic(int tagetX, int targetY)
        {
            currentRoom.MagicInRoom.Add(new FireBall(Player.PosX, Player.PosY,tagetX, targetY));
        }

        public void SpawnEnemy(int posX, int posY)
        {
            currentRoom.AliveEnemiesInRoom.Add(new Demon(posX, posY));
        }

        private void SetLevels()
        {
            var room = new Room(windowSize[0], windowSize[1]);
            levels = new List<Level>() {new Level(new List<Room>() {room})};
            currentRoom = levels[0].Rooms[0];
        }

    }
}

