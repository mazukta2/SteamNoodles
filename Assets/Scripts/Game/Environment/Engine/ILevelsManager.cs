using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using System;

namespace Game.Assets.Scripts.Game.Environment.Engine
{
    public interface ILevelsManager
    {
        void Load(GameLevel model, LevelDefinition prototype, Action<ILevel> onFinished);
        void Unload();
        ILevel GetCurrentLevel();
    }
}
