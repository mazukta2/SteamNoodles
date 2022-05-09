using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Units;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;

namespace Game.Assets.Scripts.Game.Logic.Views.Level.Units
{
    public interface IUnitView : IViewWithPresenter
    {
        ILevelPosition Position { get; }
        IRotator Rotator { get; }
        IAnimator Animator { get; }
        IUnitDresser UnitDresser { get; }
    }
}