using System.Collections.Generic;
using MagicTower.Model.EnemiesModels;
using MagicTower.Model.Magic;

namespace MagicTower.Model.MagicModels
{
    public class IceBall : Magic
    {
        public delegate void MagicHandler(Magic magic);
        public event MagicHandler OnCreateNewMagic;

        public IceBall(int startX, int startY, int endX, int endY) : base(startX, startY, endX, endY, 48, 32, 10, 2)
        {
        }

        public override void OnCollisionEnter(IGameObject gameObject)
        {
            if (gameObject is Enemy)
            {
                CurrentCondition = Condition.Destroyed;
                var iceShards = CreateIceShards();
                foreach (var iceShard in iceShards)
                    if (OnCreateNewMagic != null)
                        OnCreateNewMagic(iceShard);
            }
        }

        private IEnumerable<IceShard> CreateIceShards()
        {
            var iceShards = new List<IceShard>();
            for (int x = -1; x <= 1; x += 2)
            {
                for (int y = -1; y <= 1; y += 2)
                    iceShards.Add(new IceShard(PosX, PosY, PosX + x, PosY + y));
            }

            return iceShards;
        }
    }

    public class IceShard : Magic
    {
        public IceShard(int startX, int startY, int endX, int endY) : base(startX, startY, endX, endY, 38, 18, 12, 1)
        {
        }
    }
}