using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Units;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Level.Units;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Views;
using System;

namespace Game.Assets.Scripts.Tests.Views.Level.Units
{
    public class UnitView : PresenterView<UnitPresenter>, IUnitView
    {
        public ILevelPosition Position { get; }
        public IRotator Rotator { get; }
        public IAnimator Animator { get; }
        public IUnitDresser UnitDresser { get; }

        public UnitView(LevelView level, ILevelPosition position, IRotator rotator, IAnimator animator, IUnitDresser unitDresser) : base(level)
        {
            Position = position;
            Rotator = rotator;
            Animator = animator;
            UnitDresser = unitDresser;
        }
    }
}