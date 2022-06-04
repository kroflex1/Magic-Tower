namespace MagicTower.Model.Magic
{
    public interface IGameObject
    {
        int PosX { get; }
        int PosY { get;}
        int HitboxWidth { get;  }
        int HitboxHeight { get;  }
        Condition CurrentCondition { get; }
        void OnCollisionEnter(IGameObject gameObject);
      
    }
}