using Assets.Scripts.Logic.Models.Levels;
using System;
using Tests.Assets.Scripts.Game.Logic.ViewModel.Constructions.Placements;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Tests.Assets.Scripts.Game.Logic.ViewModel.Levels
{
    public class LevelViewModel
    {
        public LevelScreenViewModel Screen { get; private set; }
        public PlacementViewModel Placement { get; private set; }

        private GameLevel _model;
        private ILevelView _levelView;

        public LevelViewModel(GameLevel model, ILevelView view)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (view == null) throw new ArgumentNullException(nameof(view));
            _model = model;
            _levelView = view;

            Placement = new PlacementViewModel(model.Placement, view.CreatePlacement());
            Screen = new LevelScreenViewModel(model, view, Placement);
        }

    }
}
