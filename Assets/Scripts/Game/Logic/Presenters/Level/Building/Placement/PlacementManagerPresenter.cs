using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements
{
    public class PlacementManagerPresenter : BasePresenter
    {
        private ConstructionsManager _model;
        private ScreenManagerPresenter _screenManager;

        public PlacementManagerPresenter(ConstructionsManager model, ScreenManagerPresenter screenManager, PlacementManagerView view) : base(view)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _screenManager = screenManager ?? throw new ArgumentNullException(nameof(screenManager));
        }

        protected override void DisposeInner()
        {
        }

    }
}
