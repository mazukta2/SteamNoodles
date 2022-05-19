using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.External
{
    public interface IDefinitions
    {
        string LoadResourceTextfile(string path);
        string[] GetDefintionPaths(string folder);
    }
}
