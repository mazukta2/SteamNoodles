using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Settings.Convertion.Convertors;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Services;
using Game.Assets.Scripts.Game.Logic.Services.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views.Level
{
    public class GhostView : View
    {
        public event Action OnUpdate = delegate { };

        private bool _canPlace;

        public bool CanPlace
        {
            get => _canPlace; set
            {
                _canPlace = value;
                OnUpdate();
            }
        }
        public ILevelPosition LocalPosition { get; private set; }
        public ContainerView Container { get; private set; }
        public GhostPresenter Presenter { get; private set; }

        public GhostView(ILevel level, ContainerView container, ILevelPosition position) : base(level)
        {
            LocalPosition = position;
            Container = container;
        }

        public GhostPresenter Init(ConstructionsSettingsDefinition definition, IControls controls,
            ScreenManagerPresenter screenManager,
            ConstructionsManager constructionsManager,
            ConstructionCard currentCard)
        {
            Presenter = new GhostPresenter(definition, screenManager, constructionsManager, currentCard, controls, Level.Engine.Assets, this);
            return Presenter;
        }
    }
}
