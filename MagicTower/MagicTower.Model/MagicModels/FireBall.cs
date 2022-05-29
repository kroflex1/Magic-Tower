namespace MagicTower.Model.MagicModels
{
    public class FireBall : MagicModels.Magic
    {
        public override event MagicHandler CreateNewMagic;
        public FireBall(int startX, int startY, int endX, int endY) : base(startX, startY,
            endX, endY, 10, 10, 5, 1)
        {
        }
    }
}