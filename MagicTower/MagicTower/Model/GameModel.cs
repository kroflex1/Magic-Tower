using System.Collections.Generic;

namespace MagicTower.Model
{
    public class GameModel
    { 
        public Player Player { get;}

        public GameModel()
        {
            Player = new Player(10, 5);
        }
        
        
    }
}