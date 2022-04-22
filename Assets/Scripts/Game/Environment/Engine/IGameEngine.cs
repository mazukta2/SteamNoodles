﻿using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Presenters.Localization;
using System;

namespace Game.Assets.Scripts.Game.External
{
    public interface IGameEngine
    {
        ILevelsManager Levels { get; }
        GameTime Time { get; }
        void Dispose();
    }
}
