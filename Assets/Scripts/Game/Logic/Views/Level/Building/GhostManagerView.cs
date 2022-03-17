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
    public class GhostManagerView : View
    {
        public ContainerView Container { get; private set; }
        public PrototypeView GhostPrototype { get; private set; }

        private ServiceWaiter<ScreenManagerService, DefinitionsService> _wait;
        private GhostManagerPresenter _presenter;

        public GhostManagerView(ILevel level, ContainerView container, PrototypeView ghostPrototype) : base(level)
        {
            Container = container ?? throw new ArgumentNullException(nameof(container));
            GhostPrototype = ghostPrototype ?? throw new ArgumentNullException(nameof(ghostPrototype));

            _wait = level.Services.Request<ScreenManagerService, DefinitionsService>(Load);
        }

        protected override void DisposeInner()
        {
            base.DisposeInner();
            _wait.Dispose();
        }

        private void Load(ScreenManagerService manager, DefinitionsService definitionsService)
        {
            _presenter = new GhostManagerPresenter(manager.Get(), definitionsService.Get().Get<ConstructionsSettingsDefinition>(), this);
            Level.Services.Add(new GhostManagerService(_presenter));
        }
    }
}