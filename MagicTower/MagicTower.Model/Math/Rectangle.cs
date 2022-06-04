namespace MagicTower.Model
{
    public class Rectangle
    {
        public readonly int Left;
        public readonly int Top;
        public readonly int Right;
        public readonly int Bottom;

        public Rectangle(int x, int y, int width, int height)
        {
            Left = x;
            Top = y;
            Right = x + width;
            Bottom = y + height;
        }
    }
}