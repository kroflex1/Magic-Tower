namespace MagicTower.Model.EnemiesModels
{
    public class LittleDemon:Enemy
    {
        public override event EnemyHandler CreateNewEnemy;
        public LittleDemon(int posX, int posY) : base(posX, posY, 32, 48, 2, 4, 1)
        {
            Score = 50;
        }
        
    }
}