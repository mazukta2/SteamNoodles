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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views.Level
{
    public class GhostView : View
    {
        public bool CanPlace { get; set; }
        public GhostPresenter Presenter { get; private set; }
        public FloatPoint LocalPosition { get; internal set; }

        public GhostView(ILevel level) : base(level)
        {
        }

        public GhostPresenter Init(ConstructionsSettingsDefinition definition, IControls controls,
            ScreenManagerPresenter screenManager,
            ConstructionsManager constructionsManager, 
            ConstructionCard currentCard)
        {
            Presenter = new GhostPresenter(definition,  screenManager, constructionsManager, currentCard, controls, this);
            return Presenter;
        }
    }
}
