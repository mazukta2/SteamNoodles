﻿using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using System;

namespace Game.Assets.Scripts.Game.Environment.Engine
{
    public interface ILevelsManager
    {
        event Action OnLoadFinished;
        void Load(string name, IViews views);
        void Unload();
    }
}
