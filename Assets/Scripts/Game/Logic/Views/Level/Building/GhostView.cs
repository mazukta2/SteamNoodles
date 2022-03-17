using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Settings.Convertion.Convertors;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
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
        public GhostPresenter Presenter { get; private set; }

        public GhostView(ILevel level) : base(level)
        {
        }

        public GhostPresenter Init(ConstructionsSettingsDefinition definition, ConstructionCard currentCard)
        {
            Presenter = new GhostPresenter(definition, currentCard, this);
            return Presenter;
        }
    }
}
