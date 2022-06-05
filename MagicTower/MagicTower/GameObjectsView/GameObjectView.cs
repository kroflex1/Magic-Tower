using System;
using System.Collections.Generic;
using System.Drawing;
using MagicTower.Model;

namespace MagicTower
{
    public abstract class GameObjectView
    {
        protected Game gameModel;
        protected Dictionary<Type, Image> imagesForGameObjects;

        public GameObjectView(Game gameModel)
        {
            this.gameModel = gameModel;
            SetImagesForGameObjects();
        }
        
        public virtual void Draw(Graphics graphics)
        {
        }

        protected virtual void SetImagesForGameObjects()
        {
        }
    }
}