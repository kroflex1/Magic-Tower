namespace MagicTower.Model.EnemiesModels
{
    public class Demon : Enemy
    {
        public Demon(int posX, int posY, Room currentRoom) : base(posX, posY,
            currentRoom, 32, 36, 5, 0, 1)
        {
        }
    }
}