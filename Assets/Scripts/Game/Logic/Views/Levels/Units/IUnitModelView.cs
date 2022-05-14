using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Units;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;

namespace Game.Assets.Scripts.Game.Logic.Views.Level.Units
{
    public interface IUnitModelView : IViewWithPresenter, IViewWithDefaultPresenter
    {
        IAnimator Animator { get; }
        IUnitDresser UnitDresser { get; }

        void IViewWithDefaultPresenter.InitDefaultPresenter()
        {
            new UnitModelPresenter(this, IDefinitions.Default.Get<UnitsSettingsDefinition>());
        }

    }
}