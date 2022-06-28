﻿using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Definitions.Common;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Levels.Types;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
using Game.Assets.Scripts.Game.Logic.Views.Controls;
using Game.Assets.Scripts.Game.Logic.Views.Ui;

namespace Game.Assets.Scripts.Game.Logic.Views.Levels.Building
{
    public interface IGhostManagerView : IViewWithGenericPresenter<GhostManagerPresenter>, IViewWithDefaultPresenter
    {
        IViewContainer Container { get;  }
        IViewPrefab GhostPrototype { get; }
        static IGhostManagerView Default { get; set; }

        void IViewWithDefaultPresenter.InitDefaultPresenter()
        {
            Default = this;
            new GhostManagerPresenter(IScreenManagerView.Default.Presenter,
                IGameDefinitions.Default.Get<ConstructionsSettingsDefinition>(), IGameControls.Default,
                IBattleLevel.Default.Constructions, this, IGameTime.Default);
        }
    }
}