using MagicTower.Model.EnemiesModels;

namespace MagicTower.Model.Tests
{
    public class TestEnemy : Enemy
    {
        public override event EnemyHandler CreateNewEnemy;

        public TestEnemy(int posX, int posY) : base(posX, posY, 10, 10, 1, 1, 1)
        {
        }
    }
}