using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Level;
using Game.Assets.Scripts.Game.Logic.Services;
using Game.Assets.Scripts.Game.Logic.Services.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views.Level
{
    public interface IGhostManagerView : IPresenterView, IViewWithAutoInit
    {
        IViewContainer Container { get;  }
        IViewPrefab GhostPrototype { get; }
        GhostManagerPresenter Presenter { get; }

        void IViewWithAutoInit.Init()
        {
            new GhostManagerPresenter(ScreenManagerService.Default.Get(), IDefinitions.Default.Get<ConstructionsSettingsDefinition>(), IControls.Default,
                Level.Model.Constructions, this);
        }
    }
}