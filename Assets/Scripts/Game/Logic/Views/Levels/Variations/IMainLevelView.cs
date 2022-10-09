using System;
using Game.Assets.Scripts.Game.Logic.Definitions.Common;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Variations;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Building;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Views.Levels.Variations
{
    public interface IMainLevelView : IViewWithGenericPresenter<MainLevelPresenter>, IViewWithDefaultPresenter
    {

        static IMainLevelView Default { get; set; }

        void IViewWithDefaultPresenter.InitDefaultPresenter()
        {
            Default = this;
            new MainLevelPresenter(this);
        }
    }
}

