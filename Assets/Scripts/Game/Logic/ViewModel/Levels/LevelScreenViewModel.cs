using Assets.Scripts.Logic;
using Assets.Scripts.Logic.Models.Levels;
using Assets.Scripts.Models.Buildings;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Assets.Scripts.Game.Logic.ViewModel.Constructions.Placements;
using Tests.Assets.Scripts.Game.Logic.ViewModel.Levels;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Tests.Assets.Scripts.Game.Logic.ViewModel
{
    public class LevelScreenViewModel
    {
        public HandViewModel Hand { get; }

        private GameLevel _model;
        private ILevelView _view;

        public LevelScreenViewModel(GameLevel model, ILevelView view, PlacementViewModel placement)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            _model = model;
            _view = view;
            Hand = new HandViewModel(model.Hand, _view.CreateHand(), placement);
        }
    }
}
