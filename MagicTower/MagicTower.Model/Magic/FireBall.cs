using System;

namespace MagicTower.Model.Magic
{
    public class FireBall : IMagic
    {
        public double PosX { get; set; }
        public double PosY { get; set; }
        public int Speed { get; set; }
        public int Damage { get; set; }
        public (double X, double Y) DirectionVector { get; set; }

        public FireBall(double startX, double startY, double endX, double endY, int speed)
        {
            CalculateDirectionVector(startX, startY, endX, endY);
            PosX = startX;
            PosY = startY;
            Speed = speed;
            Damage = 5;
        }

        private void CalculateDirectionVector(double startX, double startY, double endX, double endY)
        {
            var vector = (endX - startX, endY - startY);
            var vectorLength = Math.Sqrt(vector.Item1 * vector.Item1 + vector.Item2 * vector.Item2);
            DirectionVector = (vector.Item1 / vectorLength, vector.Item2 / vectorLength);
        }


        public void TakeStepInDirection()
        {
            PosX += DirectionVector.X;
            PosY += DirectionVector.Y;
        }
    }
}