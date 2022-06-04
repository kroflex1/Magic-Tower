namespace MagicTower.Model
{
    public static class CollisionController
    {
        public static void CheckGameObjectsForCollisions(Arena arena)
        {
            FindCollisionBetweenMagicsAndMagic(arena);
            FindCollisionBetweenMagicsAndEnemies(arena);
            FindCollisionBetweenPlayerAndEnemies(arena);
        }

        private static void FindCollisionBetweenMagicsAndMagic(Arena arena)
        {
            for (int i = 0; i < arena.MagicInRoom.Count - 1; i++)
            {
                var firstMagicRectangle = new Rectangle(arena.MagicInRoom[i].PosX, arena.MagicInRoom[i].PosY,
                    arena.MagicInRoom[i].HitboxWidth, arena.MagicInRoom[i].HitboxHeight);
                for (int j = i + 1; j < arena.MagicInRoom.Count; j++)
                {
                    var secondMagicRectangle = new Rectangle(arena.MagicInRoom[j].PosX, arena.MagicInRoom[j].PosY,
                        arena.MagicInRoom[j].HitboxWidth, arena.MagicInRoom[j].HitboxHeight);
                    if (IsIntersection(firstMagicRectangle, secondMagicRectangle))
                    {
                        arena.MagicInRoom[i].OnCollisionEnter(arena.MagicInRoom[j]);
                        arena.MagicInRoom[j].OnCollisionEnter(arena.MagicInRoom[i]);
                    }
                }
            }
        }

        private static void FindCollisionBetweenMagicsAndEnemies(Arena arena)
        {
            foreach (var magic in arena.MagicInRoom)
            {
                var magicRectangle = new Rectangle(magic.PosX, magic.PosY, magic.HitboxWidth, magic.HitboxHeight);
                foreach (var enemy in arena.AliveEnemiesInRoom)
                {
                    var enemyRectangle = new Rectangle(enemy.PosX, enemy.PosY, enemy.HitboxWidth, enemy.HitboxHeight);
                    if (IsIntersection(enemyRectangle, magicRectangle))
                    {
                        magic.OnCollisionEnter(enemy);
                        enemy.OnCollisionEnter(magic);
                    }
                }
            }
        }

        private static void FindCollisionBetweenPlayerAndEnemies(Arena arena)
        {
            var playerRectangle = new Rectangle(arena.Player.PosX, arena.Player.PosY, arena.Player.HitboxWidth,
                arena.Player.HitboxHeight);
            foreach (var enemy in arena.AliveEnemiesInRoom)
            {
                var enemyRectangle = new Rectangle(enemy.PosX, enemy.PosY, enemy.HitboxWidth, enemy.HitboxHeight);
                if (IsIntersection(playerRectangle, enemyRectangle))
                {
                    enemy.OnCollisionEnter(arena.Player);
                    arena.Player.OnCollisionEnter(enemy);
                }
            }
        }

        public static bool IsIntersection(Rectangle firstRectangle, Rectangle secondRectangle)
        {
            return !(firstRectangle.Bottom < secondRectangle.Top || firstRectangle.Right < secondRectangle.Left ||
                     secondRectangle.Bottom < firstRectangle.Top || secondRectangle.Right < firstRectangle.Left);
        }

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
}