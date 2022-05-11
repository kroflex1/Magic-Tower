using MagicTower.Model.EnemiesModels;

namespace MagicTower.Model.Tests
{
    public class TestEnemy:Enemy
    {
        public TestEnemy(int posX, int posY, Room currentRoom) : base(posX, posY, currentRoom, 10, 10, 1, 1, 1)
        {
        }
    }
}