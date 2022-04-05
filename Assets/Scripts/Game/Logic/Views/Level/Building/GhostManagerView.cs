using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
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
    public class GhostManagerView : PresenterView<GhostManagerPresenter>
    {
        public IViewContainer Container { get; private set; }
        public IViewPrefab GhostPrototype { get; private set; }

        private ServiceWaiter<ScreenManagerService> _wait;

        public GhostManagerView(ILevel level, IViewContainer container, IViewPrefab ghostPrototype) : base(level)
        {
            Container = container ?? throw new ArgumentNullException(nameof(container));
            GhostPrototype = ghostPrototype ?? throw new ArgumentNullException(nameof(ghostPrototype));

            _wait = level.Services.Request<ScreenManagerService>(Load);
        }

        protected override void DisposeInner()
        {
            base.DisposeInner();
            _wait.Dispose();
        }

        private void Load(ScreenManagerService manager)
        {
            new GhostManagerPresenter(manager.Get(), Level.Engine.Definitions.Get<ConstructionsSettingsDefinition>(), Level.Engine.Controls, 
                Level.Model.Constructions, this);
            Level.Services.Add(new GhostManagerService(Presenter));
        }
    }
}