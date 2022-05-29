namespace MagicTower.Model.Magic
{
    public interface ICanSpawnMagic
    {
        delegate void MagicHandler(MagicModels.Magic magic);
        event MagicHandler OnCreateNewMagic;
        int X { get; set; }
    }
}