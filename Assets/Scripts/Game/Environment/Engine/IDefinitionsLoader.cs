namespace Game.Assets.Scripts.Game.Environment.Engine
{
    public interface IDefinitionsLoader
    {
        string LoadResourceTextfile(string path);
        string[] GetDefintionPaths(string folder);
    }
}
