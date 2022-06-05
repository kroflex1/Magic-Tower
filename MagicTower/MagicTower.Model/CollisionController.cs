using System.Collections.Generic;
using MagicTower.Model.Magic;

namespace MagicTower.Model
{
    public static class CollisionController
    {
        public static void CheckGameObjectsForCollisions(Room arena)
        {
            FindCollisionBetweenPlayerAndItems(arena);
            FindCollisionBetweenMagicsAndMagic(arena);
            FindCollisionBetweenMagicsAndEnemies(arena);
            FindCollisionBetweenPlayerAndEnemies(arena);
        }

        private static void FindCollisionBetweenMagicsAndMagic(Room arena)
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

        private static void FindCollisionBetweenMagicsAndEnemies(Room arena)
        {
            if (arena.AliveEnemiesInRoom.Count != 0)
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
        }

        private static void FindCollisionBetweenPlayerAndEnemies(Room arena)
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

        private static void FindCollisionBetweenPlayerAndItems(Room arena)
        {
            var playerRectangle = new Rectangle(arena.Player.PosX, arena.Player.PosY, arena.Player.HitboxWidth,
                arena.Player.HitboxHeight);
            foreach (var item in arena.ItemsInRoom)
            {
                var itemRectangle = new Rectangle(item.PosX, item.PosY, item.HitboxWidth, item.HitboxHeight);
                if (IsIntersection(playerRectangle, itemRectangle))
                {
                    item.OnCollisionEnter(arena.Player);
                    arena.Player.OnCollisionEnter(item);
                }
            }
        }

        /*private static void FindCollisionBetweenPlayerAndGameObjects<T>(Player player, List<IGameObject> gameObjects)
        {
            var playerRectangle = new Rectangle(player.PosX, player.PosY, player.HitboxWidth,
                player.HitboxHeight);
            foreach (var gameObject in gameObjects)
            {
                var gameObjectRectangle = new Rectangle(gameObject.PosX, gameObject.PosY, gameObject.HitboxWidth, gameObject.HitboxHeight);
                if (IsIntersection(playerRectangle, gameObjectRectangle))
                {
                    gameObject.OnCollisionEnter(player);
                    player.OnCollisionEnter(gameObject);
                }
            }
        }
        
        private static void FindCollisionBetweenGameObjects( List<IGameObject> gameObjectsFirst, List<IGameObject> gameObjectsSecond)
        {
            foreach (var firstObject in gameObjectsFirst)
            {
                var firstObjectRectangle = new Rectangle(firstObject.PosX, firstObject.PosY, firstObject.HitboxWidth, firstObject.HitboxHeight);
                foreach (var secondObject in gameObjectsSecond)
                {
                    var secondObjectRectangle = new Rectangle(secondObject.PosX, secondObject.PosY, secondObject.HitboxWidth, secondObject.HitboxHeight);
                    if (IsIntersection(secondObjectRectangle, firstObjectRectangle))
                    {
                        firstObject.OnCollisionEnter(secondObject);
                        secondObject.OnCollisionEnter(firstObject);
                    }
                }
            }
        }*/

        public static bool IsIntersection(Rectangle firstRectangle, Rectangle secondRectangle)
        {
            return !(firstRectangle.Bottom < secondRectangle.Top || firstRectangle.Right < secondRectangle.Left ||
                     secondRectangle.Bottom < firstRectangle.Top || secondRectangle.Right < firstRectangle.Left);
        }
    }
}