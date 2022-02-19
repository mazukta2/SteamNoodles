﻿using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Settings;
using Game.Assets.Scripts.Game.Logic.Settings.Constructions;
using Game.Assets.Scripts.Game.Logic.Settings.Rewards;
using System;
using System.Collections.Generic;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Game.Assets.Scripts.Game.Logic.Prototypes.Levels
{
    public interface ILevelSettings : IPlacementSettings, IUnitsSettings, IClashesSettings, IHandSettings
    {
        string SceneName { get; }
    }

    public interface IHandSettings
    {
        IReadOnlyCollection<IConstructionSettings> StartingHand { get; }
        int HandSize { get; }
    }

    public interface IPlacementSettings
    {
        Point Size { get; }
        FloatPoint Offset { get; }
    }

    public interface IUnitsSettings
    {
        Rect UnitsSpawnRect { get; }
        IReadOnlyDictionary<ICustomerSettings, int> Deck { get; }
    }

    public interface IClashesSettings
    {
        IReward ClashReward { get; }
        int MaxQueue { get; }
        float SpawnQueueTime{ get; }
        int NeedToServe { get; }
    }
}
