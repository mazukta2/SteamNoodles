using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Levels;
using Game.Assets.Scripts.Game.Logic.Views.Units;
using System;
using Tests.Assets.Scripts.Game.Logic.Views.Constructions;

namespace Tests.Assets.Scripts.Game.Logic.Views
{
    public interface ILevelView : IView
    {
        public IScreenView Screen { get; }
        IHandView CreateHand();
        IPlacementView CreatePlacement();
        ICurrentOrderView CreateCurrentOrder();
        void SetTimeMover(Action<float> moveTime);
        IUnitsView CreateUnits();
    }
}
