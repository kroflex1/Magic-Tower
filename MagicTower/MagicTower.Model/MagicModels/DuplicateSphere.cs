/*using System;
using System.Collections.Generic;
using System.Linq;

namespace MagicTower.Model.Magic
{
    public class DuplicateSphere : Magic
    {
        private const int degreeOfDeceleration = 1;
        private IReadOnlyList<Type> magicAllowedForDuplication;

        public DuplicateSphere(int startX, int startY, int endX, int endY) : base(startX, startY, endX, endY,
            32, 32, 10, 0)
        {
            SetMagicAllowedForDuplication();
        }

        public override void OnCollisionEnter(IGameObject gameObject)
        {
            if (gameObject is Magic && magicAllowedForDuplication.Contains(gameObject.GetType()))
            {
                Magic newMagic = (Magic) Activator.CreateInstance();
            }
                
        }
        
        public void OnCollidingMagic()

        public override void TakeStep()
        {
            if (speed - degreeOfDeceleration >= 0)
                DirectionVector.SetLength(speed - degreeOfDeceleration);
            PosX += DirectionVector.X;
            PosY += DirectionVector.Y;
        }

        private void SetMagicAllowedForDuplication()
        {
            magicAllowedForDuplication = new List<Type>()
            {
                typeof(FireBall)
            };
        }
    }
}*/