namespace MagicTower.Model.EnemiesModels
{
    public class Demon : Enemy
    {
        public Demon(int posX, int posY) : base(posX, posY,
            32, 36, 5, 0, 1)
        {
        }
    }
}