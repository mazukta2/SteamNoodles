namespace Game.Assets.Scripts.Game.External
{
    public interface IDefinitions
    {
        T Get<T>();
        T Get<T>(string id);
    }
}
