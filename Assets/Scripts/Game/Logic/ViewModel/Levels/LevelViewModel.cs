using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.ViewModel;
using Game.Assets.Scripts.Game.Logic.ViewModel.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.ViewModel.Units;
using System;
using Tests.Assets.Scripts.Game.Logic.ViewModel;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Game.Assets.Scripts.Game.Logic.ViewModel.Levels
{
    public class LevelViewModel : IViewModel
    {
        public LevelScreenViewModel Screen { get; private set; }
        public PlacementViewModel Placement { get; private set; }
        public QueueViewModel Queue { get; private set; }

        public bool IsDestoyed { get; private set; }

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
            Queue = new QueueViewModel(model.UnitsQueue);

            _levelView.SetTimeMover(_model.Time.MoveTime);
        }

        public void Destroy()
        {
            Placement.Destroy();
            Screen.Destroy();
            Queue.Destroy();
            IsDestoyed = true;
        }
    }
}
