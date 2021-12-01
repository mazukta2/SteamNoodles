using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Presenters.Units;
using System;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Levels
{
    public class LevelPresenter : IPresenter
    {
        public LevelScreenPresenter Screen { get; private set; }
        public PlacementPresenter Placement { get; private set; }
        public UnitsPresenter Units { get; private set; }

        public bool IsDestoyed { get; private set; }

        private GameLevel _model;
        private ILevelView _view;

        public LevelPresenter(GameLevel model, ILevelView view)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (view == null) throw new ArgumentNullException(nameof(view));
            _model = model;
            _view = view;

            Placement = new PlacementPresenter(model.Placement, view.CreatePlacement());
            Units = new UnitsPresenter(model.Units, view.CreateUnits());
            Screen = new LevelScreenPresenter(model, view.Screen, Placement);

            _view.SetTimeMover(_model.Time.MoveTime);
        }

        public void Destroy()
        {
            Placement.Destroy();
            Screen.Destroy();
            Units.Destroy();
            _model.Destroy();
            IsDestoyed = true;
        }
    }
}
