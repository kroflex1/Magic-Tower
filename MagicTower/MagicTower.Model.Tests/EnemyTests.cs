using NUnit.Framework;

namespace MagicTower.Model.Tests
{
    [TestFixture]
    public class EnemyTests
    {
        private const int roomWidth = 1920;
        private const int roomHeight = 1080;

        [TestCase(1, 1, 2, 1)]
        [TestCase(1, 1, 2, 2)]
        [TestCase(1, 1, 1, 1)]
        public void EnemyGoToVeryCloseTarget(int enemyPosX, int enemyPosY, int targetPosX, int targetPosY)
        {
            var enemy = new TestEnemy(enemyPosX, enemyPosY);
            enemy.UpdatePlayerPosition(targetPosX, targetPosY);
            enemy.Move();
            Assert.AreEqual((targetPosX, targetPosY), (enemy.PosX, enemy.PosY));
        }

        [TestCase(5, 5, 6, 5, 6, 5)]
        [TestCase(5, 5, 0, 5, 4, 5)]
        [TestCase(5, 5, 5, 10, 5, 6)]
        [TestCase(5, 5, 5, 0, 5, 4)]
        public void ShouldGoToPlayer(int enemyPosX, int enemyPosY, int targetPosX, int targetPosY,
            int endPosX, int endPosY)
        {
            var enemy = new TestEnemy(enemyPosX, enemyPosY);
            enemy.Speed = 1;
            enemy.UpdatePlayerPosition(targetPosX, targetPosY);
            enemy.Move();
            Assert.AreEqual((endPosX, endPosY), (enemy.PosX, enemy.PosY));
        }
        
    }
}