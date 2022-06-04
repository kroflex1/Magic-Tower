using System;
using System.Security.Cryptography.X509Certificates;

namespace MagicTower.Model
{
    public class Vector
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Vector(int startX, int startY, int endX, int endY)
        {
            X = endX - startX;
            Y = endY - startY;
        }

        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void SetLength(int length)
        {
            BecomeUnitVector();
            X = length * X;
            Y = length * Y;
        }

        public void BecomeUnitVector()
        {
            var vectorLength = Math.Sqrt(X * X + Y * Y);
            X = (int) Math.Round(X / vectorLength);
            Y = (int)Math.Round(Y / vectorLength);
        }
    }
}