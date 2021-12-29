using Assets.Scripts.Data.Buildings;
using Assets.Scripts.Logic.Prototypes.Levels;
using Assets.Scripts.Views.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Settings.Constructions;
using Game.Assets.Scripts.Game.Logic.Settings.Rewards;
using GameUnity.Assets.Scripts.Unity.Core;
using System;
using System.Collections.Generic;
using Tests.Assets.Scripts.Game.Logic.Views;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rect = Game.Assets.Scripts.Game.Logic.Common.Math.Rect;

namespace GameUnity.Assets.Scripts.Unity.Data.Levels
{
    public class LevelSettings : ILevelSettings
    {
        public Point Size => throw new NotImplementedException();

        public Rect UnitsSpawnRect => throw new NotImplementedException();

        public Dictionary<ICustomerSettings, int> Deck => throw new NotImplementedException();

        public IReward ClashReward => throw new NotImplementedException();

        public int MaxQueue => throw new NotImplementedException();

        public float SpawnQueueTime => throw new NotImplementedException();

        public int NeedToServe => throw new NotImplementedException();

        public IConstructionSettings[] StartingHand => throw new NotImplementedException();

        public int HandSize => throw new NotImplementedException();

        public string SceneName => throw new NotImplementedException();
        //[Scene]
        //public string Scene;

        //public Point Size;
        //public RectInt UnitsRect;
        //public ConstructionSettings[] Hand;
        //public UnitTypeData[] Units;
        //public OrderData[] AvailableOrders;


        //public Settings Get()
        //{
        //    return new Settings(this);
        //}

        //public class Settings : ILevelSettings
        //{
        //    public Point Size { get; private set; }
        //    public Rect UnitsSpawnRect { get; private set; }
        //    public Dictionary<ICustomerSettings, int> Deck { get; private set; }
        //    public IReward ClashReward { get; private set; }
        //    public int MaxQueue { get; private set; }
        //    public float SpawnQueueTime { get; private set; }
        //    public int NeedToServe { get; private set; }
        //    public IConstructionSettings[] StartingHand { get; private set; }
        //    public int HandSize { get; private set; }
        //    public Settings(GameLevelData data)
        //    {
        //        Size = data.Size;
        //        UnitsSpawnRect = new Rect(data.UnitsRect.x, data.UnitsRect.y, data.UnitsRect.width, data.UnitsRect.height);

        //        Deck = new Dictionary<ICustomerSettings, int>();
        //        var units = new Dictionary<UnitTypeData, int>();
        //        foreach (var item in data.Units)
        //            units[item] = units.ContainsKey(item) ? units[item] + 1 : 1;
        //        foreach (var item in units)
        //            Deck[item.Key.Get()] = item.Value;
        //    }

        //}
    }
}
