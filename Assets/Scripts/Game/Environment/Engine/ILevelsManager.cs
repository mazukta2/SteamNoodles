﻿using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using System;

namespace Game.Assets.Scripts.Game.Environment.Engine
{
    public interface ILevelsManager
    {
        void Load(ILevelDefinition prototype, Action<ILevel> onFinished);
        void Unload();
        ILevel GetCurrentLevel();
    }
}
