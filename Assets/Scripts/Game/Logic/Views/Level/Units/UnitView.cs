using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Units;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;

namespace Game.Assets.Scripts.Game.Logic.Views.Level.Units
{
    public class UnitView : PresenterView<UnitPresenter>
    {
        public ILevelPosition Position { get; }
        public IRotator Rotator { get; }
        public IAnimator Animator { get; }

        public UnitView(ILevel level, ILevelPosition position, IRotator rotator, IAnimator animator) : base(level)
        {
            Position = position;
            Rotator = rotator;
            Animator = animator;
        }
    }
}