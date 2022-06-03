using System.Collections;
using System.Collections.Generic;

namespace MagicTower.Model.EnemiesModels
{
    public class Demon : Enemy
    {
        public override event EnemyHandler CreateNewEnemy;

        public Demon(int posX, int posY) : base(posX, posY,
            32, 36, 6, 5, 1)
        {
        }

        protected override void Die()
        {
            base.Die();
            if (CreateNewEnemy != null)
            {
                foreach (var littleDemon in CreateLittleDemons())
                    CreateNewEnemy(littleDemon);
            }
        }

        private IEnumerable<LittleDemon> CreateLittleDemons()
        {
            var amountDemons = 3;
            var disntanceFormDemon = 20;
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (amountDemons > 0 && (x != 0 || y != 0))
                    {
                        yield return new LittleDemon(PosX + x * 20, PosY + y * disntanceFormDemon);
                        amountDemons--;
                    }
                }
            }
        }
    }
}