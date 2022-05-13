using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Units;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Level.Units;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Views;
using Game.Assets.Scripts.Tests.Views.Common;
using System;

namespace Game.Assets.Scripts.Tests.Views.Level.Units
{
    public class UnitView : ViewWithPresenter<UnitPresenter>, IUnitView
    {
        public IPosition Position { get; } = new PositionMock();
        public IRotator Rotator { get; }
        public IAnimator Animator { get; }
        public IUnitDresser UnitDresser { get; }

        public UnitView(IViewsCollection level, IRotator rotator, IAnimator animator, IUnitDresser unitDresser) : base(level)
        {
            Rotator = rotator;
            Animator = animator;
            UnitDresser = unitDresser;
        }
    }
}