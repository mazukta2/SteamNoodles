using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Presenters.Units;
using Game.Assets.Scripts.Game.Logic.Views.Levels;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Levels
{
    public class LevelPresenter : Disposable
    {
        public LevelScreenPresenter Screen { get; private set; }
        public PlacementPresenter Placement { get; private set; }
        public UnitsPresenter Units { get; private set; }

        private GameLevel _model;
        private ILevelView _view;

        public LevelPresenter(GameLevel model, ILevelView view)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (view == null) throw new ArgumentNullException(nameof(view));
            _model = model;
            _view = view;

            Placement = new PlacementPresenter(model.Placement, view.Placement.Create());
            Units = new UnitsPresenter(model.Units, view.CreateUnits());
            Screen = new LevelScreenPresenter(model, view.Screen, Placement);

            _model.OnDispose += Dispose;

        }
        protected override void DisposeInner()
        {
            _model.OnDispose -= Dispose;

            Placement.Dispose();
            Screen.Dispose();
            Units.Dispose();

            _view.Dispose();
        }

    }
}
