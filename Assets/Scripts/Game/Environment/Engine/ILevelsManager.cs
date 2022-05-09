using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using System;

namespace Game.Assets.Scripts.Game.Environment.Engine
{
    public interface ILevelsManager
    {
        void Load(GameLevel model, LevelDefinition prototype, Action<ILevelView> onFinished);
        void Unload();
        LevelView GetCurrentLevel();

        static ILevelsManager Default { get; set; }
    }
}
