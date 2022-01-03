using Assets.Scripts.Core;
using Assets.Scripts.Core.Prototypes;
using Assets.Scripts.Views.Buildings;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Game;
using Game.Assets.Scripts.Game.Logic.Views.Levels;
using Game.Assets.Scripts.Game.Logic.Views.Units;
using GameUnity.Assets.Scripts.Unity.Views.Orders;
using GameUnity.Assets.Scripts.Unity.Views.Units;
using System;
using Tests.Assets.Scripts.Game.Logic.Views;
using UnityEngine;

namespace Assets.Scripts.Views.Levels
{
    public class ScreenView : ViewMonoBehaviour, IScreenView
    {
        [SerializeField] ClashesView _clashes;
        [SerializeField] HandView _hand;
        [SerializeField] CurrentOrderView _order;
        [SerializeField] LevelResourcesView _resources;

        IClashesView IScreenView.Clashes => _clashes;
        IHandView IScreenView.Hand => _hand;
        ICurrentOrderView IScreenView.Customers => _order;
        ILevelResourcesView IScreenView.Resources => _resources;
    }
}