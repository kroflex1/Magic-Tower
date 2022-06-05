using System;
using System.Collections.Generic;
using System.Linq;
using MagicTower.Model.Magic;

namespace MagicTower.Model.MagicModels
{
    public class DuplicateSphere : Magic
    {
        public override event MagicHandler CreateNewMagic;

        private const int DegreeOfDeceleration = 1;
        private IReadOnlyList<Type> magicAllowedForDuplication;

        public DuplicateSphere(int startX, int startY, int endX, int endY) : base(startX, startY, endX, endY,
            150, 150, 2, 0, 2, 1000)
        {
            SetMagicAllowedForDuplication();
        }

        public override void OnCollisionEnter(IGameObject gameObject)
        {
            if (gameObject is Magic && magicAllowedForDuplication.Contains(gameObject.GetType()))
            {
                CurrentCondition = Condition.Destroyed;
                foreach (var copiedMagic in CreatDuplicatesOfMagic((Magic) gameObject))
                    if (CreateNewMagic != null)
                        CreateNewMagic(copiedMagic);
            }
        }


        public override void TakeStep()
        {
            if (speed - DegreeOfDeceleration >= 0)
                DirectionVector.SetLength(speed - DegreeOfDeceleration);
            PosX += DirectionVector.X;
            PosY += DirectionVector.Y;
        }

        private IEnumerable<Magic> CreatDuplicatesOfMagic(Magic magic)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x != 0 || y != 0)
                        yield return (Magic) Activator.CreateInstance(magic.GetType(), PosX, PosY,
                            PosX + x,
                            PosY + y);
                }
            }
        }

        private void SetMagicAllowedForDuplication()
        {
            magicAllowedForDuplication = new List<Type>()
            {
                typeof(FireBall),
                typeof(IceBall)
            };
        }
    }
}