using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Units;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;

namespace Game.Assets.Scripts.Game.Logic.Views.Level.Units
{
    public interface IUnitModelView : IPresenterView, IViewWithAutoInit
    {
        ILevelPosition Position { get; }
        IRotator Rotator { get; }
        IAnimator Animator { get; }
        IUnitDresser UnitDresser { get; }

        void IViewWithAutoInit.Init()
        {
            new UnitModelPresenter(this, Level.Engine.Definitions.Get<UnitsSettingsDefinition>());
        }

    }
}