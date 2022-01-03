using Assets.Scripts.Core;
using Assets.Scripts.Core.Prototypes;
using Assets.Scripts.Views.Buildings;
using Assets.Scripts.Views.Buildings.Grid;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Game;
using Game.Assets.Scripts.Game.Logic.Views.Levels;
using Game.Assets.Scripts.Game.Logic.Views.Units;
using GameUnity.Assets.Scripts.Unity.Views.Common;
using GameUnity.Assets.Scripts.Unity.Views.Orders;
using GameUnity.Assets.Scripts.Unity.Views.Units;
using System;
using Tests.Assets.Scripts.Game.Logic.Views;
using UnityEngine;

namespace Assets.Scripts.Views.Levels
{
    public class LevelResourcesView : ViewMonoBehaviour, ILevelResourcesView
    {
        [SerializeField] NumberView _money;
        public IIntValueView Money => _money;
    }
}